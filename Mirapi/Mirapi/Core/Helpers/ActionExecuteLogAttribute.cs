using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Mirapi.Core.Domain;
using Mirapi.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;

namespace Mirapi.Core.Helpers
{
    public class ActionExecuteLogAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        private Stopwatch _stopwatch;
        public int Order { get; set; }

        public ActionExecuteLogAttribute()
        {
            _stopwatch = new Stopwatch();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _uOWork = new UnitOfWork(new ApplicationDbContext());
            //To do : before the action executes  
            _stopwatch.Reset();
            _stopwatch.Start();

            StringBuilder sb = new StringBuilder();
            var request = filterContext.HttpContext.Request;

            string jtiUserId = "";
            try
            {
                var accesToken = request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(accesToken) as JwtSecurityToken;
                jtiUserId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
            }
            catch
            {

            }

            var user = _uOWork.Users.Find(w => w.Id.ToString() == jtiUserId && w.IsDeleted == false).FirstOrDefault();
            int resultType = (int)Enums.ResultType.Info;
            DateTime time = DateTime.Now;
            string controller = filterContext.Controller as string;
            string action = filterContext.ActionDescriptor.DisplayName as string;
            foreach (var item in filterContext.ActionArguments)
            {
                sb.Append(JsonConvert.SerializeObject(item));
            }
            string requestJson = sb.ToString();
            string responseJson = string.Empty;
            var ip = request.HttpContext.Connection.RemoteIpAddress ?? request.HttpContext.Connection.LocalIpAddress;
            string pcName = request.Host.Value;
            string url = request.Headers["Referer"];
            string processTime = _stopwatch.ElapsedMilliseconds.ToString();
            string logCaption = string.Empty;
            string logDetail = string.Empty;
            bool isBefore = true;

            //_uOWork.Logs.Add(new Log()
            //{
            //    User= user,
            //    ResultType=resultType,
            //    Time = time,
            //    Controller=controller,
            //    Action=action,
            //    Request = requestJson,
            //    Response = responseJson,
            //    Ip=ip.ToString(),
            //    PcName=pcName,
            //    Url=url,
            //    ProcessTime=processTime,
            //    LogCaption = logCaption,
            //    LogDetail =logDetail,
            //    IsBefore = isBefore
            //});

            _uOWork.Save();

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var _uOWork = new UnitOfWork(new ApplicationDbContext());
            //To do : after the action executes
            StringBuilder sb = new StringBuilder();
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            string jtiUserId = "";
            try
            {
                var accesToken = request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(accesToken) as JwtSecurityToken;
                jtiUserId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
            }
            catch
            {

            }

            var user = _uOWork.Users.Find(w => w.Id.ToString() == jtiUserId && w.IsDeleted == false).FirstOrDefault();
            int resultType = (int)Enums.ResultType.Info;
            DateTime time = DateTime.Now;
            string controller = filterContext.Controller as string;
            string action = filterContext.ActionDescriptor.DisplayName as string;
            try
            {
                sb.Append(JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ObjectResult)filterContext.Result)));
            }
            catch
            {

            }
            string requestJson = string.Empty;
            string responseJson = sb.ToString();
            var ip = request.HttpContext.Connection.RemoteIpAddress ?? request.HttpContext.Connection.LocalIpAddress;
            string pcName = request.Host.Value;
            string url = request.Headers["Referer"];
            string processTime = _stopwatch.ElapsedMilliseconds.ToString();
            string logCaption = string.Empty;
            string logDetail = string.Empty;
            bool isBefore = false;

            //_uOWork.Logs.Add(new Log()
            //{
            //    User = user,
            //    ResultType = resultType,
            //    Time = time,
            //    Controller = controller,
            //    Action = action,
            //    Request = requestJson,
            //    Response = responseJson,
            //    Ip = ip.ToString(),
            //    PcName = pcName,
            //    Url = url,
            //    ProcessTime = processTime,
            //    LogCaption = logCaption,
            //    LogDetail = logDetail,
            //    IsBefore = isBefore
            //});

            _uOWork.Save();
        }

    }
}
