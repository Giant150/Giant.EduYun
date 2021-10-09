using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giant.EduYun.Models
{
    public class MainModel
    {
        public List<XueDuanM> XueDuanList { get; set; }
    }
    /// <summary>
    /// 学段
    /// </summary>
    public class XueDuanM : BaseModel
    {
        public List<NianJiM> NianJiList { get; set; }
    }
    /// <summary>
    /// 年级
    /// </summary>
    public class NianJiM : BaseModel
    {
        public List<XueKeM> XueKeList { get; set; }
    }
    /// <summary>
    /// 学科
    /// </summary>
    public class XueKeM : BaseModel
    {
        public List<DanYuanM> DanYuanList { get; set; }
    }
    /// <summary>
    /// 单元
    /// </summary>
    public class DanYuanM : BaseModel
    {
        public List<KeChengM> KeChengList { get; set; }
    }

    /// <summary>
    /// 课程
    /// </summary>
    public class KeChengM : BaseModel
    {
        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoFile { get; set; }
        /// <summary>
        /// 在线学习任务
        /// </summary>
        public string LiveTaskDoc { get; set; }
        /// <summary>
        /// 课后练习
        /// </summary>
        public string HomeWorkDoc { get; set; }
    }
}
