using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.OSS
{
    public static class OssHelper
    {
        public static bool IsDevelopment { private get; set; }

        private static string EndPoint => IsDevelopment ? "oss-cn-beijing.aliyuncs.com" : "oss-cn-beijing-internal.aliyuncs.com";
        private const string BucketName = "mkhx";
        public static string AccessKeyID { private get; set; };
        public static string AccessKeySecret { private get; set; };

        public static OssClient Client => new OssClient(EndPoint, AccessKeyID, AccessKeySecret);

        public static Stream Get(string key)
        {
            try
            {
                return Client.GetObject(BucketName, key).Content;
            }
            catch (Exception ex)
            {
                if (ex.Message == "The specified key does not exist.")
                {
                    throw new Exception("所选择的数据不存在");
                }
                else
                {
                    throw new Exception("未定义的错误1", ex);
                }
            }
        }

        public static string GetString(string key)
        {
            return new StreamReader(Get(key)).ReadToEnd();
        }

        public static byte[] GetBytes(string key)
        {
            var s = Get(key);
            var bytes = new byte[s.Length];
            s.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public static bool Exist(string key)
        {
            return Client.DoesObjectExist(BucketName, key);
        }

        public static void Upload(string key, FileInfo fi)
        {
            Client.PutObject(BucketName, key, fi.OpenRead());
        }

        public static (string[] dir, string[] file) List(string prefix = "")
        {

            var dirlist = new List<string>();
            var filelist = new List<string>();

            var finished = false;
            var marker = "";
            while (!finished)
            {
                var listObjectsRequest = new ListObjectsRequest(BucketName)
                {
                    Delimiter = "/",
                    Marker = marker,
                    MaxKeys = 1000,
                    Prefix = prefix,
                };

                ObjectListing result = Client.ListObjects(listObjectsRequest);
                if (result.CommonPrefixes.Count() + result.ObjectSummaries.Count() < listObjectsRequest.MaxKeys)
                {
                    finished = true;
                }
                else
                {
                    marker = result.ObjectSummaries.Last().Key;
                }

                dirlist.AddRange(result.CommonPrefixes);
                filelist.AddRange(result.ObjectSummaries.Select(m => m.Key));
            }

            return (dirlist.ToArray(), filelist.ToArray());
        }
    }
}