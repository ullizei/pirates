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

	public int AddNewItem() {

		items.Add(new ItemData());
		return items.Count;
	}

	public void Test() {
		Debug.Log("Hello hej!");

	}
}
