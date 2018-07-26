using HumphreyJ.NetCore.MKHX.GameData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public readonly Guid allcards, allrunes, allskills, allmapstage, allmaphardstage, keywords;

        public ParsedCardData[] CardList { get; set; }
        public ParsedRuneData[] RuneList { get; set; }
        public ParsedSkillData[] SkillList { get; set; }
        public ParsedMapStageData[] MapStageList { get; set; }
        public ParsedMapStageData[] MapHardStageList { get; set; }
        public ParsedKeywordData[] KeywordList { get; set; }
        private GameDataManager(Guid allcards, Guid allrunes, Guid allskills, Guid allmapstage, Guid allmaphardstage, Guid keywords)
        {
            //  初始化自身数据
            CardList = PreloadCardData(allcards);
            RuneList = PreloadRuneData(allrunes);
            SkillList = PreloadSkillData(allskills);
            MapStageList = PreloadMapStageData(allmapstage);
            MapHardStageList = PreloadMapHardStageData(allmaphardstage);
            KeywordList = PreloadKeywordData(keywords);

            //  初始化关系数据
            {
                SkillData_InitSkillRelationLists();
                {
                    CardData_InitSkillRelationLists();
                    CardData_InitCardRelationLists();
                    SkillData_InitCardRelationLists();
                }
                {
                    RuneData_InitSkillRelationLists();
                    SkillData_InitRuneRelationLists();
                }

                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }

            this.allcards = allcards;
            this.allrunes = allrunes;
            this.allskills = allskills;
            this.allmapstage = allmapstage;
            this.allmaphardstage = allmaphardstage;
            this.keywords = keywords;
        }

        private ParsedCardData[] PreloadCardData(Guid v) => new CardDataManager(v).Preload();
        private ParsedRuneData[] PreloadRuneData(Guid v) => new RuneDataManager(v).Preload();
        private ParsedSkillData[] PreloadSkillData(Guid v) => new SkillDataManager(v).Preload();
        private ParsedMapStageData[] PreloadMapStageData(Guid v) => new MapStageDataManager(v).Preload();
        private ParsedMapStageData[] PreloadMapHardStageData(Guid v) => new MapStageDataManager(v).Preload();
        private ParsedKeywordData[] PreloadKeywordData(Guid v) => new KeywordDataManager(v).Preload();

        public static Guid Load(Guid allcards, Guid allrunes, Guid allskills, Guid allmapstage, Guid allmaphardstage, Guid keywords)
        {
            var kv = GameDataList.FirstOrDefault(m => m.Value.allcards == allcards && m.Value.allrunes == allrunes && m.Value.allskills == allskills && m.Value.allmapstage == allmapstage && m.Value.allmaphardstage == allmaphardstage && m.Value.keywords == keywords);
            if (kv.Key == default(Guid))
            {
                var value = new GameDataManager(allcards, allrunes, allskills, allmapstage, allmaphardstage, keywords);

                var id = new string[] {
                    allcards.ToString("N").Substring(0,8),
                    allrunes.ToString("N").Substring(0,4),
                    allskills.ToString("N").Substring(0,4),
                    allmapstage.ToString("N").Substring(0,4),
                    allmaphardstage.ToString("N").Substring(0,6),
                    keywords.ToString("N").Substring(0,6),
                };

                var key = Guid.Parse(string.Join("", id));
                GameDataList.Add(key, value);
                return key;
            }
            else
            {
                return kv.Key;
            }
        }
        public static Dictionary<Guid, GameDataManager> GameDataList { get; private set; } = new Dictionary<Guid, GameDataManager>();
        public static GameDataManager Get(Guid v) => GameDataList[v];
        public static GameDataManager Get(HttpRequest Request)
        {
            try
            {
                var id = Guid.Parse(Request.Cookies["version"]);
                if (!GameDataList.ContainsKey(id))
                {
                    var allcards = Guid.Parse(Request.Cookies["allcards"]);
                    var allrunes = Guid.Parse(Request.Cookies["allrunes"]);
                    var allskills = Guid.Parse(Request.Cookies["allskills"]);
                    var allmapstage = Guid.Parse(Request.Cookies["allmapstage"]);
                    var allmaphardstage = Guid.Parse(Request.Cookies["allmaphardstage"]);
                    var keywords = Guid.Parse(Request.Cookies["keywords"]);
                    Load(allcards, allrunes, allskills, allmapstage, allmaphardstage, keywords);
                }
                return Get(id);
            }
            catch (Exception ex)
            {
                throw new NeedVersionSelectedException(ex);
            }
        }
        public static ParsedCardData[] GetCardData(HttpRequest Request) => Get(Request).CardList;
        public static ParsedRuneData[] GetRuneData(HttpRequest Request) => Get(Request).RuneList;
        public static ParsedSkillData[] GetSkillData(HttpRequest Request) => Get(Request).SkillList;
        public static ParsedMapStageData[] GetMapStageData(HttpRequest Request) => Get(Request).MapStageList;
        public static ParsedMapStageData[] GetMapHardStageData(HttpRequest Request) => Get(Request).MapHardStageList;
        public static ParsedKeywordData[] GetKeyWordData(HttpRequest Request) => Get(Request).KeywordList;

    }
}

public class NeedVersionSelectedException : Exception
{
    public NeedVersionSelectedException(Exception ex) : base("需要选择数据版本", ex) { }
}