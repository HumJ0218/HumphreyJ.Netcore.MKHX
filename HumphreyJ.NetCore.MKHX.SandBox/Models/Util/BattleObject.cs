using System;
using System.Collections.Generic;
using System.Text;

namespace HumphreyJ.NetCore.MKHX.SandBox.Models.Util
{
    /// <summary>
    /// 战斗中要涉及的对象实体，必须带有唯一标识
    /// </summary>
    internal abstract class BattleObject : IComparable
    {
        internal Guid Guid { get; } = Guid.NewGuid();

        public int CompareTo(object obj)
        {
            return ((IComparable)Guid).CompareTo(obj);
        }

        public override bool Equals(object obj)
        {
            var @object = obj as BattleObject;
            return @object != null &&
                   Guid.Equals(@object.Guid);
        }

        public override int GetHashCode()
        {
            return -737073652 + EqualityComparer<Guid>.Default.GetHashCode(Guid);
        }

        public static bool operator ==(BattleObject a, BattleObject b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(BattleObject a, BattleObject b)
        {
            return a.CompareTo(b) != 0;
        }

    }
}