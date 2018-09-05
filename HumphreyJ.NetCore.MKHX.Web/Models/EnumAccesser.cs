using HumphreyJ.NetCore.MKHX.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public class EnumAccesser
    {
        static readonly IEnumerable<Enum> Enum = new MkhxCoreContext().Enum;

        public static Dictionary<int, Enum> CardColor = Enum.Where(m => m.Type == "CardColor").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> CardColorCss = Enum.Where(m => m.Type == "CardColorCss").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> CardRace = Enum.Where(m => m.Type == "CardRace").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> RuneSkillConditionCompare = Enum.Where(m => m.Type == "RuneSkillConditionCompare").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> RuneSkillConditionSlide = Enum.Where(m => m.Type == "RuneSkillConditionSlide").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> RuneSkillConditionType = Enum.Where(m => m.Type == "RuneSkillConditionType").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> SkillAffectType = Enum.Where(m => m.Type == "SkillAffectType").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> SkillLaunchCondition = Enum.Where(m => m.Type == "SkillLaunchCondition").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> SkillLaunchType = Enum.Where(m => m.Type == "SkillLaunchType").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> MapStageDetialType = Enum.Where(m => m.Type == "MapStageDetialType").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> RuneColor = Enum.Where(m => m.Type == "RuneColor").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> RuneProperty = Enum.Where(m => m.Type == "RuneProperty").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> SkillConditionCompare = Enum.Where(m => m.Type == "SkillConditionCompare").ToDictionary(m => m.Key, m => m);
        public static Dictionary<int, Enum> RuneFragmentColor = Enum.Where(m => m.Type == "RuneFragmentColor").ToDictionary(m => m.Key, m => m);
          public static Dictionary<int, Enum> SkillCategory = Enum.Where(m => m.Type == "SkillCategory").ToDictionary(m => m.Key, m => m);
           public static Dictionary<int, Enum> SkillAffectTypeSide = Enum.Where(m => m.Type == "SkillAffectTypeSide").ToDictionary(m => m.Key, m => m);
          public static Dictionary<int, Enum> SkillAffectTypePosition = Enum.Where(m => m.Type == "SkillAffectTypePosition").ToDictionary(m => m.Key, m => m);
 }
}