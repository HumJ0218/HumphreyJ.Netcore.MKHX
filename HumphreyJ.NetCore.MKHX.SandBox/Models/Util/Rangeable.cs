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
        internal T MinValue { get; private set; }

        /// <summary>
        /// 最大值
        /// </summary>
        internal T MaxValue { get; private set; }

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
        internal new Rangeable<T> Set(T v)
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
        /// 设置最大值
        /// </summary>
        /// <param name="v">新值</param>
        internal Rangeable<T> SetMax(T v)
        {
            this.MaxValue = v;
            return this;
        }

        /// <summary>
        /// 将值设为最大值
        /// </summary>
        internal Rangeable<T> SetToMax()
        {
            this.Set(MaxValue);
            return this;
        }

        /// <summary>
        /// 设置最小值
        /// </summary>
        /// <param name="v">新值</param>
        internal Rangeable<T> SetMin(T v)
        {
            this.MinValue = v;
            return this;
        }

        /// <summary>
        /// 将值设为最小值
        /// </summary>
        internal Rangeable<T> SetToMin()
        {
            this.Set(MinValue);
            return this;
        }

        public override string ToString()
        {
            return $"{MinValue}/{CurrentValue}/{MaxValue}";
        }
    }
}