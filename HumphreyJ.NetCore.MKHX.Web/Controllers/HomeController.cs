using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HumphreyJ.NetCore.MKHX.Web.Models;
using HumphreyJ.NetCore.MKHX.DataBase;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            try
            {
                var dm = GameDataManager.Get(Request);
                var dbContext = new MkhxCoreContext();
                var PvCounter = dbContext.PvCounter;

                ViewData["GameDataManager"] = dm;
                ViewData["PvCounter"] = PvCounter;

                return View();
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

    }
}
