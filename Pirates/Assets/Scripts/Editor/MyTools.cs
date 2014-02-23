using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyTools : MonoBehaviour {

	[MenuItem ("MyTools/Database/Create item database")]
	static void CreateItemDatabase () {

		if (Resources.Load("ItemDatabase") != null)
			Debug.Log("ItemDatabase already exists!");
		else
		{
			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ItemDatabase>(), "Assets/Database/Resources/ItemDatabase.asset");
			AssetDatabase.SaveAssets();
		}
	}
}
