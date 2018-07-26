using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.GameData;
using HumphreyJ.NetCore.MKHX.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web.Controllers
{
    public class MapStageDataController : Controller
    {
        [Route("mapstagedata")]
        public IActionResult Index()
        {
            try
            {
                var dm = GameDataManager.Get(Request);
                var normalMapStages = dm.MapStageList;
                var hardMapStages = dm.MapHardStageList;

                ViewData["normalMapStages"] = normalMapStages;
                ViewData["hardMapStages"] = hardMapStages;
                return View();
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("mapstagedata/{id}")]
        [Route("mapstagedata/normal/{id}")]
        public IActionResult Normal(string id)
        {
            try
            {
                var dm = GameDataManager.Get(Request);
                var MapStageData = dm.MapStageList;

                ViewData["type"] = "normal";
                var Id = id.Split('-');
                switch (Id.Length)
                {
                    case 1:
                        {
                            try
                            {
                                var MapStage = MapStageData.First(m => FindMapStageData(m, Id));
                                ViewData["Prev"] = MapStageData.FirstOrDefault(m => m.MapStageId == MapStage.Prev);
                                ViewData["Next"] = MapStageData.FirstOrDefault(m => m.MapStageId == MapStage.Next);
                                return View("MapStage", MapStage);
                            }
                            catch
                            {
                                ViewData["id"] = id;
                                return View("StatusCode404");
                            }
                        }
                    case 2:
                        {
                            try
                            {
                                var MapStage = MapStageData.First(m => FindMapStageData(m, Id));
                                var MapStageDetail = MapStage.MapStageDetails.First(m => FindMapStageData(m, Id));
                                return View("MapStageDetail", MapStageDetail);
                            }
                            catch
                            {
                                ViewData["id"] = id;
                                return View("StatusCode404");
                            }
                        }
                    default:
                        {
                            ViewData["id"] = id;
                            return View("StatusCode404");
                        }
                }
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        [Route("mapstagedata/hard/{id}")]
        public IActionResult Hard(string id)
        {
            try
            {
                var dm = GameDataManager.Get(Request);
                var MapHardStageData = dm.MapHardStageList;

                ViewData["type"] = "hard";
                var Id = id.Split('-');
                switch (Id.Length)
                {
                    case 1:
                        {
                            try
                            {
                                var MapStage = MapHardStageData.First(m => FindMapStageData(m, Id));
                                ViewData["Prev"] = MapHardStageData.FirstOrDefault(m => m.MapStageId == MapStage.Prev);
                                ViewData["Next"] = MapHardStageData.FirstOrDefault(m => m.MapStageId == MapStage.Next);
                                return View("MapStage", MapStage);
                            }
                            catch
                            {
                                ViewData["id"] = id;
                                return View("StatusCode404");
                            }
                        }
                    case 2:
                        {
                            try
                            {
                                var MapStage = MapHardStageData.First(m => FindMapStageData(m, Id));
                                var MapStageDetail = MapStage.MapStageDetails.First(m => FindMapStageData(m, Id));
                                return View("MapStageDetail", MapStageDetail);
                            }
                            catch
                            {
                                ViewData["id"] = id;
                                return View("StatusCode404");
                            }
                        }
                    default:
                        {
                            ViewData["id"] = id;
                            return View("StatusCode404");
                        }
                }
            }
            catch (NeedVersionSelectedException)
            {
                return View("Blank");
            }
        }

        private static bool FindMapStageData(ParsedMapStageData m, string[] Id)
        {
            return m.Name.Trim().ToLower() == Id[0].Trim().ToLower() || (int.TryParse(Id[0], out int r) && m.Rank + 1 == r);
        }
        private static bool FindMapStageData(ParsedMapStageDetailData m, string[] Id)
        {
            return m.Name.Trim().ToLower() == Id[1].Trim().ToLower() || (int.TryParse(Id[1], out int r) && m.Rank + 1 == r);
        }

    }
}