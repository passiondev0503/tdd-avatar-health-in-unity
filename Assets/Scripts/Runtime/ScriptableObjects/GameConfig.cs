using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "GameConfigInstance",
menuName = "Avatar Health/Create GameConfig Instance",
order = 1)]
public class GameConfig : ScriptableObject {
	public int StartingPoints = 12;
	public int PointsPerUnit = 4;
	public int MaxUnits = 30;
	public int MaxNegativePointsForInstantKillProtection = -20;

	private void OnValidate()
	{
		var v = Validation.Validate(StartingPoints, 1);
		StartingPoints = ProcessValidation(v, nameof(StartingPoints));

		v = Validation.Validate(PointsPerUnit, 2);
		PointsPerUnit = ProcessValidation(v, nameof(PointsPerUnit));

		v = Validation.Validate(MaxNegativePointsForInstantKillProtection, Int32.MinValue, -1);
		MaxNegativePointsForInstantKillProtection = ProcessValidation(v, nameof(MaxNegativePointsForInstantKillProtection));

		double lowestMaxUnits = Math.Ceiling((double)StartingPoints / (double)PointsPerUnit);
		v = Validation.Validate(MaxUnits, (int)lowestMaxUnits);
		MaxUnits = ProcessValidation(v, nameof(MaxUnits));
	}

	private int ProcessValidation((bool IsValid, int Value, string FailMessage) v, string fieldName)
	{
		if (!v.IsValid)
			{
				Debug.LogWarning(v.FailMessage + $", for '{fieldName}'. Will set value to {v.Value}.");
			}
			return v.Value;
	}
}
