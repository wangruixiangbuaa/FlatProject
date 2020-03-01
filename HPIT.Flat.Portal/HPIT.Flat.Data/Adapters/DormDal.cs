using HPIT.Data.Core;
using HPIT.Flat.Data.Entitys;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HPIT.Flat.Data.Entitys.Enumerations;

namespace HPIT.Flat.Data.Adapters
{
    public class DormDal
    {
        public static DormDal Instance = new DormDal();

        public FlatContext context { get; set; }
        public DormDal()
        {
            this.context = new FlatContext();
        }

        public List<Dorm> GetPageData(SearchModel<Dorm> search, out int count)
        {
            GetPageListParameter<Dorm, string> parameter = new GetPageListParameter<Dorm, string>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.DormNo;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.Status > -1;
            //查询数据
            DBBaseService baseService = new DBBaseService(FlatContext.Instance);
            List<Dorm> list = baseService.GetSimplePagedData<Dorm, string>(parameter, out count);
            foreach (var dorm in list)
            {
                dorm.SizeStr = EnumDescriptionAttribute.GetDescription((DormSize)dorm.DormSize);
                dorm.StatusStr = EnumDescriptionAttribute.GetDescription((DormStatus)dorm.Status);
                dorm.TypeStr = EnumDescriptionAttribute.GetDescription((DormType)dorm.DormType);
                dorm.DormJsonStr = JsonConvert.SerializeObject(dorm);
            }
            return list;
        }

        public int AddDorm(Dorm dorm)
        {
            dorm.Status = (int)DormStatus.none;
            dorm.DID = Guid.NewGuid().ToString();
            context.Dorm.Add(dorm);
            return context.SaveChanges();
        }

        /// <summary>
        /// 根据公寓编号
        /// </summary>
        /// <param name="dormNo"></param>
        /// <returns></returns>
        public Dorm GetDormByNo(string dormNo)
        {
            var dorm = context.Dorm.FirstOrDefault(r => r.DormNo == dormNo);
            return dorm;
        }

        public int UpdateDorm(Dorm dorm)
        {
            int result = 0;
            Dorm match = context.Dorm.FirstOrDefault(r => r.DID == dorm.DID);
            if (match != null)
            {
                match.DormNo = dorm.DormNo;
                match.DormSize = dorm.DormSize;
                match.DormType = dorm.DormType;
                match.Phone = dorm.Phone;
                match.Remark = dorm.Remark;
                match.DormName = dorm.DormName;
                context.Entry(match).State = System.Data.Entity.EntityState.Modified;
                result = context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 添加公寓分配
        /// </summary>
        /// <param name="assigns"></param>
        /// <returns></returns>
        public int DormAssign(List<DormAssign> assigns)
        {
            foreach (var item in assigns)
            {
                item.AID = Guid.NewGuid().ToString();
                item.CreateTime = DateTime.Now;
                //为什么 有外键的要查出来数据 才能在外表里面进行插入。
                //item.Dorm = context.Dorm.Where(r => r.DID == item.DID).FirstOrDefault();
            }
            //批量添加宿舍分配记录
            context.DormAssign.AddRange(assigns);
            return context.SaveChanges();
        }
    }
}
