using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Data.Core
{
    public class GetPageListParameter<T, Tkey>
    {
        public Expression<Func<T, T>> selectLambda { get; set; }

        public Expression<Func<T, bool>> whereLambda { get; set; }

        public Expression<Func<T,Tkey>> orderByLambda { get; set; }

        public int pageSize { get; set; }

        public int pageIndex { get; set; }

        public bool isAsc { get; set; }
    }
    public class DBBaseService  //泛型约束  T就只能传递BaseEntity类型或者BaseEntity的子类
    {
        private DbContext db;
        public DBBaseService(DbContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> GetSimplePagedData<T,Tkey>(GetPageListParameter<T,Tkey> parameter,out int count) where T:class
        {
            count = db.Set<T>().Where<T>(parameter.whereLambda).Count();
            var list = db.Set<T>().Where<T>(parameter.whereLambda);
            if (parameter.isAsc)
            {
                list = list.OrderBy(parameter.orderByLambda);
            }
            else
            {
                list = list.OrderByDescending(parameter.orderByLambda);
            }
            if (parameter.pageIndex <= 0)
            {
                parameter.pageIndex = 1;
            }
            if(parameter.pageIndex >=count)
            {
                parameter.pageIndex = count;
            }
            parameter.pageIndex = parameter.pageIndex == 0 ? 1 : parameter.pageIndex;
            //.AsNoTracking()  去除缓存读取数据。
            return list.AsNoTracking().Skip((parameter.pageIndex - 1) * parameter.pageSize).Take(parameter.pageSize).ToList();
        }

        /// <summary>
        /// 支持sql 语句查询返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> GetSqlPagedData<T, Tkey>(string sql,GetPageListParameter<T, Tkey> parameter, out int count) where T : class
        {
            count = db.Database.SqlQuery<T>(sql).Count();
            DbRawSqlQuery<T> list = db.Database.SqlQuery<T>(sql);
            
            if (parameter.pageIndex <= 0)
            {
                parameter.pageIndex = 1;
            }
            if (parameter.pageIndex >= count)
            {
                parameter.pageIndex = count;
            }
            return list.Skip((parameter.pageIndex - 1) * parameter.pageSize).Take(parameter.pageSize).ToList();
        }


        public List<T> GetSqlPagedData<T,TEntity,Tkey>(string sql, GetPageListParameter<TEntity, Tkey> parameter, out int count) where T : class
        {
            count = db.Database.SqlQuery<T>(sql).Count();
            DbRawSqlQuery<T> list = db.Database.SqlQuery<T>(sql);

            if (parameter.pageIndex <= 0)
            {
                parameter.pageIndex = 1;
            }
            if (parameter.pageIndex >= count)
            {
                parameter.pageIndex = count;
            }
            return list.Skip((parameter.pageIndex - 1) * parameter.pageSize).Take(parameter.pageSize).ToList();
        }
    }
}
