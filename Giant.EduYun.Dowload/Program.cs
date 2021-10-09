using System;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Giant.EduYun.Models;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Giant.EduYun.Dowload
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(0);
            Console.WriteLine("程序开始运行");
            var config = new DowloadConfig
            {
                BaseDir = "E:\\国家中小学网络云平台",
                XueDuan = "xd0001",//小学：xd0001，初中：xd0002，高中：xd0003
                NianJi = "njs001",//一年级上
                XueKe = "meishu",
                DanYuan = "dy1203"
            };

            var mainData = JsonSerializer.Deserialize<MainModel>(File.ReadAllText("MainData.json"));
            var xd = mainData.XueDuanList.SingleOrDefault(w => w.Code == config.XueDuan);
            if (xd == null) throw new Exception("学段编号错误");
            else
            {
                var path = Path.Combine(config.BaseDir, xd.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            var nj = xd.NianJiList.SingleOrDefault(w => w.Code == config.NianJi);
            if (nj == null) throw new Exception("年级编号错误");
            else
            {
                var path = Path.Combine(config.BaseDir, xd.Name, nj.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            var xk = nj.XueKeList.SingleOrDefault(w => w.Code == config.XueKe);
            if (xk == null) throw new Exception("学科编号错误");
            else
            {
                var path = Path.Combine(config.BaseDir, xd.Name, nj.Name, xk.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            var dy = xk.DanYuanList.SingleOrDefault(w => w.Code == config.DanYuan);
            if (dy == null) throw new Exception("单元编号错误");
            else
            {
                var path = Path.Combine(config.BaseDir, xd.Name, nj.Name, xk.Name, dy.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            foreach (var kc in dy.KeChengList)
            {
                var para = new string[] {
                    kc.VideoFile,
                    $"--workDir \"{Path.Combine(config.BaseDir, xd.Name, nj.Name, xk.Name, dy.Name)}\"",
                    $"--saveName \"{kc.Name}\"",
                    "--enableDelAfterDone",
                    "--disableDateInfo"
                };
                //Process.Start(Path.Combine(config.BaseDir, "N_m3u8DL-CLI_v2.9.7.exe"), para).WaitForExit();

                await Process.Start(new ProcessStartInfo()
                {
                    FileName = Path.Combine(config.BaseDir, "N_m3u8DL-CLI_v2.9.7.exe"),
                    WorkingDirectory = Path.Combine(config.BaseDir, xd.Name, nj.Name, xk.Name, dy.Name),
                    Arguments = String.Join(" ", para),
                    CreateNoWindow = false
                }).WaitForExitAsync();
            }

            Console.WriteLine("视频下载完成,按任意键结束");
            Console.ReadKey();
        }
    }
}
