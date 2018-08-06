using HumphreyJ.NetCore.MKHX.DataBase;
using HumphreyJ.NetCore.MKHX.OSS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

namespace HumphreyJ.NetCore.MKHX.GameData.Uploader
{
    class Program
    {
        readonly static Dictionary<string, string> Config = File.ReadAllLines("Config.txt").Select(m => m.Split('\t')).Where(m => m.Length > 2 && string.IsNullOrEmpty(m[0])).ToDictionary(m => m[1].Trim(), m => m[2].Trim());

        static void Main(string[] args)
        {

            try
            {

                {
                    Console.Title = "服务器" + "\t" + Config["服务器"] + "\r\n";

                    WriteText("服务器" + "\t" + Config["服务器"] + "\r\n");
                    WriteText("本地数据路径" + "\t" + Config["本地数据路径"] + "\r\n");


                    if (Config["服务器"].ToLower() == "x")
                    {

                        WriteText("卡牌" + "\t" + Config["卡牌"] + "\r\n");
                        WriteText("符文" + "\t" + Config["符文"] + "\r\n");
                        WriteText("技能" + "\t" + Config["技能"] + "\r\n");
                        WriteText("关卡" + "\t" + Config["关卡"] + "\r\n");

                        new Thread(DownloadWebData).Start();
                    }
                }

                WriteText("\r\n");
                WriteText("\r\n");

                UploadData();
            }
            catch (Exception ex)
            {

                WriteError($"\r\n\r\n{ex}\r\n\r\n");
                Console.ReadLine();

            }

        }

