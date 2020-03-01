using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Data.Core
{
    /// <summary>
    /// dapper 
    /// </summary>
    public class DapperDBHelper
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["SqlDBAddress"].ConnectionString;
        public static DapperDBHelper Instance = new DapperDBHelper();

        public DapperDBHelper() {
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ExcuteInsert<T>(string sql, T model)
        {
            IDbConnection connection = new SqlConnection(connStr);
            return connection.Execute(sql,model);
        }

        public int ExcuteUpdate<T>(string sql, T model)
        {
            IDbConnection connection = new SqlConnection(connStr);
            return connection.Execute(sql, model);
        }

        /// <summary>
        /// 根据查询条件查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<T> ExcuteQuery<T>(string sql, T model)
        {
            IDbConnection connection = new SqlConnection(connStr);
            return connection.Query<T>(sql, model).ToList();
        }

        /// <summary>
        /// 根据查询条件查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ExcuteScalarQuery<T>(string sql, T model)
        {
            IDbConnection connection = new SqlConnection(connStr);
            return connection.ExecuteScalar<int>(sql, model);
        }

        /// <summary>
        /// 根据不同的查询条件，查询不同的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<TResult> ExcuteQuery<T,TResult>(string sql, T model)
        {
            IDbConnection connection = new SqlConnection(connStr);
            return connection.Query<TResult>(sql, model).ToList();
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public int ExcuteBulkInsert<T>(string sql, List<T> models)
        {
            IDbConnection connection = new SqlConnection(connStr);
            return connection.Execute(sql, models);
        }

    }
}
