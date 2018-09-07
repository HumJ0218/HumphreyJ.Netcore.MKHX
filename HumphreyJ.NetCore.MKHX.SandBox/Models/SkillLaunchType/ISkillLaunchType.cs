using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillLaunchType
{
    /// <summary>
    /// 技能触发条件接口
    /// </summary>
    interface ISkillLaunchType
    {
        /// <summary>
        /// 技能触发时间点
        /// </summary>
        int LanchType { get; }

        /// <summary>
        /// 技能触发条件
        /// </summary>
        int LanchCondition { get; }

        /// <summary>
        /// 技能触发条件参数
        /// </summary>
        string[] LanchConditionValue { get; }
    }
}