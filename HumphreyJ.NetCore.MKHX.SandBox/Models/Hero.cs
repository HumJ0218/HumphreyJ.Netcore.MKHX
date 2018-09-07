using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 英雄信息
    /// </summary>
    internal class Hero : BattleObject
    {
        /// <summary>
        /// 英雄等级（无实际作用）
        /// </summary>
        internal Rangeable<int> Level { get; } = new Rangeable<int>(0, 0, int.MaxValue, "英雄等级");

        /// <summary>
        /// 血量
        /// </summary>
        internal Rangeable<double> Hp { get; } = new Rangeable<double>(0, 0, Config.HERO_MAXHP, "英雄血量");
    }
}