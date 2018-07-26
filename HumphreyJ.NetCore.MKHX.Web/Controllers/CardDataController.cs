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
    public class CardDataController : Controller
    {
        [Route("carddata")]
        public IActionResult Index()
        {
            try
            {
                var type = string.Join(",", Request.Query["type"]).ToLower();
                var color = string.Join(",", Request.Query["color"]).ToLower();
                var race = string.Join(",", Request.Query["race"]).ToLower();
                var orderby = string.Join(",", Request.Query["orderby"]).ToLower();
                var desc = string.Join(",", Request.Query["desc"]).ToLower();
                var view = string.Join(",", Request.Query["view"]).ToLower();

                ViewData["type"] = type;
                ViewData["color"] = color;
                ViewData["race"] = race;
                ViewData["orderby"] = orderby;
                ViewData["desc"] = desc;
                ViewData["view"] = view;

                var dm = GameDataManager.Get(Request);
                var cardList = dm.CardList;

                var list = (IEnumerable<ParsedCardData>)cardList;

                switch (type)
                {
                    default:
                        {
                            list = list.Where(m => dm.CardData_GetIsBattleCard(m));
                            break;
                        }
                    case "materialcards":
                        {
                            list = list.Where(m => !dm.CardData_GetIsBattleCard(m));
                            break;
                        }
                    case "all":
                        {
                            break;
                        }
                }

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
                    if (!string.IsNullOrEmpty(race))
                    {
                        var r = race.Split(',').Select(m => int.Parse(m));
                        list = list.Where(m => r.Contains(m.Race));
                    }
                }
                catch { }

                list = list.OrderBy(m => m.CardId);
                switch (orderby)
                {
                    default:
                        {
                            list = list.OrderBy(m => m.CardId);
                            break;
                        }
                    case "name":
                        {
                            list = list.OrderBy(m => m.CardName);
                            break;
                        }
                    case "reversename":
                        {
                            list = list.OrderBy(m => string.Join("", m.CardName.Reverse()));
                            break;
                        }
                    case "color":
                        {
                            list = list.OrderBy(m => m.Color);
                            break;
                        }
                    case "race":
                        {
                            list = list.OrderBy(m => m.Race);
                            break;
                        }
                    case "cost":
                        {
                            list = list.OrderBy(m => m.Cost);
                            break;
                        }
                    case "evocost":
                        {
                            list = list.OrderBy(m => m.EvoCost);
                            break;
                        }
                    case "wait":
                        {
                            list = list.OrderBy(m => m.Wait);
                            break;
                        }
                    case "atk0":
                        {
                            list = list.OrderBy(m => m.AttackArray[0]);
                            break;
                        }
                    case "atk10":
                        {
                            list = list.OrderBy(m => m.AttackArray[10]);
                            break;
                        }
                    case "atk15":
                        {
                            list = list.OrderBy(m => m.AttackArray[15]);
                            break;
                        }
                    case "hp0":
                        {
                            list = list.OrderBy(m => m.HpArray[0]);
                            break;
                        }
                    case "hp10":
                        {
                            list = list.OrderBy(m => m.HpArray[10]);
                            break;
                        }
                    case "hp15":
                        {
                            list = list.OrderBy(m => m.HpArray[15]);
                            break;
                        }
                    case "rank":
                        {
                            list = list.OrderBy(m => m.Rank);
                            break;
                        }
                }

                if (desc == "1")
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

        [Route("carddata/{id}")]
        [Route("carddata/detail/{id}")]
        public IActionResult Detail(string id)
        {
            try
            {
                var dbContext = new MkhxCoreContext();
                var dm = GameDataManager.Get(Request);
                var cardList = dm.CardList;

                ParsedCardData card = cardList.FirstOrDefault(m => m.CardId + "" == id || m.CardName == id);

                ViewData["id"] = id;
                if (card == null)
                {
                    return new NotFoundResult();
                }
                else
                {
                    {
                        var picture = dbContext.Picture.ToArray().Where(m => m.Name.Split(' ').Contains(card.CardName)).ToArray();
                        ViewData["picture"] = picture;
                    }
                    {
                        var SummonerCards = dm.CardData_GetAllSummonerCards(card);
                        ViewData["SummonerCards"] = SummonerCards;
                    }
                    {
                        var SummoneeCards = dm.CardData_GetAllSummoneeCards(card);
                        ViewData["SummoneeCards"] = SummoneeCards;
                    }
                    {
                        var ShowInMapStageLevel = dm.CardData_GetShowInMapStageLevel(card);
                        ViewData["ShowInMapStageLevel"] = ShowInMapStageLevel;
                    }
                    {
                        var RewardInMapStage = dm.CardData_GetRewardInMapStage(card);
                        var ChipRewardInMapStage = dm.CardData_GetChipRewardInMapStage(card);
                        var list = new List<KeyValuePair<ParsedMapStageData, int>>();
                        list.AddRange(RewardInMapStage);
                        list.AddRange(ChipRewardInMapStage);
                        ViewData["RewardInMapStage"] = list.Distinct().ToArray();
                    }
                    {
                        var RewardInMapStageLevel = dm.CardData_GetRewardInMapStageLevel(card);
                        var ChipRewardInMapStageLevel = dm.CardData_GetChipRewardInMapStageLevel(card);
                        var list = new List<KeyValuePair<ParsedMapStageDetailLevelData, int>>();
                        list.AddRange(RewardInMapStageLevel);
                        list.AddRange(ChipRewardInMapStageLevel);
                        ViewData["RewardInMapStageLevel"] = list.Distinct().ToArray();
                    }
                    {
                        var vc = Request.Cookies["vc"] ?? "";
                        if (!vc.Contains((char)(card.CardId + 1024)))
                        {
                            Response.Cookies.Append("vc", vc + ((char)(card.CardId + 1024)));
                            dbContext.PvCounter.Add(new PvCounter
                            {
                                Id = Guid.NewGuid(),
                                Time = DateTime.Now,
                                Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                                Ua = Request.Headers["User-Agent"],
                                Type = "card",
                                Name = card.CardName,
                            });
                            var timeDelete = DateTime.Now.AddDays(-7);
                            dbContext.PvCounter.RemoveRange(dbContext.PvCounter.Where(m => m.Time < timeDelete));
                            dbContext.SaveChanges();
                        }
                    }

                    return View(card);
                }
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

    }
}