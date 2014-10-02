using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(CharacterData))]
public class CharacterDataDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		var characterName = property.FindPropertyRelative("name");
		var characterDescription = property.FindPropertyRelative("description");
		var characterStats = property.FindPropertyRelative("stats");

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Name");
		characterName.stringValue = EditorGUILayout.TextField(characterName.stringValue);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Description");
		characterDescription.stringValue = EditorGUILayout.TextField(characterDescription.stringValue);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.PropertyField(characterStats);
		//Name
//		GUILayout.Label("Item info", GetHeaderStyle());
//		EditorGUILayout.BeginHorizontal();
//		EditorGUILayout.PrefixLabel("Name");
//		itemName.stringValue = EditorGUILayout.TextField(itemName.stringValue);
//		EditorGUILayout.EndHorizontal();
	}

	private GUIStyle GetHeaderStyle() {
		
		GUIStyle headerStyle = new GUIStyle(EditorStyles.label);
		
		headerStyle.fontSize = 11;
		headerStyle.fontStyle = FontStyle.BoldAndItalic;
		
		return headerStyle;
	}
}
