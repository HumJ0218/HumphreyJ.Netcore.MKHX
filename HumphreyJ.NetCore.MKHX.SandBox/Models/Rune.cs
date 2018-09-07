using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 符文
    /// </summary>
    internal class Rune : BattleObject
    {
        /// <summary>
        /// 符文编号（无实际作用）
        /// </summary>
        internal int RuneId { get; }

        /// <summary>
        /// 符文名称（无实际作用）
        /// </summary>
        internal string RuneName { get; }

        /// <summary>
        /// 符文属性（无实际作用）
        /// </summary>
        internal int RuneProperty { get; }

        /// <summary>
        /// 符文等级（无实际作用）
        /// </summary>
        internal int Level { get; }

        /// <summary>
        /// 符文技能
        /// </summary>
        internal Skill[] Skills { get; }

        /// <summary>
        /// 剩余发动次数
        /// </summary>
        internal Rangeable<int> RemainSkillTimes { get; } = new Rangeable<int>(0, 0, int.MaxValue, "符文剩余发动次数");

    }
}