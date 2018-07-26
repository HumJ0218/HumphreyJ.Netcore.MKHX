namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 战斗回合数据
    /// </summary>
    public class BattleRecordRoundData
    {
        public BattleRecordRoundData() { }

        /// <summary>
        /// 当前回合数
        /// </summary>
        public int Round;

        /// <summary>
        /// 行动方标记
        /// </summary>
        public bool isAttack;

        /// <summary>
        /// 动画数据
        /// </summary>
        public BattleRecordRouneOppData[] Opps;
    }
}