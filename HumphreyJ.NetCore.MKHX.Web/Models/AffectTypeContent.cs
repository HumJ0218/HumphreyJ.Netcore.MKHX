using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.DataBase;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public class AffectTypeContent
    {
        public int AffectType { get; private set; }
        public string Name { get; private set; }
        public string AffectValue1 { get; private set; }
        public string AffectValue2 { get; private set; }
        public string Desc { get; private set; }

        private AffectTypeContent() { }

        public static Dictionary<int, AffectTypeContent> List { get; }
            = EnumAccesser.SkillAffectType.ToDictionary(m => m.Key, m => (AffectTypeContent)m.Value);

        public static explicit operator AffectTypeContent(DataBase.Enum v)
        {
            return new AffectTypeContent
            {
                AffectType = v.Key,
                Name = v.Name,
                AffectValue1 = v.Value1Format,
                AffectValue2 = v.Value2Format,
                Desc = v.Desc,
            };
        }
    }
}