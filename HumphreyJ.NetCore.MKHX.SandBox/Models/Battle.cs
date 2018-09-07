using HumphreyJ.NetCore.MKHX.SandBox.Models.Enum;
using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 战局
    /// </summary>
    internal class Battle
    {
        /// <summary>
        /// 当前回合数
        /// </summary>
        internal Rangeable<int> Round { get; } = new Rangeable<int>(0, 0, Config.BATTLE_MAXROUND, "当前回合数");

        /// <summary>
        /// 当前是否为进攻方行动
        /// </summary>
        internal bool IsAttackPlayerMoving { get; } = false;

        /// <summary>
        /// 当前战局状态
        /// </summary>
        internal BattleStatus Status { get;} = BattleStatus.Undefined;

        /// <summary>
        /// 防御方玩家（通常位于画面上方）
        /// </summary>
        internal Player DefendPlayer { get;  }

        /// <summary>
        /// 进攻方玩家（通常位于画面下方）
        /// </summary>
        internal Player AttackPlayer { get; }
    }
}