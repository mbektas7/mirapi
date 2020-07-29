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
        public IActionResult GetById()
        {
            IActionResult response = BadRequest();

            try
            {
                var brands = unitOfWork.Brands.GetAll();
                response = StatusCode(StatusCodes.Status200OK, new ResultModel<BrandsDTO>() { data = (BrandsDTO)brands, message = "" });
            }
            catch (Exception)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." });
            }


            return response;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            IActionResult response = BadRequest();

            try
            {

                List<Brands> datas = null;
                datas = unitOfWork.Brands.GetAll().ToList();
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
        public IActionResult Update([FromBody] CarDTO car)
        {
            IActionResult response = BadRequest();

            try
            {
                Cars oldCar = null;
                oldCar = unitOfWork.Cars.SingleOrDefault(u => u.Id.ToString().Equals(oldCar.Id));
                var brand = unitOfWork.Brands.SingleOrDefault(u => u.Id.ToString().Equals(car.brandId));
                if (oldCar != null)
                {
                    oldCar.name = car.name;
                    oldCar.modelYear = car.modelYear;
                    oldCar.Brand = brand;

                    unitOfWork.Save();
                    response = StatusCode(StatusCodes.Status200OK, new ResultModel<CarDTO>() { data = null, message = "" });
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
