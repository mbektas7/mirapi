using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mirapi.Core.Domain;
using Mirapi.Core.DTOs;
using Mirapi.Core.Helpers;
using Mirapi.Persistence;

namespace Mirapi.Controllers
{


    [ErrorLog, ActionExecuteLog, Route("api/Post"), Produces("application/json")]
    public class PostController : Controller
    {

        private UnitOfWork unitOfWork;

        public PostController()
        {
            unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }


        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] PostDTO post)
        {
            IActionResult response = BadRequest();



            if (String.IsNullOrEmpty(post.message) || String.IsNullOrEmpty(post.title))
            {
                return response;
            }
            var car = unitOfWork.Cars.Find(q => q.Id.ToString() == post.carId.ToString()).FirstOrDefault();
            var tokenS = Functions.tokenS(Request);
            var uId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
            var user = unitOfWork.Users.Find(s => s.Id.ToString() == uId).FirstOrDefault();
            Post newPost = new Post
            {
                title = post.title,
                message = post.message,
                user = user,
                isAnswered = false,
                car = car
            };
            try
            {
                unitOfWork.Post.Add(newPost);
                unitOfWork.Save();
                response = StatusCode(StatusCodes.Status201Created, "Soru başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "Bişeyler ters gitti.");
            }

            return response;
        }


        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = BadRequest();

            try
            {
                List<Post> datas = null;
                datas = unitOfWork.Post.GetAll().Where(s=>s.IsDeleted == false).Include(a=>a.user).Include(q=>q.car).ToList();
                response = StatusCode(StatusCodes.Status200OK, new ResultModel<List<Post>>() { data = datas, message = "" });
            }
            catch (Exception ex)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." });
            }
            
            
            return response;

        }

    }
}
