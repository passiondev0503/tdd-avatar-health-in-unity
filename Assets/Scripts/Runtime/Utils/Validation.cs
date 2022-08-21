public static class Validation
{
	public static (bool, int, string) Validate(int v, int lowestValidV, int highestValidV = int.MaxValue)
	{
		var message = "";
		if (v >= lowestValidV && v <= highestValidV)
		{
			return (true, v, message);
		}

		message = $"Value {v} is invalid, it should be within the range of {lowestValidV} and {highestValidV}";
		int retV = highestValidV == int.MaxValue ? lowestValidV : highestValidV;
		
		return (false, retV, message);
	}
}