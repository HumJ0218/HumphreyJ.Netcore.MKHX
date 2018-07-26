using HumphreyJ.NetCore.MKHX.OSS;
using HumphreyJ.NetCore.MKHX.Web.Models.Helpers;
using System;
using System.Net;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public abstract class DataManager
        {
            public GameDataManager GameDataManager { get; private set; }
            public Guid Version { get; protected set; }

            protected string RawData => OssHelper.GetString($"data/{Version}.json");

            public override string ToString()
            {
                return Version.ToString();
            }
        }
    }
}