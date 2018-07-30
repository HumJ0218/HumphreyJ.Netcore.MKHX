using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.DataBase;
using HumphreyJ.NetCore.MKHX.Web.Models;
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

        [Route("Util/ShowCacheStatus")]
        public IActionResult ShowCacheStatus() {
            var V_GameData = new MkhxCoreContext().V_GameData;

            var s = new List<string>
            {
                "CACHE STATUS",
                "________________________________",
                ""
            };

            foreach (var gdm in GameDataManager.GameDataList) {
                s.Add("\t" + gdm.Key);
                s.Add("\t\t" + "C"+"\t" + gdm.Value.allcards + "\t" + V_GameData.FirstOrDefault(m=>m.Version== gdm.Value.allcards).Time);
                s.Add("\t\t" + "R" + "\t" + gdm.Value.allrunes + "\t" + V_GameData.FirstOrDefault(m => m.Version == gdm.Value.allrunes).Time);
                s.Add("\t\t" + "S" + "\t" + gdm.Value.allskills + "\t" + V_GameData.FirstOrDefault(m => m.Version == gdm.Value.allskills).Time);
                s.Add("\t\t" + "MS" + "\t" + gdm.Value.allmapstage + "\t" + V_GameData.FirstOrDefault(m => m.Version == gdm.Value.allmapstage).Time);
                s.Add("\t\t" + "MHS" + "\t" + gdm.Value.allmaphardstage + "\t" + V_GameData.FirstOrDefault(m => m.Version == gdm.Value.allmaphardstage).Time);
                s.Add("\t\t" + "KW" + "\t" + gdm.Value.keywords + "\t" + V_GameData.FirstOrDefault(m => m.Version == gdm.Value.keywords).Time);
                s.Add("");
            }

            return new ContentResult {
                ContentType = "text/plain",
                Content = string.Join("\r\n", s),
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