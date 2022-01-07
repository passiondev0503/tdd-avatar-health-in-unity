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
		public void ThrowsError_WhenStartingValueIsInvalid(int startingPoints)
		{
			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
			delegate
			{
				new Health(startingPoints);
			});
		}
	}
}