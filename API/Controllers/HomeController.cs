using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return "Hello API";
        }

        [Route("things")]
        public IEnumerable<string> GetThings()
        {
            return new List<string> { "A thing", "Another thing" };
        }
    }
}