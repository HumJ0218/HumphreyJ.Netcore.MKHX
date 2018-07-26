using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.DataBase;
using HumphreyJ.NetCore.MKHX.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class ArticleController : Controller
    {
        [Route("article")]
        public IActionResult Index()
        {
            var dbContext = new MkhxCoreContext();
            return View(dbContext.V_Article.OrderByDescending(m => m.CreateTime));
        }

        [Route("article/{id}")]
        public IActionResult Detail(Guid id)
        {
            var dbContext = new MkhxCoreContext();
            var article = dbContext.Article.FirstOrDefault(m => m.Id == id);
            if (article == null)
            {
                return new NotFoundResult();
            }
            else
            {
                var ba = id.ToByteArray();
                var c = (char)(ba[4] * 0x100 + ba[5]);

                {
                    var va = Request.Cookies["va"] ?? "";
                    if (!va.Contains(c))
                    {
                        article.AccessCount++;
                        dbContext.SaveChanges();
                        Response.Cookies.Append("va", va + c);
                    }
                }

                return View("Detail", article);
            }
        }

        #region 文章动态数据
        [Route("getdata/journeydeck")]
        public JsonResult JourneyDeck(string server)
        {
            var dbContext = new MkhxCoreContext();

            if (string.IsNullOrEmpty(server))
            {
                return new JsonResult(dbContext.V_JourneyDeck.GroupBy(m => m.Server).ToDictionary(m => m.Key, m => m.GroupBy(n => n.DefendPlayerName).ToDictionary(n => n.Key, n => n)));
            }
            else
            {
                var dm = GameDataManager.Get(Request);
                var cardList = dm.CardList;
                var runeList = dm.RuneList;
                var skillList = dm.SkillList;

                return new JsonResult(dbContext.V_JourneyDeck.Where(m => m.Server == server).GroupBy(m => m.DefendPlayerName).ToDictionary(n => n.Key, n => n.Select(i => new
                {
                    i.DefendPlayerName,
                    DefendPlayerCards = i.DefendPlayerCards.Split(',').Select(m => m.Split('_')).Select(c => new
                    {
                        Card = cardList.FirstOrDefault(m => m.CardId + "" == c[0] || m.CardName == c[0]),
                        Level = int.Parse(c[1]),
                        BonusSkill = skillList.FirstOrDefault(m => m.SkillId + "" == c[2] || m.Name == c[2]),
                        Attack = double.Parse(c[3]),
                        Hp = double.Parse(c[4]),
                    }),
                    DefendPlayerRunes = i.DefendPlayerRunes.Split(',').Select(m => m.Split('_')).Select(r => new
                    {
                        Rune = runeList.FirstOrDefault(m => m.RuneId + "" == r[0] || m.RuneName == r[0]),
                        Level = int.Parse(r[1]),
                    }),
                }).Select(i => new
                {
                    i.DefendPlayerName,
                    DefendPlayerCards = i.DefendPlayerCards.Select(c => new
                    {
                        c.Card.CardId,
                        c.Card.CardName,
                        c.Card.Color,
                        c.Card.Race,
                        c.Level,
                        BonusSkill = c.BonusSkill == null ? null : new
                        {
                            c.BonusSkill.SkillId,
                            c.BonusSkill.Name,
                            c.BonusSkill.SkillCategory,
                        },
                        c.Attack,
                        c.Hp,
                    }),
                    DefendPlayerRunes = i.DefendPlayerRunes.Select(r => new
                    {
                        r.Rune.RuneId,
                        r.Rune.RuneName,
                        r.Rune.Color,
                        r.Rune.Property,
                        r.Level,
                    }),
                    HpSum = i.DefendPlayerCards.Sum(h => h.Hp)
                }).OrderBy(i => i.HpSum)));
            }
        }

        [Route("getdata/herohp")]
        public JsonResult HeroHp(string server)
        {
            var dbContext = new MkhxCoreContext();

            if (string.IsNullOrEmpty(server))
            {
                server = Request.Cookies["server"];
            }
            return new JsonResult(dbContext.V_HeroHp.Where(m => m.Server == server).Select(m => new { m.Server, m.Level, m.Hp }).OrderByDescending(m => m.Level));
        }

        [Route("article/card2d")]
        public IActionResult Card2d()
        {
            var dm = GameDataManager.Get(Request);
            var cardList = dm.CardList;
            var list = cardList.Where(m => dm.CardData_GetIsBattleCard(m));

            return View(list);
        }
        #endregion
    }
}