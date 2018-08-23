using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData.Enum
{
    /// <summary>
    /// 符文触发条件比较符号
    /// </summary>
    public enum RuneSkillConditionCompare
    {
        /// <summary>
        /// 不限
        /// </summary>
        SkillConditionCompare0 = 0,

        /// <summary>
        /// 大于
        /// </summary>
        SkillConditionCompare1 = 1,

        /// <summary>
        /// 等于
        /// </summary>
        SkillConditionCompare2 = 2,

        /// <summary>
        /// 小于
        /// </summary>
        SkillConditionCompare3 = 3,
    }
}
