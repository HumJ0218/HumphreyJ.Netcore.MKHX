using System;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class PvCounter
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string Ip { get; set; }
        public string Ua { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
