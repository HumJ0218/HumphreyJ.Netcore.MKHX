using HumphreyJ.NetCore.MKHX.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public class EnumAccesser
    {

        static readonly Dictionary<string, Dictionary<int, Enum>> Enum = new MkhxCoreContext().Enum.GroupBy(m => m.Type).ToDictionary(m => m.Key, m => m.ToDictionary(n => n.Key, n => n));

        public static Dictionary<int, Enum> CardColor = Enum[nameof(CardColor)];
        public static Dictionary<int, Enum> CardColorCss = Enum[nameof(CardColorCss)];
        public static Dictionary<int, Enum> CardRace = Enum[nameof(CardRace)];

        public static Dictionary<int, Enum> RuneColor = Enum[nameof(RuneColor)];
        public static Dictionary<int, Enum> RuneFragmentColor = Enum[nameof(RuneFragmentColor)];
        public static Dictionary<int, Enum> RuneProperty = Enum[nameof(RuneProperty)];
        public static Dictionary<int, Enum> RuneSkillConditionCompare = Enum[nameof(RuneSkillConditionCompare)];
        public static Dictionary<int, Enum> RuneSkillConditionSlide = Enum[nameof(RuneSkillConditionSlide)];
        public static Dictionary<int, Enum> RuneSkillConditionType = Enum[nameof(RuneSkillConditionType)];

        public static Dictionary<int, Enum> SkillAffectType = Enum[nameof(SkillAffectType)];
        public static Dictionary<int, Enum> SkillAffectTypePosition = Enum[nameof(SkillAffectTypePosition)];
        public static Dictionary<int, Enum> SkillAffectTypeSide = Enum[nameof(SkillAffectTypeSide)];
        public static Dictionary<int, Enum> SkillCategory = Enum[nameof(SkillCategory)];
        public static Dictionary<int, Enum> SkillLaunchCondition = Enum[nameof(SkillLaunchCondition)];
        public static Dictionary<int, Enum> SkillLaunchType = Enum[nameof(SkillLaunchType)];

        public static Dictionary<int, Enum> MapStageDetialType = Enum[nameof(MapStageDetialType)];

    }
}