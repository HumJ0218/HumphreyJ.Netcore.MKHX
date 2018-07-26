using System;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 玩家英雄信息
    /// </summary>
    public class BattleRecordPlayerHeroData
    {
        public BattleRecordPlayerHeroData() { }

        /// <summary>
        /// 玩家ID
        /// </summary>
        public int Uid;

        /// <summary>
        /// 玩家昵称
        /// </summary>
        public string NickName;

        /// <summary>
        /// 玩家头像
        /// </summary>
        public int Avatar;

        /// <summary>
        /// 玩家性别
        /// </summary>
        public int Sex;

        /// <summary>
        /// 玩家等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 英雄血量
        /// </summary>
        public double HP;

        /// <summary>
        /// 战斗结束剩余血量
        /// </summary>
        public double HeroHP;

        /// <summary>
        /// 玩家卡组
        /// </summary>
        public BattleRecordPlayerCardData[] Cards;

        /// <summary>
        /// 玩家符文
        /// </summary>
        public BattleRecordPlayerRuneData[] Runes;

    }
}