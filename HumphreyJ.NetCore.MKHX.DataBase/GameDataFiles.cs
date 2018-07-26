using System;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class GameDataFiles
    {
        public Guid Id { get; set; }
        public Guid Version { get; set; }
        public string FileName { get; set; }
        public string Server { get; set; }
        public DateTime Time { get; set; }
        public string Remark { get; set; }
    }
}
