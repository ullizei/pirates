using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory {

	[SerializeField]
	private Dictionary<string, ItemData> items;

	[SerializeField]
	private Dictionary<string, ItemInventoryInfo> itemInfos;

	private static Inventory instance = null;
	public static Inventory Instance {
		get
		{
			if (instance == null)
			{
				if (PlayerPrefs.HasKey(Keys.InventoryPPKey))
					instance = (Inventory) PlayerPrefsHelper.LoadObject(Keys.InventoryPPKey);
				else
					instance = new Inventory();
			}
			return instance;
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
		}
		Save();
	}

	public void RemoveItem(string itemName, int quantity = 1) {

		if (items.ContainsKey(itemName)) 
		{
			int newAmount = itemInfos[itemName].amount - quantity;
			if (newAmount <= 0)
			{
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

	public void Save() {
		PlayerPrefsHelper.SaveObject<Inventory>(this, Keys.InventoryPPKey);
	}		
}

[System.Serializable]
public class ItemInventoryInfo {

	public int amount;
	public List<Character> equippedBy;
	
}
