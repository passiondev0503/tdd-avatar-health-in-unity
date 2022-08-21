using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigInstance",
menuName = "Avatar Health/Create GameConfig Instance",
order = 1)]
public class GameConfig : ScriptableObject {
	public int StartingUnits = 3;
	public int PointsPerUnit = 4;
	public int MaxUnits = 30;
	public int MaxNegativeUnitsForInstantKillProtection = -5;

	private void OnValidate()
	{
		var v = Validation.Validate(StartingUnits, 1);
		StartingUnits = ProcessValidation(v, nameof(StartingUnits));

		v = Validation.Validate(PointsPerUnit, 2);
		PointsPerUnit = ProcessValidation(v, nameof(PointsPerUnit));

		v = Validation.Validate(MaxNegativeUnitsForInstantKillProtection, int.MinValue, -1);
		MaxNegativeUnitsForInstantKillProtection = ProcessValidation(v, nameof(MaxNegativeUnitsForInstantKillProtection));

		v = Validation.Validate(MaxUnits, StartingUnits);
		MaxUnits = ProcessValidation(v, nameof(MaxUnits));
	}

	private int ProcessValidation((bool IsValid, int Value, string FailMessage) validation, string fieldName)
	{
		if (!validation.IsValid)
		{
			Debug.LogWarning(validation.FailMessage + $", for '{fieldName}'. Will set value to {validation.Value}.");
		}
		return validation.Value;
	}
}