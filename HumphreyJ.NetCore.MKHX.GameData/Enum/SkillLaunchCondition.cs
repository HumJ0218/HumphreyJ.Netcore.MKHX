using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData.Enum
{
    /// <summary>
    /// 技能发动条件
    /// </summary>
    public enum SkillLaunchCondition
    {
        /// <summary>
        /// 无条件或交由技能判定
        /// </summary>
        LaunchCondition0 = 0,

        /// <summary>
        /// 目标符合参数指定种族
        /// </summary>
        LaunchCondition1 = 1,

        /// <summary>
        /// 造成物理伤害值大于0
        /// </summary>
        LaunchCondition2 = 2,

        /// <summary>
        /// 受到物理伤害值大于0
        /// </summary>
        LaunchCondition3 = 3,

        /// <summary>
        /// 目标卡牌生命值高于自身
        /// </summary>
        LaunchCondition4 = 4,

        /// <summary>
        /// 目标卡牌附有燃烧、毒、冻结、麻痹效果
        /// </summary>
        LaunchCondition5 = 5,
    }
}
