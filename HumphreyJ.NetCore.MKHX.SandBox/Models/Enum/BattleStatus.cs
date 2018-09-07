using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.Enum
{
    /// <summary>
    /// 战局当前状态
    /// </summary>
    internal enum BattleStatus
    {
        /// <summary>
        /// 胜负未分
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// 进攻方获胜
        /// </summary>
        Win = 1,

        /// <summary>
        /// 进攻方失败
        /// </summary>
        Lose = -1,
    }
}