using System;
using System.Linq;

using LinqToDB;

using NUnit.Framework;

namespace Tests.Linq
{
	using Model;

	[TestFixture]
	public class ElementOperationTests : TestBase
	{
		[Test, DataContextSource, Explicit("Fails")]
		public void First(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(
					   Parent.OrderByDescending(p => p.ParentID).First().ParentID,
					db.Parent.OrderByDescending(p => p.ParentID).First().ParentID);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void FirstWhere(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(2, db.Parent.First(p => p.ParentID == 2).ParentID);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void FirstOrDefault(string context)
		{
			using (var db = GetDataContext(context))
				Assert.IsNull((from p in db.Parent where p.ParentID == 100 select p).FirstOrDefault());
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void FirstOrDefaultWhere(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(2, db.Parent.FirstOrDefault(p => p.ParentID == 2).ParentID);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void Single(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(1, db.Parent.Where(p => p.ParentID == 1).Single().ParentID);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void SingleWhere(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(2, db.Parent.Single(p => p.ParentID == 2).ParentID);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void SingleOrDefault(string context)
		{
			using (var db = GetDataContext(context))
				Assert.IsNull((from p in db.Parent where p.ParentID == 100 select p).SingleOrDefault());
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void SingleOrDefaultWhere(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(2, db.Parent.SingleOrDefault(p => p.ParentID == 2).ParentID);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void FirstOrDefaultScalar(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(
					   Parent.OrderBy(p => p.ParentID).FirstOrDefault().ParentID,
					db.Parent.OrderBy(p => p.ParentID).FirstOrDefault().ParentID);
		}

		[Test, DataContextSource(ProviderName.Informix, ProviderName.Sybase, ProviderName.SapHana), Explicit("Fails")]
		public void NestedFirstOrDefaultScalar1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    Parent select    Child.FirstOrDefault().ChildID,
					from p in db.Parent select db.Child.FirstOrDefault().ChildID);
		}

		[Test, DataContextSource(ProviderName.Informix, ProviderName.OracleNative, ProviderName.OracleManaged, ProviderName.Sybase, ProviderName.SapHana), Explicit("Fails")]
		public void NestedFirstOrDefaultScalar2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					select new
					{
						p.ParentID,
						MaxChild =
							Child
								.Where(c => c.Parent == p)
								.OrderByDescending(c => c.ChildID * c.ParentID)
								.FirstOrDefault() == null ?
							0 :
							Child
								.Where(c => c.Parent == p)
								.OrderByDescending(c => c.ChildID * c.ParentID)
								.FirstOrDefault()
								.ChildID
					},
					from p in db.Parent
					select new
					{
						p.ParentID,
						MaxChild = db.Child
							.Where(c => c.Parent == p)
							.OrderByDescending(c => c.ChildID * c.ParentID)
							.FirstOrDefault()
							.ChildID
					});
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void NestedFirstOrDefault1(string context)
		{
			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

			using (var db = GetDataContext(context))
				AreEqual(
					from p in    Parent select    Child.FirstOrDefault(),
					from p in db.Parent select db.Child.FirstOrDefault());

			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = false;
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void NestedFirstOrDefault2(string context)
		{
			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

			using (var db = GetDataContext(context))
				AreEqual(
					from p in    Parent select p.Children.FirstOrDefault(),
					from p in db.Parent select p.Children.FirstOrDefault());

			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = false;
		}

		[Test, DataContextSource(ProviderName.Informix, ProviderName.Firebird, ProviderName.SapHana), Explicit("Fails")]
		public void NestedFirstOrDefault3(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    Parent select p.Children.Select(c => c.ParentID).Distinct().FirstOrDefault(),
					from p in db.Parent select p.Children.Select(c => c.ParentID).Distinct().FirstOrDefault());
		}

		[Test, DataContextSource(ProviderName.Informix, ProviderName.Firebird, ProviderName.PostgreSQL), Explicit("Fails")]
		public void NestedFirstOrDefault4(string context)
		{
			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

			using (var db = GetDataContext(context))
				AreEqual(
					from p in    Parent select p.Children.Where(c => c.ParentID > 0).Distinct().FirstOrDefault(),
					from p in db.Parent select p.Children.Where(c => c.ParentID > 0).Distinct().FirstOrDefault());

			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = false;
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void NestedFirstOrDefault5(string context)
		{
			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

			using (var db = GetDataContext(context))
				AreEqual(
					from p in    GrandChild select p.Child.Parent.Children.FirstOrDefault(),
					from p in db.GrandChild select p.Child.Parent.Children.FirstOrDefault());

			LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = false;
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void NestedSingleOrDefault1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    Parent select p.Children.Select(c => c.ParentID).Distinct().SingleOrDefault(),
					from p in db.Parent select p.Children.Select(c => c.ParentID).Distinct().SingleOrDefault());
		}

		[Test, NorthwindDataContext, Explicit("Fails")]
		public void FirstOrDefaultEntitySet(string context)
		{
			using (var db = new NorthwindDB())
			{
				AreEqual(
					   Customer.Select(c => c.Orders.FirstOrDefault()),
					db.Customer.Select(c => c.Orders.FirstOrDefault()));
			}
		}

		[Test, NorthwindDataContext, Explicit("Fails")]
		public void NestedSingleOrDefaultTest(string context)
		{
			using (var db = new NorthwindDB())
			{
				AreEqual(
					   Customer.Select(c => c.Orders.Take(1).SingleOrDefault()),
					db.Customer.Select(c => c.Orders.Take(1).SingleOrDefault()));
			}
		}

		[Test, NorthwindDataContext, Explicit("Fails")]
		public void MultipleQuery(string context)
		{
			using (var db = new NorthwindDB())
			{
				var q =
					from p in db.Product
					select db.Category.Select(zrp => zrp.CategoryName).FirstOrDefault();

				q.ToList();
			}
		}
	}
}
