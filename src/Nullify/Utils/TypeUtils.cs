using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Nullify.Utils
{
    public static class TypeUtils
    {
        /// <summary>
        /// Get MemberInfo(PropertyInfo/Method Info) based on lambda expression.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static MemberInfo GetMemberInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> lambda)
        {
            switch(lambda.Body.NodeType)
            {
                case ExpressionType.Call:
                    return GetMethodInfo(lambda);
                case ExpressionType.MemberAccess:
                    return GetPropertyInfo(lambda);
                default:
                    return null;
            }
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> lambda)
        {
            var member = lambda.Body as MemberExpression;
            if (member != null)
            {
                var propInfo = member.Member as PropertyInfo;
                if (propInfo == null)
                    throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", lambda.ToString()));

                return propInfo;
            }

            return null;
        }

        private static MethodInfo GetMethodInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> lambda)
        {
            var method = lambda.Body as MethodCallExpression;
            if (method != null)
            {
                var methodInfo = method.Method;
                return methodInfo;
            }

            return null;
        }
    }
}
