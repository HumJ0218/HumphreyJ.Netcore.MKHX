using System;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 战场记录数据
    /// </summary>
    public class BattleRecordData
    {
        public BattleRecordData() { }

        /// <summary>
        /// 战斗编号
        /// </summary>
        public string BattleId;

        /// <summary>
        /// 获胜状态
        /// </summary>
        public int Win;

        /// <summary>
        /// 战斗附加数据
        /// </summary>
        public object ExtData;

        /// <summary>
        /// 战斗准备数据（？）
        /// </summary>
        public object prepare;

        /// <summary>
        /// 进攻方玩家数据
        /// </summary>
        public BattleRecordPlayerHeroData AttackPlayer;

        /// <summary>
        /// 防守方玩家数据
        /// </summary>
        public BattleRecordPlayerHeroData DefendPlayer;

        /// <summary>
        /// 战斗详情
        /// </summary>
        public List<BattleRecordRoundData> Battle;

        /// <summary>
        /// 先行方用户编号
        /// </summary>
        public int FirstUid;

    }
}