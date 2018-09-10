using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class GalleryController : Controller
    {
        [Route("Gallery")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Gallery/Picture")]
        public IActionResult Picture()
        {
            return View();
        }

        [Route("Gallery/GameData")]
        public IActionResult GameData()
        {
            return View();
        }

        [Route("Gallery/BattleRecord")]
        public IActionResult BattleRecord()
        {
            return View();
        }
    }
}