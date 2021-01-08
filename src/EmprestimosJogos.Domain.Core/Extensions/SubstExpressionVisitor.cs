using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmprestimosJogos.Domain.Core.Extensions
{
    internal class SubstExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return subst.TryGetValue(node, out Expression newValue) ? newValue : node;
        }
    }
}