using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using Giant.EduYun.Models;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace Giant.EduYun.DownloadUI
{
    public partial class Form1 : Form
    {
        public MainModel MainData { get; set; }
        public string RootFolder { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.txtBaseDir.Text = this.RootFolder;
            //this.MainData = JsonSerializer.Deserialize<MainModel>(File.ReadAllText("MainData.json"));
            //this.cbXueDuan.Items.AddRange(this.MainData.XueDuanList.ToArray());

            Task.Factory.StartNew(this.LoadData).ContinueWith(async (result) =>
            {
                var data = await result.Result;
                var mi = new MethodInvoker(() =>
                {
                    this.MainData = data;
                    this.cbXueDuan.Items.AddRange(this.MainData.XueDuanList.ToArray());
                });
                this.BeginInvoke(mi);
            });

        }

        public async Task<MainModel> LoadData()
        {
            var url = "https://tongbu.eduyun.cn/tbkt/tbkthtml/ItemJsonData.js";
            using (var httpClient = new HttpClient())
            {
                var js = await httpClient.GetStringAsync(url);
                var index = js.IndexOf('{');
                var json = js.Substring(index, js.Length - index - 1);
                var yktModel = JsonSerializer.Deserialize<YktModel>(json);
                var mainData = new MainModel();
                mainData.XueDuanList = yktModel.xueDuan.Select((xd, xdi) => new XueDuanM()
                {
                    Name = $"{xd.xueDuanName}",
                    Code = xd.xueDuanCode,
                    NianJiList = xd.nianJiList.Select((nj, nji) => new NianJiM()
                    {
                        Name = $"{nj.njName}",
                        Code = nj.njCode,
                        XueKeList = nj.subjectsList.Select((xk, xki) => new XueKeM()
                        {
                            Name = $"{xk.xkName}",
                            Code = xk.xkCode,
                            DanYuanList = xk.danYuanList.Select((dy, dyi) => new DanYuanM()
                            {
                                Name = $"{dyi + 1}.{dy.danyuanName}",
                                Code = dy.danyuanCode,
                                KeChengList = dy.caseList.Select((kc, kci) => new KeChengM()
                                {
                                    Code = kc.caseCode
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();
                return mainData;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.BaseDirDialog.RootFolder = Environment.SpecialFolder.Desktop;
            this.BaseDirDialog.SelectedPath = String.IsNullOrWhiteSpace(this.txtBaseDir.Text) ? this.RootFolder : this.txtBaseDir.Text;
            if (this.BaseDirDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtBaseDir.Text = this.BaseDirDialog.SelectedPath;
            }
        }

        private void cbXueDuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xdCode = this.cbXueDuan.SelectedItem == null ? "" : (this.cbXueDuan.SelectedItem as XueDuanM).Code;
            if (String.IsNullOrEmpty(xdCode)) return;
            var xd = this.MainData.XueDuanList.SingleOrDefault(w => w.Code == xdCode);
            if (xd == null) return;
            this.cbNainJi.Items.Clear();
            this.cbNainJi.Text = "";
            this.cbNainJi.Items.AddRange(xd.NianJiList.ToArray());

            this.cbXueKe.Items.Clear();
            this.cbXueKe.Text = "";

            this.cbDanYuan.Items.Clear();
            this.cbDanYuan.Text = "";
        }

        private void cbNainJi_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xdCode = this.cbXueDuan.SelectedItem == null ? "" : (this.cbXueDuan.SelectedItem as XueDuanM).Code;
            if (String.IsNullOrEmpty(xdCode)) return;
            var xd = this.MainData.XueDuanList.SingleOrDefault(w => w.Code == xdCode);
            if (xd == null) return;

            var njCode = this.cbNainJi.SelectedItem == null ? "" : (this.cbNainJi.SelectedItem as NianJiM).Code;
            if (String.IsNullOrEmpty(njCode)) return;
            var nj = xd.NianJiList.SingleOrDefault(w => w.Code == njCode);
            if (nj == null) return;

            this.cbXueKe.Items.Clear();
            this.cbXueKe.Text = "";
            this.cbXueKe.Items.AddRange(nj.XueKeList.ToArray());

            this.cbDanYuan.Items.Clear();
            this.cbDanYuan.Text = "";
        }

        private void cbXueKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xdCode = this.cbXueDuan.SelectedItem == null ? "" : (this.cbXueDuan.SelectedItem as XueDuanM).Code;
            if (String.IsNullOrEmpty(xdCode)) return;
            var xd = this.MainData.XueDuanList.SingleOrDefault(w => w.Code == xdCode);
            if (xd == null) return;

            var njCode = this.cbNainJi.SelectedItem == null ? "" : (this.cbNainJi.SelectedItem as NianJiM).Code;
            if (String.IsNullOrEmpty(njCode)) return;
            var nj = xd.NianJiList.SingleOrDefault(w => w.Code == njCode);
            if (nj == null) return;

            var xkCode = this.cbXueKe.SelectedItem == null ? "" : (this.cbXueKe.SelectedItem as XueKeM).Code;
            if (String.IsNullOrEmpty(xkCode)) return;
            var xk = nj.XueKeList.SingleOrDefault(w => w.Code == xkCode);
            if (xk == null) return;

            this.cbDanYuan.Items.Clear();
            this.cbDanYuan.Text = "";
            this.cbDanYuan.Items.AddRange(xk.DanYuanList.ToArray());
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            var xdCode = this.cbXueDuan.SelectedItem == null ? "" : (this.cbXueDuan.SelectedItem as XueDuanM).Code;
            if (String.IsNullOrEmpty(xdCode)) return;
            var xd = this.MainData.XueDuanList.SingleOrDefault(w => w.Code == xdCode);
            if (xd == null) return;

            var njCode = this.cbNainJi.SelectedItem == null ? "" : (this.cbNainJi.SelectedItem as NianJiM).Code;
            if (String.IsNullOrEmpty(njCode)) return;
            var nj = xd.NianJiList.SingleOrDefault(w => w.Code == njCode);
            if (nj == null) return;

            var xkCode = this.cbXueKe.SelectedItem == null ? "" : (this.cbXueKe.SelectedItem as XueKeM).Code;
            if (String.IsNullOrEmpty(xkCode)) return;
            var xk = nj.XueKeList.SingleOrDefault(w => w.Code == xkCode);
            if (xk == null) return;

            var dyCode = this.cbDanYuan.SelectedItem == null ? "" : (this.cbDanYuan.SelectedItem as DanYuanM).Code;
            if (String.IsNullOrEmpty(dyCode)) return;
            var dy = xk.DanYuanList.SingleOrDefault(w => w.Code == dyCode);
            if (dy == null) return;

            var path = Path.Combine(this.txtBaseDir.Text, xd.Name, nj.Name, xk.Name, dy.Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var jsonOption = new JsonSerializerOptions() { WriteIndented = true };
            var httpClient = new HttpClient();
            var kcIndex = 1;
            foreach (var kc in dy.KeChengList)
            {
                Console.WriteLine($"开始分析{kc.Code}视频地址");
                var htmlUrl = $"https://tongbu.eduyun.cn/tbkt/tbkthtml/wk/weike/{kc.Code.Substring(0, 6)}/{kc.Code}.html";
                var html = await httpClient.GetStringAsync(htmlUrl);
                var info = GetInfo(html);
                kc.Name = $"{kcIndex}.{info.title}";
                kc.VideoFile = info.video;
                kc.LiveTaskDoc = info.liveTaskDoc;
                kc.HomeWorkDoc = info.homeWorkDoc;
                Console.WriteLine(JsonSerializer.Serialize(kc, jsonOption));

                var para = new string[] {
                    Uri.EscapeUriString(kc.VideoFile),
                    $"--workDir \"{path}\"",
                    $"--saveName \"{kc.Name}\"",
                    "--enableDelAfterDone",
                    "--disableDateInfo"
                };

                if (!String.IsNullOrEmpty(kc.LiveTaskDoc))
                {
                    var url = new Uri(kc.LiveTaskDoc);
                    var name = url.LocalPath.Substring(url.LocalPath.LastIndexOf("/") + 1);
                    var result = await httpClient.GetAsync(kc.LiveTaskDoc);
                    using (var fs = new FileStream($"{path}/{name}", FileMode.OpenOrCreate))
                    {
                        await result.Content.CopyToAsync(fs);
                    }
                }
                if (!String.IsNullOrEmpty(kc.HomeWorkDoc))
                {
                    var url = new Uri(kc.HomeWorkDoc);
                    var name = url.LocalPath.Substring(url.LocalPath.LastIndexOf("/") + 1);
                    var result = await httpClient.GetAsync(kc.HomeWorkDoc);
                    using (var fs = new FileStream($"{path}/{name}", FileMode.OpenOrCreate))
                    {
                        await result.Content.CopyToAsync(fs);
                    }
                }

                await Process.Start(new ProcessStartInfo()
                {
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "N_m3u8DL-CLI_v2.9.7.exe"),
                    WorkingDirectory = path,
                    Arguments = String.Join(" ", para),
                    CreateNoWindow = false
                }).WaitForExitAsync();

                kcIndex++;
            }
        }

        private (string title, string video, string liveTaskDoc, string homeWorkDoc) GetInfo(string html)
        {
            var lines = html.Split("\r\n");

            var title = lines.Where(w => w.Trim().StartsWith("<h3")).SingleOrDefault();
            if (!String.IsNullOrEmpty(title))
            {
                var start = title.IndexOf(">");
                var end = title.IndexOf('<', start);
                title = title.Substring(start + 1, end - start - 1);
            }

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
            return (title, video, liveTaskDoc, homeWorkDoc);
        }
    }
}
