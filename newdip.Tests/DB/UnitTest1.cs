using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using newdip.Models;

namespace newdip.Tests.DB
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			ApplicationDbContext db = new ApplicationDbContext();
			db.Database.Delete();
			db.Floors.Add(new Floor());
			db.SaveChanges();
			int count = db.Floors.ToList().Count;
			Assert.AreEqual(count, 1);
		}
	}
}
