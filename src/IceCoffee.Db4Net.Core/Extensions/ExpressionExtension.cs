using System.Linq.Expressions;

namespace IceCoffee.Db4Net.Core.Extensions
{
    internal static class ExpressionExtension
    {
        public static string GetPropertyName(this LambdaExpression expression)
        {
            if (expression.Body is MemberExpression memberExpr)
            {
                return memberExpr.Member.Name;
            }
            else if (expression.Body is UnaryExpression unaryExpr && unaryExpr.Operand is MemberExpression memberOperand)
            {
                return memberOperand.Member.Name;
            }
            else
            {
                throw new InvalidOperationException("The expression must be a member expression or a unary expression with a member operand.");
            }
        }
    }
}
