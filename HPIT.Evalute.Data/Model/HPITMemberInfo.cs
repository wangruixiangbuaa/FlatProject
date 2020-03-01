using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Evalute.Data.Model
{
    public class HPITMemberInfo
    {
        public string RealName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string oFullName { get; set; }

        public string Manager { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 学生学号
        /// </summary>
        public string StuNo { get; set; }

        /// <summary>
        /// 学生学习的周期  几个月
        /// </summary>
        public int? Period { get; set; }

        /// <summary>
        /// 项目部
        /// </summary>
        public string ProjectName { get; set; }
    }
}
