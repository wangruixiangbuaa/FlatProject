using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Flat.Data.Entitys
{
    public class Enumerations
    {
        public enum Day
        {
            [Display(Name = "星期天")]
            Sunday,
            [Display(Name = "星期一")]
            Monday,
            [Display(Name = "星期二")]
            Tuesday,
            [Display(Name = "星期三")]
            Wednesday,
            [Display(Name = "星期四")]
            Thursday,
            [Display(Name = "星期五")]
            Friday,
            [Display(Name = "星期六")]
            Saturday
        }

        public enum PayRequestStatus
        {
            [EnumDescription("草稿")]
            draft,
            [EnumDescription("审核中")]
            audit,
            [EnumDescription("通过")]
            pass,
            [EnumDescription("完成")]
            complete,
            [EnumDescription("未通过")]
            reject,
            [EnumDescription("作废")]
            cancel,

        }

        public enum DataStatus
        {
            [Display(Name = "启用")]
            use,
            [Display(Name = "删除")]
            delete
        }

        public enum DormType
        {
            [EnumDescription("男生宿舍")]
            manUse,
            [EnumDescription("女生宿舍")]
            womanUse
        }
        public enum DormSize
        {
            [EnumDescription("4人间")]
            four,
            [EnumDescription("6人间")]
            six,
            [EnumDescription("8人间")]
            eight
        }

        public enum DormStatus
        {
            [EnumDescription("未使用")]
            none,
            [EnumDescription("部分使用")]
            part,
            [EnumDescription("全部使用")]
            use
        }
    }
}
