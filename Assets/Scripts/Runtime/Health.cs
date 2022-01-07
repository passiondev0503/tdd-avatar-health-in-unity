using System;

public class Health
{
	public int CurrentPoints { get; private set; }

	public Health(int startingPoints)
	{
		ValidatePoints(startingPoints, 1, nameof(startingPoints));
		CurrentPoints = startingPoints;
	}

	public void TakeDamage(int damagePoints)
	{
		ValidatePoints(damagePoints, 1, nameof(damagePoints));
		CurrentPoints -= damagePoints;
	}

	private void ValidatePoints(int points, int lowestValidValue, string paramName)
	{
		if (points < lowestValidValue)
		{
			var message = $"Value '{points}' is invalid, it should be equal or higher than '{lowestValidValue}'";
			throw new ArgumentOutOfRangeException(paramName, message);
		}
	}
}