using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterStats {

	public int swagger;
	public int strength;
	public int agility;
	public int mind;
	public int health;

	public static CharacterStats Copy(CharacterStats original) {

		CharacterStats copy = new CharacterStats();
		copy.agility = original.agility;
		copy.strength = original.strength;
		copy.health = original.health;
		copy.mind = original.mind;
		copy.swagger = original.swagger;
		return copy;
	}

	public int GetStat(StatType stat) {
		switch (stat)
		{
		case StatType.Agility:
			return agility;

		case StatType.Health:
			return health;

		case StatType.Mind:
			return mind;

		case StatType.Strength:
			return strength;

		case StatType.Swagger:
			return swagger;
		}
		return 0;
	}

	public void SubtractStats(CharacterStats statsToSubtract) {
		swagger -= statsToSubtract.swagger;
		strength -= statsToSubtract.strength;
		agility -= statsToSubtract.agility;
		mind -= statsToSubtract.mind;
		health -= statsToSubtract.health;
	}

	public void AddStats(CharacterStats statsToAdd) {
		swagger += statsToAdd.swagger;
		strength += statsToAdd.strength;
		agility += statsToAdd.agility;
		mind += statsToAdd.mind;
		health += statsToAdd.health;
	}

	public void Print() {
		Debug.Log("[Swagger: "+swagger+"] [Strength: "+strength+"] [Agility: "+agility+"] [Mind: "+mind+"] [Health: "+health+"]");
	}
}

public enum StatType {
	Swagger,
	Strength,
	Agility,
	Mind,
	Health
}
