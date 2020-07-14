using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Mirapi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using IActionFilter = Microsoft.AspNetCore.Mvc.Filters.IActionFilter;

namespace Mirapi.Core.Helpers
{
    public class TokenBlacklistValidator : Attribute, IOrderedFilter, IActionFilter
    {
        private UnitOfWork _uOWork;
        public int Order { get; set; }

        public TokenBlacklistValidator()
        {
            _uOWork = new UnitOfWork(new ApplicationDbContext());
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //epic comment
            //To do : before the action executes  
            StringBuilder sb = new StringBuilder();
            var request = filterContext.HttpContext.Request;
            var accesToken = request.Headers["Authorization"];
            //var claim = User.FindFirst("userId")?.Value;
            var tokenHash = Cipher.ConvertToMd5(accesToken);
            try
            {
                var blacklistItem = _uOWork.TokenBlacklist.Find(t => t.TokenHash.Equals(tokenHash));
                if (blacklistItem == null)
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.Headers.Clear();
                    filterContext.Result = new EmptyResult();
                }
                else
                {
                    //ignore
                }

            }
            catch
            {
                //ignore
            }
            //todo check token concurrency stamp 
            //todo if concurrency not equal than add token to blacklist
            //todo change user's concurrency at each update

            foreach (var item in filterContext.ActionArguments)
            {
                sb.Append(JsonConvert.SerializeObject(item));
            }
            var ip = request.HttpContext.Connection.RemoteIpAddress ?? request.HttpContext.Connection.LocalIpAddress;
            //todo get user token
            //todo run it thru cipher
            //todo check if token in blacklist


            //_uOWork.Logs.Add(new Log()
            //{
            //    IsBefore = true,
            //    Time = DateTime.Now,
            //    LogCaption = $"{filterContext.Controller} - {filterContext.ActionDescriptor.DisplayName}",
            //    LogDetail =
            //        "Ip: " + ip +
            //        "-Host Name: " + request.Host.Value +
            //        "-Url: " + request.Headers["Referer"] +
            //        "-User : " +
            //        "-Request: " + sb.ToString()
            //});

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //To do : after the action executes
            StringBuilder sb = new StringBuilder();
            var response = filterContext.HttpContext.Response;
            //var props = ((Microsoft.AspNetCore.Mvc.ObjectResult)filterContext.Result).Value.GetType().GetProperties();
            //sb.Append(JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ObjectResult)filterContext.Result)));
            var ip = response.HttpContext.Connection.RemoteIpAddress ?? response.HttpContext.Connection.LocalIpAddress;
            //_uOWork.Logs.Add(new Log()
            //{
            //    IsBefore = false,
            //    Time = DateTime.Now,
            //    LogCaption = $"{filterContext.Controller} - {filterContext.ActionDescriptor.DisplayName}",
            //    LogDetail =
            //        "Ip: " + ip +
            //        "-Host Name: Server" +
            //        "-Url: " + response.Headers["Referer"] +
            //        "-Process Time: " + _stopwatch.ElapsedMilliseconds.ToString() +
            //        "-User : " +
            //        "-Response: " + sb.ToString()
            //});

            //_uOWork.Save();
        }

    }
}

