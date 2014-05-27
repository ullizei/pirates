using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemDatabase : ScriptableObject {

	[SerializeField]
	public List<ItemData> items;

	private static ItemDatabase instance;
	public static ItemDatabase Instance {
		get 
		{
			if (instance == null)
				instance = Resources.Load("ItemDatabase") as ItemDatabase;

			return instance;
		}
	}

	public List<ItemData> GetAllItemsOfType(ItemType itemType) {

		List<ItemData> resultList = new List<ItemData>();
		foreach (ItemData data in items)
		{
			if (data.itemType == itemType)
				resultList.Add(data);
		}
		return resultList;
	}
}
