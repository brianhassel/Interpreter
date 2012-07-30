/* _________________________________________________

  (c) Hi-Integrity Systems 2012. All rights reserved.
  www.hisystems.com.au - Toby Wicks
  github.com/hisystems/Interpreter
 ___________________________________________________ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiSystems.Interpreter
{
    /// <summary>
	/// Compares two numeric, boolean or datetime values.
    /// </summary>
    public class EqualToOperator : Operator
    {
        public EqualToOperator()
        {
        }

        internal override Literal Execute(IConstruct argument1, IConstruct argument2)
		{
			var argument1Transformed = base.GetTransformedConstruct<Literal>(argument1);
			var argument2Transformed = base.GetTransformedConstruct<Literal>(argument2);

			if (argument1Transformed is Number && argument2Transformed is Number)
				return ((Number)argument1Transformed) == ((Number)argument2Transformed);
			else if (argument1Transformed is Boolean && argument2Transformed is Boolean)
				return ((Boolean)argument1Transformed) == ((Boolean)argument2Transformed);
            else if (argument1Transformed is DateTime && argument2Transformed is DateTime)
                return ((DateTime)argument1Transformed) == ((DateTime)argument2Transformed);
			else
                throw new InvalidOperationException(String.Format("Equality operator requires arguments of type Number, DateTime or Boolean. Argument types are {0} {1}.", argument1Transformed.GetType().Name, argument2Transformed.GetType().Name));
        }

        public override string Token
        {
            get 
            {
                return "=";
            }
        }
    }
}