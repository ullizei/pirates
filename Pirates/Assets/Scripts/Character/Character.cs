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

	public int GetStatModifier(StatType stat) {
		return inventory.GetStatModifiersFromEquipment().GetStat(stat);
	}

	public void EquipItem(ItemData item, int slotId) {
		inventory.EquipItemInSlot(item, slotId);
	}

	public void UnequipItem(ItemData item, int slotId) {
		inventory.UnequipItemInSlot(item, slotId);
	}

	public bool GetItemInSlot(int slotId, out ItemData itemData) {
		return inventory.GetItemInSlot(slotId, out itemData);
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
