using System;
using System.Linq;

using LinqToDB;

using NUnit.Framework;

namespace Tests.Linq
{
	[TestFixture]
	public class ConvertTests : TestBase
	{
		[Test, DataContextSource(ProviderName.SQLite), Explicit("Fails")]
		public void Test1(string context)
		{
			using (var db = GetDataContext(context))
				Assert.AreEqual(1, (from t in db.Types where t.MoneyValue * t.ID == 1.11m  select t).Single().ID);
		}

		#region Int

		[Test, DataContextSource, Explicit("Fails")]
		public void ToInt1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select           Sql.ConvertTo<int>.From(t.MoneyValue),
					from t in db.Types select Sql.AsSql(Sql.ConvertTo<int>.From(t.MoneyValue)));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToInt2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select           Sql.Convert<int,decimal>(t.MoneyValue),
					from t in db.Types select Sql.AsSql(Sql.Convert<int,decimal>(t.MoneyValue)));
		}

		[Test, DataContextSource(ProviderName.MySql, TestProvName.MariaDB), Explicit("Fails")]
		public void ToBigInt(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.BigInt, t.MoneyValue),
					from t in db.Types select Sql.Convert(Sql.BigInt, t.MoneyValue));
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ToInt64(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (Int64)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (Int64)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ConvertToInt64(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToInt64(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToInt64(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToInt(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.Int, t.MoneyValue),
					from t in db.Types select Sql.Convert(Sql.Int, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToInt32(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (Int32)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (Int32)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToInt32(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToInt32(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToInt32(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSmallInt(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.SmallInt, t.MoneyValue),
					from t in db.Types select Sql.Convert(Sql.SmallInt, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToInt16(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (Int16)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (Int16)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToInt16(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToInt16(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToInt16(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToTinyInt(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.TinyInt, t.MoneyValue),
					from t in db.Types select Sql.Convert(Sql.TinyInt, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSByte(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (sbyte)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (sbyte)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToSByte(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToSByte(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToSByte(t.MoneyValue) where p > 0 select p);
		}

		#endregion

		#region UInts

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ToUInt1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select           Sql.ConvertTo<uint>.From(t.MoneyValue),
					from t in db.Types select Sql.AsSql(Sql.ConvertTo<uint>.From(t.MoneyValue)));
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ToUInt2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select           Sql.Convert<uint,decimal>(t.MoneyValue),
					from t in db.Types select Sql.AsSql(Sql.Convert<uint,decimal>(t.MoneyValue)));
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ToUInt64(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (UInt64)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (UInt64)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ConvertToUInt64(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToUInt64(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToUInt64(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ToUInt32(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (UInt32)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (UInt32)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource(ProviderName.MySql), Explicit("Fails")]
		public void ConvertToUInt32(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToUInt32(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToUInt32(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToUInt16(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (UInt16)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (UInt16)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToUInt16(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToUInt16(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToUInt16(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToByte(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (byte)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (byte)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToByte(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToByte(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToByte(t.MoneyValue) where p > 0 select p);
		}

		#endregion

		#region Floats

		[Test, DataContextSource, Explicit("Fails")]
		public void ToDefaultDecimal(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DefaultDecimal, t.MoneyValue * 1000),
					from t in db.Types select Sql.Convert(Sql.DefaultDecimal, t.MoneyValue * 1000));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToDecimal1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.Decimal(10), t.MoneyValue * 1000),
					from t in db.Types select Sql.Convert(Sql.Decimal(10), t.MoneyValue * 1000));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToDecimal2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.Decimal(10,4), t.MoneyValue),
					from t in db.Types select Sql.Convert(Sql.Decimal(10,4), t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToDecimal3(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (Decimal)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (Decimal)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToDecimal(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToDecimal(t.MoneyValue) where p > 0 select p,
					from p in from t in db.Types select Convert.ToDecimal(t.MoneyValue) where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToMoney(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select (int)Sql.Convert(Sql.Money, t.MoneyValue),
					from t in db.Types select (int)Sql.Convert(Sql.Money, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSmallMoney(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select (decimal)Sql.Convert(Sql.SmallMoney, t.MoneyValue),
					from t in db.Types select (decimal)Sql.Convert(Sql.SmallMoney, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlFloat(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select (int)Sql.Convert(Sql.Float, t.MoneyValue),
					from t in db.Types select (int)Sql.Convert(Sql.Float, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToDouble(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (int)(Double)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (int)(Double)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToDouble(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToDouble(t.MoneyValue) where p > 0 select (int)p,
					from p in from t in db.Types select Convert.ToDouble(t.MoneyValue) where p > 0 select (int)p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlReal(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select (int)Sql.Convert(Sql.Real, t.MoneyValue),
					from t in db.Types select (int)Sql.Convert(Sql.Real, t.MoneyValue));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSingle(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select (Single)t.MoneyValue where p > 0 select p,
					from p in from t in db.Types select (Single)t.MoneyValue where p > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToSingle(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToSingle(t.MoneyValue) where p > 0 select (int)p,
					from p in from t in db.Types select Convert.ToSingle(t.MoneyValue) where p > 0 select (int)p);
		}

		#endregion

		#region DateTime

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlDateTime(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DateTime, t.DateTimeValue.Year + "-01-01 00:20:00"),
					from t in db.Types select Sql.Convert(Sql.DateTime, t.DateTimeValue.Year + "-01-01 00:20:00"));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlDateTime2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DateTime2, t.DateTimeValue.Year + "-01-01 00:20:00"),
					from t in db.Types select Sql.Convert(Sql.DateTime2, t.DateTimeValue.Year + "-01-01 00:20:00"));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlSmallDateTime(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.SmallDateTime, t.DateTimeValue.Year + "-01-01 00:20:00"),
					from t in db.Types select Sql.Convert(Sql.SmallDateTime, t.DateTimeValue.Year + "-01-01 00:20:00"));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlDate(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.Date, t.DateTimeValue.Year + "-01-01"),
					from t in db.Types select Sql.Convert(Sql.Date, t.DateTimeValue.Year + "-01-01"));
		}

		[Test, DataContextSource(ProviderName.SQLite
			, ProviderName.Access, ProviderName.Sybase ///////// TODO
			), Explicit("Fails")]
		public void ToSqlTime(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.Time, t.DateTimeValue.Hour + ":01:01"),
					from t in db.Types select Sql.Convert(Sql.Time, t.DateTimeValue.Hour + ":01:01"));
		}

		DateTime ToDateTime(DateTimeOffset dto)
		{
			return new DateTime(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, dto.Second);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToSqlDateTimeOffset(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select ToDateTime(Sql.Convert(Sql.DateTimeOffset, t.DateTimeValue.Year + "-01-01 00:20:00")),
					from t in db.Types select ToDateTime(Sql.Convert(Sql.DateTimeOffset, t.DateTimeValue.Year + "-01-01 00:20:00")));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToDateTime(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select DateTime.Parse(t.DateTimeValue.Year + "-01-01 00:00:00") where p.Day > 0 select p,
					from p in from t in db.Types select DateTime.Parse(t.DateTimeValue.Year + "-01-01 00:00:00") where p.Day > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToDateTime(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToDateTime(t.DateTimeValue.Year + "-01-01 00:00:00") where p.Day > 0 select p,
					from p in from t in db.Types select Convert.ToDateTime(t.DateTimeValue.Year + "-01-01 00:00:00") where p.Day > 0 select p);
		}

		#endregion

		#region String

		[Test, DataContextSource, Explicit("Fails")]
		public void ToChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.Char(20), t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.Char(20), t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource(ProviderName.OracleNative, ProviderName.OracleManaged, ProviderName.Firebird, ProviderName.PostgreSQL), Explicit("Fails")]
		public void ToDefaultChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DefaultChar, t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.DefaultChar, t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToVarChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.VarChar(20), t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.VarChar(20), t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource(ProviderName.OracleNative, ProviderName.OracleManaged, ProviderName.Firebird, ProviderName.PostgreSQL), Explicit("Fails")]
		public void ToDefaultVarChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DefaultVarChar, t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.DefaultVarChar, t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToNChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.NChar(20), t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.NChar(20), t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource(ProviderName.OracleNative, ProviderName.OracleManaged, ProviderName.Firebird, ProviderName.PostgreSQL), Explicit("Fails")]
		public void ToDefaultNChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DefaultNChar, t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.DefaultNChar, t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToNVarChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.NVarChar(20), t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.NVarChar(20), t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource(ProviderName.OracleNative, ProviderName.OracleManaged, ProviderName.Firebird, ProviderName.PostgreSQL), Explicit("Fails")]
		public void ToDefaultNVarChar(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types select Sql.Convert(Sql.DefaultNVarChar, t.MoneyValue).Trim(' ', '0', '.'),
					from t in db.Types select Sql.Convert(Sql.DefaultNVarChar, t.MoneyValue).Trim(' ', '0', '.'));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void DecimalToString(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToString(t.MoneyValue) where p.Length > 0 select p.Replace(',', '.').TrimEnd('0', '.'),
					from p in from t in db.Types select Convert.ToString(t.MoneyValue) where p.Length > 0 select p.Replace(',', '.').TrimEnd('0', '.'));
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ByteToString(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select ((byte)t.ID).ToString() where p.Length > 0 select p,
					from p in from t in db.Types select ((byte)t.ID).ToString() where p.Length > 0 select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void GuidToString(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in    Types where Sql.ConvertTo<string>.From(t.GuidValue) == "febe3eca-cb5f-40b2-ad39-2979d312afca" select t.GuidValue,
					from t in db.Types where Sql.ConvertTo<string>.From(t.GuidValue) == "febe3eca-cb5f-40b2-ad39-2979d312afca" select t.GuidValue);
		}

		#endregion

		#region Boolean

		[Test, DataContextSource, Explicit("Fails")]
		public void ToBit1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in from t in    Types where Sql.Convert(Sql.Bit, t.MoneyValue) select t select t,
					from t in from t in db.Types where Sql.Convert(Sql.Bit, t.MoneyValue) select t select t);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ToBit2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from t in from t in    Types where !Sql.Convert(Sql.Bit, t.MoneyValue - 4.5m) select t select t,
					from t in from t in db.Types where !Sql.Convert(Sql.Bit, t.MoneyValue - 4.5m) select t select t);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToBoolean1(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToBoolean(t.MoneyValue) where p == true select p,
					from p in from t in db.Types select Convert.ToBoolean(t.MoneyValue) where p == true select p);
		}

		[Test, DataContextSource, Explicit("Fails")]
		public void ConvertToBoolean2(string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in from t in    Types select Convert.ToBoolean(t.MoneyValue - 4.5m) where !p select p,
					from p in from t in db.Types select Convert.ToBoolean(t.MoneyValue - 4.5m) where !p select p);
		}

		#endregion
	}
}
