using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class V_DungeonDeck
    {
        [Key]
        public Guid RowId { get; set; }
        public string DefendPlayerName { get; set; }
        public string DefendPlayerLevel { get; set; }
        public double DefendPlayerHP { get; set; }
        public string DefendPlayerCards { get; set; }
        public string DefendPlayerRunes { get; set; }
        public int Count { get; set; }
    }
}