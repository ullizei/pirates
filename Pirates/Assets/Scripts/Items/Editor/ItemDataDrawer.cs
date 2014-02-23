using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ItemData))]
public class ItemDataDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		var itemName = property.FindPropertyRelative("itemName");
		var itemType = property.FindPropertyRelative("itemType");
		var users = property.FindPropertyRelative("users");
		var levelReq = property.FindPropertyRelative("levelRequirement");

		var hpBonus = property.FindPropertyRelative("hpBonus");
		var swaggerBonus = property.FindPropertyRelative("swaggerBonus");
		var strengthBonus = property.FindPropertyRelative("strengthBonus");
		var agilityBonus = property.FindPropertyRelative("agilityBonus");
		var mindBonus = property.FindPropertyRelative("mindBonus");
		var healthBonus = property.FindPropertyRelative("healthBonus");

		EditorGUI.BeginProperty (position, label, property);

		//Name
		GUILayout.Label("Item info", GetHeaderStyle());
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Name");
		itemName.stringValue = EditorGUILayout.TextField(itemName.stringValue);
		EditorGUILayout.EndHorizontal();

		//Type
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Type");
		ItemType selectedType = (ItemType) EditorGUILayout.EnumPopup((ItemType) itemType.enumValueIndex);
		itemType.enumValueIndex = (int) selectedType;
		EditorGUILayout.EndHorizontal();

		//Users
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Usable by");
		ItemUserGroup selectedGroup = (ItemUserGroup) EditorGUILayout.EnumPopup((ItemUserGroup) users.enumValueIndex);
		users.enumValueIndex = (int) selectedGroup;
		EditorGUILayout.EndHorizontal();

		//Level requirement
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Level");
		levelReq.intValue = EditorGUILayout.IntField(levelReq.intValue);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		//Bonuses...
		GUILayout.Label("Stat modifiers", GetHeaderStyle());
		swaggerBonus.intValue = EditorGUILayout.IntField("Swagger bonus", swaggerBonus.intValue);
		strengthBonus.intValue = EditorGUILayout.IntField("Strength bonus", strengthBonus.intValue);
		agilityBonus.intValue = EditorGUILayout.IntField("Agility bonus", agilityBonus.intValue);
		mindBonus.intValue = EditorGUILayout.IntField("Mind bonus", mindBonus.intValue);
		healthBonus.intValue = EditorGUILayout.IntField("Health bonus", healthBonus.intValue);
		hpBonus.intValue = EditorGUILayout.IntField("HP bonus", hpBonus.intValue);

		EditorGUI.EndProperty();
	}



	private GUIStyle GetHeaderStyle() {
		
		GUIStyle headerStyle = new GUIStyle();
		
		headerStyle.fontSize = 11;
		headerStyle.fontStyle = FontStyle.BoldAndItalic;
		
		return headerStyle;
	}
}
