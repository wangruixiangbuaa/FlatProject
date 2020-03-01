using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPIT.Data.Core
{
    public class SearchModel<T> : PageModel
    {

        public SearchModel()
        {

        }
        /// <summary>
        /// 初始化分页数据模型
        /// </summary>
        /// <param name="total"></param>
        /// <param name="size"></param>
        public void Init(int total, int size)
        {
            this.TotalCount = total;
            this.PageSize = size;
            this.PageCount = this.TotalCount % this.PageSize == 0 ? this.TotalCount / this.PageSize : this.TotalCount / this.PageSize + 1;
            for (int i = 1; i < this.PageCount; i++)
            {
                int show = i;

                this.Pages.Add(new PageIndexModel() { Index = i, ShowIndex = show, CurrentIndex = this.CurrentPageIndex });
            }
        }
        public string Number { get; set; }

        public string TeamName { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public List<T> Datas { get; set; }

        public T Entity { get; set; }

        public List<PageIndexModel> Pages { get; set; } = new List<PageIndexModel>();

        public IDictionary<string, object> ExtraDatas { get; set; } = new Dictionary<string, object>();

    }
}