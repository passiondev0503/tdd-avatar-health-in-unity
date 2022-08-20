using NUnit.Framework;
using System;
using UnityEngine;

[TestFixture]
public class HealthTest
{

	public static Health MakeHealth(int startingUnits)
	{
			var config = MakeConfig(startingUnits);
			var health = new Health(config);
			return health;
	}

	public static GameConfig MakeConfig(int startingUnits = 3)
	{
			GameConfig config = ScriptableObject.CreateInstance<GameConfig>();
			config.StartingUnits = startingUnits;
			config.PointsPerUnit = 4;
			config.MaxUnits = 30;
			return config;
	}
	
	public class Constructor
	{
		[TestCase(3)]
		[TestCase(1)]
		public void CurrentPoints_HasStartingValue(int startingUnits)
		{
			var health = MakeHealth(startingUnits);
			Assert.That(health.CurrentPoints, Is.EqualTo(startingUnits * health.PointsPerUnit));
		}

		[TestCase(3)]
		[TestCase(1)]
		public void FullPoints_HasStartingValue(int startingUnits)
		{
			var health = MakeHealth(startingUnits);
			Assert.That(health.FullPoints, Is.EqualTo(startingUnits * health.PointsPerUnit));
		}

		[Test]
		public void IsDead_IsFalse()
		{
			var health = MakeHealth(3);
			Assert.That(health.IsDead, Is.False);
		}
	}

	public class TakeDamage
	{
		[Test]
		public void CurrentPoints_Decrease()
		{
			var health = MakeHealth(3);
			health.TakeDamage(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(11));
		}

		[TestCase(1, 1, 4)]
		[TestCase(1, 1, 5)]
		[TestCase(1, 1, 24)]
		[TestCase(-21, 1, 25)]
		public void CurrentPoints_WhenStartingUnits_ThenDamagePoints(
			int currentPoints,
			int startingUnits,
			int damagePoints)
		{
			var health = MakeHealth(startingUnits);
			health.TakeDamage(damagePoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(currentPoints));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenDamagePointsIsInvalid(int damagePoints)
		{
			var health = MakeHealth(3);
			var exception = Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(), 
				delegate
				{
					health.TakeDamage(damagePoints);
				});
			Assert.That(exception.Message, Does.Match("invalid").IgnoreCase);
		}

		[Test]
		public void IsDead_AfterTwoInvocations()
		{
			var health = MakeHealth(3);
			health.TakeDamage(11);
			Assert.That(health.IsDead, Is.False);

			health.TakeDamage(1);
			Assert.That(health.IsDead, Is.True);
		}
	}

	public class Replenish
	{
		[Test]
		public void CurrentPoints_WhenFullHealth()
		{
			var health = MakeHealth(3);
			health.Replenish(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(12));
		}

		[Test]
		public void CurrentPoints_WhenNotFullHealth()
		{
			var health = MakeHealth(3);
			health.TakeDamage(2);
			health.Replenish(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(11));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenReplenishPointsIsInvalid(int replenishPoints)
		{
			var health = MakeHealth(3);
			var exception = Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(), 
				delegate
				{
					health.Replenish(replenishPoints);
				});
			Assert.That(exception.Message, Does.Match("invalid").IgnoreCase);
		}
	}

	public class IncreaseByUnit
	{
		[Test]
		public void FullPoints_Increase()
		{
			var health = MakeHealth(3);
			health.IncreaseByUnit();
			Assert.That(health.FullPoints, Is.EqualTo(16));
		}

		[Test]
		public void CurrentPoints_Increase()
		{
			var health = MakeHealth(3);
			health.IncreaseByUnit();
			Assert.That(health.CurrentPoints, Is.EqualTo(16));
		}

		[Test]
		public void ThrowsError_WhenMaxUnitsReached()
		{
			var config = MakeConfig();
			config.StartingUnits = config.MaxUnits;
			var health = new Health(config);
			var exception = Assert.Throws(Is.TypeOf<InvalidOperationException>(),
				delegate
				{
					health.IncreaseByUnit();
				});
			Assert.That(exception.Message, Does.Match("invalid").IgnoreCase);
		}
	}

	public class IsMaxUnitsReached
	{
		[Test]
		public void ReturnsTrue_WhenStartingUnitsAtMax()
		{
			var config = MakeConfig();
			config.StartingUnits = config.MaxUnits;
			var health = new Health(config);
			Assert.That(health.IsMaxUnitsReached, Is.True);
		}

		[Test]
		public void ReturnsFalse_WhenStartingUnitsNotAtMax()
		{
			var config = MakeConfig();
			config.StartingUnits = config.MaxUnits - 1;
			var health = new Health(config);
			Assert.That(health.IsMaxUnitsReached, Is.False);
		}
	}
}