using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giant.EduYun.YKT
{


    public class YktModel
    {
        public Xueduan[] xueDuan { get; set; }
    }

    public class Xueduan
    {
        public Nianjilist[] nianJiList { get; set; }
        public string xueDuanCode { get; set; }
        public string xueDuanName { get; set; }
    }

    public class Nianjilist
    {
        public string njCode { get; set; }
        public string njName { get; set; }
        public Subjectslist[] subjectsList { get; set; }
    }

    public class Subjectslist
    {
        public Danyuanlist[] danYuanList { get; set; }
        public string xkCode { get; set; }
        public string xkName { get; set; }
    }

    public class Danyuanlist
    {
        public Caselist[] caseList { get; set; }
        public string danYuanText { get; set; }
        public string danyuanCode { get; set; }
        public string danyuanName { get; set; }
    }

    public class Caselist
    {
        public string caseCode { get; set; }
    }



    public class YktCaseModel
    {
        public Clist[] clist { get; set; }
    }

    public class Clist
    {
        public Casebeanlist[] caseBeanList { get; set; }
        public string caseCode { get; set; }
    }

    public class Casebeanlist
    {
        public string caseName { get; set; }
        public string picUrl { get; set; }
        public string teacher { get; set; }
        public string guildTeache { get; set; }
    }

}
