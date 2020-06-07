> Project Date: 2018

Current repository contains two projects
+ LambdaExpressionBuilder
+ TestQueryFilter

# Project LambdaExpressionBuilder
> Project library to create expresions to filter data with Linq

# Project TestQueryFilter
> Project console as an example of how to use LambdaExpressionBuilder library

# Example of use

## Json Data

```json
[
  {
    "Id": "0",
    "Name": "Mathematics",
    "Teacher": {
      "Id": "0",
      "Name": "Daniel Maldonado"
    },
    "StudentList": [
      {
        "Id": "0",
        "Name": "David Maldonado"
      },
      {
        "Id": "1",
        "Name": "Renata Torres"
      },
      {
        "Id": "2",
        "Name": "Chiristian Zapata"
      }
    ]
  },
  {
    "Id": "1",
    "Name": "Language Arts",
    "Teacher": {
      "Id": "1",
      "Name": "Sofia Jimenez"
    },
    "StudentList": [
      {
        "Id": "0",
        "Name": "David Maldonado"
      },
      {
        "Id": "1",
        "Name": "Renata Torres"
      },
      {
        "Id": "3",
        "Name": "Dannah Pimbo"
      }
    ]
  },
  {
    "Id": "2",
    "Name": "Science",
    "Teacher": {
      "Id": "2",
      "Name": "Martha Mera"
    },
    "StudentList": [
      {
        "Id": "1",
        "Name": "Renata Torres"
      },
      {
        "Id": "3",
        "Name": "Dannah Pimbo"
      },
      {
        "Id": "4",
        "Name": "Sebastian Morales"
      }
    ]
  },
  {
    "Id": "3",
    "Name": "Health",
    "Teacher": {
      "Id": "3",
      "Name": "Ximena Sanchez"
    },
    "StudentList": [
      {
        "Id": "3",
        "Name": "Dannah Pimbo"
      },
      {
        "Id": "4",
        "Name": "Sebastian Morales"
      },
      {
        "Id": "5",
        "Name": "Robin Mena"
      }
    ]
  }
]
```

## Data Structure

+ Subject
  * Id
  * Name
  * Teacher
    - Id
    - Name
  * List < Student >
    - Student
      - Id
      - Name

## Main list to use to filter/order
    IEnumerable<Subject> foodList = new Seed().GetSubjectList();

## How to Filter 
Define LambdaExpressionBuilder and pass to it the root object

    LambdaExpressionBuilder<Subject> expressionBuilder = new LambdaExpressionBuilder<Subject>();

Define QueryFilter as list  

    IList<QueryFilter> queryFilterList = new List<QueryFilter>
    {
      new QueryFilter {
        PropertyPath = "Id",
        Operator = OperatorEnum.EqualTo,
        PropertyValue = "0"
        }
    };

Define one variable to hold the expression

    Expression<Func<Subject, bool>> filteringQuery = expressionBuilder.BuildQueryFilteringExpression(queryFilterList);

Apply filter

    List<Subject> result = foodList.Where(filteringQuery.Compile()).ToList();

## How to Order

Define LambdaExpressionBuilder and pass to it the root object

    LambdaExpressionBuilder<Subject> expressionBuilder = new LambdaExpressionBuilder<Subject>();

Define QuerySort as one Object

    QuerySort querySort = new QuerySort
    {
      PropertyPath = "Id",
      Desc = false
    };

Define one variable to hold the expression

    Expression<Func<Subject, object>> sortingQuery = expressionBuilder.BuildQuerySortingExpression(querySort);
    
Apply filter

    List<Subject> result = foodList.OrderBy(sortingQuery).ToList();
    
## Result
```json
[
  {
    "Id": "0",
    "Name": "Mathematics",
    "Teacher": {
      "Id": "0",
      "Name": "Daniel Maldonado"
    },
    "StudentList": [
      {
        "Id": "0",
        "Name": "David Maldonado"
      },
      {
        "Id": "1",
        "Name": "Renata Torres"
      },
      {
        "Id": "2",
        "Name": "Chiristian Zapata"
      }
    ]
  }
]
```
