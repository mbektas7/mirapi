using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Mirapi.Core.Domain;
using Mirapi.Core.DTOs;
using Mirapi.Core.Helpers;
using Mirapi.Persistence;
using Microsoft.AspNetCore.Cors;

namespace Mirapi.Controllers
{

    [ErrorLog, ActionExecuteLog, Route("api/Token"), Produces("application/json")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _conf = null;
        private UnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());
        public TokenController(IConfiguration conf)
        {
            _conf = conf;
        }

        [AllowAnonymous]
        [HttpPost]
   
        public IActionResult Post([FromBody] LoginDTO login)
        {
            var response = Authenticate(login, ipAddress());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            HttpContext.Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Lax, Expires = DateTime.Now.AddMinutes(30) });          //  setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = unitOfWork.Users.Find(s => s.Id.ToString().Equals(id.ToString())).FirstOrDefault();
            if (user == null) return NotFound();

            return Ok(user.RefreshTokens);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = RefreshToken(refreshToken, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost]
        [Route("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }


        [HttpGet]
        [Authorize]
        [Route("isAuthenticatedLoginThatPage")]
        public IActionResult isAuthenticatedLoginThatPage(string pageType)
        {
            IActionResult response = BadRequest();

            response = StatusCode(StatusCodes.Status200OK, new ResultModel<bool>() { data = true, message = "" });

            return response;
        }


        [HttpPost]
        [Authorize]
        [Route("isUserLoggedInWithoutRefreshing")]
        public IActionResult isUserLoggedInWithoutRefreshing(string pageType)
        {
            IActionResult response = BadRequest();

            response = StatusCode(StatusCodes.Status200OK, new ResultModel<bool>() { data = true, message = "" });

            return response;
        }



        private string BuildToken(User user)
        {
            /*
             an array of Claims instances, each created from a key and a value. 
             The keys are values of a structure (JwtRegisteredClaimNames) that provides names for public standardized claims.
             */

            var rols = "";
            //var rols = unitOfWork.UserRoles.Find(u => u.User.Id == user.Id && (u.Role.Name == "Super Admin" || u.Role.Name == "Süper Admin" || u.Role.Name == "SuperAdmin") && u.IsDeleted == false).Include(a => a.Role).ToList();
            string superAdmin = rols.Count() > 0 ? Enums.SuperAdmin : String.Empty;

            var claims = new[] {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Name + " " + user.Surname),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new System.Security.Claims.Claim("userid", user.Id.ToString()),
                new System.Security.Claims.Claim("claims", superAdmin)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(_conf["Jwt:Issuer"],
                _conf["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(25),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private AuthenticateResponse Authenticate(LoginDTO login, string ipAddress)
        {

            User user = unitOfWork.Users.Find(u =>
            u.Username == login.Username &&
            u.Password == Cipher.ConvertToMd5(login.Password) &&
            u.IsDeleted.Equals(false))
            .FirstOrDefault();

            var jwtToken = BuildToken(user);
            var refreshToken = generateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            unitOfWork.Save();


            return new AuthenticateResponse(user, jwtToken, refreshToken.Token); ;
        }


        private AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = unitOfWork.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            unitOfWork.Save();

            // generate new jwt
            var jwtToken = BuildToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        private bool RevokeToken(string token, string ipAddress)
        {
            var user = unitOfWork.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            unitOfWork.Save();

            return true;
        }


        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }


        public class RevokeTokenRequest
        {
            public string Token { get; set; }
        }


    }
}