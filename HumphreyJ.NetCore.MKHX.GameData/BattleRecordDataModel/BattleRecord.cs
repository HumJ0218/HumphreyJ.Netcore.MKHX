using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.GameData
{
    /// <summary>
    /// 战斗记录
    /// </summary>
    public class BattleRecord
    {
        public BattleRecord() { }

        /// <summary>
        /// 结局状态
        /// </summary>
        public int status;

        /// <summary>
        /// 战场数据
        /// </summary>
        public BattleRecordData data;

        /// <summary>
        /// 版本信息（？）
        /// </summary>
        public object version;

    }
}
