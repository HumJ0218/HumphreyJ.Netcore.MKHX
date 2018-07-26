using HumphreyJ.NetCore.MKHX.GameData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public class CardDataManager : DataManager
        {
            public CardDataManager(Guid version)
            {
                Version = version;
            }

            private new RawCardData[] RawData => GetRawData();
            public ParsedCardData[] ParsedData => GetParsedData();

            private RawCardData[] rawData;
            private RawCardData[] GetRawData()
            {
                if (rawData == null)
                {
                    rawData = RawCardData.ParseJsonString(base.RawData);
                }
                return rawData;
            }
            private static Dictionary<Guid, ParsedCardData[]> parsedData = new Dictionary<Guid, ParsedCardData[]>();
            private ParsedCardData[] GetParsedData()
            {
                if (!parsedData.ContainsKey(Version))
                {
                    parsedData.Add(Version, RawData.Select(m => new ParsedCardData(m)).ToArray());
                }
                return parsedData[Version];
            }

            internal ParsedCardData[] Preload()
            {
                var data = ParsedData;
                {
                    //  GC
                    rawData = null;
                }
                return data;
            }
        }

        private readonly Dictionary<ParsedCardData, bool> CardData_IsBattleCard = new Dictionary<ParsedCardData, bool>();
        private readonly Dictionary<ParsedCardData, ParsedSkillData[]> CardData_AllSkills = new Dictionary<ParsedCardData, ParsedSkillData[]>();
        private readonly Dictionary<ParsedCardData, ParsedSkillData[]> CardData_AllExpandSkills = new Dictionary<ParsedCardData, ParsedSkillData[]>();
        private readonly Dictionary<ParsedCardData, ParsedCardData[]> CardData_DirectSummoneeCards = new Dictionary<ParsedCardData, ParsedCardData[]>();
        private readonly Dictionary<ParsedCardData, ParsedCardData[]> CardData_DirectSummonerCards = new Dictionary<ParsedCardData, ParsedCardData[]>();
        private readonly Dictionary<ParsedCardData, ParsedCardData[]> CardData_AllSummoneeCards = new Dictionary<ParsedCardData, ParsedCardData[]>();
        private readonly Dictionary<ParsedCardData, ParsedCardData[]> CardData_AllSummonerCards = new Dictionary<ParsedCardData, ParsedCardData[]>();

        /// <summary>
        /// 获取卡牌是否为战斗性卡牌
        /// </summary>
        public bool CardData_GetIsBattleCard(ParsedCardData card)
        {
            if (!CardData_IsBattleCard.ContainsKey(card))
            {
                CardData_InitSkillRelationLists();
            }
            return CardData_IsBattleCard[card];
        }

        /// <summary>
        /// 获取卡牌所有技能
        /// </summary>
        public ParsedSkillData[] CardData_GetAllSkills(ParsedCardData card)
        {
            if (!CardData_AllSkills.ContainsKey(card))
            {
                CardData_InitSkillRelationLists();
            }
            return CardData_AllSkills[card];
        }

        /// <summary>
        /// 获取卡牌直接召唤物
        /// </summary>
        public ParsedCardData[] CardData_GetDirectSummoneeCards(ParsedCardData card)
        {
            if (!CardData_DirectSummoneeCards.ContainsKey(card))
            {
                CardData_InitCardRelationLists();
            }
            return CardData_DirectSummoneeCards[card];
        }

        /// <summary>
        /// 获取卡牌所有召唤物
        /// </summary>
        public ParsedCardData[] CardData_GetAllSummoneeCards(ParsedCardData card)
        {
            if (!CardData_AllSummoneeCards.ContainsKey(card))
            {
                CardData_InitCardRelationLists();
            }
            return CardData_AllSummoneeCards[card];
        }

        /// <summary>
        /// 获取卡牌直接召唤者
        /// </summary>
        public ParsedCardData[] CardData_GetDirectSummonerCards(ParsedCardData card)
        {
            if (!CardData_DirectSummonerCards.ContainsKey(card))
            {
                CardData_InitCardRelationLists();
            }
            return CardData_DirectSummonerCards[card];
        }

        /// <summary>
        /// 获取卡牌所有召唤者
        /// </summary>
        public ParsedCardData[] CardData_GetAllSummonerCards(ParsedCardData card)
        {
            if (!CardData_AllSummonerCards.ContainsKey(card))
            {
                CardData_InitCardRelationLists();
            }
            return CardData_AllSummonerCards[card];
        }

        /// <summary>
        /// 获取卡牌所有技能（展开形式）
        /// </summary>
        public ParsedSkillData[] CardData_GetExpandSkills(ParsedCardData card)
        {
            if (!CardData_AllExpandSkills.ContainsKey(card))
            {
                CardData_InitSkillRelationLists();
            }
            return CardData_AllExpandSkills[card];
        }

        /// <summary>
        /// 初始化 卡牌-卡牌 关系列表
        /// </summary>
        public void CardData_InitCardRelationLists()
        {
            //  1   直接召唤物列表
            foreach (var card in CardList)
            {
                var list = new List<ParsedCardData>();
                foreach (var i in CardData_GetExpandSkills(card))
                {
                    if (i.AffectType == 154)    //  镜像
                    {
                        list.Add(card);
                    }
                    else
                    {
                        list.AddRange(SkillData_GetSummoneeCards(i));
                    }
                }
                var summoneeCards = list.Where(m => m != null).Distinct().ToArray();
                CardData_DirectSummoneeCards.TryAdd(card, summoneeCards);
            }

            //  1.1 所有召唤物列表（遍历直接召唤物列表树）
            foreach (var card in CardList)
            {
                var list = new List<ParsedCardData>();
                var finishList = new List<ParsedCardData>();
                Stack<ParsedCardData> stack = new Stack<ParsedCardData>();
                stack.Push(card);
                while (stack.Count > 0)
                {
                    var i = stack.Pop();
                    if (finishList.Contains(i)) { continue; }
                    var summoneeCards = CardData_GetDirectSummoneeCards(i).ToList();
                    var containsSelf = summoneeCards.Remove(card);
                    foreach (var summonee in summoneeCards)
                    {
                        stack.Push(summonee);
                        list.Add(summonee);
                    }
                    if (containsSelf)
                    {
                        list.Add(card);
                    }
                    finishList.Add(i);
                }
                CardData_AllSummoneeCards.TryAdd(card, list.ToArray());
            }

            //  2   直接召唤者列表（反查直接召唤物列表）
            foreach (var card in CardList)
            {
                var summonerCards = CardData_DirectSummoneeCards.Where(m => m.Value.Contains(card)).Select(m => m.Key).ToArray();
                CardData_DirectSummonerCards.TryAdd(card, summonerCards);
            }

            //  2.1 所有召唤者列表（遍历直接召唤者列表树）
            foreach (var card in CardList)
            {
                var list = new List<ParsedCardData>();
                var finishList = new List<ParsedCardData>();
                Stack<ParsedCardData> stack = new Stack<ParsedCardData>();
                stack.Push(card);
                while (stack.Count > 0)
                {
                    var i = stack.Pop();
                    if (finishList.Contains(i)) { continue; }
                    var summonerCards = CardData_GetDirectSummonerCards(i).ToList();
                    var containsSelf = summonerCards.Remove(card);
                    foreach (var summoner in summonerCards)
                    {
                        stack.Push(summoner);
                        list.Add(summoner);
                    }
                    if (containsSelf)
                    {
                        list.Add(card);
                    }
                    finishList.Add(i);
                }
                CardData_AllSummonerCards.TryAdd(card, list.ToArray());
            }
        }

        /// <summary>
        /// 初始化 卡牌-技能 关系列表
        /// </summary>
        public void CardData_InitSkillRelationLists()
        {
            //  1   卡牌所有技能列表
            foreach (var card in CardList)
            {
                var list = new List<ParsedSkillData>();
                list.AddRange(card.Skill.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                list.AddRange(card.LockSkill1.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                list.AddRange(card.LockSkill2.Select(m => SkillList.FirstOrDefault(n => n.SkillId == m)).Where(m => m != null));
                CardData_AllSkills.TryAdd(card, list.ToArray());
            }

            //  1.1 卡牌所有技能展开形式列表（展开卡牌所有技能列表）
            foreach (var card in CardList)
            {
                var list = new List<ParsedSkillData>();
                foreach (var i in CardData_GetAllSkills(card))
                {
                    list.AddRange(SkillData_GetExpandSkills(i));
                }
                CardData_AllExpandSkills.TryAdd(card, list.ToArray());
            }

            //  1.2 战斗性卡牌列表（检查卡牌所有技能列表）
            foreach (var card in CardList)
            {
                var allskills = CardData_GetAllSkills(card);

                //  所有技能都不是战斗技能，则卡牌不是战斗卡牌。没有技能的卡牌算作战斗卡牌（如双子座幻影、九命猫神幻影）
                var isBattleCard = !(allskills.Length > 0 && allskills.All(m => !m.IsBattleSkill));

                CardData_IsBattleCard.Add(card, isBattleCard);
            }

        }

    }
}