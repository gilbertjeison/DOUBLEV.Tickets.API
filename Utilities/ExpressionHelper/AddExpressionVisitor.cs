using System.Linq.Expressions;

namespace Utilities.ExpressionHelper
{
    public class AddExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression from;

        private readonly Expression to;

        public AddExpressionVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }

        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}
