using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.Util
{
    /// <summary>
    /// 范围量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Rangeable<T> : Variable<T> where T : IComparable
    {

        /// <summary>
        /// 最小值
        /// </summary>
        internal T MinValue { get; }

        /// <summary>
        /// 最大值
        /// </summary>
        internal T MaxValue { get; }

        /// <summary>
        /// 创建一个范围量
        /// </summary>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="MinValue">最小值</param>
        /// <param name="MaxValue">最大值</param>
        /// <param name="Description">描述</param>
        internal Rangeable(T DefaultValue, T MinValue, T MaxValue, string Description = "") : base(DefaultValue, Description)
        {
            this.MinValue = MinValue;
            this.MaxValue = MaxValue;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="v">新值</param>
        internal new Variable<T> Set(T v)
        {
            if (v.CompareTo(MinValue) >= 0 && v.CompareTo(MaxValue) <= 0)
            {
                base.Set(v);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{Description}值{v}必须在{MinValue}和{MaxValue}之间");
            }
            return this;
        }

        /// <summary>
        /// 当前值
        /// </summary>
        internal new T CurrentValue { get; private set; }

    }
}