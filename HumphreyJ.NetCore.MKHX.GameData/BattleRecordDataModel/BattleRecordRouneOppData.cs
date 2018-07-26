using System;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 战局动画数据
    /// </summary>
    public class BattleRecordRouneOppData
    {
        public BattleRecordRouneOppData() { }

        /// <summary>
        /// 唯一对象ID
        /// </summary>
        public string UUID;

        /// <summary>
        /// 动画编号（对应技能数据中的 Type ）
        /// </summary>
        public int Opp;

        /// <summary>
        /// 动画目标
        /// </summary>
        public object[] Target;

        /// <summary>
        /// 动画参数
        /// </summary>
        public double? Value;
    }
}