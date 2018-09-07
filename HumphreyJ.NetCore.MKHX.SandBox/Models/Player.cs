using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 玩家
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// 头像编号（无实际作用）
        /// </summary>
        internal int Avatar { get; }

         /// <summary>
        /// 头像性别（无实际作用）
        /// </summary>
        internal PlayerSex Sex { get; }

        /// <summary>
        /// 玩家名称（无实际作用）
        /// </summary>
        internal int NickName { get; }

       /// <summary>
        /// 英雄
        /// </summary>
        internal Hero Hero { get; }

        /// <summary>
        /// 符文槽
        /// </summary>
        internal Rune[] Runes { get;  }

        /// <summary>
        /// 牌堆（卡组）
        /// </summary>
        internal List<Card> Heap { get;  }

        /// <summary>
        /// 召唤池
        /// </summary>
        internal List<Card> SummonPool { get;  }

        /// <summary>
        /// 手牌
        /// </summary>
        internal List<Card> Stack { get;  }

        /// <summary>
        /// 场上
        /// </summary>
        internal List<Card> Ground { get; }

        /// <summary>
        /// 墓地
        /// </summary>
        internal List<Card> Graveyard { get; }

        /// <summary>
        /// 战局外
        /// </summary>
        internal List<Card> Outside { get; }
    }
}