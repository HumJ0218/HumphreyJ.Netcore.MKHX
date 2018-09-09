using System;
using System.Collections.Generic;
using HumphreyJ.NetCore.MKHX.SandBox.Models.Enum;
using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 卡牌
    /// </summary>
    internal class Card : BattleObject
    {
        public Card(int cardId, string cardName, int level, int evolution, Skill[] skills, Skill[] skillsEvolution, CardRace race, int wait, double attack, double hp)
        {
            Console.WriteLine(string.Join("\t", nameof(Card), "创建卡牌", cardName));

            CardId = cardId;
            CardName = cardName ?? throw new ArgumentNullException(nameof(cardName));
            Level.Set(level);
            Evolution.Set(evolution);
            Skills = skills ?? throw new ArgumentNullException(nameof(skills));
            SkillsEvolution = skillsEvolution ?? throw new ArgumentNullException(nameof(skillsEvolution));
            Race.Set(race);
            Wait.SetMax(wait).SetToMax();
            Attack.SetMax(attack).SetToMax();
            Hp.SetMax(hp).SetToMax();
        }

        /// <summary>
        /// 卡牌编号（无实际作用）
        /// </summary>
        internal int CardId { get; }

        /// <summary>
        /// 卡牌名称（无实际作用）
        /// </summary>
        internal string CardName { get; }

        /// <summary>
        /// 卡牌等级（无实际作用）
        /// </summary>
        internal Rangeable<int> Level { get; } = new Rangeable<int>(10, 0, 15, "卡牌等级");

        /// <summary>
        /// 进化次数（无实际作用）
        /// </summary>
        internal Rangeable<int> Evolution { get; } = new Rangeable<int>(0, 0, int.MaxValue, "卡牌进化次数");

        /// <summary>
        /// 卡牌自带技能
        /// </summary>
        internal Skill[] Skills { get; }

        /// <summary>
        /// 卡牌进化技能
        /// </summary>
        internal Skill[] SkillsEvolution { get; }

        /// <summary>
        /// 卡牌附加技能
        /// </summary>
        internal List<Skill> SkillsBonus { get; } = new List<Skill>();

        /// <summary>
        /// 卡牌种族
        /// </summary>
        internal Variable<CardRace> Race { get; } = new Variable<CardRace>(CardRace.未定义, "卡牌种族");

        /// <summary>
        /// 卡牌等待时间
        /// </summary>
        internal Rangeable<int> Wait { get; } = new Rangeable<int>(0, 0, 0, "卡牌等待时间");

        /// <summary>
        /// 卡牌攻击力
        /// </summary>
        internal Rangeable<double> Attack { get; } = new Rangeable<double>(0, 0, 0, "卡牌攻击力");

        /// <summary>
        /// 卡牌生命值
        /// </summary>
        internal Rangeable<double> Hp { get; } = new Rangeable<double>(0, 0, 0, "卡牌生命值");

        public override string ToString()
        {
            return $"[{CardId}]{CardName}";
        }
    }
}