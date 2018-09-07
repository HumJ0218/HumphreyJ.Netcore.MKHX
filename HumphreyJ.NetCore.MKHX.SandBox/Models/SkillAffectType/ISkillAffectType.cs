using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    /// <summary>
    /// 技能效果接口
    /// </summary>
    internal interface ISkillAffectType
    {
        /// <summary>
        /// 技能效果
        /// </summary>
        int AffectType { get; }

        /// <summary>
        /// 技能效果参数1
        /// </summary>
        string[] AffectValue { get; }

        /// <summary>
        /// 技能效果参数2
        /// </summary>
        string[] AffectValue2 { get; }
    }
}