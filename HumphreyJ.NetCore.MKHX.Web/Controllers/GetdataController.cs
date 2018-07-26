using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.Web.Models;
using HumphreyJ.NetCore.MKHX.DataBase;
using HumphreyJ.NetCore.MKHX.Web.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using HumphreyJ.NetCore.MKHX.OSS;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class GetdataController : Controller
    {
        [Route("getgamedata/{id}")]
        public IActionResult GameData(Guid id)
        {
            var content = OssHelper.GetString($"data/{id}.json");
            return new ContentResult
            {
                ContentType = "application/json",
                Content = content,
            };
        }

        [Route("getdata/webcard/img_maxCard_{id}.jpg")]
        public RedirectResult WebMaxCard(int id)
        {
            return new RedirectResult("http://cache.ifreecdn.com/mkhx_web/20150715/public/swf/card/192_275/img_maxCard_" + id + ".jpg", true);
        }

        [Route("getdata/webcard/img_photoCard_{id}.jpg")]
        public RedirectResult WebPhotoCard(int id)
        {
            return new RedirectResult("http://cache.ifreecdn.com/mkhx_web/20150715/public/swf/card/80_80/img_photoCard_" + id + ".jpg", true);
        }

        const int levels = 16;
        [Route("getdata/cardrank")]
        public JsonResult CardRank(int cardid)
        {

            var dbContext = new MkhxCoreContext();
            var dm = GameDataManager.Get(Request);
            var cardList = dm.CardList;

            var card = cardList.FirstOrDefault(m => m.CardId == cardid);

            var HpArray = cardList.Select(m => m.HpArray).ToList();
            var AttackArray = cardList.Select(m => m.AttackArray).ToList();

            var HpRank = new int[levels];
            var AttackRank = new int[levels];

            for (var i = 0; i < levels; i++)
            {
                HpRank[i] = HpArray.Select(m => m[i]).OrderByDescending(m => m).ToList().IndexOf(card.HpArray[i]) + 1;
                AttackRank[i] = AttackArray.Select(m => m[i]).OrderByDescending(m => m).ToList().IndexOf(card.AttackArray[i]) + 1;
            }

            return new JsonResult(new
            {
                CardsCount = cardList.Length,
                HpRank,
                AttackRank
            });

        }

        [Route("getdata/keywords")]
        public JsonResult KeyWords()
        {
            var dbContext = new MkhxCoreContext();
            var dm = GameDataManager.Get(Request);
            var keywordList = dm.KeywordList;

            return new JsonResult(keywordList.ToDictionary(m => m.id.ToString(), m => new
            {
                m.key,
                m.des,
            }));
        }

    }
}