        private static void UploadData()
        {
            var 服务器 = Config["服务器"];
            var 本地数据路径 = new DirectoryInfo(Config["本地数据路径"]);
            var 上传间隔 = int.Parse(Config["上传间隔"]);
            var DB用户名 = Config["DB用户名"];
            var DB密码 = Config["DB密码"];
            var OSSAKI = Config["OSSAKI"];
            var OSSAKS = Config["OSSAKS"];

            OssHelper.IsDevelopment = true;
            OssHelper.AccessKeyID = OSSAKI;
            OssHelper.AccessKeySecret = OSSAKS;

            var dataContext = new MkhxCoreContext(DB用户名, DB密码);

            while (true)
            {
                WriteText("\r\n");
                WriteText(DateTime.Now+"\t");
                WriteInfo("更新数据..." + "\r\n");

                var files = 本地数据路径.GetFiles("*.txt");
                foreach (var fi in files)
                {
                    var FileName = fi.Name.Split('_')[0].ToLower();

                    switch (FileName)
                    {
                        case "allcards":
                        case "allrunes":
                        case "allskills":
                        case "allmapstage":
                        case "allmaphardstage":
                        case "keywords":
                            {
                                try
                                {
                                    var Version = GetVersion(fi);

                                    WriteText(DateTime.Now + "\t");
                                    WriteText(FileName + "\t");
                                    WriteText(Version + "\t");

                                    var newest = dataContext.GameDataFiles.Where(m => m.Server == 服务器 && m.FileName == FileName).OrderByDescending(m => m.Time).FirstOrDefault();

                                    if (newest?.Version != Version)
                                    {
                                        var key = "data/" + Version + ".json";
                                        if (!OssHelper.Exist(key))
                                        {
                                            OssHelper.Upload(key, fi);
                                        }

                                        var GameDataFiles = new GameDataFiles
                                        {
                                            Id = Guid.NewGuid(),
                                            Version = Version,
                                            FileName = FileName,
                                            Server = 服务器,
                                            Time = fi.LastWriteTime,
                                            Remark = "",
                                        };
                                        dataContext.GameDataFiles.Add(GameDataFiles);
                                        dataContext.SaveChanges();
                                        WriteSuccess("成功" + "\r\n");

                                        Console.Title = $"服务器{Config["服务器"]}　更新时间：{fi.LastWriteTime}";

                                    }
                                    else {
                                        WriteWarning("不需要更新" + "\r\n");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    WriteError("失败 - " + ex + "\r\n");
                                }
                                break;
                            }
                    }
                }

                WriteText(DateTime.Now + "\t");
                WriteInfo("数据更新完毕" + "\r\n");
                WriteText("\r\n");
                Thread.Sleep(上传间隔);
            }
        }

        private static void DownloadWebData()
        {
            var 本地数据路径 = new DirectoryInfo(Config["本地数据路径"]);
            var 下载间隔 = int.Parse(Config["下载间隔"]);
            var 卡牌 = new Uri(Config["卡牌"]);
            var 符文 = new Uri(Config["符文"]);
            var 技能 = new Uri(Config["技能"]);
            var 关卡 = new Uri(Config["关卡"]);

            var wc = new WebClient();
            本地数据路径.Create();

            while (true)
            {

                WriteText("\r\n");
                WriteText(DateTime.Now + "\t");
                WriteInfo("下载网页版数据..." + "\r\n");
                try
                {
                    WriteText(DateTime.Now + "\t");
                    WriteText("\t卡牌..." + "");
                    var allcards = wc.DownloadCompressedString(卡牌);
                    allcards = JsonConvert.SerializeObject(JObject.Parse(allcards)["data"]["Cards"]);
                    File.WriteAllText(本地数据路径.FullName + "\\" + "AllCards_CARDNEW-WEB-CHS.txt", allcards);
                    WriteText("\tOK" + "\r\n");
                }
                catch (Exception e)
                {
                    WriteError("\t失败 - " + e.GetBaseException().Message + "\r\n");
                }
                try
                {
                    WriteText(DateTime.Now + "\t");
                    WriteText("\t符文..." + "");
                    var allrunes = wc.DownloadCompressedString(符文);
                    allrunes = JsonConvert.SerializeObject(JObject.Parse(allrunes)["data"]["Runes"]);
                    File.WriteAllText(本地数据路径.FullName + "\\" + "AllRunes_CARDNEW-WEB-CHS.txt", allrunes);
                    WriteSuccess("\tOK" + "\r\n");
                }
                catch (Exception e)
                {
                    WriteError("\t失败 - " + e.GetBaseException().Message + "\r\n");
                }
                try
                {
                    WriteText(DateTime.Now + "\t");
                    WriteText("\t技能..." + "");
                    var allskills = wc.DownloadCompressedString(技能);
                    allskills = JsonConvert.SerializeObject(JObject.Parse(allskills)["data"]["Skills"]);
                    File.WriteAllText(本地数据路径.FullName + "\\" + "AllSkills_CARDNEW-WEB-CHS.txt", allskills);
                    WriteSuccess("\tOK" + "\r\n");
                }
                catch (Exception e)
                {
                    WriteError("\t失败 - " + e.GetBaseException().Message + "\r\n");
                }
                try
                {
                    WriteText(DateTime.Now + "\t");
                    WriteText("\t关卡..." + "");
                    var allmapstage = wc.DownloadCompressedString(关卡);
                    allmapstage = JsonConvert.SerializeObject(JObject.Parse(allmapstage)["data"]);
                    File.WriteAllText(本地数据路径.FullName + "\\" + "AllMapStage_CARDNEW-WEB-CHS.txt", allmapstage);
                    WriteSuccess("\tOK" + "\r\n");
                }
                catch (Exception e)
                {
                    WriteError("\t失败 - " + e.GetBaseException().Message + "\r\n");
                }
                try
                {
                    var allmaphardstage = "[]";
                    File.WriteAllText(本地数据路径.FullName + "\\" + "AllMapHardStage_CARDNEW-WEB-CHS.txt", allmaphardstage);
                }
                catch { }
                try
                {
                    var keywords = "{}";
                    File.WriteAllText(本地数据路径.FullName + "\\" + "Keywords_CARDNEW-WEB-CHS.txt", keywords);
                }
                catch { }
                WriteText(DateTime.Now + "\t");
                WriteInfo("网页版数据下载完毕" + "\r\n");
                WriteText("\r\n");

                Thread.Sleep(下载间隔);

            }
        }

        private static Guid GetVersion(FileInfo fi)
        {
            byte[] v = File.ReadAllBytes(fi.FullName);

            MD5 md5 = MD5.Create();
            byte[] array = md5.ComputeHash(v);

            return Guid.Parse(string.Join("", array.Select(m => m.ToString("x2"))));
        }

        static readonly object consoleLock = new object();
        static void WriteError(object message)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(message);
            }
        }
        static void WriteWarning(object message)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(message);
            }
        }
        static void WriteInfo(object message)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(message);
            }
        }
        static void WriteSuccess(object message)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);
            }
        }
        static void WriteText(object message)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(message);
            }
        }
    }

    internal static class WebClientExtend {
        public static string DownloadCompressedString(this WebClient wc, Uri path)
        {
            var bytes = wc.DownloadData(path);

            try
            {
                using (var compressStream = new MemoryStream(bytes))
                {
                    using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                    {
                        using (var resultStream = new MemoryStream())
                        {
                            zipStream.CopyTo(resultStream);
                            bytes = resultStream.ToArray();
                        }
                    }
                }
            }
            catch { }

            var s = System.Text.Encoding.Default.GetString(bytes);

            return s;
        }
    }
}