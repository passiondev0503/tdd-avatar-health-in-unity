using NUnit.Framework;
using System;

public class HealthTest
{
	public class Constructor
	{
		[TestCase(12)]
		[TestCase(1)]
		public void CurrentPointsHasStartingValue(int startingPoints)
		{
			var health = new Health(startingPoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(startingPoints));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenStartingPointsIsInvalid(int startingPoints)
		{
			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
			delegate
			{
				new Health(startingPoints);
			});
		}
	}

	public class TakeDamage
	{
		[Test]
		public void CurrentPointsDecrease()
		{
			var health = new Health(12);
			health.TakeDamage(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(11));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenDamagePointsIsInvalid(int damagePoints)
		{
			var health = new Health(12);
			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
			delegate
			{
				health.TakeDamage(damagePoints);
			});
		}
	}

	public class IsDead
	{
		[Test]
		public void IsFalse_AtStart()
		{
			var health = new Health(12);
			Assert.That(health.IsDead, Is.False);
		}
	}
}