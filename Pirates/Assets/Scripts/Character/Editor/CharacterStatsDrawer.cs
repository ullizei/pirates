using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(CharacterStats))]
public class CharacterStatsDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		var swagger = property.FindPropertyRelative("swagger");
		var strength = property.FindPropertyRelative("strength");
		var agility = property.FindPropertyRelative("agility");
		var mind = property.FindPropertyRelative("mind");
		var health = property.FindPropertyRelative("health");


		GUILayout.Label("Stats", GetHeaderStyle());

		//Swagger
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Swagger");
		swagger.intValue = EditorGUILayout.IntField(swagger.intValue);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Strength");
		strength.intValue = EditorGUILayout.IntField(strength.intValue);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Agility");
		agility.intValue = EditorGUILayout.IntField(agility.intValue);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Mind");
		mind.intValue = EditorGUILayout.IntField(mind.intValue);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Health");
		health.intValue = EditorGUILayout.IntField(health.intValue);
		EditorGUILayout.EndHorizontal();
	}

	private GUIStyle GetHeaderStyle() {
		
		GUIStyle headerStyle = new GUIStyle(EditorStyles.label);
		
		headerStyle.fontSize = 11;
		headerStyle.fontStyle = FontStyle.BoldAndItalic;
		
		return headerStyle;
	}
}
