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
        public static Dictionary<int, AffectTypeContent> AffectTypeContent { get; } = new MkhxCoreContext().AffectTypeContent.ToDictionary(m => m.AffectType, m => m);

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
                            list = list.OrderBy(m => m.Name);
                            break;
                        }
                    case "reversename":
                        {
                            list = list.OrderBy(m => m.Name.Reverse());
                            break;
                        }
                    case "skillcategory":
                        {
                            list = list.OrderBy(m => m.SkillCategory);
                            break;
                        }
                    case "affecttype":
                        {
                            list = list.OrderBy(m => m.AffectType);
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

                return View(list.GroupBy(m => m.Abbreviation));
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("skilldata/{id}")]
        [Route("skilldata/detail/{id}")]
        public IActionResult Detail(string id, string all, string affecttype)
        {

            try
            {
                var dbContext = new MkhxCoreContext();
                var dm = GameDataManager.Get(Request);

                ViewData["AffectTypeContent"] = AffectTypeContent;
                ViewData["GameDataManager"] = dm;

                if (string.IsNullOrEmpty(affecttype))
                {
                    var listall = dm.SkillList.Where(m => m.Abbreviation == id);
                    ViewData["listall"] = listall;

                    if (string.IsNullOrEmpty(all))
                    {
                        var list = new List<ParsedSkillData>();
                        var i = dm.SkillList.FirstOrDefault(m => m.SkillId + "" == id || m.Name == id);
                        if (i == null)
                        {
                            list.AddRange(listall);
                        }
                        else
                        {
                            list.Add(i);
                        }
                        if (list.Count() < 1)
                        {
                            return new NotFoundResult();
                        }
                        else
                        {
                            ViewBag.listall = dm.SkillList.Where(m => m.Abbreviation == i.Abbreviation);
                            {
                                var sr = Request.Cookies["sr"] ?? "";
                                if (!sr.Contains((char)(i.SkillId + 1024)))
                                {
                                    Response.Cookies.Append("sr", sr + ((char)(i.SkillId + 1024)));
                                    dbContext.PvCounter.Add(new PvCounter
                                    {
                                        Id = Guid.NewGuid(),
                                        Time = DateTime.Now,
                                        Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                                        Ua = Request.Headers["User-Agent"],
                                        Type = "skill",
                                        Name = i.Abbreviation,
                                    });
                                    var timeDelete = DateTime.Now.AddDays(-7);
                                    dbContext.PvCounter.RemoveRange(dbContext.PvCounter.Where(m => m.Time < timeDelete));
                                    dbContext.SaveChanges();
                                }
                            }
                            return View(list.OrderBy(m => m.SkillId));
                        }
                    }
                    else
                    {
                        var i = listall.FirstOrDefault() ?? dm.SkillList.FirstOrDefault(m => m.SkillId + "" == id || m.Name == id);
                        if (i == null) {
                            return new NotFoundResult();
                        }
                        {
                            var sr = Request.Cookies["sr"] ?? "";
                            if (!sr.Contains((char)(i.SkillId + 1024)))
                            {
                                Response.Cookies.Append("sr", sr + ((char)(i.SkillId + 1024)));
                                dbContext.PvCounter.Add(new PvCounter
                                {
                                    Id = Guid.NewGuid(),
                                    Time = DateTime.Now,
                                    Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                                    Ua = Request.Headers["User-Agent"],
                                    Type = "skill",
                                    Name = i.Abbreviation,
                                });
                                var timeDelete = DateTime.Now.AddDays(-7);
                                dbContext.PvCounter.RemoveRange(dbContext.PvCounter.Where(m => m.Time < timeDelete));
                                dbContext.SaveChanges();
                            }
                        }
                        return View(listall.OrderBy(m => m.SkillId));
                    }
                }
                else
                {
                    if (int.TryParse(affecttype, out int AffectType))
                    {
                        var affectType = dbContext.AffectTypeContent.FirstOrDefault(m => m.AffectType == AffectType);
                        if (affecttype == null || string.IsNullOrEmpty(affectType.AffectValue1 + affectType.AffectValue2 + affectType.Desc))
                        {
                            return new NotFoundResult();
                        }

                        var SkillList = dm.SkillList.Where(m => m.IsBattleSkill && m.AffectType == AffectType);
                        ViewData["SkillList"] = SkillList.OrderBy(m => m.SkillId);
                        return View("detail_affectType", affectType);
                    }
                    else
                    {
                        return new NotFoundResult();
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
                ViewData["AffectTypeContent"] = AffectTypeContent;

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
    }
}