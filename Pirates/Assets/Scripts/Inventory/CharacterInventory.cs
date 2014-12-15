using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterInventory {

	[SerializeField]
	private Dictionary<int, ItemData> inventory;

	[SerializeField]
	private string characterName;

	[SerializeField]
	private CharacterStats statModifiers;

	public static CharacterInventory Load(string _characterName) {

		if (PlayerPrefs.HasKey("CharacterInventory_"+_characterName))
			return (CharacterInventory) PlayerPrefsHelper.LoadObject("CharacterInventory_"+_characterName);
		else
			return new CharacterInventory(_characterName);
	}

	private CharacterInventory(string _characterName) {
		characterName = _characterName;
		inventory = new Dictionary<int, ItemData>();
		statModifiers = new CharacterStats();
	}

	public void EquipItemInSlot(ItemData item, int slotId) {

		if (inventory.ContainsKey(slotId))
			UnequipItemInSlot(item, slotId);

		inventory.Add(slotId, item);
		statModifiers.AddStats(item.GetStatModifiers());
		//CrewInventory.Instance.OnEquippedItem(item.itemName, this);
	}

	public void UnequipItemInSlot(ItemData item, int slotId) {

		//CrewInventory.Instance.OnUnequippedItem(item.itemName, this);
		statModifiers.SubtractStats(inventory[slotId].GetStatModifiers());
		inventory.Remove(slotId);
	}

	public bool GetItemInSlot(int slotId, out ItemData itemData) {

		itemData = null;
		if (inventory.ContainsKey(slotId)) {
			itemData = inventory[slotId];
			return true;
		}
		return false;
	}

	public CharacterStats GetStatModifiersFromEquipment() {
		return statModifiers;
		/*CharacterStats modifiers = new CharacterStats();

		foreach (KeyValuePair<int, itemData> pair in inventory)
		{
			modifiers.agility += inventory[pair.Key].agilityBonus;
			modifiers.mind += inventory[pair.Key].mindBonus;
			modifiers.strength += inventory[pair.Key].strengthBonus;
			modifiers.health += inventory[pair.Key].healthBonus;
			modifiers.swagger += inventory[pair.Key].swaggerBonus;
		}
		return modifiers;*/
	}
}
