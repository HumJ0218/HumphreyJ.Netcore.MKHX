using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class OssViewerController : Controller
    {
        private static readonly Dictionary<string, string> RootPath = new Dictionary<string, string> {
            { "image", "images/" },
            { "gamedata", "data/" },
            { "battle", "battle/" },
        };

        [Route("ossviewer/{key}")]
        public IActionResult Index(string key)
        {
            try
            {
                return View(RootPath[key]);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}