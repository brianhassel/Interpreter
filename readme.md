﻿Interpreter
===========

Overview
--------
The Interpreter is an expression interpreter written in pure C#. It parses any mathematical or logical expression and returns a result. The return result depends on the return type of the last function / operation. An expression can contain variables that can be supplied before the expression is executed and the result returned. 

### Examples:
1. Passing a variable to the expression. The example below parses the express - and creates the expression tree via Engine.Parse(). The variables are then supplied to the expression and the expression executed via Execute().
```csharp
var expression = Engine.Parse("SUM(A) * 2 - B");
expression.Variables["A"].Value = new Array(new decimal[] { 1, 2, 3, 4 });
expression.Variables["B"].Value = new Number(10);
decimal result = expression.Execute();
```

2. Using an IF function:
```csharp
decimal result = Engine.Parse("IF(1 < 2, 10, 20)").Execute();
```

3. Custom functions can provide support for accessing data from a database:
```csharp
class GetMyDataFunction : Function
{
	public override string Name 
	{
		get 
		{
			return "GETMYDATA"; 
		}
	}

	public override Literal Execute(IConstruct[] arguments)
	{
		base.EnsureArgumentCountIs(arguments, 2);
	
		var tableName = base.GetVariable(arguments, 0).Name;
		var fieldName = base.GetVariable(arguments, 1).Name;

		// Retrieve data using tableName and fieldName and return Array<Number>.
		// This return value can then be used by any functions that accept Array<Number> as an argument such as SUM().
		// return new Array(new decimal[] { 1, 2, 3, 4 });
	}
}
Engine.Register(new GetMyDataFunction());	
decimal result = Engine.Parse("SUM(GETMYDATA(MyTable, MyField))").Execute();
```

4. Custom functions that manipulate values:
```csharp
class NegateNumber : Function
{
    public override string Name 
    {
        get 
        {
	        return "NEGATE"; 
        }
    }

    public override Literal Execute(IConstruct[] arguments)
    {
        base.EnsureArgumentCountIs(arguments, 1);

		decimal inputValue = base.GetTransformedArgument<Number>(arguments, argumentIndex: 0);

        return new Number(-inputValue);
    }
}
Engine.Register(new NegateNumber());
decimal result = Engine.Parse("NEGATE(1)").Execute();
```

### Supported Functions 
* SUM(array)
* AVG(array)
* IF(condition, trueResult, falseResult)
* Custom functions can be created by extending Function and registered it via `Engine.Register(Function)`

### Supported data types (can be extended)
* Number/decimal
* Boolean
* Array - Of either Number or Boolean

### Supported Operations (can be extended)
* +		- addition
* -		- subtraction
* /		- divide
* *		- multiply
* =		- equal
* <>	- not equal to
* <		- less than
* >		- greater than
* >=	- greater than or equal to
* <=	- less than or equal to
* OR	- logical or
* AND	- logical and

Supported Platforms
-------------------
Supported platforms are Windows and MonoTouch/Mono. 

License
-------
The library can be used for commercial and non-commercial purposes.

Unit Tests
----------
The unit test project is available in a separate repository on [Github here](https://github.com/hisystems/Interpreter-UnitTests). It is also a good resource for examples on how to utilise the library. To run the unit tests project in conjunction with the library it must be located in the same directory as the library.

For example:

/Interpreter
/Interpreter.UnitTests