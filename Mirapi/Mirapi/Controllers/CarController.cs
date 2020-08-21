using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mirapi.Core.Domain;
using Mirapi.Core.DTOs;
using Mirapi.Core.Helpers;
using Mirapi.Persistence;

namespace Mirapi.Controllers
{
    [ErrorLog, Authorize, ActionExecuteLog, Route("api/Car"), Produces("application/json")]
    public class CarController : Controller
    {
        private UnitOfWork unitOfWork;

        public CarController()
        {
            unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }


        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] CarDTO car)
        {
            IActionResult response = BadRequest();



            if (String.IsNullOrEmpty(car.name) || String.IsNullOrEmpty(car.brandId))
            {
                return response;
            }


            var brand = unitOfWork.Brands.SingleOrDefault(u => u.Id.ToString().Equals(car.brandId));

            Cars newCar = new Cars
            {
              name = car.name,
              modelYear =  car.modelYear,
              Brand = brand
              
            };
            try
            {
                unitOfWork.Cars.Add(newCar);
                unitOfWork.Save();
                response = StatusCode(StatusCodes.Status201Created, "Araç başarıyla kaydedildi.");
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
                Cars datas = null;
                datas = unitOfWork.Cars.SingleOrDefault(s=>s.Id.ToString().Equals(id) && s.IsDeleted == false);
                response = StatusCode(StatusCodes.Status200OK, new ResultModel<Cars>() { data = datas, message = "" });
            }
            catch (Exception)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." });
            }


            return response;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response = BadRequest();

            try
            {
                List<Cars> datas = null;

                datas = unitOfWork.Cars.GetAll().Where(s=>s.IsDeleted==false).Include(s => s.Brand).ToList();
                response = StatusCode(StatusCodes.Status200OK, new ResultModel<List<Cars>>() { data = datas , message = "" });
            }
            catch (Exception ex)
            {

                response = StatusCode(StatusCodes.Status500InternalServerError, new ResultModel<PostDTO>() { data = null, message = "Hata oluştu." + ex.InnerException });
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
                oldCar = unitOfWork.Cars.SingleOrDefault(u => u.Id.ToString().Equals(car.Id.ToString()));
                var brand = unitOfWork.Brands.SingleOrDefault(u=>u.Id.ToString().Equals(car.brandId.ToString()));

                if (oldCar!=null)
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


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            IActionResult response = BadRequest();

            try
            {
                Cars oldCar = null;
                oldCar = unitOfWork.Cars.SingleOrDefault(u => u.Id.ToString().Equals(id.ToString()));
               

                if (oldCar != null)
                {
                    oldCar.IsDeleted = true;

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
