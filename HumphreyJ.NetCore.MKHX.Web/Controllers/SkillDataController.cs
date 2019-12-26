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
    public class SkillDataController : Controller
    {
        [Route("skilldata")]
        public IActionResult Index()
        {
            try
            {
                var type = string.Join(",", Request.Query["type"]).ToLower();
                var skillCategory = string.Join(",", Request.Query["skillCategory"]).ToLower();
                var orderby = string.Join(",", Request.Query["orderby"]).ToLower();
                var desc = string.Join(",", Request.Query["desc"]).ToLower();

                ViewData["type"] = type;
                ViewData["skillCategory"] = skillCategory;
                ViewData["orderby"] = orderby;
                ViewData["desc"] = desc;

                var dm = GameDataManager.Get(Request);
                var skillList = dm.SkillList;

                var list = (IEnumerable<ParsedSkillData>)skillList;

                switch (type.ToLower())
                {
                    default:
                        {
                            list = list.Where(m => m.IsBattleSkill && !m.IsMultipleSkill);
                            break;
                        }
                    case "multipleskill":
                        {
                            list = list.Where(m => m.IsMultipleSkill);
                            break;
                        }
                    case "materialskill":
                        {
                            list = list.Where(m => !m.IsBattleSkill);
                            break;
                        }
                    case "all":
                        {
                            break;
                        }
                }

                try
                {
                    if (!string.IsNullOrEmpty(skillCategory))
                    {
                        var c = skillCategory.Split(',').Select(m => int.Parse(m));
                        list = list.Where(m => c.Contains(m.SkillCategory));
                    }
                }
                catch { }

                switch (orderby.ToLower())
                {
                    default:
                        {
                            list = list.OrderBy(m => m.SkillId);
                            break;
                        }
                    case "name":
                        {
                            list = list.OrderBy(m => m.Abbreviation);
                            break;
                        }
                    case "reversename":
                        {
                            list = list.OrderBy(m => string.Join("", m.Abbreviation.Reverse()));
                            break;
                        }
                    case "skillcategory":
                        {
                            list = list.OrderBy(m => m.SkillCategory);
                            break;
                        }
                    case "affecttype":
                        {
                            list = list.OrderBy(m => m.AffectType[0]);
                            break;
                        }
                    case "lanchtype":
                        {
                            list = list.OrderBy(m => m.LanchType);
                            break;
                        }
                    case "lanchcondition":
                        {
                            list = list.OrderBy(m => m.LanchCondition);
                            break;
                        }
                }

                if (desc == "1")
                {
                    list = list.Reverse();
                }

                ViewData["AffectTypeContent"] = AffectTypeContent.List;
                return View(list.GroupBy(m => m.Abbreviation));
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("skilldata/{id}")]
        [Route("skilldata/detail/{id}")]
        public IActionResult Detail(string id, string all)
        {
            try
            {
                var dbContext = new MkhxCoreContext();
                var dm = GameDataManager.Get(Request);

                ViewData["AffectTypeContent"] = AffectTypeContent.List;
                ViewData["GameDataManager"] = dm;

                {
                    var listall = dm.SkillList.Where(m => m.Abbreviation == id || m.SkillId + "" == id || m.Name == id);

                    var count = listall.Count();
                    if (count < 1)
                    {
                        return new NotFoundResult();
                    }

                    var skill = listall.First();
                    if (!int.TryParse(id, out int SkillId)) //  如果使用了名称选取，则跳转为编号选取，避免Edge浏览器Header编码问题
                    {
                        return new RedirectResult($"/skilldata/{skill.SkillId}{(string.IsNullOrEmpty(all) ? "" : $"?all={all}")}", false);
                    }
                    if (count == 1)
                    {
                        listall = dm.SkillList.Where(m => m.Abbreviation == skill.Abbreviation);
                    }

                    ViewData["listall"] = listall;

                    {
                        var sr = Request.Cookies["sr"] ?? "";
                        if (!sr.Contains((char)(skill.SkillId + 1024)))
                        {
                            Response.Cookies.Append("sr", sr + ((char)(skill.SkillId + 1024)));
                            dbContext.PvCounter.Add(new PvCounter
                            {
                                Id = Guid.NewGuid(),
                                Time = DateTime.Now,
                                Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                                Ua = Request.Headers["User-Agent"],
                                Type = "skill",
                                Name = skill.Abbreviation,
                            });
                            var timeDelete = DateTime.Now.AddDays(-7);
                            dbContext.PvCounter.RemoveRange(dbContext.PvCounter.Where(m => m.Time < timeDelete));
                            dbContext.SaveChanges();
                        }
                    }

                    if (string.IsNullOrEmpty(all))
                    {
                        //  选择特定技能
                        return View(new ParsedSkillData[] { skill });
                    }
                    else
                    {
                        //  选择所有同系列技能
                        return View(listall.OrderBy(m => m.SkillId));
                    }

                }
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("skilldata/fromkw/{id}")]
        public IActionResult DetailFromKW(int id)
        {
            try
            {
                var dm = GameDataManager.Get(Request);
                ViewData["AffectTypeContent"] = AffectTypeContent.List;

                try
                {
                    var skill = dm.SkillList.First(m => m.SkillId == id);
                    var abbr = skill.Abbreviation;
                    var path = System.Web.HttpUtility.UrlPathEncode("/skilldata/detail/" + abbr + "?all=1");
                    return new RedirectResult(path, true);
                }
                catch
                {
                    return new NotFoundResult();
                }

            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("skilldata/affecttype/{id}")]
        public IActionResult AffectType(string id)
        {
            try
            {
                var dbContext = new MkhxCoreContext();
                var dm = GameDataManager.Get(Request);

                ViewData["AffectTypeContent"] = AffectTypeContent.List;
                ViewData["GameDataManager"] = dm;

                if (int.TryParse(id, out int AffectType))
                {

                    if (id == null || !AffectTypeContent.List.TryGetValue(AffectType, out AffectTypeContent affectType))
                    {
                        return new NotFoundResult();
                    }

                    var SkillList = dm.SkillList.Where(m => m.IsBattleSkill && m.AffectType[0] == AffectType);
                    ViewData["SkillList"] = SkillList.OrderBy(m => m.SkillId);
                    return View("detail_affectType", affectType);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

    }
}