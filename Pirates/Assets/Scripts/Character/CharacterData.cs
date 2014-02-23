using UnityEngine;
using System.Collections;

public enum CharacterJob {
	Pirate
}


[System.Serializable]
public class CharacterData {

	public string name;
	public CharacterJob job; 

	public int level;
	public int exp;
	public int hp;

	public int swagger;
	public int strength;
	public int agility;
	public int mind;
	public int health;
}
