using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.GameData.Enum
{
    /// <summary>
    /// 符文发动条件比较位置
    /// </summary>
    enum RuneSkillConditionType
    {
        /// <summary>
        /// 不限
        /// </summary>
        SkillConditionType0 = 0,

        /// <summary>
        /// 手牌数
        /// </summary>
        SkillConditionType1 = 1,

        /// <summary>
        /// 墓地卡牌数
        /// </summary>
        SkillConditionType2 = 2,

        /// <summary>
        /// 牌堆卡牌数
        /// </summary>
        SkillConditionType3 = 3,

        /// <summary>
        /// 场上卡牌数
        /// </summary>
        SkillConditionType4 = 4,

        /// <summary>
        /// 回合数
        /// </summary>
        SkillConditionType5 = 5,

        /// <summary>
        /// 英雄生命百分比
        /// </summary>
        SkillConditionType6 = 6,

        /// <summary>
        /// 7
        /// </summary>
        SkillConditionType7 = 7,

        /// <summary>
        /// 敌我卡差
        /// </summary>
        SkillConditionType8 = 8,
    }
}