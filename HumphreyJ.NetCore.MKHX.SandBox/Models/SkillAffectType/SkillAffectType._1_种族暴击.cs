namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _1_种族暴击 : SkillAffectType
        {
            protected _1_种族暴击(string[] AffectValue, string[] AffectValue2) : base(1, AffectValue, AffectValue2)
            {

                {
                    percentage = double.Parse(AffectValue[0]) / 100;
                }

                {
                    SkillPercentageAll = Config.SKILL_CARDRACEPERCENT_VIRTUAL;
                }

                {
                    SkillPercentageMean = Config.SKILL_CARDRACEPERCENT_VIRTUAL;
                }
            }

            public override string AffectTypeName => "种族暴击";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  当目标符合指定种族时发生暴击
            //  主要种族有4个。小概率遇到魔族、魔王、魔神等，此概率暂时忽略

            private readonly double percentage;

        }
    }
}