using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;
using System;

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

        /// <summary>
        /// 创建一个英雄
        /// </summary>
        /// <param name="Hp">上限血量</param>
        /// <param name="Level">英雄等级</param>
        public Hero(double Hp, int Level = 0)
        {
            Console.WriteLine(string.Join("\t", nameof(Hero), "创建英雄", Hp));

            this.Hp.Set(Hp).SetMax(Hp);
            this.Level.Set(Level);
        }

        public override string ToString()
        {
            return $"({Hp})";
        }
    }
}