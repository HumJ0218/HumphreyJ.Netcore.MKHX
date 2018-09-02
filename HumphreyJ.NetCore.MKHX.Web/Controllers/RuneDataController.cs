using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.GameData;
using HumphreyJ.NetCore.MKHX.Web.Models;
using HumphreyJ.NetCore.MKHX.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class RuneDataController : Controller
    {

        [Route("runedata")]
        public IActionResult Index()
        {
            try
            {
                var color = string.Join(",", Request.Query["color"]).ToLower();
                var property = string.Join(",", Request.Query["property"]).ToLower();
                var orderby = string.Join(",", Request.Query["orderby"]).ToLower();
                var desc = string.Join(",", Request.Query["desc"]).ToLower();

                var dm = GameDataManager.Get(Request);
                var runeList = dm.RuneList;

                var list = (IEnumerable<ParsedRuneData>)runeList;

                try
                {
                    if (!string.IsNullOrEmpty(color))
                    {
                        var c = color.Split(',').Select(m => int.Parse(m));
                        list = list.Where(m => c.Contains(m.Color));
                    }
                }
                catch { }

                try
                {
                    if (!string.IsNullOrEmpty(property))
                    {
                        var r = property.Split(',').Select(m => int.Parse(m));
                        list = list.Where(m => r.Contains(m.Property));
                    }
                }
                catch { }

                switch (orderby.ToLower())
                {
                    default:
                        {
                            list = list.OrderBy(m => m.RuneId);
                            break;
                        }
                    case "name":
                        {
                            list = list.OrderBy(m => m.RuneName);
                            break;
                        }
                    case "reversename":
                        {
                            list = list.OrderBy(m => m.RuneName.Reverse());
                            break;
                        }
                    case "color":
                        {
                            list = list.OrderBy(m => m.Color);
                            break;
                        }
                    case "property":
                        {
                            list = list.OrderBy(m => m.Property);
                            break;
                        }
                }

                if (!string.IsNullOrEmpty(desc))
                {
                    list = list.Reverse();
                }

                return View(list);
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("runedata/{id}")]
        [Route("runedata/detail/{id}")]
        public IActionResult Detail(string id)
        {
            try
            {
                var dbContext = new MkhxCoreContext();
                var dm = GameDataManager.Get(Request);
                var runeList = dm.RuneList;

                ParsedRuneData rune = runeList.FirstOrDefault(m => m.RuneId + "" == id || m.RuneName == id);

                if (rune == null)
                {
                    return new NotFoundResult();
                }
                else
                {
                    if (!int.TryParse(id, out int RuneId))  //  如果使用了名称选取，则跳转为编号选取，避免Edge浏览器Header编码问题
                    {
                        return new RedirectResult($"/runedata/{rune.RuneId}", false);
                    }

                    {
                        var ShowInMapStageLevel = dm.RuneData_GetShowInMapStageLevel(rune);
                        ViewData["ShowInMapStageLevel"] = ShowInMapStageLevel;
                    }
                    {
                        var RewardInMapStage = dm.RuneData_GetRewardInMapStage(rune);
                        var list = new List<KeyValuePair<ParsedMapStageData, int>>();
                        list.AddRange(RewardInMapStage);
                        ViewData["RewardInMapStage"] = list.Distinct().ToArray();
                    }
                    {
                        var RewardInMapStageLevel = dm.RuneData_GetRewardInMapStageLevel(rune);
                        var list = new List<KeyValuePair<ParsedMapStageDetailLevelData, int>>();
                        list.AddRange(RewardInMapStageLevel);
                        ViewData["RewardInMapStageLevel"] = list.Distinct().ToArray();
                    }
                    {
                        var vr = Request.Cookies["vr"] ?? "";
                        if (!vr.Contains((char)(rune.RuneId + 1024)))
                        {
                            Response.Cookies.Append("vr", vr + ((char)(rune.RuneId + 1024)));
                            dbContext.PvCounter.Add(new PvCounter
                            {
                                Id = Guid.NewGuid(),
                                Time = DateTime.Now,
                                Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                                Ua = Request.Headers["User-Agent"],
                                Type = "rune",
                                Name = rune.RuneName,
                            });
                            var timeDelete = DateTime.Now.AddDays(-7);
                            dbContext.PvCounter.RemoveRange(dbContext.PvCounter.Where(m => m.Time < timeDelete));
                            dbContext.SaveChanges();
                        }
                    }
                    return View(rune);
                }
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }
    }
}