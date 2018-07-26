namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 玩家符文信息
    /// </summary>
    public class BattleRecordPlayerRuneData
    {
        public BattleRecordPlayerRuneData() { }

        /// <summary>
        /// 唯一对象ID
        /// </summary>
        public string UUID;

        /// <summary>
        /// 符文编号
        /// </summary>
        public int RuneId;

        /// <summary>
        /// 用户符文编号（用以识别相同编号的不同符文）
        /// </summary>
        public long UserRuneId;

        /// <summary>
        /// 符文等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 符文可发动次数
        /// </summary>
        public int RemainSkillTimes;

    }
}