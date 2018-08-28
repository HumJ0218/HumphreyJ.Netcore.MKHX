using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class AboutController : Controller
    {
        [Route("about")]
        public IActionResult Index()
        {
            return View();
        }
    }
}