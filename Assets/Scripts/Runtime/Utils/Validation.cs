using System;

public static class Validation
{
	public static (bool, int, string) Validate(int v, int lowestValidV = Int32.MinValue, int highestValidV = Int32.MaxValue)
	{
		string message = "";
		if (v >= lowestValidV && v <= highestValidV)
		{
			return (true, v, message);
		}

		message = $"Value {v} is invalid, it should be within the range of {lowestValidV} and {highestValidV}";
		int retV = highestValidV == Int32.MaxValue ? lowestValidV : highestValidV;
			return (false, retV, message);
	}
}
