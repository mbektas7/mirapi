using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mirapi.Core.Domain;
using Mirapi.Core.DTOs;
using Mirapi.Core.Helpers;
using Mirapi.Persistence;


namespace Mirapi.Controllers
{

    [ErrorLog, ActionExecuteLog, Route("api/Brand"), Produces("application/json")]
    public class BrandController : Controller
    {
        private UnitOfWork unitOfWork;

        public BrandController()
        {
            unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }


        [AllowAnonymous]
        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] BrandsDTO brand)
        {
            IActionResult response = BadRequest();



            if (String.IsNullOrEmpty(brand.name))
            {
                return response;
            }

            Brands newBrand = new Brands
            {
                name = brand.name

            };
            try
            {
                unitOfWork.Brands.Add(newBrand);
                unitOfWork.Save();
                response = StatusCode(StatusCodes.Status201Created, "Marka başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, "Bişeyler ters gitti.");
            }

            return response;
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            IActionResult response = BadRequest();

            try
            {
                Brands datas = null;
                datas = unitOfWork.Brands.SingleOrDefault(s=>s.Id.ToString().Equals(id) && s.IsDeleted== false);
                response = StatusCode(StatusCodes.Status200OK, new ResultModel<Brands>() { data = datas, message = "" });
            }
            catch (Exception)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." });
            }


            return response;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = BadRequest();

            try
            {

                List<Brands> datas = null;
                datas = unitOfWork.Brands.GetAll().Where(s=>s.IsDeleted==false).ToList();
                response = StatusCode(StatusCodes.Status200OK, new ResultModel<List<Brands>>() { data = datas, message = "" });
            }
            catch (Exception ex)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." + ex.InnerException.ToString() });
            }


            return response;
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] BrandsDTO request)
        {
            IActionResult response = BadRequest();

            try
            {
             
                var brand = unitOfWork.Brands.SingleOrDefault(u => u.Id.ToString().Equals(request.Id.ToString()));
                if (brand!=null)
                {
                    brand.name = request.name;
                    unitOfWork.Save();
                    response = StatusCode(StatusCodes.Status200OK, new ResultModel<BrandsDTO>() { data = null, message = "Marka başarıyla güncellendi" });
                    return response;
                }
                else
                {
                    response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." });

                }


            }
            catch (Exception)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." });
            }


            return response;
        }
    }
}
