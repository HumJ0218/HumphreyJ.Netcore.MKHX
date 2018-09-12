using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;
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
        /// 技能效果编号
        /// </summary>
        int AffectType { get; }

        /// <summary>
        /// 技能效果名称
        /// </summary>
        string AffectTypeName { get; }

        /// <summary>
        /// 技能效果参数1
        /// </summary>
        string[] AffectValue { get; }

        /// <summary>
        /// 技能效果参数2
        /// </summary>
        string[] AffectValue2 { get; }

        /// <summary>
        /// 技能完整发动的概率（所有概率相乘）
        /// </summary>
        double SkillPercentageAll { get; }

        /// <summary>
        /// 技能发动总概率期望
        /// </summary>
        double SkillPercentageMean { get; }
    }
}