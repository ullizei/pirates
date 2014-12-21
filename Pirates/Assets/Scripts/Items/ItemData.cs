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

	public string GetItemStatModifiersList() {

		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

		//list stat modifiers first
		if (swaggerBonus > 0)
			stringBuilder.AppendLine("+"+swaggerBonus.ToString()+" Swagger");
		else if (swaggerBonus < 0)
			stringBuilder.AppendLine(swaggerBonus.ToString()+" Swagger");

		if (strengthBonus > 0)
			stringBuilder.AppendLine("+"+strengthBonus.ToString()+" Strength");
		else if (strengthBonus < 0)
			stringBuilder.AppendLine(strengthBonus.ToString()+" Strength");

		if (agilityBonus > 0)
			stringBuilder.AppendLine("+"+agilityBonus.ToString()+" Agility");
		else if (agilityBonus < 0)
			stringBuilder.AppendLine(agilityBonus.ToString()+" Agility");

		if (mindBonus > 0)
			stringBuilder.AppendLine("+"+mindBonus.ToString()+" Mind");
		else if (mindBonus < 0)
			stringBuilder.AppendLine(mindBonus.ToString()+" Mind");

		if (healthBonus > 0)
			stringBuilder.AppendLine("+"+healthBonus.ToString()+" Health");
		else if (healthBonus < 0)
			stringBuilder.AppendLine(healthBonus.ToString()+" Health");

		//then others...
		if (hpBonus > 0)
			stringBuilder.AppendLine("+"+hpBonus.ToString()+" HP");
		else if (hpBonus < 0)
			stringBuilder.AppendLine(hpBonus.ToString()+" HP");

		return stringBuilder.ToString();
	}

	public Sprite LoadItemIcon() {
		return Resources.Load<Sprite>("items/Red"+itemType.ToString());
	}
}
