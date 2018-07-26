using HumphreyJ.NetCore.MKHX.GameData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public class MapStageDataManager : DataManager
        {
            public MapStageDataManager(Guid version)
            {
                Version = version;
            }

            public new RawMapStageData[] RawData => GetRawData();
            public ParsedMapStageData[] ParsedData => GetParsedData();

            private RawMapStageData[] rawData;
            private RawMapStageData[] GetRawData()
            {
                if (rawData == null)
                {
                    rawData = RawMapStageData.ParseJsonString(base.RawData);
                }
                return rawData;
            }
            private static Dictionary<Guid, ParsedMapStageData[]> parsedData = new Dictionary<Guid, ParsedMapStageData[]>();
            private ParsedMapStageData[] GetParsedData()
            {
                if (!parsedData.ContainsKey(Version))
                {
                    parsedData.Add(Version, RawData.Select(m => new ParsedMapStageData(m)).ToArray());
                }
                return parsedData[Version];
            }

            internal ParsedMapStageData[] Preload()
            {
                var data = ParsedData;
                {
                    //  GC
                    rawData = null;
                }
                return data;
            }
        }

        private readonly Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]> CardData_ShowInMapStageLevel = new Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]>();
        private readonly Dictionary<ParsedRuneData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]> RuneData_ShowInMapStageLevel = new Dictionary<ParsedRuneData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]>();
        private readonly Dictionary<ParsedSkillData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]> SkillData_ShowInMapStageLevel = new Dictionary<ParsedSkillData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]>();

        private readonly Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]> CardData_RewardInMapStageLevel = new Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]>();
        private readonly Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]> CardData_ChipRewardInMapStageLevel = new Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]>();
        private readonly Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageData, int>[]> CardData_RewardInMapStage = new Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageData, int>[]>();
        private readonly Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageData, int>[]> CardData_ChipRewardInMapStage = new Dictionary<ParsedCardData, KeyValuePair<ParsedMapStageData, int>[]>();

        private readonly Dictionary<ParsedRuneData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]> RuneData_RewardInMapStageLevel = new Dictionary<ParsedRuneData, KeyValuePair<ParsedMapStageDetailLevelData, int>[]>();
        private readonly Dictionary<ParsedRuneData, KeyValuePair<ParsedMapStageData, int>[]> RuneData_RewardInMapStage = new Dictionary<ParsedRuneData, KeyValuePair<ParsedMapStageData, int>[]>();

        /// <summary>
        /// 初始化 卡牌、符文、技能-关卡 关系列表
        /// </summary>
        public void CardData_RuneData_SkillData_InitMapStageRelationLists()
        {
            var CardData = new Dictionary<ParsedCardData, List<KeyValuePair<ParsedMapStageDetailLevelData, int>>>();
            var RuneData = new Dictionary<ParsedRuneData, List<KeyValuePair<ParsedMapStageDetailLevelData, int>>>();
            var SkillData = new Dictionary<ParsedSkillData, List<KeyValuePair<ParsedMapStageDetailLevelData, int>>>();

            var CardRewardInMapStageLevelData = new Dictionary<ParsedCardData, List<KeyValuePair<ParsedMapStageDetailLevelData, int>>>();
            var CardChipRewardInMapStageLevelData = new Dictionary<ParsedCardData, List<KeyValuePair<ParsedMapStageDetailLevelData, int>>>();
            var CardRewardInMapStageData = new Dictionary<ParsedCardData, List<KeyValuePair<ParsedMapStageData, int>>>();
            var CardChipRewardInMapStageData = new Dictionary<ParsedCardData, List<KeyValuePair<ParsedMapStageData, int>>>();

            var RuneRewardInMapStageLevelData = new Dictionary<ParsedRuneData, List<KeyValuePair<ParsedMapStageDetailLevelData, int>>>();
            var RuneRewardInMapStageData = new Dictionary<ParsedRuneData, List<KeyValuePair<ParsedMapStageData, int>>>();

            void GetData(int difficulty, ParsedMapStageData[] MapStageList)
            {
                foreach (var ms in MapStageList)
                {
                    if (ms.OpenStatus == 0)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (var msd in ms.MapStageDetails)
                        {
                            foreach (var msdl in msd.Levels)
                            {
                                //  遍历卡牌列表
                                foreach (var c in msdl.CardList)
                                {
                                    var a = c.Split('_');
                                    var card = CardList.FirstOrDefault(m => m.CardId + "" == a[0] || m.CardName == a[0]);
                                    if (card == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //  添加卡牌
                                        if (CardData.ContainsKey(card))
                                        {
                                            CardData[card].Add(new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty));
                                        }
                                        else
                                        {
                                            CardData.Add(card, new List<KeyValuePair<ParsedMapStageDetailLevelData, int>> { new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty) });
                                        }

                                        //  添加技能
                                        if (a.Length > 2)
                                        {
                                            var skill = SkillList.FirstOrDefault(m => m.SkillId + "" == a[2] || m.Name == a[2]);
                                            if (skill == null)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                if (SkillData.ContainsKey(skill))
                                                {
                                                    SkillData[skill].Add(new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty));
                                                }
                                                else
                                                {
                                                    SkillData.Add(skill, new List<KeyValuePair<ParsedMapStageDetailLevelData, int>> { new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty) });
                                                }
                                            }
                                        }
                                    }
                                }

                                //  遍历符文列表
                                foreach (var r in msdl.RuneList)
                                {
                                    var a = r.Split('_');
                                    var rune = RuneList.FirstOrDefault(m => m.RuneId + "" == a[0] || m.RuneName == a[0]);
                                    if (rune == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (RuneData.ContainsKey(rune))
                                        {
                                            RuneData[rune].Add(new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty));
                                        }
                                        else
                                        {
                                            RuneData.Add(rune, new List<KeyValuePair<ParsedMapStageDetailLevelData, int>> { new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty) });
                                        }
                                    }
                                }

                                //  奖励列表
                                {
                                    var list = new List<string>();
                                    list.AddRange(msdl.BonusExplore);
                                    list.AddRange(msdl.BonusLose);
                                    list.AddRange(msdl.BonusWin);
                                    list.AddRange(msdl.FirstBonusWin);
                                    foreach (var i in list.Distinct())
                                    {
                                        var a = i.ToLower().Split('_');
                                        switch (a[0])
                                        {
                                            case "card":
                                                {
                                                    var card = CardList.FirstOrDefault(m => m.CardId + "" == a[1] || m.CardName == a[1]);
                                                    //  添加卡牌
                                                    if (CardRewardInMapStageLevelData.ContainsKey(card))
                                                    {
                                                        CardRewardInMapStageLevelData[card].Add(new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty));
                                                    }
                                                    else
                                                    {
                                                        CardRewardInMapStageLevelData.Add(card, new List<KeyValuePair<ParsedMapStageDetailLevelData, int>> { new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty) });
                                                    }
                                                    break;
                                                }
                                            case "rune":
                                                {
                                                    var rune = RuneList.FirstOrDefault(m => m.RuneId + "" == a[1] || m.RuneName == a[1]);
                                                    //  添加符文
                                                    if (RuneRewardInMapStageLevelData.ContainsKey(rune))
                                                    {
                                                        RuneRewardInMapStageLevelData[rune].Add(new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty));
                                                    }
                                                    else
                                                    {
                                                        RuneRewardInMapStageLevelData.Add(rune, new List<KeyValuePair<ParsedMapStageDetailLevelData, int>> { new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty) });
                                                    }
                                                    break;
                                                }
                                        }
                                    }

                                    //  卡牌碎片奖励
                                    if (msdl.BonusChip > 0)
                                    {
                                        var card = CardList.FirstOrDefault(m => m.CardId == msdl.BonusChip);
                                        //  添加卡牌
                                        if (CardChipRewardInMapStageLevelData.ContainsKey(card))
                                        {
                                            CardChipRewardInMapStageLevelData[card].Add(new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty));
                                        }
                                        else
                                        {
                                            CardChipRewardInMapStageLevelData.Add(card, new List<KeyValuePair<ParsedMapStageDetailLevelData, int>> { new KeyValuePair<ParsedMapStageDetailLevelData, int>(msdl, difficulty) });
                                        }
                                        break;
                                    }
                                }

                            }
                        }

                        //  通关奖励
                        {
                            foreach (var i in ms.FinishAward)
                            {
                                var a = i.ToLower().Split('_');
                                switch (a[0])
                                {
                                    case "card":
                                        {
                                            var card = CardList.FirstOrDefault(m => m.CardId + "" == a[1] || m.CardName == a[1]);
                                            //  添加卡牌
                                            if (CardRewardInMapStageData.ContainsKey(card))
                                            {
                                                CardRewardInMapStageData[card].Add(new KeyValuePair<ParsedMapStageData, int>(ms, difficulty));
                                            }
                                            else
                                            {
                                                CardRewardInMapStageData.Add(card, new List<KeyValuePair<ParsedMapStageData, int>> { new KeyValuePair<ParsedMapStageData, int>(ms, difficulty) });
                                            }
                                            break;
                                        }
                                    case "rune":
                                        {
                                            var rune = RuneList.FirstOrDefault(m => m.RuneId + "" == a[1] || m.RuneName == a[1]);
                                            //  添加符文
                                            if (RuneRewardInMapStageData.ContainsKey(rune))
                                            {
                                                RuneRewardInMapStageData[rune].Add(new KeyValuePair<ParsedMapStageData, int>(ms, difficulty));
                                            }
                                            else
                                            {
                                                RuneRewardInMapStageData.Add(rune, new List<KeyValuePair<ParsedMapStageData, int>> { new KeyValuePair<ParsedMapStageData, int>(ms, difficulty) });
                                            }
                                            break;
                                        }
                                }
                            }

                            //  通关探索碎片
                            foreach (var i in ms.HideChip.Where(m => m > 0))
                            {
                                var card = CardList.FirstOrDefault(m => m.CardId == i);
                                //  添加卡牌
                                if (CardChipRewardInMapStageData.ContainsKey(card))
                                {
                                    CardChipRewardInMapStageData[card].Add(new KeyValuePair<ParsedMapStageData, int>(ms, difficulty));
                                }
                                else
                                {
                                    CardChipRewardInMapStageData.Add(card, new List<KeyValuePair<ParsedMapStageData, int>> { new KeyValuePair<ParsedMapStageData, int>(ms, difficulty) });
                                }
                                break;
                            }
                        }

                    }
                }
            }

            GetData(0, MapStageList);
            GetData(1, MapHardStageList);

            var blank1 = new KeyValuePair<ParsedMapStageDetailLevelData, int>[] { };
            var blank2 = new KeyValuePair<ParsedMapStageData, int>[] { };
            void CopyList(dynamic fromListDictionary,dynamic toArrayDictionary,dynamic item,dynamic blankItem) {
                if (fromListDictionary.ContainsKey(item))
                {
                    toArrayDictionary.Add(item, fromListDictionary[item].ToArray());
                }
                else
                {
                    toArrayDictionary.Add(item, blankItem);
                }
            }
            foreach (var c in CardList)
            {
                CopyList(CardData, CardData_ShowInMapStageLevel, c, blank1);

                CopyList(CardRewardInMapStageLevelData, CardData_RewardInMapStageLevel, c, blank1);
                CopyList(CardChipRewardInMapStageLevelData, CardData_ChipRewardInMapStageLevel, c, blank1);
                CopyList(CardRewardInMapStageData, CardData_RewardInMapStage, c, blank2);
                CopyList(CardChipRewardInMapStageData, CardData_ChipRewardInMapStage, c, blank2);
            }
            foreach (var r in RuneList)
            {
                CopyList(RuneData, RuneData_ShowInMapStageLevel, r, blank1);

                CopyList(RuneRewardInMapStageLevelData, RuneData_RewardInMapStageLevel, r, blank1);
                CopyList(RuneRewardInMapStageData, RuneData_RewardInMapStage, r, blank2);
            }
            foreach (var s in SkillList)
            {
                CopyList(SkillData, SkillData_ShowInMapStageLevel, s, blank1);
            }
        }

        /// <summary>
        /// 获取卡牌出现的关卡
        /// </summary>
        public KeyValuePair<ParsedMapStageDetailLevelData, int>[] CardData_GetShowInMapStageLevel(ParsedCardData card)
        {
            if (!CardData_ShowInMapStageLevel.ContainsKey(card))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return CardData_ShowInMapStageLevel[card];
        }

        /// <summary>
        /// 获取符文出现的关卡
        /// </summary>
        public KeyValuePair<ParsedMapStageDetailLevelData, int>[] RuneData_GetShowInMapStageLevel(ParsedRuneData rune)
        {
            if (!RuneData_ShowInMapStageLevel.ContainsKey(rune))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return RuneData_ShowInMapStageLevel[rune];
        }

        /// <summary>
        /// 获取技能出现的关卡
        /// </summary>
        public KeyValuePair<ParsedMapStageDetailLevelData, int>[] SkillData_GetShowInMapStageLevel(ParsedSkillData skill)
        {
            if (!SkillData_ShowInMapStageLevel.ContainsKey(skill))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return SkillData_ShowInMapStageLevel[skill];
        }

        /// <summary>
        /// 获取奖励卡牌的关卡
        /// </summary>
        public KeyValuePair<ParsedMapStageDetailLevelData, int>[] CardData_GetRewardInMapStageLevel(ParsedCardData card)
        {
            if (!CardData_RewardInMapStageLevel.ContainsKey(card))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return CardData_RewardInMapStageLevel[card];
        }

        /// <summary>
        /// 获取奖励卡牌碎片的关卡
        /// </summary>
        public KeyValuePair<ParsedMapStageDetailLevelData, int>[] CardData_GetChipRewardInMapStageLevel(ParsedCardData card)
        {
            if (!CardData_ChipRewardInMapStageLevel.ContainsKey(card))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return CardData_ChipRewardInMapStageLevel[card];
        }

        /// <summary>
        /// 获取奖励卡牌的地图
        /// </summary>
        public KeyValuePair<ParsedMapStageData, int>[] CardData_GetRewardInMapStage(ParsedCardData card)
        {
            if (!CardData_RewardInMapStage.ContainsKey(card))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return CardData_RewardInMapStage[card];
        }

        /// <summary>
        /// 获取奖励卡牌碎片的地图
        /// </summary>
        public KeyValuePair<ParsedMapStageData, int>[] CardData_GetChipRewardInMapStage(ParsedCardData card)
        {
            if (!CardData_ChipRewardInMapStage.ContainsKey(card))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return CardData_ChipRewardInMapStage[card];
        }

        /// <summary>
        /// 获取奖励符文的关卡
        /// </summary>
        public KeyValuePair<ParsedMapStageDetailLevelData, int>[] RuneData_GetRewardInMapStageLevel(ParsedRuneData rune)
        {
            if (!RuneData_RewardInMapStageLevel.ContainsKey(rune))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return RuneData_RewardInMapStageLevel[rune];
        }

        /// <summary>
        /// 获取奖励符文的地图
        /// </summary>
        public KeyValuePair<ParsedMapStageData, int>[] RuneData_GetRewardInMapStage(ParsedRuneData rune)
        {
            if (!RuneData_RewardInMapStage.ContainsKey(rune))
            {
                CardData_RuneData_SkillData_InitMapStageRelationLists();
            }
            return RuneData_RewardInMapStage[rune];
        }

    }
}