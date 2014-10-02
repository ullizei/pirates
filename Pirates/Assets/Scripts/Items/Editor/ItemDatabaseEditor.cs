using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor {

	private ItemDatabase _target;
	private List<bool> foldoutStatus;

	void OnEnable() {
		_target = (ItemDatabase) target;
		
		if (_target.items == null)
		{
			Debug.Log("Creating new iten list");
			_target.items = new List<ItemData>();
		}

		foldoutStatus = new List<bool>();
		for (int i = 0; i < _target.items.Count; i++)
			foldoutStatus.Add(false);
	}

	public override void OnInspectorGUI() {

		var items = serializedObject.FindProperty("items");

		GUILayout.Label("Item Database", GetHeaderStyle());
		EditorGUILayout.Space();

		EditorGUI.BeginChangeCheck();

		EditorGUILayout.BeginHorizontal();
		{
			//Create new ItemData with default settings
			if (GUILayout.Button("Add new item"))
			{
				int newItemIndex = items.arraySize++;
				var newItem = items.GetArrayElementAtIndex(newItemIndex);
				newItem.FindPropertyRelative("itemName").stringValue = "NEW ITEM";
				newItem.FindPropertyRelative("itemDescription").stringValue = "";
				newItem.FindPropertyRelative("itemType").enumValueIndex = (int) ItemType.NONE;
				newItem.FindPropertyRelative("users").enumValueIndex = (int) ItemUserGroup.All;
				newItem.FindPropertyRelative("rarity").enumValueIndex = (int) ItemRarity.Common;
				newItem.FindPropertyRelative("levelRequirement").intValue = 1;
				newItem.FindPropertyRelative("hpBonus").intValue = 0;
				newItem.FindPropertyRelative("swaggerBonus").intValue = 0;
				newItem.FindPropertyRelative("strengthBonus").intValue = 0;
				newItem.FindPropertyRelative("agilityBonus").intValue = 0;
				newItem.FindPropertyRelative("mindBonus").intValue = 0;
				newItem.FindPropertyRelative("healthBonus").intValue = 0;
				foldoutStatus.Add(true);
			}
			if (GUILayout.Button("Clear"))
				items.ClearArray();
		}
		EditorGUILayout.EndHorizontal();

		//Draw controls for each item
		int itemMarkedForDelete = -1;
		for (int i = 0; i < items.arraySize; i++)
		{
			foldoutStatus[i] = EditorGUILayout.Foldout(foldoutStatus[i], items.GetArrayElementAtIndex(i).FindPropertyRelative("itemName").stringValue, GetFoldoutStyle());
			if (foldoutStatus[i])
			{
				EditorGUILayout.PropertyField(items.GetArrayElementAtIndex(i));
				EditorGUILayout.Space();
				if (GUILayout.Button("DELETE ITEM", GUILayout.Width(100f)))
					itemMarkedForDelete = i;
				EditorGUILayout.Space();
			}
		}

		//check if an item should be deleted
		if (itemMarkedForDelete >= 0)
		{
			items.DeleteArrayElementAtIndex(itemMarkedForDelete);
			foldoutStatus.RemoveAt(itemMarkedForDelete);
		}

		//save changes
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(_target);
			AssetDatabase.SaveAssets();
		}
	}

	private GUIStyle GetHeaderStyle() {
		
		GUIStyle headerStyle = new GUIStyle(EditorStyles.label);
		
		headerStyle.fontSize = 12;
		headerStyle.fontStyle = FontStyle.Bold;
		
		return headerStyle;
	}

	private GUIStyle GetFoldoutStyle() {
		
		GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);

		foldoutStyle.fontSize = 11;
		foldoutStyle.fontStyle = FontStyle.Bold;
		
		return foldoutStyle;
	}
}
