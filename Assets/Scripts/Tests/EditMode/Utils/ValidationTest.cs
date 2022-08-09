using NUnit.Framework;
using System;

[TestFixture]
public class ValidationTest
{
	public class Validate
	{
		[TestCase(12, 1, Int32.MaxValue)]
		[TestCase(1, 1, Int32.MaxValue)]
		[TestCase(4, 2, Int32.MaxValue)]
		[TestCase(2, 2, Int32.MaxValue)]
		[TestCase(-20, Int32.MinValue, -1)]
		[TestCase(-1, Int32.MinValue, -1)]
		public void Passes(int v, int lowestValidV, int highestValidV)
		{
			(bool, int, string) ret = Validation.Validate(v, lowestValidV, highestValidV);
			Assert.That(ret.Item1, Is.True);
			Assert.That(ret.Item2, Is.EqualTo(v));
			Assert.That(ret.Item3, Is.EqualTo(""));
		}

		[TestCase(0, 1, Int32.MaxValue)]
		[TestCase(1, 2, Int32.MaxValue)]
		public void Fails_WhenValueLower(int v, int lowestValidV, int highestValidV)
		{
			(bool, int, string) ret = Validation.Validate(v, lowestValidV, highestValidV);
			Assert.That(ret.Item1, Is.False);
			Assert.That(ret.Item2, Is.EqualTo(lowestValidV));
			Assert.That(ret.Item3, Does.Match("invalid").IgnoreCase);
		}

		[TestCase(0, Int32.MinValue, -1)]
		[TestCase(1, Int32.MinValue, -1)]
		public void Fails_WhenValueHigher(int v, int lowestValidV, int highestValidV)
		{
			(bool, int, string) ret = Validation.Validate(v, lowestValidV, highestValidV);
			Assert.That(ret.Item1, Is.False);
			Assert.That(ret.Item2, Is.EqualTo(highestValidV));
			Assert.That(ret.Item3, Does.Match("invalid").IgnoreCase);
		}
	}	
}