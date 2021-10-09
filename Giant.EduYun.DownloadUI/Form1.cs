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
            this.MainData = JsonSerializer.Deserialize<MainModel>(File.ReadAllText("MainData.json"));
            this.cbXueDuan.Items.AddRange(this.MainData.XueDuanList.ToArray());
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

        private void btnStart_Click(object sender, EventArgs e)
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

            var client = new WebClient();
            foreach (var kc in dy.KeChengList)
            {
                var para = new string[] {
                    Uri.EscapeUriString(kc.VideoFile),
                    $"--workDir \"{path}\"",
                    $"--saveName \"{kc.Name}\"",
                    "--enableDelAfterDone",
                    "--disableDateInfo"
                };

                Process.Start(new ProcessStartInfo()
                {
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "N_m3u8DL-CLI_v2.9.7.exe"),
                    WorkingDirectory = path,
                    Arguments = String.Join(" ", para),
                    CreateNoWindow = false
                }).WaitForExit();

                if (!String.IsNullOrEmpty(kc.LiveTaskDoc))
                    client.DownloadFile(kc.LiveTaskDoc, Path.Combine(path, $"{kc.Name}(在线学习任务单).docx"));
                if (!String.IsNullOrEmpty(kc.HomeWorkDoc))
                    client.DownloadFile(kc.HomeWorkDoc, Path.Combine(path, $"{kc.Name}(课后练习).docx"));
            }
        }
    }
}
