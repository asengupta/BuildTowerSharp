using System;
using NUnit.Framework;

namespace BuildTower
{
	[TestFixture]
	public class BuildTowerTest
	{
		private Tower tower;

		[SetUp]
		public void SetUp()
		{
			tower = new Tower();
		}

		[Test]
		public void TestInitiallyAllPinsAreZero()
		{
			Assert.AreEqual(0, tower.Read());
		}

		[Test]
		public void BasicWritingAndReadingWorks()
		{
			tower.Write(255);
			Assert.AreEqual(255, tower.Read());
		}

		[Test]
		public void IndividualPinWritingAndReadingWorks()
		{
			tower.Activate(0);
			Assert.AreEqual(1, tower.ReadFrom(0));
			Assert.AreEqual(1, tower.Read());

			tower.Deactivate(0);
			Assert.AreEqual(0, tower.ReadFrom(0));
			Assert.AreEqual(0, tower.Read());
		}

		[Test]
		public void CombinationPinWritingAndReadingWorks()
		{
			tower.Activate(0);
			tower.Activate(1);
			Assert.AreEqual(1, tower.ReadFrom(0));
			Assert.AreEqual(1, tower.ReadFrom(1));
			Assert.AreEqual(3, tower.Read());
		
			tower.Activate(7);
			Assert.AreEqual(1, tower.ReadFrom(0));
			Assert.AreEqual(1, tower.ReadFrom(1));
			Assert.AreEqual(1, tower.ReadFrom(7));
			Assert.AreEqual(131, tower.Read());

			tower.Deactivate(0);
			Assert.AreEqual(130, tower.Read());
		}
	}
}
