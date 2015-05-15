using UnityEngine;
using System.Collections.Generic;

public enum ResourceType {
	Gold,
	Water,
	Rum,
	Oranges,
	Provisions
}

[System.Serializable]
public class CrewInventory {

	//Resources
	[SerializeField]
	private int gold = 5000;

	[SerializeField]
	private int rum = 50;

	[SerializeField]
	private int water = 50;

	[SerializeField]
	private int oranges = 50;

	[SerializeField]
	private int provisions = 50;


	//Items
	[SerializeField]
	private Dictionary<string, ItemData> items;

	[SerializeField]
	private Dictionary<string, ItemInventoryInfo> itemInfos;

	private Dictionary<ItemType, List<ItemData>> itemsByType;


	private static CrewInventory instance = null;
	public static CrewInventory Instance {
		get
		{
			PlayerPrefs.DeleteKey(PPKeys.CrewInventoryPPKey);

			if (instance == null)
			{
				if (PlayerPrefs.HasKey(PPKeys.CrewInventoryPPKey))
					instance = Load();
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

	public int GetFreeItemAmount(string itemName) {

		if (itemInfos.ContainsKey(itemName))
		{
			int totalAmount = itemInfos[itemName].amount;
			return totalAmount - itemInfos[itemName].equippedBy.Count;
		}
		else
			return 0;
	}

	public int GetTotalItemAmount(string itemName) {

		if (itemInfos.ContainsKey(itemName))
			return itemInfos[itemName].amount;
		else
			return 0;
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

	public int GetResourceAmount(ResourceType type) {

		switch (type) {
		case ResourceType.Gold:
			return gold;
		case ResourceType.Oranges:
			return oranges;
		case ResourceType.Provisions:
			return provisions;
		case ResourceType.Rum:
			return rum;
		case ResourceType.Water:
			return water;
		}
		return 0; //<- just to make compiler happy...
	}

	public void UpdateResourceAmount(ResourceType type, int amount) {

		switch (type) {
		case ResourceType.Gold:
			gold += amount;
			gold = Mathf.Max(0, gold);
			break;
		case ResourceType.Oranges:
			oranges += amount;
			oranges = Mathf.Max(0, oranges);
			break;
		case ResourceType.Provisions:
			provisions += amount;
			provisions = Mathf.Max(0, provisions);
			break;
		case ResourceType.Rum:
			rum += amount;
			rum = Mathf.Max(0, rum);
			break;
		case ResourceType.Water:
			water += amount;
			water = Mathf.Max(0, water);
			break;
		}
	}

	public void Save() {
		PlayerPrefsHelper.SaveObject<CrewInventory>(this, PPKeys.CrewInventoryPPKey);
	}

	public static CrewInventory Load() {
		return (CrewInventory) PlayerPrefsHelper.LoadObject(PPKeys.CrewInventoryPPKey);
	}	
}

[System.Serializable]
public class ItemInventoryInfo {

	public int amount;
	public List<Character> equippedBy;
	
}
