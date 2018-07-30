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
        public IActionResult ShowCacheStatus()
        {
            var V_GameData = new MkhxCoreContext().V_GameData;

            var s = new List<string>
            {
                "CACHE STATUS FROM " + Program.StartTime,
                "________________________________",
                ""
            };

            foreach (var gdm in GameDataManager.GameDataList)
            {
                s.Add(gdm.Key.ToString());

                {
                    var v = V_GameData.Where(m => m.Version == gdm.Value.allcards);
                    var version = gdm.Value.allcards;
                    var time = v.Max(m => m.Time);
                    var server = string.Join(",", v.GroupBy(m => m.Server).Select(m => m.Key).OrderBy(m => m));
                    s.Add($"{"C"}\t{version}\t{time}\t{server}");
                }
                {
                    var v = V_GameData.Where(m => m.Version == gdm.Value.allrunes);
                    var version = gdm.Value.allrunes;
                    var time = v.Max(m => m.Time);
                    var server = string.Join(",", v.GroupBy(m => m.Server).Select(m => m.Key).OrderBy(m => m));
                    s.Add($"{"R"}\t{version}\t{time}\t{server}");
                }
                {
                    var v = V_GameData.Where(m => m.Version == gdm.Value.allskills);
                    var version = gdm.Value.allskills;
                    var time = v.Max(m => m.Time);
                    var server = string.Join(",", v.GroupBy(m => m.Server).Select(m => m.Key).OrderBy(m => m));
                    s.Add($"{"S"}\t{version}\t{time}\t{server}");
                }
                {
                    var v = V_GameData.Where(m => m.Version == gdm.Value.allmapstage);
                    var version = gdm.Value.allmapstage;
                    var time = v.Max(m => m.Time);
                    var server = string.Join(",", v.GroupBy(m => m.Server).Select(m => m.Key).OrderBy(m => m));
                    s.Add($"{"MS"}\t{version}\t{time}\t{server}");
                }
                {
                    var v = V_GameData.Where(m => m.Version == gdm.Value.allmaphardstage);
                    var version = gdm.Value.allmaphardstage;
                    var time = v.Max(m => m.Time);
                    var server = string.Join(",", v.GroupBy(m => m.Server).Select(m => m.Key).OrderBy(m => m));
                    s.Add($"{"MHS"}\t{version}\t{time}\t{server}");
                }
                {
                    var v = V_GameData.Where(m => m.Version == gdm.Value.keywords);
                    var version = gdm.Value.keywords;
                    var time = v.Max(m => m.Time);
                    var server = string.Join(",", v.GroupBy(m => m.Server).Select(m => m.Key).OrderBy(m => m));
                    s.Add($"{"KW"}\t{version}\t{time}\t{server}");
                }

                s.Add("");
            }

            return new ContentResult
            {
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