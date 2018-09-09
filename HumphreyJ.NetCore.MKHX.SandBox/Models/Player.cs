using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 玩家
    /// </summary>
    internal class Player
    {
        #region 基本信息
        /// <summary>
        /// 头像编号（无实际作用）
        /// </summary>
        internal int Avatar { get; }

        /// <summary>
        /// 头像性别（决定默认头像，无实际作用）
        /// </summary>
        internal int Sex { get; }

        /// <summary>
        /// 玩家名称（无实际作用）
        /// </summary>
        internal string NickName { get; }
        #endregion

        #region 对局用信息
        /// <summary>
        /// 英雄
        /// </summary>
        internal Hero Hero { get; private set; }

        /// <summary>
        /// 符文槽
        /// </summary>
        internal Rune[] Runes { get; private set; }

        /// <summary>
        /// 牌堆（卡组）
        /// </summary>
        internal List<Card> Heap { get; private set; }

        /// <summary>
        /// 召唤池
        /// </summary>
        internal List<Card> SummonPool { get; private set; }

        /// <summary>
        /// 手牌
        /// </summary>
        internal List<Card> Stack { get; private set; }

        /// <summary>
        /// 场上
        /// </summary>
        internal List<Card> Ground { get; private set; }

        /// <summary>
        /// 墓地
        /// </summary>
        internal List<Card> Graveyard { get; private set; }

        /// <summary>
        /// 战局外
        /// </summary>
        internal List<Card> Outside { get; private set; }
        #endregion

        /// <summary>
        /// 创建一个玩家
        /// </summary>
        /// <param name="NickName">玩家名称</param>
        /// <param name="Avatar">头像编号</param>
        /// <param name="Sex">头像性别</param>
        public Player(string NickName, int Avatar = 0, int Sex = 0) {

            Console.WriteLine(string.Join("\t",nameof(Player),"创建玩家", NickName));

            this.NickName = NickName;
            this.Avatar = Avatar;
            this.Sex = Sex;
        }

        /// <summary>
        /// 设置英雄
        /// </summary>
        public Player SetHero(Hero Hero)
        {
            if (IsHeroSet) {
                throw new NotSupportedException("英雄信息已经写入，禁止覆盖");
            }

            this.Hero = Hero;
            IsHeroSet = true;
            return this;
        }
        /// <summary>
        /// 英雄是否已经设置完毕
        /// </summary>
       internal bool IsHeroSet { get; private set; } = false;

        /// <summary>
        /// 设置卡牌
        /// </summary>
        public Player SetCards(Card[] Cards)
        {
            if (IsCardsSet)
            {
                throw new NotSupportedException("卡牌信息已经写入，禁止覆盖");
            }

            this.Heap = Cards.ToList();
            this.SummonPool =InitSummonPool();
            this.Stack = new List<Card>();
            this.Ground = new List<Card>();
            this.Graveyard = new List<Card>();
            this.Outside = new List<Card>();
            IsCardsSet = true;
            return this;
        }
        /// <summary>
        /// 卡牌是否已经设置完毕
        /// </summary>
        internal bool IsCardsSet { get; private set; } = false;

        /// <summary>
        /// 设置符文
        /// </summary>
        public Player SetRunes(Rune[] Runes)
        {
            if (IsRunesSet)
            {
                throw new NotSupportedException("符文信息已经写入，禁止覆盖");
            }

            this.Runes = Runes.ToArray();
            IsRunesSet = true;
            return this;
        }
        /// <summary>
        /// 符文是否已经设置完毕
        /// </summary>
        internal bool IsRunesSet { get; private set; } = false;

        /// <summary>
        /// 初始化召唤池
        /// </summary>
        private List<Card> InitSummonPool()
        {
            return new List<Card>();
        }

        public override string ToString()
        {
            return $"{NickName}{Hero}";
        }
    }
}