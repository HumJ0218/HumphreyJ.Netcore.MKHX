using HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType;
using HumphreyJ.NetCore.MKHX.SandBox.Models.SkillLaunchType;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models
{
    /// <summary>
    /// 技能
    /// </summary>
    internal class Skill
    {
        /// <summary>
        /// 技能名称（无实际作用）
        /// </summary>
        public string SkillName { get; }

        /// <summary>
        /// 技能编号（无实际作用）
        /// </summary>
        public int SkillId { get; }

        /// <summary>
        /// 技能类型（无实际作用）
        /// </summary>
        public int SkillCategory { get; }

        /// <summary>
        /// 技能动画
        /// </summary>
        public int[] AnimationType { get; }

        /// <summary>
        /// 技能效果
        /// </summary>
        public ISkillAffectType[] SkillAffect { get; }

        /// <summary>
        /// 技能触发条件
        /// </summary>
        public ISkillLaunchType SkillLaunch { get; }

    }
}