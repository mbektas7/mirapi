using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Mirapi.Controllers;
using Mirapi.Core.Domain;
using Mirapi.Core.Interfaces;
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
    [Authorize]
    public class AuthorizesControlAttribute : Attribute, IActionFilter
    {
        public string[] Claim { get; set; }
        private UnitOfWork _uOWork;

        public AuthorizesControlAttribute()
        {
            _uOWork = new UnitOfWork(new ApplicationDbContext());
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ipRestriction burada yapılacak
            var request = filterContext.HttpContext.Request;
            var accesToken = request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(accesToken) as JwtSecurityToken;
            var jtiUserId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
            var jtiSuperAdmin = tokenS.Claims.First(claim => claim.Type == "claims").Value;
            // ip havuzu kontrolü burada yapılacak
       
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

    }
}
