using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(EnemyDatabase))]
public class EnemyDatabaseEditor : Editor {

	private EnemyDatabase _target;
	private List<bool> foldoutStatus;

	void OnEnable() {
		_target = (EnemyDatabase) target;
		
		if (_target.enemies == null)
		{
			Debug.Log("Creating new enemy list");
			_target.enemies = new List<CharacterData>();
		}
		
		foldoutStatus = new List<bool>();
		for (int i = 0; i < _target.enemies.Count; i++)
			foldoutStatus.Add(false);
		
	}

	public override void OnInspectorGUI() {
		
		var enemies = serializedObject.FindProperty("enemies");
		EditorGUI.BeginChangeCheck();

		GUILayout.Label("Enemy Database", MyTools.GetHeaderStyle());
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		{
			//Create new ItemData with default settings
			if (GUILayout.Button("Add new enemy"))
			{
				int newEnemyIndex = enemies.arraySize++;
				var newEnemy = enemies.GetArrayElementAtIndex(newEnemyIndex);
//				newItem.FindPropertyRelative("itemName").stringValue = "NEW ITEM";
//				newItem.FindPropertyRelative("itemDescription").stringValue = "";
//				newItem.FindPropertyRelative("itemType").enumValueIndex = (int) ItemType.NONE;
//				newItem.FindPropertyRelative("users").enumValueIndex = (int) ItemUserGroup.All;
//				newItem.FindPropertyRelative("rarity").enumValueIndex = (int) ItemRarity.Common;
//				newItem.FindPropertyRelative("levelRequirement").intValue = 1;
//				newItem.FindPropertyRelative("hpBonus").intValue = 0;
//				newItem.FindPropertyRelative("swaggerBonus").intValue = 0;
//				newItem.FindPropertyRelative("strengthBonus").intValue = 0;
//				newItem.FindPropertyRelative("agilityBonus").intValue = 0;
//				newItem.FindPropertyRelative("mindBonus").intValue = 0;
//				newItem.FindPropertyRelative("healthBonus").intValue = 0;
				foldoutStatus.Add(true);
			}
			if (GUILayout.Button("Clear"))
				enemies.ClearArray();
		}
		EditorGUILayout.EndHorizontal();

		//save changes
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(_target);
			AssetDatabase.SaveAssets();
		}
	}

}
