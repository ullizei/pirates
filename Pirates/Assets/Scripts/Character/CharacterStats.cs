using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterStats {

	public int swagger;
	public int strength;
	public int agility;
	public int mind;
	public int health;	
}

public enum StatType {
	Swagger,
	Strength,
	Agility,
	Mind,
	Health
}
