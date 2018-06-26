using System;
using System.Text;
using System.Linq.Expressions;

namespace CyPhone.DbUtility
{
    public static class LambdaToSql
    {
        public static string ToSql<T>(Expression<Func<T, bool>> func)
        {
            string result;
            if (func != null)
            {
                if (func.Body is BinaryExpression)
                {
                    BinaryExpression be = (BinaryExpression)func.Body;
                    result = BinarExpressionProvider(be.Left, be.Right, be.NodeType);
                }
                else if (func.Body is MethodCallExpression)
                {
                    MethodCallExpression be = ((MethodCallExpression)func.Body);
                    result = ExpressionRouter(be);
                }
                else
                {
                    result =string.Empty;
                }
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }

        static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
        {
            string sb = "(";
            //先处理左边
            sb += ExpressionRouter(left);

            sb += ExpressionTypeCast(type);

            //再处理右边
            string tmpStr = ExpressionRouter(right);
            if (tmpStr == "null")
            {
                if (sb.EndsWith(" ="))
                    sb = sb.Substring(0, sb.Length - 2) + " is null";
                else if (sb.EndsWith("<>"))
                    sb = sb.Substring(0, sb.Length - 2) + " is not null";
            }
            else
                sb += tmpStr;
            return sb + ")";

        }

        static string ExpressionRouter(Expression exp)
        {
            var expression = exp as BinaryExpression;
            if (exp is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            var memberExpression = exp as MemberExpression;
            if (memberExpression != null)
            {
                //MemberExpression me = ((MemberExpression)exp);
                //return me.Member.Name;
                if (!memberExpression.ToString().StartsWith("value("))
                {
                    MemberExpression me = memberExpression;
                    return me.Member.Name;
                }
                var result = Expression.Lambda(memberExpression).Compile().DynamicInvoke();
                if (result == null)
                    return "null";
                if (result is ValueType)
                    return result.ToString();
                if (result is string || result is DateTime || result is char)
                    return $"'{result}'";
            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionRouter(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression mce = (MethodCallExpression)exp;
                if (mce.Method.Name == "Like")
                    //return $"({ExpressionRouter(mce.Arguments[0])} like {ExpressionRouter(mce.Arguments[1])})";
                    return $"(charindex({ExpressionRouter(mce.Arguments[0])},{ExpressionRouter(mce.Object)},0)>0)";
                else if (mce.Method.Name == "Contains")
                    //return $"({ExpressionRouter(mce.Object)} like {ExpressionRouter(mce.Arguments[0])})";
                    return $"(charindex({ExpressionRouter(mce.Arguments[0])},{ExpressionRouter(mce.Object)},0)>0)";
                else if (mce.Method.Name == "NotLike")
                    return $"({ExpressionRouter(mce.Arguments[0])} Not like {ExpressionRouter(mce.Arguments[1])})";
                else if (mce.Method.Name == "In")
                    return $"{ExpressionRouter(mce.Arguments[0])} In ({ExpressionRouter(mce.Arguments[1])})";
                else if (mce.Method.Name == "NotIn")
                    return $"{ExpressionRouter(mce.Arguments[0])} Not In ({ExpressionRouter(mce.Arguments[1])})";
                else if (mce.Method.Name == "Equals")
                    return $"{ExpressionRouter(mce.Object)}={ExpressionRouter(mce.Arguments[0])}";

            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression ce = ((ConstantExpression)exp);
                if (ce.Value == null)
                    return "null";
                else if (ce.Value is ValueType)
                    return ce.Value.ToString();
                else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                    return $"'{ce.Value}'";
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);
                return ExpressionRouter(ue.Operand);
            }
            return null;
        }
        static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " =";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    return null;
            }
        }
    }
}
