using System;

public class Health
{
	public int PointsPerUnit
	{
		get { return config.PointsPerUnit; }
	}
	public int MaxUnits
	{
		get { return config.MaxUnits; }
	}
		public int MaxNegativePointsForInstantKillProtection
	{
		get { return config.MaxNegativePointsForInstantKillProtection; }
	}
	public int CurrentPoints { get; private set; }
	public int FullPoints { get; private set; }
	public bool IsMaxUnitsReached => FullPoints / PointsPerUnit == MaxUnits;
	public bool IsDead => CurrentPoints < 1;
	
	private GameConfig config;

	public Health(GameConfig gameConfig)
	{
		config = gameConfig;
		FullPoints = CurrentPoints = config.StartingPoints;
	}

	public void IncreaseByUnit()
	{
		if (IsMaxUnitsReached)
		{
			var message = $"Method invocation is invalid as {nameof(IsMaxUnitsReached)} is true";
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
		(bool IsValid, int Value, string FailMessage) v = Validation.Validate(points, lowestValidValue);
		if (!v.IsValid)
		{
			throw new ArgumentOutOfRangeException(nameof(points), v.FailMessage);
		}
	}
}