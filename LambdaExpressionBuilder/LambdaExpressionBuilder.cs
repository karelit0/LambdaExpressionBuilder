
using LambdaExpressionBuilder.Enums;
using LambdaExpressionBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdaExpressionBuilder
{
    /// <summary>
    /// Defines the <see cref="LambdaExpressionBuilder{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LambdaExpressionBuilder<TEntity>
    {
        #region Methods

        /// <summary>
        /// The BuildQueryFilteringExpression
        /// </summary>
        /// <param name="filterList">The filterList<see cref="IList{QueryFilter}"/></param>
        /// <returns>The <see cref="Expression{Func{TEntity, bool}}"/></returns>
        public Expression<Func<TEntity, bool>> BuildQueryFilteringExpression(IList<QueryFilter> filterList)
        {
            if (filterList == null)
            {
                return null;
            }

            if (filterList.Count == 0)
            {
                return null;
            }

            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "lambdaParameter");
            Expression expression = BuildExpression(filterList, null, parameterExpression);

            return Expression.Lambda<Func<TEntity, bool>>(expression, parameterExpression);
        }

        /// <summary>
        /// Get sort expression from <seealso cref="PageParameters.SortBy"/>
        /// </summary>
        /// <param name="querySort">The querySort<see cref="QuerySort"/></param>
        /// <returns></returns>
        public Expression<Func<TEntity, object>> BuildQuerySortingExpression(QuerySort querySort)
        {
            if (querySort == null)
            {
                return null;
            }

            string propertyPath = GetPropertyPath(typeof(TEntity), querySort.PropertyPath.Split('.'), string.Empty);

            ParameterExpression param = Expression.Parameter(typeof(TEntity), "lambdaParameter");
            Expression body = propertyPath.Split('.').Aggregate((Expression)param, Expression.PropertyOrField);
            Expression<Func<TEntity, object>> expression = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(body, typeof(object)), param);

            return expression;
        }

        /// <summary>
        /// The ToString
        /// </summary>
        /// <param name="expression">The expression<see cref="Expression{Func{TEntity, bool}}"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string ToString(Expression expression)
        {
            string expressionString = expression.ToString();

            return expressionString;
        }

        /// <summary>
        /// The BuildExpression
        /// </summary>
        /// <param name="filterList">The filterList<see cref="IList{QueryFilter}"/></param>
        /// <param name="expression">The expression<see cref="Expression"/></param>
        /// <param name="parameterExpression">The parameterExpression<see cref="ParameterExpression"/></param>
        /// <returns>The <see cref="Expression"/></returns>
        private Expression BuildExpression(IList<QueryFilter> filterList, Expression expression, ParameterExpression parameterExpression)
        {
            if (expression == null)
            {
                expression = GetExpression(parameterExpression, filterList[0]);
                filterList.RemoveAt(0);

                return BuildExpression(filterList, expression, parameterExpression);
            }

            if (filterList.Count == 0)
            {
                return expression;
            }

            expression = Expression.AndAlso(expression, GetExpression(parameterExpression, filterList[0]));

            filterList.RemoveAt(0);

            return BuildExpression(filterList, expression, parameterExpression);
        }

        /// <summary>
        /// The GetConstant
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="propertyValue">The propertyValue<see cref="string"/></param>
        /// <returns>The <see cref="ConstantExpression"/></returns>
        private ConstantExpression GetConstant(Type type, string propertyValue)
        {
            if (type == typeof(string))
            {
                return Expression.Constant(propertyValue);
            }

            if (type == typeof(bool))
            {
                if (bool.TryParse(propertyValue, out bool flag))
                {
                    flag = true;
                }

                return Expression.Constant(flag);
            }

            if (type == typeof(int))
            {
                int.TryParse(propertyValue, out int result);
                return Expression.Constant(result);
            }

            if (type == typeof(double))
            {
                double.TryParse(propertyValue, out double result);
                return Expression.Constant(result);
            }


            if (type == typeof(decimal))
            {
                decimal.TryParse(propertyValue, out decimal result);
                return Expression.Constant(result);
            }

            if (type == typeof(DateTime))
            {
                DateTime.TryParse(propertyValue, out DateTime result);
                return Expression.Constant(result);
            }

            return null;
        }

        /// <summary>
        /// The GetExpression
        /// </summary>
        /// <param name="parameterExpression">The parameterExpression<see cref="ParameterExpression"/></param>
        /// <param name="queryFilter">The queryFilter<see cref="QueryFilter"/></param>
        /// <returns>The <see cref="Expression"/></returns>
        private Expression GetExpression(ParameterExpression parameterExpression, QueryFilter queryFilter)
        {
            string propertyPath = GetPropertyPath(typeof(TEntity), queryFilter.PropertyPath.Split('.'), string.Empty);

            Expression memberExpression = propertyPath.Split('.').Aggregate((Expression)parameterExpression, Expression.PropertyOrField);

            //MemberExpression memberExpression = MemberExpression.Property(parameterExpression, queryFilter.PropertyPath);

            ConstantExpression constantExpression = GetConstant(memberExpression.Type, queryFilter.PropertyValue);

            switch (queryFilter.Operator)
            {
                case OperatorEnum.EqualTo:
                    {
                        return Expression.Equal(memberExpression, constantExpression);
                    }
                case OperatorEnum.Contains:
                    {
                        MethodInfo containsMethod = memberExpression.Type.GetMethod("Contains", new[] { memberExpression.Type });
                        return Expression.Call(memberExpression, containsMethod, constantExpression);
                    }
                case OperatorEnum.GreaterThan:
                    {
                        return Expression.GreaterThan(memberExpression, constantExpression);
                    }
                case OperatorEnum.GreaterThanOrEqualTo:
                    {
                        return Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                    }
                case OperatorEnum.LessThan:
                    {
                        return Expression.LessThan(memberExpression, constantExpression);
                    }
                case OperatorEnum.LessThanOrEqualTo:
                    {
                        return Expression.LessThanOrEqual(memberExpression, constantExpression);
                    }
                case OperatorEnum.StartsWith:
                    {
                        MethodInfo startsWithMethod = memberExpression.Type.GetMethod("StartsWith", new[] { memberExpression.Type });
                        return Expression.Call(memberExpression, startsWithMethod, constantExpression);
                    }
                case OperatorEnum.EndsWith:
                    {
                        MethodInfo endsWithMethod = memberExpression.Type.GetMethod("EndsWith", new[] { memberExpression.Type });
                        return Expression.Call(memberExpression, endsWithMethod, constantExpression);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        /// <summary>
        /// Get property path from <typeparamref name="TEntity"/> using <paramref name="propertyPath"/>
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="propertyPath"></param>
        /// <param name="nestPropertyPath"></param>
        /// <returns></returns>
        private string GetPropertyPath(Type objectType, IList<string> propertyPath, string nestPropertyPath)
        {
            if (propertyPath == null)
            {
                return string.Empty;
            }

            if (propertyPath.Count == 0)
            {
                return nestPropertyPath;
            }

            PropertyInfo[] propertyList = objectType.GetProperties();

            for (int i = 0; i < propertyList.Length; i++)
            {
                // Compare string using case-insensitve
                if (!propertyList[i].Name.Equals(propertyPath[0], StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(nestPropertyPath))
                {
                    nestPropertyPath = propertyList[i].Name;
                }
                else
                {
                    nestPropertyPath += "." + propertyList[i].Name;
                }

                return GetPropertyPath(propertyList[i].PropertyType, propertyPath.TakeLast(propertyPath.Count - 1).ToList(), nestPropertyPath);
            }

            return string.Empty;
        }

        #endregion
    }
}
