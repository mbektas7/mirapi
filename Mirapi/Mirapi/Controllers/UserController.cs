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

    [ErrorLog, ActionExecuteLog, Route("api/Users"), Produces("application/json")]
    public class UserController : Controller
    {
        private UnitOfWork unitOfWork;
        private IMailHelper _emailSender;

        public UserController(IMailHelper emailSender)
        {
            _emailSender = emailSender;
            unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }




    }
}