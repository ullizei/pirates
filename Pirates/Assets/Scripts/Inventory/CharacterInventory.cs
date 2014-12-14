using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterInventory {

	[SerializeField]
	private Dictionary<int, ItemData> inventory;

	[SerializeField]
	private string characterName;



	public static CharacterInventory Load(string _characterName) {

		if (PlayerPrefs.HasKey("CharacterInventory_"+_characterName))
			return (CharacterInventory) PlayerPrefsHelper.LoadObject("CharacterInventory_"+_characterName);
		else
			return new CharacterInventory(_characterName);
	}

	private CharacterInventory(string _characterName) {
		characterName = _characterName;
		inventory = new Dictionary<int, ItemData>();
	}

	public void OnEquippedItemInSlot(ItemData item, int slotId) {

		if (inventory.ContainsKey(slotId))
			OnUnequippedItem(item, slotId);

		inventory.Add(slotId, item);
		//CrewInventory.Instance.OnEquippedItem(item.itemName, this);
	}

	public void OnUnequippedItem(ItemData item, int slotId) {

		//CrewInventory.Instance.OnUnequippedItem(item.itemName, this);
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
}
