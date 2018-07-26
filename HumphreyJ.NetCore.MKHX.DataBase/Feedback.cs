using System;
using System.Collections.Generic;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class Feedback
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Ua { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
        public string Reply { get; set; }
        public DateTime? ReplayTime { get; set; }
    }
}
