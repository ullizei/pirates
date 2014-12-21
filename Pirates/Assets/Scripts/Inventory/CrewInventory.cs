using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CrewInventory {

	[SerializeField]
	private Dictionary<string, ItemData> items;

	[SerializeField]
	private Dictionary<string, ItemInventoryInfo> itemInfos;

	private Dictionary<ItemType, List<ItemData>> itemsByType;

	private static CrewInventory instance = null;
	public static CrewInventory Instance {
		get
		{
			if (instance == null)
			{
				if (PlayerPrefs.HasKey(PPKeys.CrewInventoryPPKey))
					instance = (CrewInventory) PlayerPrefsHelper.LoadObject(PPKeys.CrewInventoryPPKey);
				else {
					instance = new CrewInventory();
					instance.items = new Dictionary<string, ItemData>();
					instance.itemInfos = new Dictionary<string, ItemInventoryInfo>();
					instance.itemsByType = new Dictionary<ItemType, List<ItemData>>();
					instance.AddAllItemsFromItemdatabase();
				}
			}
			return instance;
		}
	}

	//use this for testing!
	private void AddAllItemsFromItemdatabase() {

		ItemDatabase itemDatabase = ItemDatabase.Instance;

		for (int i = 0; i < itemDatabase.items.Count; i++) {
			AddItem(itemDatabase.items[i], 2);
		}
	}

	public void AddItem(ItemData item, int amount = 1) {

		if (items.ContainsKey(item.itemName))
			itemInfos[item.itemName].amount += amount;
		else
		{
			items.Add(item.itemName, item);
			itemInfos.Add(item.itemName, new ItemInventoryInfo());
			itemInfos[item.itemName].amount = amount;
			itemInfos[item.itemName].equippedBy = new List<Character>();

			if (!itemsByType.ContainsKey(item.itemType))
				itemsByType.Add(item.itemType, new List<ItemData>());

			itemsByType[item.itemType].Add(item);
		}
		Save();
	}

	public void RemoveItem(string itemName, int quantity = 1) {

		if (items.ContainsKey(itemName)) 
		{
			int newAmount = itemInfos[itemName].amount - quantity;
			if (newAmount <= 0)
			{
				itemsByType[items[itemName].itemType].Remove(items[itemName]);
				items.Remove(itemName);
				itemInfos.Remove(itemName);
			}
			Save();
		}
	}

	public void OnEquippedItem(string itemName, Character owner) {
		if (itemInfos.ContainsKey(itemName))
			itemInfos[itemName].equippedBy.Add(owner);
	}

	public void OnUnequippedItem(string itemName, Character formerOwner) {
		if (itemInfos.ContainsKey(itemName))
			itemInfos[itemName].equippedBy.Remove(formerOwner);
	}

	public void GetItemAmounts(string itemName, out int totalAmount, out int freeAmount) {

		totalAmount = 0;
		freeAmount = 0;

		if (itemInfos.ContainsKey(itemName))
		{
			totalAmount = itemInfos[itemName].amount;
			freeAmount = totalAmount - itemInfos[itemName].equippedBy.Count;
		}
	}

	public List<Character> GetOwnersOfItem(string itemName) {
		if (itemInfos.ContainsKey(itemName))
			return itemInfos[itemName].equippedBy;
		else
			return new List<Character>();
	}

	public List<ItemData> GetItemsOfType(ItemType itemType) {

		if (itemsByType.ContainsKey(itemType))
			return itemsByType[itemType];
		else
			return new List<ItemData>();
	}

	public bool HasAnyItemsOfType(ItemType itemType) {

		if (itemsByType.ContainsKey(itemType)) {
			if (itemsByType[itemType].Count > 0)
				return true;
		}
		return false;
	}

	public void Save() {
		PlayerPrefsHelper.SaveObject<CrewInventory>(this, PPKeys.CrewInventoryPPKey);
	}		
}

[System.Serializable]
public class ItemInventoryInfo {

	public int amount;
	public List<Character> equippedBy;
	
}
