using HumphreyJ.NetCore.MKHX.SandBox.Models.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.SkillAffectType
{
    /// <summary>
    /// 技能效果基类
    /// </summary>
    internal abstract partial class SkillAffectType : ISkillAffectType
    {
        /// <summary>
        /// 创建技能逻辑
        /// </summary>
        protected SkillAffectType(int AffectType, string[] AffectValue = null, string[] AffectValue2 = null)
        {
            this.AffectType = AffectType;
            this.AffectValue = AffectValue;
            this.AffectValue2 = AffectValue2;
        }

        /// <summary>
        /// 返回技能效果编号
        /// </summary>
        public int AffectType { get; }

        /// <summary>
        /// 在派生类中重写时，返回技能效果名称
        /// </summary>
        public abstract string AffectTypeName { get; }

        /// <summary>
        /// 返回技能效果参数1
        /// </summary>
        public string[] AffectValue { get; }

        /// <summary>
        /// 返回技能效果参数2
        /// </summary>
        public string[] AffectValue2 { get; }

        /// <summary>
        /// 在派生类中重写时，返回技能完整发动的概率（所有概率相乘）
        /// </summary>
        public abstract double SkillPercentageAll { get; }

        /// <summary>
        /// 在派生类中重写时，返回技能发动总概率期望
        /// </summary>
        public abstract double SkillPercentageMean { get; }

        /// <summary>
        /// 获取指定参数的技能逻辑
        /// </summary>
        /// <param name="AffectType">技能效果编号</param>
        /// <param name="AffectValue">技能效果参数1</param>
        /// <param name="AffectValue2">技能效果参数2</param>
        public static ISkillAffectType Get(int AffectType, string[] AffectValue = null, string[] AffectValue2 = null)
        {
            switch (AffectType)
            {
                case 0: return new _0_普通攻击(null, null);
                default: throw new NotImplementedException();
            }
        }

    }
}