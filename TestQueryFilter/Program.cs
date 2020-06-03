using LambdaExpressionBuilder;
using LambdaExpressionBuilder.Enums;
using LambdaExpressionBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestQueryFilter.Data;
using TestQueryFilter.Models;

namespace TestQueryFilter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LambdaExpressionBuilder<Subject> expressionBuilder = new LambdaExpressionBuilder<Subject>();

            IEnumerable<Subject> foodList = new Seed().GetSubjectList();

            IList<QueryFilter> queryFilterList = new List<QueryFilter>
            {
                new QueryFilter {
                    PropertyPath = "Id",
                    Operator = OperatorEnum.EqualTo,
                    PropertyValue = "0"
                }
            };

            QuerySort querySort = new QuerySort
            {
                PropertyPath = "Id",
                Desc = false
            };

            Expression<Func<Subject, bool>> filteringQuery = expressionBuilder.BuildQueryFilteringExpression(queryFilterList);
            Expression<Func<Subject, object>> sortingQuery = expressionBuilder.BuildQuerySortingExpression(querySort);

            Console.WriteLine("------------------");
            Console.WriteLine(expressionBuilder.ToString(filteringQuery));
            Console.WriteLine(expressionBuilder.ToString(sortingQuery));
            Console.WriteLine("------------------");

            List<Subject> result = foodList.Where(filteringQuery.Compile())
                .AsQueryable()
                .OrderBy(sortingQuery)
                .ToList();

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].ToString());
            }

            Console.WriteLine("------------------");
            Console.WriteLine(expressionBuilder.ToString(filteringQuery));
            Console.WriteLine(expressionBuilder.ToString(sortingQuery));
            Console.WriteLine("------------------");

            queryFilterList = new List<QueryFilter>
            {
                new QueryFilter { PropertyPath = "Teacher.Name", Operator = OperatorEnum.Contains, PropertyValue = "a" },
            };

            querySort = new QuerySort
            {
                PropertyPath = "teacher.name",
                Desc = false
            };

            filteringQuery = expressionBuilder.BuildQueryFilteringExpression(queryFilterList);
            sortingQuery = expressionBuilder.BuildQuerySortingExpression(querySort);

            result = foodList.Where(filteringQuery.Compile())
               .AsQueryable()
               .OrderBy(sortingQuery)
               .ToList();

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].ToString());
            }

            Console.ReadLine();
        }
    }
}
