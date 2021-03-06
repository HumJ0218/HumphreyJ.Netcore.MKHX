﻿using System;
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
            var s = Request.Cookies["server"];
            return View(
                dbContext.V_Article
                .Where(m => string.IsNullOrEmpty(m.Server) || (" " + m.Server + " ").Contains(" " + s + " ") || s[0] == 'T')
                .OrderByDescending(m => m.CreateTime)
            );
        }

        [Route("article/{id}")]
        public IActionResult Detail(string id)
        {
            var dbContext = new MkhxCoreContext();
            var g = Guid.TryParse(id, out Guid guid);
            var article = g ? dbContext.Article.FirstOrDefault(m => m.Id == guid) : dbContext.Article.FirstOrDefault(m => m.Title.ToLower() == id.ToLower());
            if (article == null)
            {
                return new NotFoundResult();
            }
            else
            {

                {
                    var ba = article.Id.ToByteArray();
                    var c = (char)(ba[4] * 0x100 + ba[5]);

                    var va = Request.Cookies["va"] ?? "";
                    if (!va.Contains(c))
                    {
                        article.AccessCount++;
                        dbContext.SaveChanges();
                        Response.Cookies.Append("va", va + c);
                    }
                }

                switch (article.Content.Split('|')[0].ToLower())
                {
                    case "{url}":
                        {
                            var url = article.Content.Split('|')[1].Trim();
                            return new RedirectResult(url, true);
                        }
                    default:
                        {
                            return View("Detail", article);
                        }
                }

            }
        }

        #region 文章动态数据
        [Route("getdata/journeydeck")]
        public IActionResult JourneyDeck(string server)
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
        public IActionResult HeroHp(string server)
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

        [Route("article/cardfragment")]
        public IActionResult CardFragment()
        {
            var dm = GameDataManager.Get(Request);
            var cardList = dm.CardList;

            ViewData["data"] = cardList
                .GroupBy(m => m.Rank)
                .ToDictionary(rank => rank.Key, rank => rank.GroupBy(card => (card.CanDecompose > 0 ? 1 : 0) * 0b1 + (card.DecomposeGet > 0 ? 1 : 0) * 0b10).ToDictionary(m => m.Key, m => m.ToArray()));
            ViewData["keyMask"] = new Dictionary<int, string> {
                { 0b1, "可以分解" },
                { 0b10, "可以获得" },
            };

            return View();
        }

        [Route("article/allcards_Rank")]
        public IActionResult Allcards_Rank()
        {
            return new RedirectResult("/article/cardfragment", true);
        }
        #endregion

    }
}