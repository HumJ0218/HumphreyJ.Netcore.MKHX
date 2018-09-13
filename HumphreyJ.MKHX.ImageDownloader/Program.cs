using AssetStudio;
//using HumphreyJ.MKHX.ImageDownloader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

internal class Program
{
    public enum FileType
    {
        AssetsFile,
        BundleFile,
        WebFile
    }

    private static readonly DateTime StartTime = DateTime.Now;

    private static readonly string path0 = ConfigurationManager.AppSettings["path0"];

    private static readonly string path1 = ConfigurationManager.AppSettings["path1"];

    private static readonly string waifu2x_mode = ConfigurationManager.AppSettings["waifu2x_mode"];

    private static string GetPath0()
    {
        return Program.path0;
    }

    private static string GetPath1(int id, int ver)
    {
        return Program.path1.Replace("{id}", id.ToString()).Replace("{ver}", ver.ToString());
    }

    private static string GetPath1(int id0, int id1, int ver)
    {
        return Program.path1.Replace("{id0}", id0.ToString()).Replace("{id1}", id1.ToString()).Replace("{ver}", ver.ToString());
    }

    private static void Main(string[] args)
    {
        Console.Title = "第一阶段 - 下载并解压资源";
        if (ConfigurationManager.AppSettings["id"].Split(',').Length > 1)
        {
            Program.Main2(args);
        }
        else
        {
            Program.Main1(args);
        }
        Console.Title = "第二阶段 - waifu2x调整图像尺寸，模式：" + Program.waifu2x_mode;
        Program.ScaleImages();
        Console.Title = "第三阶段 - OSS上传文件";
    }

    private static void ScaleImages()
    {
        foreach (FileInfo item in from m in new DirectoryInfo("assets").EnumerateFiles("*.png")
                                  where m.LastWriteTime > Program.StartTime
                                  select m)
        {
            string filename = item.Name;
            string[] namesplit = filename.ToLower().Split('_');
            int _a = default(int);
            int width;
            int height;
            string format;
            if (int.TryParse(namesplit[0], out _a))
            {
                width = 512;
                height = 512;
                format = "png";
            }
            else
            {
                switch (namesplit[1])
                {
                    case "map":
                        break;
                    case "maxcard":
                        goto IL_00ef;
                    case "photocard":
                        goto IL_0107;
                    case "rune":
                        goto IL_0119;
                    default:
                        continue;
                }
                width = 960;
                height = 540;
                format = "jpg";
            }
            goto IL_012f;
            IL_0107:
            width = 110;
            height = 110;
            format = "jpg";
            goto IL_012f;
            IL_012f:
            Program.Waifu2x(item, width, height, format);
            continue;
            IL_00ef:
            width = 370;
            height = 530;
            format = "jpg";
            goto IL_012f;
            IL_0119:
            width = 110;
            height = 110;
            format = "png";
            goto IL_012f;
        }
    }

