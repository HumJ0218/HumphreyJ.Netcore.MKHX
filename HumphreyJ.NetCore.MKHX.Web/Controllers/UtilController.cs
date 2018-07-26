using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}