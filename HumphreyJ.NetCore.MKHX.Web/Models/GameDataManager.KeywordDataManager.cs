using HumphreyJ.NetCore.MKHX.GameData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public partial class GameDataManager
    {
        public class KeywordDataManager : DataManager
        {
            public KeywordDataManager(Guid version)
            {
                Version = version;
            }

            public new RawKeywordData[] RawData => GetRawData();
            public ParsedKeywordData[] ParsedData => GetParsedData();

            private RawKeywordData[] rawData;
            private RawKeywordData[] GetRawData()
            {
                if (rawData == null)
                {
                    rawData = RawKeywordData.ParseJsonString(base.RawData);
                }
                return rawData;
            }
            private static Dictionary<Guid, ParsedKeywordData[]> parsedData = new Dictionary<Guid, ParsedKeywordData[]>();
            private ParsedKeywordData[] GetParsedData()
            {
                if (!parsedData.ContainsKey(Version))
                {
                    parsedData.Add(Version, RawData.Select(m => new ParsedKeywordData(m)).ToArray());
                }
                return parsedData[Version];
            }

            internal ParsedKeywordData[] Preload()
            {
                var data = ParsedData;
                {
                    //  GC
                    rawData = null;
                }
                return data;
            }
        }
    }
}