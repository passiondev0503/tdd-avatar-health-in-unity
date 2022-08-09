using NUnit.Framework;
using System;
using UnityEngine;

[TestFixture]
public class HealthTest
{

	public static Health MakeHealth(int startingPoints)
	{
			GameConfig config = ScriptableObject.CreateInstance<GameConfig>();
			config.StartingPoints = startingPoints;
			config.PointsPerUnit = 4;
			config.MaxUnits = 30;
			var health = new Health(config);
			return health;
	}
	
	public class Constructor
	{
		[TestCase(12)]
		[TestCase(1)]
		public void CurrentPoints_HasStartingValue(int startingPoints)
		{
			var health = MakeHealth(startingPoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(startingPoints));
		}

		[TestCase(12)]
		[TestCase(1)]
		public void FullPoints_HasStartingValue(int startingPoints)
		{
			var health = MakeHealth(startingPoints);
			Assert.That(health.FullPoints, Is.EqualTo(startingPoints));
		}

		[Test]
		public void IsDead_IsFalse()
		{
			var health = MakeHealth(12);
			Assert.That(health.IsDead, Is.False);
		}
	}

	public class TakeDamage
	{
		[Test]
		public void CurrentPoints_Decrease()
		{
			var health = MakeHealth(12);
			health.TakeDamage(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(11));
		}

		[TestCase(1, 2, 2)]
		[TestCase(1, 2, 3)]
		[TestCase(1, 2, 22)]
		[TestCase(-21, 2, 23)]
		public void CurrentPoints_WhenStartingPoints_ThenDamagePoints(
			int currentPoints,
			int startingPoints,
			int damagePoints)
		{
			var health = MakeHealth(startingPoints);
			health.TakeDamage(damagePoints);
			Assert.That(health.CurrentPoints, Is.EqualTo(currentPoints));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenDamagePointsIsInvalid(int damagePoints)
		{
			var health = MakeHealth(12);
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
			var health = MakeHealth(10);
			health.TakeDamage(9);
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
			var health = MakeHealth(12);
			health.Replenish(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(12));
		}

		[Test]
		public void CurrentPoints_WhenNotFullHealth()
		{
			var health = MakeHealth(12);
			health.TakeDamage(2);
			health.Replenish(1);
			Assert.That(health.CurrentPoints, Is.EqualTo(11));
		}

		[TestCase(0)]
		[TestCase(-1)]
		public void ThrowsError_WhenReplenishPointsIsInvalid(int replenishPoints)
		{
			var health = MakeHealth(12);
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
			var health = MakeHealth(12);
			health.IncreaseByUnit();
			Assert.That(health.FullPoints, Is.EqualTo(16));
		}

		[Test]
		public void CurrentPoints_Increase()
		{
			var health = MakeHealth(12);
			health.IncreaseByUnit();
			Assert.That(health.CurrentPoints, Is.EqualTo(16));
		}

		[TestCase(7, 4, 1)]
		[TestCase(6, 4, 2)]
		[TestCase(5, 4, 3)]
		public void CurrentPoints_WhenStartingPoints_ThenDamagePoints(
			int currentPoints,
			int startingPoints,
			int damagePoints)
		{
			var health = MakeHealth(startingPoints);
			health.TakeDamage(damagePoints);
			health.IncreaseByUnit();
			Assert.That(health.CurrentPoints, Is.EqualTo(currentPoints));
		}

		[Test]
		public void ThrowsError_WhenMaxUnitsReached()
		{
			int startingPointsAtMax = 120; // MaxUnits * PointsPerUnit (config vars set in MakeHealth method)
			var health = MakeHealth(startingPointsAtMax);
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
		public void ReturnsTrue()
		{
			int startingPointsAtMax = 120; // MaxUnits * PointsPerUnit (config vars set in MakeHealth method)
			var health = MakeHealth(startingPointsAtMax);
			Assert.That(health.IsMaxUnitsReached, Is.True);
		}

		[Test]
		public void ReturnsFalse()
		{
			var health = MakeHealth(1);
			Assert.That(health.IsMaxUnitsReached, Is.False);
		}
	}
}