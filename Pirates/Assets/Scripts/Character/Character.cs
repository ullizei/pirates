using UnityEngine;
using System.Collections;

public class Character {

	private CharacterData characterData;
	private CharacterInventory inventory;

	public Character(string name) {

		characterData = new CharacterData(name, GetRandomStats());
		inventory = CharacterInventory.Load(name);
	}

	public int GetBaseStat(StatType stat) {
		return characterData.stats.GetStat(stat);
	}

	public int GetModifiedStat(StatType stat) {
		return characterData.stats.GetStat(stat) + inventory.GetStatModifiersFromEquipment().GetStat(stat);
	}

	public int GetStatModifier(StatType stat) {
		return inventory.GetStatModifiersFromEquipment().GetStat(stat);
	}

	public CharacterStats GetStatsIfEquippedItem(ItemData item, int slotId) {

		CharacterStats altStats = CharacterStats.Copy(characterData.stats);
		altStats.AddStats(inventory.GetStatsModifiersIfEquippedItem(item, slotId));
		return altStats;
	}

	public void EquipItem(ItemData item, int slotId) {
		inventory.EquipItemInSlot(item, slotId);
		CrewInventory.Instance.OnEquippedItem(item.itemName, this);
	}

	public void UnequipItem(ItemData item, int slotId) {
		inventory.UnequipItemInSlot(item, slotId);
		CrewInventory.Instance.OnUnequippedItem(item.itemName, this);
	}

	public bool GetItemInSlot(int slotId, out ItemData itemData) {
		return inventory.GetItemInSlot(slotId, out itemData);
	}
	
	public bool CanUseItemOfType(ItemType itemType) {

		if (itemType == ItemType.NONE)
			return false;
		else
			return true;
	}

	public string Name {
		get { return characterData.name; }
	}

	//for testing...
	private CharacterStats GetRandomStats() {
		
		CharacterStats stats = new CharacterStats();
		stats.agility = Random.Range(5, 18);
		stats.health = Random.Range(5, 18);
		stats.mind = Random.Range(5, 18);
		stats.strength = Random.Range(5, 18);
		stats.swagger = Random.Range(5, 18);
		
		return stats;
	}
}
