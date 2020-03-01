using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Data.Core.Tool
{
    public class ExpressionVistorMy : ExpressionVisitor
    {
        private ParameterExpression _Parameter
        {
            get;
            set;
        }

        public ExpressionVistorMy(ParameterExpression ParameterT)
        {
            _Parameter = ParameterT;
        }

        public System.Linq.Expressions.Expression Modify(System.Linq.Expressions.Expression exp)
        {
            Expression e = this.Visit(exp);//这个Visit会根据VisitParameter返回的Expression修改这里的exp变量
            return e;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            //不管传入的是什么参数，都会返回我的参数
            return _Parameter;
        }
    }
}
