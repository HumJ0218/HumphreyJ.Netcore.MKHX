using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.Web.Models;
using HumphreyJ.NetCore.MKHX.DataBase;
using HumphreyJ.NetCore.MKHX.Web.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using HumphreyJ.NetCore.MKHX.OSS;
using HumphreyJ.NetCore.MKHX.GameData;
using static HumphreyJ.NetCore.MKHX.Web.Models.GameDataManager;

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

        [Route("getdata/datacompare")]
        public JsonResult DataCompare(string type, Guid oldVersion, Guid newVersion)
        {

            try
            {
                var dbContext = new MkhxCoreContext();

                var oldGuid = oldVersion;
                var newGuid = newVersion;
                object a, d, c;

                switch (type.ToLower())
                {
                    default: throw new NotImplementedException();
                    case "card":
                        {
                            var oldData = new CardDataManager(oldGuid).Preload();
                            var newData = new CardDataManager(newGuid).Preload();
                            DataComparer.CardDataComparer(oldData, newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed);
                            a = added.Select(m => newData.First(n => n.CardId.ToString() == m).CardName);
                            d = deleted.Select(m => oldData.First(n => n.CardId.ToString() == m).CardName);
                            c = changed;
                            break;
                        }
                    case "rune":
                        {
                            var oldData = new RuneDataManager(oldGuid).Preload();
                            var newData = new RuneDataManager(newGuid).Preload();
                            DataComparer.RuneDataComparer(oldData, newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed);
                            a = added.Select(m => newData.First(n => n.RuneId.ToString() == m).RuneName);
                            d = deleted.Select(m => oldData.First(n => n.RuneId.ToString() == m).RuneName);
                            c = changed;
                            break;
                        }
                    case "skill":
                        {
                            var oldData = new SkillDataManager(oldGuid).Preload();
                            var newData = new SkillDataManager(newGuid).Preload();
                            DataComparer.SkillDataComparer(oldData, newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed);
                            a = added.Select(m => newData.First(n => n.SkillId.ToString() == m).Name);
                            d = deleted.Select(m => oldData.First(n => n.SkillId.ToString() == m).Name);
                            c = changed;
                            break;
                        }
                    //case "mapstage":
                    //    {
                    //        var oldData = RawMapStageData.ParseJsonString(dbContext.GameDataFiles.First(m => m.Id == oldGuid).FileContent).Select(m => new ParsedMapStageData(m)).ToList();
                    //        var newData = RawMapStageData.ParseJsonString(dbContext.GameDataFiles.First(m => m.Id == newGuid).FileContent).Select(m => new ParsedMapStageData(m)).ToList();
                    //        DataComparer.MapStageDataComparer(oldData, newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed);
                    //        a = added;
                    //        d = deleted;
                    //        c = changed;
                    //        break;
                    //    }
                    case "keyword":
                        {
                            var oldData = new KeywordDataManager(oldGuid).Preload();
                            var newData = new KeywordDataManager(newGuid).Preload();
                            DataComparer.KeywordDataComparer(oldData, newData, out List<string> added, out List<string> deleted, out Dictionary<string, string> changed);
                            a = added.Select(m => newData.First(n => n.key.ToString() == m).key);
                            d = deleted.Select(m => oldData.First(n => n.key.ToString() == m).key);
                            c = changed;
                            break;
                        }
                }

                return new JsonResult(new
                {
                    success = true,
                    added = a,
                    deleted = d,
                    changed = c,
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = new
                    {
                        e.GetBaseException().Message,
                        e.GetBaseException().Source,
                        e.GetBaseException().StackTrace,
                    },
                });
            }

        }

    }
}