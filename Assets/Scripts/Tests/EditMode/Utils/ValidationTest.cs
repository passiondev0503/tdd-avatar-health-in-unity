using NUnit.Framework;

[TestFixture]
public class ValidationTest
{
	public class Validate
	{
		[TestCase(12, 1)]
		[TestCase(1, 1)]
		[TestCase(4, 2)]
		[TestCase(2, 2)]
		[TestCase(-20, int.MinValue, -1)]
		[TestCase(-1, int.MinValue, -1)]
		public void Passes(int v, int lowestValidV, int highestValidV = int.MaxValue)
		{
			(bool, int, string) ret = Validation.Validate(v, lowestValidV, highestValidV);
			Assert.That(ret.Item1, Is.True);
			Assert.That(ret.Item2, Is.EqualTo(v));
			Assert.That(ret.Item3, Is.EqualTo(""));
		}

		[TestCase(0, 1)]
		[TestCase(-1, 0)]
		public void Fails_WhenValueLower(int v, int lowestValidV)
		{
			(bool, int, string) ret = Validation.Validate(v, lowestValidV, int.MaxValue);
			Assert.That(ret.Item1, Is.False);
			Assert.That(ret.Item2, Is.EqualTo(lowestValidV));
			Assert.That(ret.Item3, Does.Match("invalid").IgnoreCase);
		}

		[TestCase(0, int.MinValue, -1)]
		[TestCase(1, int.MinValue, 0)]
		public void Fails_WhenValueHigher(int v, int lowestValidV, int highestValidV)
		{
			(bool, int, string) ret = Validation.Validate(v, lowestValidV, highestValidV);
			Assert.That(ret.Item1, Is.False);
			Assert.That(ret.Item2, Is.EqualTo(highestValidV));
			Assert.That(ret.Item3, Does.Match("invalid").IgnoreCase);
		}
	}	
}