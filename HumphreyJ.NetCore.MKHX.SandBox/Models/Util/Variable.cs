using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.Util
{
    /// <summary>
    /// 可变量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Variable<T> where T : IComparable
    {
        /// <summary>
        /// 描述
        /// </summary>
        internal string Description { get; }

        /// <summary>
        /// 默认值
        /// </summary>
        internal T DefaultValue { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        internal T CurrentValue { get; private set; }

        /// <summary>
        /// 创建一个可变量
        /// </summary>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="Description">描述</param>
        internal Variable(T DefaultValue, string Description = "")
        {
            this.DefaultValue = DefaultValue;
            this.CurrentValue = DefaultValue;
            this.Description = Description;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="v">新值</param>
        internal Variable<T> Set(T v)
        {
            CurrentValue = v;
            return this;
        }

        /// <summary>
        /// 重置
        /// </summary>
        internal Variable<T> Reset()
        {
            CurrentValue = DefaultValue;
            return this;
        }

        /// <summary>
        /// 隐式转换为当前值，用于取数
        /// </summary>
        public static implicit operator T(Variable<T> v)
        {
            return v.CurrentValue;
        }

    }
}