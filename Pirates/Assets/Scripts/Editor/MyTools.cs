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

	[MenuItem ("MyTools/Database/Create enemy database")]
	static void CreateEnemyDatabase () {
		
		if (Resources.Load("EnemyDatabase") != null)
			Debug.Log("EnemyDatabase already exists!");
		else
		{
			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<EnemyDatabase>(), "Assets/Database/Resources/EnemyDatabase.asset");
			AssetDatabase.SaveAssets();
		}
	}

	public static GUIStyle GetHeaderStyle() {
		
		GUIStyle headerStyle = new GUIStyle(EditorStyles.label);
		
		headerStyle.fontSize = 12;
		headerStyle.fontStyle = FontStyle.Bold;
		
		return headerStyle;
	}

	public static GUIStyle GetFoldoutStyle() {
		
		GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
		
		foldoutStyle.fontSize = 11;
		foldoutStyle.fontStyle = FontStyle.Bold;
		
		return foldoutStyle;
	}
}
