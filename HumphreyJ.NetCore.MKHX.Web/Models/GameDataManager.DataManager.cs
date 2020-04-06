using HumphreyJ.NetCore.MKHX.Web.Models.Helpers;
using System;
using System.Net;
using System.Net.Http;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public abstract class DataManager
        {
            private static readonly HttpClient hc = new HttpClient();

            public GameDataManager GameDataManager { get; private set; }
            public Guid Version { get; protected set; }

            //protected string RawData => OssHelper.GetString($"data/{Version}.json");
            protected string RawData
            {
                get
                {
                    string content;
                    lock (hc)
                    {
                        content = hc.GetStringAsync($"https://oss.mkhx.humphreyj.com/data/{Version}.json").Result;
                    }
                    return content;
                }
            }

            public override string ToString()
            {
                return Version.ToString();
            }
        }
    }
}