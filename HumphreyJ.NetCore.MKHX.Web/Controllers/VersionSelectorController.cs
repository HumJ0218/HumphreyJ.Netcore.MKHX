using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.Web.Models;
using HumphreyJ.NetCore.MKHX.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using HumphreyJ.NetCore.MKHX.Web.Models.Helpers;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class VersionSelectorController : Controller
    {
        [Route("versionselector")]
        public IActionResult Index() => View();

        [Route("versionselector/versions")]
        public JsonResult Versions()
        {
            try
            {
                var data = (IEnumerable<V_GameData>)(new MkhxCoreContext().V_GameData);
                var IsDevelopment = Startup.Environment.IsDevelopment();
                var IsAkChecked = TestDataAccessKeyHelper.Check(Request.Cookies["accesskey"]);
                if (!(IsDevelopment|| IsAkChecked))
                {
                    data = data.Where(m => m.Server[0] != 'T');
                }

                return new JsonResult(new
                {
                    success = true,
                    data = data.GroupBy(m => m.Server)
                        .OrderBy(m => m.Key)
                        .ToDictionary(server => server.Key, f => f.GroupBy(m => m.FileName)
                        .ToDictionary(file => file.Key, v => v.OrderByDescending(m => m.Time)
                        .Select(m => new
                        {
                            m.Version,
                            Time = $"{m.Time.ToShortDateString()} {m.Time.ToShortTimeString()}",
                            m.Remark
                        }))),
                    current = new
                    {
                        server = Request.Cookies["server"],
                        allcards = Request.Cookies["allcards"],
                        allrunes = Request.Cookies["allrunes"],
                        allskills = Request.Cookies["allskills"],
                        allmapstage = Request.Cookies["allmapstage"],
                        allmaphardstage = Request.Cookies["allmaphardstage"],
                        keywords = Request.Cookies["keywords"],
                    }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = ex.GetBaseException().Message,
                });
            }
        }

        [Route("versionselector/select")]
        public JsonResult Select(string server, Guid allcards, Guid allrunes, Guid allskills, Guid allmapstage, Guid allmaphardstage, Guid keywords)
        {
            try
            {
                var version = GameDataManager.Load(allcards, allrunes, allskills, allmapstage, allmaphardstage, keywords);
                var gdm = GameDataManager.Get(version);

                Response.Cookies.Append("server", server, new CookieOptions { Expires = DateTime.MaxValue });
                Response.Cookies.Append("version", version.ToString());
                Response.Cookies.Append("allcards", gdm.allcards.ToString());
                Response.Cookies.Append("allrunes", gdm.allrunes.ToString());
                Response.Cookies.Append("allskills", gdm.allskills.ToString());
                Response.Cookies.Append("allmapstage", gdm.allmapstage.ToString());
                Response.Cookies.Append("allmaphardstage", gdm.allmaphardstage.ToString());
                Response.Cookies.Append("keywords", gdm.keywords.ToString());

                return new JsonResult(new
                {
                    success = true,
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = ex.GetBaseException().Message,
                });
            }
        }
    }
}