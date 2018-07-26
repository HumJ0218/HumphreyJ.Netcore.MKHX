using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class V_Article
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public DateTime AccessTime { get; set; }
        public int AccessCount { get; set; }
    }
}