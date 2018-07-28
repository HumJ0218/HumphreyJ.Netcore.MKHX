using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class UtilController : Controller
    {
        [Route("Util/ClearCookies")]
        public IActionResult ClearCookies()
        {
            foreach (var i in Request.Cookies)
            {
                Response.Cookies.Append(i.Key, "", new CookieOptions { Expires = new DateTime(2000, 1, 1) });
            }
            return new ContentResult
            {
                ContentType = "text/plain",
                Content = "OK",
            };
        }

        [Route("Util/DataComparer")]
        public IActionResult DataComparer()
        {
            var dbContext = new MkhxCoreContext();
            return View(dbContext.V_GameData);
        }
    }
}