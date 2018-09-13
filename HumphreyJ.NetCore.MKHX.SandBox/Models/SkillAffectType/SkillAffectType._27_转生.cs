namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    internal abstract partial class SkillAffectType
    {
        public class _27_转生 : SkillAffectType
        {
            protected _27_转生(string[] AffectValue, string[] AffectValue2) : base(27, AffectValue, AffectValue2)
            {
                {
                    percentage = double.Parse(AffectValue[0]) / 100;
                }

                {
                    SkillPercentageAll = 1;
                }

                {
                    SkillPercentageMean = 1;
                }
            }

            public override string AffectTypeName => "转生";

            public override double SkillPercentageAll { get; }

            public override double SkillPercentageMean { get; }

            //  进入墓地前有{percentage}%的概率进入手牌，如果手牌已满则进入牌堆。 

            private readonly double percentage;
        }

    }
}