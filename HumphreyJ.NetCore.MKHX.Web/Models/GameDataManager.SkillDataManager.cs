using HumphreyJ.NetCore.MKHX.GameData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public class SkillDataManager : DataManager
        {
            public SkillDataManager(Guid version)
            {
                Version = version;
            }

            public new RawSkillData[] RawData => GetRawData();
            public ParsedSkillData[] ParsedData => GetParsedData();

            private RawSkillData[] rawData;
            private RawSkillData[] GetRawData()
            {
                if (rawData == null)
                {
                    rawData = RawSkillData.ParseJsonString(base.RawData);
                }
                return rawData;
            }
            private static Dictionary<Guid, ParsedSkillData[]> parsedData = new Dictionary<Guid, ParsedSkillData[]>();
            private ParsedSkillData[] GetParsedData()
            {
                if (!parsedData.ContainsKey(Version))
                {
                    parsedData.Add(Version, RawData.Select(m => new ParsedSkillData(m)).ToArray());
                }
                return parsedData[Version];
            }

            internal ParsedSkillData[] Preload()
            {
                var data = ParsedData;
                {
                    //  GC
                    rawData = null;
                }
                return data;
            }
        }

        private readonly Dictionary<ParsedSkillData, ParsedSkillData[]> SkillData_ExpandSkills = new Dictionary<ParsedSkillData, ParsedSkillData[]>();
         private readonly Dictionary<ParsedSkillData, ParsedSkillData[]> SkillData_AllSkills = new Dictionary<ParsedSkillData, ParsedSkillData[]>();
       private readonly Dictionary<ParsedSkillData, ParsedSkillData[]> SkillData_IncludedSkills = new Dictionary<ParsedSkillData, ParsedSkillData[]>();
        private readonly Dictionary<ParsedSkillData, ParsedCardData[]> SkillData_DirectIncludedCards = new Dictionary<ParsedSkillData, ParsedCardData[]>();
        private readonly Dictionary<ParsedSkillData, ParsedCardData[]> SkillData_IndirectIncludedCards = new Dictionary<ParsedSkillData, ParsedCardData[]>();
        private readonly Dictionary<ParsedSkillData, ParsedRuneData[]> SkillData_DirectIncludedRunes = new Dictionary<ParsedSkillData, ParsedRuneData[]>();
        private readonly Dictionary<ParsedSkillData, ParsedCardData[]> SkillData_SummoneeCards = new Dictionary<ParsedSkillData, ParsedCardData[]>();

        /// <summary>
        /// 子技能（包含本技能）
        /// </summary>
        public ParsedSkillData[] SkillData_GetExpandSkills(ParsedSkillData skill)
        {
            if (!SkillData_ExpandSkills.ContainsKey(skill))
            {
                SkillData_InitSkillRelationLists();
            }
            return SkillData_ExpandSkills[skill];
        }

        /// <summary>
        /// 父技能
        /// </summary>
        public  ParsedSkillData[] SkillData_GetIncludedSkills(ParsedSkillData skill)
        {
            if (!SkillData_IncludedSkills.ContainsKey(skill))
            {
                SkillData_InitSkillRelationLists();
            }
            if (SkillData_IncludedSkills.ContainsKey(skill))
            {
                return SkillData_IncludedSkills[skill];
            }
            else
            {
                return new ParsedSkillData[] { };
            }
        }

        /// <summary>
        /// 获取直接包含此技能的卡牌
        /// </summary>
        public ParsedCardData[] SkillData_GetDirectIncludedCards(ParsedSkillData skill)
        {
            if (!SkillData_DirectIncludedCards.ContainsKey(skill))
            {
                SkillData_InitCardRelationLists();
            }
            return SkillData_DirectIncludedCards[skill];
        }

        /// <summary>
        /// 获取间接包含此技能的卡牌
        /// </summary>
        public ParsedCardData[] SkillData_GetIndirectIncludedCards(ParsedSkillData skill)
        {
            if (!SkillData_IndirectIncludedCards.ContainsKey(skill))
            {
                SkillData_InitCardRelationLists();
            }
            return SkillData_IndirectIncludedCards[skill];
        }

        /// <summary>
        /// 获取直接包含此技能的符文
        /// </summary>
        public ParsedRuneData[] SkillData_GetDirectIncludedRunes(ParsedSkillData skill)
        {
            if (!SkillData_DirectIncludedRunes.ContainsKey(skill))
            {
                SkillData_InitRuneRelationLists();
            }
            return SkillData_DirectIncludedRunes[skill];
        }

        /// <summary>
        /// 获取技能召唤物
        /// </summary>
        public ParsedCardData[] SkillData_GetSummoneeCards(ParsedSkillData skill)
        {
            if (!skill.IsSummonSkill)
            {
                return new ParsedCardData[] { };
            }
            if (!SkillData_SummoneeCards.ContainsKey(skill))
            {
                SkillData_InitCardRelationLists();
            }
            return SkillData_SummoneeCards[skill];
        }

        /// <summary>
        /// 初始化 技能-技能 关系列表
        /// </summary>
        public void SkillData_InitSkillRelationLists()
        {
            //  1   初始化子技能表
            foreach (var skill in SkillList)
            {
                var list = new List<ParsedSkillData>();
                switch (skill.AffectType)
                {
                    default:
                        {
                            list.Add(skill);
                            break;
                        }
                    case 122:
                        {
                            //  多重技能
                            list.AddRange(skill.AffectValue.Select(m => SkillList.FirstOrDefault(n => n.SkillId + "" == m)).Where(m => m != null));
                            break;
                        }
                    case 158:
                        {
                            //  双向觉醒
                            list.AddRange(skill.AffectValue2.Select(m => SkillList.FirstOrDefault(n => n.SkillId + "" == m)).Where(m => m != null));
                            break;
                        }
                }
                SkillData_ExpandSkills.TryAdd(skill, list.ToArray());
            }

            //  1.1 初始化父技能表（反查子技能列表）
            foreach (var skill in SkillList)
            {
                var expandSkills = SkillData_ExpandSkills.Where(m => !m.Value.Contains(m.Key) && m.Value.Contains(skill)).Select(m => m.Key).ToArray();
                SkillData_IncludedSkills.TryAdd(skill, expandSkills);
            }

            ////  1.2   初始化后代技能列表（处理多级调用）
            //foreach (var skill in SkillList)
            //{

            //    //  正序遍历树结构
            //    var list = new List<ParsedSkillData>();
            //    var stack = new Stack<ParsedSkillData>();

            //    stack.Push(skill);
            //    while (stack.Count > 0)
            //    {
            //        var i = stack.Pop();

            //        var expand = SkillData_ExpandSkills[i];
            //        if (expand.Length == 1 && expand[0] == i)
            //        {
            //            list.Add(i);
            //        }
            //        else
            //        {
            //            expand.Reverse().ToList().ForEach(m =>
            //            {
            //                if (m != i)
            //                {
            //                    stack.Push(m);
            //                }
            //            });
            //        }
            //    }
            //    SkillData_AllSkills.TryAdd(skill, list.ToArray());
            //}
        }

        /// <summary>
        /// 初始化 技能-卡牌 关系列表
        /// </summary>
        public  void SkillData_InitCardRelationLists()
        {
            //  1   包含此技能的卡牌，遍历卡牌，将技能对应卡牌添加进列表
            {
                //  临时列表，方便操作值列表项
                var directIncludedCards = new Dictionary<ParsedSkillData, List<ParsedCardData>>();
                var indirectIncludedCards = new Dictionary<ParsedSkillData, List<ParsedCardData>>();

                foreach (var card in CardList)
                {
                    var allSkills = CardData_GetAllSkills(card);
                    var expandSkills = CardData_GetExpandSkills(card);
                    var onlyExpandSkills = expandSkills.Except(allSkills);

                    //  遍历技能列表，处理直接包含技能的卡牌
                    foreach (var skill in allSkills)
                    {
                        if (directIncludedCards.ContainsKey(skill))
                        {
                            directIncludedCards[skill].Add(card);
                        }
                        else
                        {
                            directIncludedCards.Add(skill, new List<ParsedCardData> { card });
                        }
                    }

                    //  遍历技能差集列表，处理间接包含技能的卡牌
                    foreach (var skill in onlyExpandSkills)
                    {
                        if (indirectIncludedCards.ContainsKey(skill))
                        {
                            indirectIncludedCards[skill].Add(card);
                        }
                        else
                        {
                            indirectIncludedCards.Add(skill, new List<ParsedCardData> { card });
                        }
                    }
                }

                //  将临时列表中的项目复制进永久列表
                foreach (var i in directIncludedCards)
                {
                    SkillData_DirectIncludedCards.TryAdd(i.Key, i.Value.ToArray());
                }
                foreach (var i in indirectIncludedCards)
                {
                    SkillData_IndirectIncludedCards.TryAdd(i.Key, i.Value.ToArray());
                }

            }

            //  2   技能可召唤出的卡牌，只包含召唤技能
            {
                foreach (var skill in SkillList.Where(m => m.IsSummonSkill))
                {
                    var list = new List<ParsedCardData>();
                    foreach (var i in SkillData_GetExpandSkills(skill))
                    {
                        switch (i.AffectType)
                        {
                            case 101:   //  召唤
                            case 184:   //  对场召唤
                                {

                                    if (i.AffectValue.Length > 1)
                                    {
                                        list.AddRange(i.AffectValue.Select(m => CardList.FirstOrDefault(n => n.CardId + "" == m || n.CardName == m)));
                                    }
                                    else
                                    {
                                        list.Add(CardList.FirstOrDefault(n => n.CardId + "" == i.AffectValue[0] || n.CardName == i.AffectValue[0]));
                                        list.Add(CardList.FirstOrDefault(n => n.CardId + "" == i.AffectValue2[0] || n.CardName == i.AffectValue2[0]));
                                    }
                                    break;
                                }
                            case 180:   //  合体
                                {

                                    list.Add(CardList.FirstOrDefault(n => n.CardId + "" == i.AffectValue2[0] || n.CardName == i.AffectValue2[0]));
                                    break;
                                }
                            case 193:   //  生成
                            case 203:   //  死亡诅咒
                                {
                                    list.AddRange(i.AffectValue2[0].Split('&').Select(m => CardList.FirstOrDefault(n => n.CardId + "" == m || n.CardName == m)));
                                    break;
                                }
                        }
                    }
                    var summoneeCards = list.Where(m => m != null).Distinct().ToArray();
                    SkillData_SummoneeCards.TryAdd(skill, summoneeCards);
                }
            }
        }

        /// <summary>
        /// 初始化 技能-符文 关系列表
        /// </summary>
        public  void SkillData_InitRuneRelationLists()
        {
            //  1   包含此技能的符文，遍历符文，将技能对应符文添加进列表
            {
                //  临时列表，方便操作值列表项
                var directIncludedRunes = new Dictionary<ParsedSkillData, List<ParsedRuneData>>();
                var indirectIncludedRunes = new Dictionary<ParsedSkillData, List<ParsedRuneData>>();

                foreach (var rune in RuneList)
                {
                    var allSkills = RuneData_GetAllSkills(rune);

                    //  遍历技能列表，处理直接包含技能的符文
                    foreach (var skill in allSkills)
                    {
                        if (directIncludedRunes.ContainsKey(skill))
                        {
                            directIncludedRunes[skill].Add(rune);
                        }
                        else
                        {
                            directIncludedRunes.Add(skill, new List<ParsedRuneData> { rune });
                        }
                    }
                }

                //  将临时列表中的项目复制进永久列表
                foreach (var i in directIncludedRunes)
                {
                    SkillData_DirectIncludedRunes.TryAdd(i.Key, i.Value.ToArray());
                }

            }
        }


    }
}