using System;
using System.Linq;

using NUnit.Framework;

namespace Tests.Exceptions
{
	[TestFixture]
	public class ElementOperationTests : TestBase
	{
		[Test, DataContextSource, Explicit("Fails")]
		public void First(string context)
		{
			using (var db = GetDataContext(context))
				Assert.Throws(typeof(InvalidOperationException), () => db.Parent.First(p => p.ParentID == 100));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void Single(string context)
		{
			using (var db = GetDataContext(context))
				Assert.Throws(typeof(InvalidOperationException), () => db.Parent.Single());
		}
	}
}
