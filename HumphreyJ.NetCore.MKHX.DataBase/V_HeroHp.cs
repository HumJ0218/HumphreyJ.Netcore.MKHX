using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class V_HeroHp
    {
        [Key]
        public Guid RowId { get; set; }
        public string Server { get; set; }
        public int Level { get; set; }
        public double Hp { get; set; }
    }
}
