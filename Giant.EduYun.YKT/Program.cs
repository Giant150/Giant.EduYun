using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Giant.EduYun.Models;

namespace Giant.EduYun.YKT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(0);
            Console.WriteLine("开始分析数据");
            var yktModel = await JsonSerializer.DeserializeAsync<YktModel>(File.OpenRead("ItemJsonData.json"));
            var yktCaseModel = await JsonSerializer.DeserializeAsync<YktCaseModel>(File.OpenRead("CaseListJsonData.json"));
            var yktCaseDic = yktCaseModel.clist.ToDictionary(k => k.caseCode, v => v.caseBeanList.First().caseName);
            var mainModel = new MainModel();
            mainModel.XueDuanList = yktModel.xueDuan.Select((xd, xdi) => new XueDuanM()
            {
                Name = $"{xdi + 1}.{xd.xueDuanName}",
                Code = xd.xueDuanCode,
                NianJiList = xd.nianJiList.Select((nj, nji) => new NianJiM()
                {
                    Name = $"{nji + 1}.{nj.njName}",
                    Code = nj.njCode,
                    XueKeList = nj.subjectsList.Select((xk, xki) => new XueKeM()
                    {
                        Name = $"{xki + 1}.{xk.xkName}",
                        Code = xk.xkCode,
                        DanYuanList = xk.danYuanList.Select((dy, dyi) => new DanYuanM()
                        {
                            Name = $"{dyi + 1}.{dy.danyuanName}",
                            Code = dy.danyuanCode,
                            KeChengList = dy.caseList.Select((kc, kci) => new KeChengM()
                            {
                                Name = $"{kci + 1}.{yktCaseDic[kc.caseCode]}",
                                Code = kc.caseCode
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList();
            var jsonOption = new JsonSerializerOptions() { WriteIndented = true };
            var mainJson = JsonSerializer.Serialize(mainModel, jsonOption);
            File.WriteAllText("MainData.json", mainJson, System.Text.Encoding.Unicode);
            Console.WriteLine("主数据分析完成，开始分析视频地址");

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://tongbu.eduyun.cn");
            //    var keChengs = mainModel.XueDuanList.SelectMany(xd => xd.NianJiList.SelectMany(nj => nj.XueKeList.SelectMany(xk => xk.DanYuanList.SelectMany(dy => dy.KeChengList))));
            //    foreach (var kc in keChengs)
            //    {
            //        Console.WriteLine($"开始分析{kc.Name}视频地址");
            //        var url = $"/tbkt/tbkthtml/wk/weike/{kc.Code.Substring(0, 6)}/{kc.Code}.html";
            //        var html = await client.GetStringAsync(url);
            //        var info = GetInfo(html);
            //        kc.VideoFile = info.video;
            //        kc.LiveTaskDoc = info.liveTaskDoc;
            //        kc.HomeWorkDoc = info.homeWorkDoc;
            //        Console.WriteLine(JsonSerializer.Serialize(kc, jsonOption));
            //    }
            //}

            //mainJson = JsonSerializer.Serialize(mainModel, jsonOption);
            //File.WriteAllText("MainData.json", mainJson, System.Text.Encoding.Unicode);

            Console.WriteLine("分析数据完成，按任意键结束");
            Console.ReadKey();
        }

        private static (string video, string liveTaskDoc, string homeWorkDoc) GetInfo(string html)
        {
            var lines = html.Split("\r\n");
            var video = lines.Where(w => w.Trim().StartsWith("file:")).SingleOrDefault();
            if (!String.IsNullOrEmpty(video))
                video = video.Trim().Replace("file: \"", "").Replace("\",", "");
            var liveTaskDoc = lines.Where(w => w.Contains("在线学习任务单")).SingleOrDefault();
            if (!String.IsNullOrEmpty(liveTaskDoc))
            {
                var start = liveTaskDoc.IndexOf("download=\"");
                var end = liveTaskDoc.IndexOf("\" href=\"");
                liveTaskDoc = liveTaskDoc.Substring(start + 10, end - start - 10);
            }
            var homeWorkDoc = lines.Where(w => w.Contains("课后练习")).SingleOrDefault();
            if (!String.IsNullOrEmpty(homeWorkDoc))
            {
                var start = homeWorkDoc.IndexOf("download=\"");
                var end = homeWorkDoc.IndexOf("\" href=\"");
                homeWorkDoc = homeWorkDoc.Substring(start + 10, end - start - 10);
            }
            return (video, liveTaskDoc, homeWorkDoc);
        }
    }

}
