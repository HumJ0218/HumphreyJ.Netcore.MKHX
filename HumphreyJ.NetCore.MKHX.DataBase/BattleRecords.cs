using System;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class BattleRecords
    {
        public string BattleId { get; set; }
        public string Server { get; set; }
        public string AttackPlayerName { get; set; }
        public int AttackPlayerAvatar { get; set; }
        public int AttackPlayerLevel { get; set; }
        public double AttackPlayerHp { get; set; }
        public string AttackPlayerCards { get; set; }
        public string AttackPlayerRunes { get; set; }
        public string DefendPlayerName { get; set; }
        public int DefendPlayerAvatar { get; set; }
        public int DefendPlayerLevel { get; set; }
        public double DefendPlayerHp { get; set; }
        public string DefendPlayerCards { get; set; }
        public string DefendPlayerRunes { get; set; }
        public int First { get; set; }
        public int? Win { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
    }
}
