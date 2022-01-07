using System;

public class Health
{
	public int CurrentPoints { get; private set; }

	public Health(int startingPoints)
	{
		int lowestValidValue = 1;
		if (startingPoints < lowestValidValue)
		{
			var paramName = nameof(startingPoints);
			var message = $"Value '{startingPoints}' is invalid, it should be equal or higher than '{lowestValidValue}'";
			throw new ArgumentOutOfRangeException(paramName, message);
		}
		CurrentPoints = startingPoints;
	}
}