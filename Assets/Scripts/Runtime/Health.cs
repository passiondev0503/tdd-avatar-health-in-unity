using System;

public class Health
{
	public const int MaxNegativePointsForInstantKillProtection = -20;
	public const int PointsPerUnit = 4;
	public const int MaxFullPoints = 120;

	public int CurrentPoints { get; private set; }
	public int FullPoints { get; private set; }
	public bool IsMaxFullPointsReached => FullPoints == MaxFullPoints;
	public bool IsDead => CurrentPoints < 1;

	public Health(int startingPoints)
	{
		ValidatePoints(startingPoints, 1);
		FullPoints = CurrentPoints = startingPoints;
	}

	public void IncreaseByUnit()
	{
		if (IsMaxFullPointsReached)
		{
			var message = $"Method invocation is invalid as {nameof(IsMaxFullPointsReached)} is true";
			throw new InvalidOperationException(message);
		}

		FullPoints += PointsPerUnit;
		CurrentPoints += PointsPerUnit;
	}

	public void TakeDamage(int damagePoints)
	{
		ValidatePoints(damagePoints, 1);

		if (CurrentPoints == FullPoints
			&& damagePoints >= FullPoints
			&& damagePoints <= FullPoints - MaxNegativePointsForInstantKillProtection)
		{
			CurrentPoints = 1;
			return;
		}

		CurrentPoints -= damagePoints;
	}

	public void Replenish(int replenishPoints)
	{
		ValidatePoints(replenishPoints, 1);
		CurrentPoints = Math.Min(replenishPoints + CurrentPoints, FullPoints);
	}

	private static void ValidatePoints(int points, int lowestValidValue)
	{
		if (points < lowestValidValue)
		{
			var message = $"Value {points} is invalid, it should be equal or higher than {lowestValidValue}";
			throw new ArgumentOutOfRangeException(nameof(points), message);
		}
	}
}