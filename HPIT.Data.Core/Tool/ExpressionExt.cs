using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Data.Core.Tool
{
    public class ExpressionExt
    {
        public static Expression<Func<T, bool>> ReBuildExpression<T>(Expression<Func<T, bool>> lambd0, Expression<Func<T, bool>> lambd1)
        {

            ParameterExpression parameter = Expression.Parameter(typeof(T), "item");//这里第二个参数可以是任意字符值
            ExpressionVistorMy visitor = new ExpressionVistorMy(parameter);
            Expression left = visitor.Modify(lambd0.Body);//left = {(item.Length > 2)}
            Expression right = visitor.Modify(lambd1.Body);//right = {(item.Length < 4)}
            BinaryExpression expression = Expression.AndAlso(left, right);//expression = {((item.Length > 2) AndAlso (item.Length < 4))}
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(expression, parameter);//lambda = {item => ((item.Length > 2) AndAlso (item.Length < 4))}
            //return lambda.Compile();
            return lambda;
        }
    }
}
