using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Mirapi.Core;
using Mirapi.Core.Domain;
using Mirapi.Core.DTOs;
using Mirapi.Core.Helpers;
using Mirapi.Core.Interfaces;
using Mirapi.Persistence;


namespace Mirapi.Controllers
{

    [ErrorLog,Authorize, ActionExecuteLog, Route("api/Users"), Produces("application/json")]
    public class UserController : Controller
    {
        private UnitOfWork unitOfWork;
      

        public UserController()
        {
            
            unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        /// <summary>
        /// user register beginning function
        /// </summary>
        /// <param name="registerModel">username and password</param>
        /// <returns></returns>
        ///
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] UserRegisterDTO registerModel)
        {
            IActionResult response = BadRequest();



            if (String.IsNullOrEmpty(registerModel.Email) || String.IsNullOrEmpty(registerModel.Password) || !IsMailValid(registerModel.Email))
            {
                return response;
            }


            var hashedPassword = Cipher.ConvertToMd5(registerModel.Password);

            User existingUser = null;
            try
            {
                existingUser = unitOfWork.Users.SingleOrDefault(u => u.Email.Equals(registerModel.Email.ToString()));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Hata oluştu. Sistem yöneticisiyle görüşün.");
            }

            if (existingUser != null)
            {
                return StatusCode(StatusCodes.Status428PreconditionRequired, "Email zaten kayıtlı");

            }

            User user = new User
            {
                Email = registerModel.Email,
                Password = hashedPassword,
                Username = registerModel.Email,
                IsMailConfirmed = true
            };

            try
            {
                unitOfWork.Users.Add(user);
                unitOfWork.Save();

            

                response = StatusCode(StatusCodes.Status201Created, "Kullanıcı kaydı başarılı.");
              
                string fullLink = "<a href='" + Startup.Configuration["UrlsMail:angular"] + "#/auth/register/mail-confirm/' target='_blank'>Tıklayın</a>";
                string mail = "Üyeliğinizi tamamlamak için lütfen bağlantıya tıklayın." + fullLink;
              //  _emailSender.Send("Mail Doğrulama - Akıllı İş Yeri Asistanı", mail, user.Email);
            }
            catch (Exception ex)
            {
                //todo log ex
                response = StatusCode(StatusCodes.Status500InternalServerError, "Bişeyler ters gitti.");
            }

            return response;
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetUserDetails(string id)
        {

            IActionResult response = BadRequest();
            if (String.IsNullOrEmpty(id))
            {
                return response;
            }
            User user = null;
            try
            {
                user = unitOfWork.Users.SingleOrDefault(u => u
                .Where(b => b.Id.ToString().Equals(id)));
            }
            catch
            {
                response = StatusCode(StatusCodes.Status400BadRequest, "Kayıtlı kullanıcı bulunamadı.");
                return response;
            }

            if (user == null)
            {
                response = StatusCode(StatusCodes.Status400BadRequest, "Kayıtlı kullanıcı bulunamadı.");
            }
            else
            {
                UserDetailDTO dto = new UserDetailDTO();
                dto.UserId = user.Id.ToString();
                dto.Name = user.Name;
                dto.Surname = user.Surname;
                dto.IsFemale = user.IsFemale;
                dto.Birthday = user.Birthday;
                dto.About = user.About;
                dto.Address = user.Address;
                dto.Phone = user.Phone;
                response = StatusCode(StatusCodes.Status201Created, new ResultModel<UserDetailDTO>() { data = dto, message = "kullanıcı detayları getirildi" });
            }

            return response;
        }



        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUser([FromBody] UserDetailDTO userDetails)
        {
            IActionResult response = BadRequest();
            if (String.IsNullOrEmpty(userDetails.UserId))
            {
                return response;
            }
            User user = null;
            try
            {
                user = unitOfWork.Users.SingleOrDefault(u => u.Id.ToString().Equals(userDetails.UserId));
            }
            catch
            {
                response = StatusCode(StatusCodes.Status400BadRequest, "Hata oluştu. Sistem yöneticisiyle görüşün.");
                return response;
            }

            if (user != null)
            {
              
                user.About = userDetails.About;
                user.Birthday = userDetails.Birthday;
                user.Name = userDetails.Name;
                user.IsFemale = userDetails.IsFemale;
                user.Surname = userDetails.Surname;
                user.Phone = userDetails.Phone;
                user.Address = userDetails.Address;

                unitOfWork.Save();
                response = Ok("Kayıt başarılı");
            }
            else
            {
                response = StatusCode(StatusCodes.Status400BadRequest, "Kullanıcıyı bulunamadı.");

            }

            return response;
        }



        [HttpPost]
        [Route("changePassword")]
        public IActionResult changePassword([FromBody] ChangePasswordDTO password)
        {
            IActionResult response = BadRequest();

            if (!String.IsNullOrEmpty(password.newPassword) && !String.IsNullOrEmpty(password.oldPassword))
            {
                var tokenS = Functions.tokenS(Request);
                var uId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
                var hashPass = Cipher.ConvertToMd5(password.oldPassword);
                var user = unitOfWork.Users.SingleOrDefault(u=>u.Id.ToString().Equals(uId) && u.Password.Equals(hashPass));
                if (user!=null)
                {
                    var hashedPassword = Cipher.ConvertToMd5(password.newPassword);
                    user.Password = hashedPassword;
                    unitOfWork.Save();
                    response = StatusCode(StatusCodes.Status200OK, "Şifre değiştirildi");
                }
                else
                {
                    response = StatusCode(StatusCodes.Status400BadRequest, "Mevcut şifreniz hatalı.");
                }
               
            }
            else
            {
                response = StatusCode(StatusCodes.Status400BadRequest, "Şifre alanları dolu olmalıdır");
            }
          

            return response;
        }

        #region Is-Mail-Valid
        private bool IsMailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}