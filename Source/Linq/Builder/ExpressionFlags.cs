using System;

namespace LinqToDB.Linq.Builder
{
	[Flags]
	enum ExpressionFlags
	{
		None      = 0x00000000,
		Parameter = 0x00000001,
		Object    = 0x00000002,
	}
}
