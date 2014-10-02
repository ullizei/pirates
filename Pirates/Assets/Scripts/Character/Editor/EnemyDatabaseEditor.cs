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

		EditorGUI.BeginChangeCheck();

		var enemies = serializedObject.FindProperty("enemies");

		GUILayout.Label("Enemy Database", MyTools.GetHeaderStyle());
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Add new enemy"))
			{
				//_target.AddNewEnemy();
				int newEntry = enemies.arraySize++;
				foldoutStatus.Add(true);
				_target.InitEntry(newEntry);
			}

			if (GUILayout.Button("Clear"))
				enemies.ClearArray();
		}
		EditorGUILayout.EndHorizontal();

		//Draw controls for each item
		int itemMarkedForDelete = -1;
		for (int i = 0; i < enemies.arraySize; i++)
		{
			foldoutStatus[i] = EditorGUILayout.Foldout(foldoutStatus[i], enemies.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue, GetFoldoutStyle());
			if (foldoutStatus[i])
			{
				EditorGUILayout.PropertyField(enemies.GetArrayElementAtIndex(i));
				EditorGUILayout.Space();
				if (GUILayout.Button("DELETE", GUILayout.Width(100f)))
					itemMarkedForDelete = i;
				EditorGUILayout.Space();
			}
		}

		if (itemMarkedForDelete >= 0)
			enemies.DeleteArrayElementAtIndex(itemMarkedForDelete);

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
