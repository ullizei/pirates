using UnityEngine;
using System.Collections.Generic;

public enum ItemType {

	Weapon,
	Bandana,
	Hat,
	Shirt,
	Pants,
	Vest,
	Coat,
	Belt,
	Shoes,
	Necklace,
	Ring,
	Pegleg,
	Eyepatch,
	NONE
}

public enum ItemUserGroup {
	All
}

public enum ItemRarity {
	Common,
	Unusual,
	Rare,
	Unique
}


[System.Serializable]
public class ItemData {

	public string itemName = "NEW ITEM";
	public string itemDescription = "";
	public ItemType itemType = ItemType.NONE;
	public ItemUserGroup users = ItemUserGroup.All;
	public ItemRarity rarity = ItemRarity.Common;
	public int levelRequirement = 1;

	public int hpBonus = 0;
	public int swaggerBonus = 0;
	public int strengthBonus = 0;
	public int agilityBonus = 0;
	public int mindBonus = 0;
	public int healthBonus = 0;
	

	public void Test() {
		Debug.Log("Hello hej!");	
	}

	public CharacterStats GetStatModifiers() {

		CharacterStats stats = new CharacterStats();
		stats.agility = agilityBonus;
		stats.health = healthBonus;
		stats.mind = mindBonus;
		stats.strength = strengthBonus;
		stats.swagger = swaggerBonus;
		return stats;
	}
}
