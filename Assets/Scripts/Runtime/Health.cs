using System;

public class Health
{
	public int CurrentPoints { get; private set; }
	public int FullPoints { get; private set; }
	public bool IsDead => CurrentPoints < 1;

	public Health(int startingPoints)
	{
		ValidatePoints(startingPoints, 1, nameof(startingPoints));
		FullPoints = CurrentPoints = startingPoints;
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