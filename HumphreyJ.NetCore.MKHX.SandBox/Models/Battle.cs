using HumphreyJ.NetCore.MKHX.SandBox.Models.Enum;
using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Walterlv.ComponentModel;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 战局
    /// </summary>
    internal class Battle
    {
        /// <summary>
        /// 战局编号
        /// </summary>
        internal string BattleId { get; } = GetBattleId();

        /// <summary>
        /// 当前回合数
        /// </summary>
        internal Rangeable<int> Round { get; } = new Rangeable<int>(0, 0, Config.BATTLE_MAXROUND, "当前回合数");

        /// <summary>
        /// 当前是否为进攻方行动
        /// </summary>
        internal bool IsAttackPlayerMoving { get; }

        /// <summary>
        /// 当前战局状态
        /// </summary>
        internal BattleStatus Status { get; } = BattleStatus.胜负未分;

        /// <summary>
        /// 防御方玩家（通常位于画面上方）
        /// </summary>
        internal Player DefendPlayer { get; }

        /// <summary>
        /// 进攻方玩家（通常位于画面下方）
        /// </summary>
        internal Player AttackPlayer { get; }

        /// <summary>
        /// 创建两名玩家间的对战
        /// </summary>
        /// <param name="AttackPlayer">进攻方玩家</param>
        /// <param name="DefendPlayer">防守方玩家</param>
        /// <param name="IsAttackPlayerFirst">进攻方是否先手</param>
        public Battle(Player AttackPlayer, Player DefendPlayer, bool IsAttackPlayerFirst = true)
        {
            Console.WriteLine(string.Join("\t", nameof(Battle), "创建战场", BattleId));

            this.AttackPlayer = AttackPlayer;
            this.DefendPlayer = DefendPlayer;
            this.IsAttackPlayerMoving = !IsAttackPlayerFirst;
        }

        /// <summary>
        /// 生成战局编号
        /// </summary>
        private static string GetBattleId()
        {
            var guid = Guid.NewGuid();
            var time = DateTime.Now;
            var thread = 0;
            var version = DebuggingProperties.IsDebug ? "Debug" : "Release";

            var id = $"{version[0]}{thread}{time.Ticks.ToString("x16")}{string.Join("", guid.ToString("N").Reverse())}";
            return id;
        }

    }
}