    private static void Waifu2x(FileInfo file, int width, int height, string format)
    {
        format = format.ToLower();
        int[] zoom = new int[3]
        {
            1,
            2,
            4
        };
        int[] array = zoom;
        foreach (int z in array)
        {
            FileInfo targetfile = new FileInfo(file.FullName.Replace("assets", string.Format("output/{0}x", z)).Replace(file.Extension, string.Format(".{0}", format)));
            targetfile.Directory.Create();
            List<string> args = new List<string>
            {
                string.Format("-i {0}", file.FullName),
                string.Format("-o {0}", targetfile.FullName),
                "--model_dir models/upconv_7_anime_style_art_rgb",
                string.Format("--scale_width {0}", width * z),
                string.Format("--scale_height {0}", height * z),
                "--mode noise_scale",
                "--noise_level 3",
                "--process " + Program.waifu2x_mode,
                (format == "jpg") ? "--output_quality 100" : ((format == "png") ? "--output_depth 16" : "")
            };
            using (Process waifu2xcui = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    FileName = "waifu2x-caffe-cui.exe",
                    Arguments = string.Join(" ", args),
                    CreateNoWindow = false
                }
            })
            {
                Console.Write(DateTime.Now + "\t" + targetfile.DirectoryName.Split('\\').LastOrDefault() + "\\" + targetfile.Name + "\t");
                waifu2xcui.Start();
                waifu2xcui.WaitForExit();
                int code = waifu2xcui.ExitCode;
            }
        }
    }

    private static void Main1(string[] args)
    {
        int id = int.Parse(ConfigurationManager.AppSettings["id"]);
        int ver = int.Parse(ConfigurationManager.AppSettings["ver"]);
        DirectoryInfo directoryInfo4 = new DirectoryInfo("Temp");
        directoryInfo4.Create();
        DirectoryInfo directoryInfo3 = new DirectoryInfo("First");
        directoryInfo3.Create();
        DirectoryInfo directoryInfo2 = new DirectoryInfo("Last");
        directoryInfo2.Create();
        WebClient webClient = new WebClient();
        for (int j = 1; j < id; j++)
        {
            List<FileInfo> list = new List<FileInfo>();
            for (int i = 1; i <= ver; i++)
            {
                string p2 = Program.GetPath0();
                string p = Program.GetPath1(j, i);
                Console.Write(DateTime.Now + "\t下载\t");
                try
                {
                    FileInfo fileInfo4 = new FileInfo("Temp/" + p);
                    if (fileInfo4.Exists && fileInfo4.Length > 0)
                    {
                        Console.WriteLine(p + "\t已存在");
                    }
                    else
                    {
                        webClient.DownloadFile(p2 + "/" + p, fileInfo4.FullName);
                        Console.WriteLine(p + "\t成功");
                    }
                    list.Add(fileInfo4);
                }
                catch
                {
                    Console.WriteLine(p + "\t失败");
                    break;
                }
            }
            if (list.Count > 0)
            {
                FileInfo fileInfo3 = list.Last();
                Program.ExtractFile(fileInfo3.FullName, "assets\\");
            }
        }
    }

    private static void Main2(string[] args)
    {
        int[] id = (from m in ConfigurationManager.AppSettings["id"].Split(',')
                    select int.Parse(m)).ToArray();
        int ver = int.Parse(ConfigurationManager.AppSettings["ver"]);
        DirectoryInfo directoryInfo0 = new DirectoryInfo("Temp");
        directoryInfo0.Create();
        WebClient webClient = new WebClient();
        for (int i3 = 0; i3 < id[0]; i3++)
        {
            for (int i2 = 0; i2 < id[1]; i2++)
            {
                List<FileInfo> list = new List<FileInfo>();
                for (int i = 1; i <= ver; i++)
                {
                    string p2 = Program.GetPath0();
                    string p = Program.GetPath1(i3, i2, i);
                    Console.Write(DateTime.Now + "\t下载\t");
                    try
                    {
                        FileInfo fileInfo4 = new FileInfo("Temp/" + p);
                        if (fileInfo4.Exists && fileInfo4.Length > 0)
                        {
                            Console.WriteLine(p + "\t已存在");
                        }
                        else
                        {
                            webClient.DownloadFile(p2 + "/" + p, fileInfo4.FullName);
                            Console.WriteLine(p + "\t成功");
                        }
                        list.Add(fileInfo4);
                    }
                    catch
                    {
                        Console.WriteLine(p + "\t失败");
                        break;
                    }
                }
                if (list.Count > 0)
                {
                    FileInfo fileInfo3 = list.Last();
                    Program.ExtractFile(fileInfo3.FullName, "assets\\");
                }
            }
        }
    }

    public static void ExtractFile(string fullName, string exportpath)
    {
        Console.WriteLine();
        Console.Write(DateTime.Now + "\t解包\t");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(new FileInfo(fullName).Name + "\t");
        Console.ResetColor();
        EndianBinaryReader reader = default(EndianBinaryReader);
        FileType fileType = Program.CheckFileType(fullName, out reader);
        if (fileType == FileType.BundleFile)
        {
            Console.WriteLine("类型：资源包");
            List<AssetsFile> list = Program.LoadBundleFile(fullName, reader, null);
            Program.ExportAssets(list, exportpath);
        }
        else
        {
            Console.WriteLine("未知类型");
        }
        Console.WriteLine();
    }

    private static void ExportAssets(List<AssetsFile> list, string exportpath)
    {
        list.ForEach(delegate (AssetsFile file)
        {
            var assetList=file.preloadTable.Values.ToList();

            foreach (AssetPreloadData asset in assetList)
            {
                ClassIDReference type = asset.Type;
                if (type == ClassIDReference.Texture2D)
                {
                    Exporter.ExportTexture2D(asset, exportpath, true);
                    var imageFile = new FileInfo("assets\\" + asset.Text + ".png");

                    var bmp = new Bitmap(imageFile.FullName);
                    var pixel = bmp.GetPixel(0, 0);
                    bmp.Dispose();

                    if (pixel.A != 0)
                    {
                        Console.Write(DateTime.Now + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\t" + asset.uniqueID + "\t" + asset.Text + "\t不是符合要求的图片，清除");
                        Console.ResetColor();
                        imageFile.Delete();
                    }
                    else
                    {
                        Console.Write(DateTime.Now + "\t");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t" + asset.uniqueID + "\t" + asset.Text);
                        Console.ResetColor();
                        break;
                    }
                }
            }

        });
    }

    public static FileType CheckFileType(string fileName, out EndianBinaryReader reader)
    {
        reader = new EndianBinaryReader(File.OpenRead(fileName), EndianType.BigEndian);
        return Program.CheckFileType(reader);
    }

    private static FileType CheckFileType(EndianBinaryReader reader)
    {
        string signature = reader.ReadStringToNull();
        reader.Position = 0L;
        switch (signature)
        {
            case "UnityWeb":
            case "UnityRaw":
            case "úúúúúúúú":
            case "UnityFS":
                return FileType.BundleFile;
            case "UnityWebData1.0":
                return FileType.WebFile;
            default:
                {
                    byte[] magic = reader.ReadBytes(2);
                    reader.Position = 0L;
                    if (WebFile.gzipMagic.SequenceEqual(magic))
                    {
                        return FileType.WebFile;
                    }
                    reader.Position = 32L;
                    magic = reader.ReadBytes(6);
                    reader.Position = 0L;
                    if (WebFile.brotliMagic.SequenceEqual(magic))
                    {
                        return FileType.WebFile;
                    }
                    return FileType.AssetsFile;
                }
        }
    }

    private static List<AssetsFile> LoadBundleFile(string fullName, EndianBinaryReader reader, string parentPath = null)
    {
        string fileName = Path.GetFileName(fullName);
        BundleFile bundleFile = new BundleFile(reader);
        reader.Dispose();
        List<AssetsFile> assetsfileList = new List<AssetsFile>();
        List<string> assetsfileListHash = new List<string>();
        Dictionary<string, EndianBinaryReader> resourceFileReaders = new Dictionary<string, EndianBinaryReader>();
        bundleFile.fileList.ForEach(delegate (MemoryFile file)
        {
            AssetsFile assetsFile = new AssetsFile(Path.GetDirectoryName(fullName) + "\\" + file.fileName, new EndianBinaryReader(file.stream, EndianType.BigEndian));
            if (assetsFile.valid)
            {
                assetsFile.parentPath = (parentPath ?? fullName);
                if (assetsFile.fileGen == 6)
                {
                    assetsFile.m_Version = bundleFile.versionEngine;
                    assetsFile.version = (from Match m in Regex.Matches(bundleFile.versionEngine, "\\d")
                                          select int.Parse(m.Value)).ToArray();
                    assetsFile.buildType = Regex.Replace(bundleFile.versionEngine, "\\d", "").Split(new string[1]
                    {
                        "."
                    }, StringSplitOptions.RemoveEmptyEntries);
                }
                assetsfileList.Add(assetsFile);
                assetsfileListHash.Add(assetsFile.upperFileName);
            }
            else
            {
                resourceFileReaders.Add(assetsFile.upperFileName, assetsFile.reader);
            }
        });
        return assetsfileList;
    }
}
