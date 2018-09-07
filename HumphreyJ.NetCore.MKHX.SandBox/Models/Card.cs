using HumphreyJ.NetCore.MKHX.SandBox.Models.Enum;
using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 卡牌
    /// </summary>
    internal class Card : BattleObject
    {
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
        internal Skill[] SkillsBonus { get; }

        /// <summary>
        /// 卡牌种族
        /// </summary>
        internal Variable<CardRace> Race { get; } = new Variable<CardRace>(CardRace.未定义, "卡牌种族");

        /// <summary>
        /// 卡牌等待时间
        /// </summary>
        internal Rangeable<int> Wait { get; } = new Rangeable<int>(0, 0, Config.CARD_MAXWAIT, "卡牌等待时间");

        /// <summary>
        /// 卡牌攻击力
        /// </summary>
        internal Rangeable<double> Attack { get; } = new Rangeable<double>(0, 0, Config.CARD_MAXATK, "卡牌攻击力");

        /// <summary>
        /// 卡牌生命值
        /// </summary>
        internal Rangeable<double> Hp { get; } = new Rangeable<double>(0, 0, Config.CARD_MAXHP, "卡牌生命值");

    }
}