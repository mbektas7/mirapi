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
            IActionResult response = Unauthorized();
            var user = Authenticate(login);
            if (user != null)
            {
                if (user.IsMailConfirmed)
                {
                    var bearerToken = BuildToken(user);
                    response = Ok(bearerToken);

                }
                else
                {
                    response = StatusCode(StatusCodes.Status401Unauthorized, "Mail adresinizi doğrulayınız.");
                }

            }
            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("isAuthenticatedLoginThatPage")]
        public IActionResult isAuthenticatedLoginThatPage(string pageType)
        {
            IActionResult response = BadRequest();

            response = StatusCode(StatusCodes.Status200OK, new ResultModel<bool>() { data = true, message = "" });

            return response;
        }
        [AllowAnonymous]
        [HttpPost]
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

        private User Authenticate(LoginDTO login)
        {

            User user = unitOfWork.Users.Find(u =>
            u.Username == login.Username &&
            u.Password == Cipher.ConvertToMd5(login.Password) &&
            u.IsDeleted.Equals(false))
            .FirstOrDefault();

            return user;
        }

        //Proje tüm bilgileri alınıyor
        private Assembly GetAssemblyInfo() => Assembly.GetExecutingAssembly();

        //Proje içindeki tüm typler alınıyor
        private Type[] GetAllTypes() => GetAssemblyInfo().GetTypes();

        //Proje içindeki tüm controller listesi alınıyor
        private IEnumerable<Type> GetAllControllers() => GetAllTypes().Where(type => typeof(Controller).IsAssignableFrom(type));

        //Proje içindeki tüm controller içindeki methotlar listesi alınıyor
        private IEnumerable<MethodInfo> GetAllMethods() => GetAllControllers().SelectMany(type => type.GetMethods())
            .Where(method =>
            method.IsPublic &&
            (method.IsDefined(typeof(HttpPostAttribute)) ||
            method.IsDefined(typeof(HttpGetAttribute)) ||
            method.IsDefined(typeof(HttpPutAttribute)) ||
            method.IsDefined(typeof(HttpDeleteAttribute)))
            );

        //metotlardaki claim atributlerindeki valueler alınıyor ve eğer db de yoksa ekleniyor
        //private void ClaimsInsertToDBInMethodsOfAssembly()
        //{
        //    try
        //    {
        //        var methodList = GetAllMethods();
        //        ClaimController claimController = new ClaimController();
        //        foreach (var item in methodList)
        //        {
        //            Attribute authorizesControl = item.GetCustomAttribute(typeof(AuthorizesControlAttribute));
        //            if (authorizesControl != null)
        //            {
        //                string[] authorizesControlAttributeClaimValue = ((Mirapi.Core.Helpers.AuthorizesControlAttribute)authorizesControl).Claim;
        //                if (authorizesControlAttributeClaimValue != null)
        //                {
        //                    string authorizesControlAttributeValue = ((Mirapi.Core.Helpers.AuthorizesControlAttribute)authorizesControl).Claim.FirstOrDefault();
        //                    string authorizesControlAttributeValueName = ((Mirapi.Core.Helpers.AuthorizesControlAttribute)authorizesControl).Claim.LastOrDefault();
        //                    claimController.Create(
        //                        new ClaimDTO()
        //                        {
        //                            url = authorizesControlAttributeValue,
        //                            name = authorizesControlAttributeValueName

        //                        }
        //                        );
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

  
        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public string status { get; set; }
            public string htmlLink { get; set; }
            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string summary { get; set; }
            public Creator creator { get; set; }
            public Organizer organizer { get; set; }
            public Start start { get; set; }
            public End end { get; set; }
            public string transparency { get; set; }
            public string visibility { get; set; }
            public string iCalUID { get; set; }
            public int sequence { get; set; }
        }

        public class Creator
        {
            public string email { get; set; }
            public string displayName { get; set; }
            public bool self { get; set; }
        }

        public class Organizer
        {
            public string email { get; set; }
            public string displayName { get; set; }
            public bool self { get; set; }
        }

        public class Start
        {
            public string date { get; set; }
        }

        public class End
        {
            public string date { get; set; }
        }
        public class HolidayCostum
        {
            public string summary { get; set; }
            public Start start { get; set; }
            public End end { get; set; }
        }

    }
}