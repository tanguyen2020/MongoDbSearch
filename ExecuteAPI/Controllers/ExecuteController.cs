using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExecuteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecuteController : ControllerBase
    {
        public ExecuteController()
        {

        }
        [Route("querylist/{svcname}/{actionname}")]
        [HttpPost]
        public IActionResult QueryForList(string svcname, string actionname)
        {
            var svc = Activator.CreateInstance("ExcuteService", svcname);
            
            return Ok();
        }
    }
}
