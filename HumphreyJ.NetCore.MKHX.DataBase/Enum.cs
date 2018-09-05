using System;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class Enum
    {
        public string Type { get; set; }
        public int Key { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Value1Format { get; set; }
        public string Value2Format { get; set; }
    }
}
