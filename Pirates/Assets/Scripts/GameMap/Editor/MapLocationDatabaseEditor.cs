using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MapLocationDatabase))]
public class MapLocationDatabaseEditor : Editor{

	private MapLocationDatabase _target;
	private List<bool> foldoutStatus;
	
	void OnEnable() {
		_target = (MapLocationDatabase) target;
		
		if (_target.locations == null)
		{
			Debug.Log("Creating new locations dictionary");
			_target.locations = new List<MapLocationData>();
		}
		
		foldoutStatus = new List<bool>();
		for (int i = 0; i < _target.locations.Count; i++)
			foldoutStatus.Add(false);
	}

	public override void OnInspectorGUI() {
		
		var locations = serializedObject.FindProperty("locations");
		var imgResourcePath = serializedObject.FindProperty("imageResourcePath");
		
		GUILayout.Label("Map location Database", GetHeaderStyle());
		EditorGUILayout.Space();
		
		EditorGUI.BeginChangeCheck();

		//Draw image resource path property
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Image resource path");
		imgResourcePath.stringValue = EditorGUILayout.TextField(imgResourcePath.stringValue);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		{
			//Create new ItemData with default settings
			if (GUILayout.Button("Add new item"))
			{
				int newItemIndex = locations.arraySize++;
				var newItem = locations.GetArrayElementAtIndex(newItemIndex);

				var name = newItem.FindPropertyRelative("name").stringValue = "NEW LOCATION";
				var info = newItem.FindPropertyRelative("info").stringValue = "";
				var imgResourceName = newItem.FindPropertyRelative("imageResourceName").stringValue = "";
				var image = newItem.FindPropertyRelative("image").objectReferenceValue = null;

				foldoutStatus.Add(true);
			}
			if (GUILayout.Button("Clear"))
				locations.ClearArray();
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		//Draw controls for each item
		GUILayout.Label("Locations", GetHeaderStyle());
		GUILayout.Label("----------------------------------------------------------", GetHeaderStyle());

		int itemMarkedForDelete = -1;
		for (int i = 0; i < locations.arraySize; i++)
		{
			foldoutStatus[i] = EditorGUILayout.Foldout(foldoutStatus[i], locations.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue, GetFoldoutStyle());
			if (foldoutStatus[i])
			{
				EditorGUILayout.PropertyField(locations.GetArrayElementAtIndex(i));
				EditorGUILayout.Space();
				if (GUILayout.Button("DELETE ITEM", GUILayout.Width(100f)))
					itemMarkedForDelete = i;
				EditorGUILayout.Space();
			}
		}
		
		//check if an item should be deleted
		if (itemMarkedForDelete >= 0)
		{
			locations.DeleteArrayElementAtIndex(itemMarkedForDelete);
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
