using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mirapi.Controllers
{

    [ Route("api/Values"), Produces("application/json")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
      
            return Ok("miraç");
        }
    }
}
