namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _0_普通攻击 : SkillAffectType
        {
            public _0_普通攻击(string[] AffectValue, string[] AffectValue2) : base(0, AffectValue, AffectValue2)
            {
                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            override public string AffectTypeName => "普通攻击";

            override public double SkillPercentageAll { get; }

            override public double SkillPercentageMean { get; }

            //  普通攻击虚拟技能

        }

    }
}