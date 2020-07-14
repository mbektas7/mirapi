using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Mirapi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Mirapi.Core.Helpers
{
    public class MyErrorResponse
    {
        public string ErrorMessage { get; set; }
        public string ErrorAction { get; set; }
        public string ErrorController { get; set; }
    }

    public class ErrorLogAttribute : ExceptionFilterAttribute
    {
        public string[] Claim { get; set; }

        public ErrorLogAttribute()
        {

        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var _uOWork = new UnitOfWork(new ApplicationDbContext());
            //1. Loglama
            //Logger.LogYaz(actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace);

            //2. Response Hazırlama
            MyErrorResponse result = new MyErrorResponse();
            result.ErrorAction = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            result.ErrorController = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            result.ErrorMessage = actionExecutedContext.Exception.ToString();

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
        }

    }

}
