namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 玩家卡牌信息
    /// </summary>
    public class BattleRecordPlayerCardData
    {
        public BattleRecordPlayerCardData() { }

        /// <summary>
        /// 唯一对象ID
        /// </summary>
        public string UUID;

        /// <summary>
        /// 卡牌编号
        /// </summary>
        public int CardId;

        /// <summary>
        /// 用户卡牌编号（用以识别相同编号的不同卡牌）
        /// </summary>
        public long UserCardId;

        /// <summary>
        /// 卡牌基础攻击
        /// </summary>
        public double Attack;

        /// <summary>
        /// 卡牌基础血量
        /// </summary>
        public double HP;

        /// <summary>
        /// 卡牌基础等待
        /// </summary>
        public int Wait;

        /// <summary>
        /// 卡牌等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 洗练技能
        /// </summary>
        public int? SkillNew;

        /// <summary>
        /// 洗练标记（未洗练为空，否则为 1 ）
        /// </summary>
        public int? Evolution;

        /// <summary>
        /// 洗练次数
        /// </summary>
        public int? WashTime;

        /// <summary>
        /// 超级洗练次数
        /// </summary>
        public int? SuperWashTime;
    }
}