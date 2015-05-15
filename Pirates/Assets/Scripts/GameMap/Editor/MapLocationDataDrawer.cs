using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(MapLocationData))]
public class MapLocationDataDrawer : PropertyDrawer{

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		var name = property.FindPropertyRelative("name");
		var info = property.FindPropertyRelative("info");
		var imgResourceName = property.FindPropertyRelative("imageResourceName");
		var image = property.FindPropertyRelative("image");

		EditorGUI.BeginProperty (position, label, property);

		GUILayout.Label("Location info", MyTools.GetHeaderStyle());

		//Name
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Name");
		name.stringValue = EditorGUILayout.TextField(name.stringValue);
		EditorGUILayout.EndHorizontal();

		//Description/info
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Description");
		info.stringValue = EditorGUILayout.TextField(info.stringValue);
		EditorGUILayout.EndHorizontal();


		//draw sprite field if no image has been set, and path to image otherwise
		//(this is to avoid having sprites in memory when consulting the location database)
		if (string.IsNullOrEmpty(imgResourceName.stringValue) && image.objectReferenceValue == null) {
			//Image
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Image");
			image.objectReferenceValue = (Sprite) EditorGUILayout.ObjectField(image.objectReferenceValue, typeof(Sprite), false);
			EditorGUILayout.EndHorizontal();
		}
		else {
			if (image.objectReferenceValue != null) {
				imgResourceName.stringValue = ExtractAssetName(AssetDatabase.GetAssetPath(image.objectReferenceValue));
				image.objectReferenceValue = null;
				image = null;
			}

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Image resource name");
			GUILayout.Label(imgResourceName.stringValue, MyTools.GetItalicStyle());
			if (GUILayout.Button("Change", GUILayout.Width(75f))) {
				imgResourceName.stringValue = "";
				image.objectReferenceValue = null;
				image = null;
			}
			EditorGUILayout.EndHorizontal();
		}

		EditorGUI.EndProperty();
	}

	private string ExtractAssetName(string assetPath) {

		string[] splitPath = assetPath.Split(new char[] {'/', '.'}, System.StringSplitOptions.RemoveEmptyEntries);
		return splitPath[splitPath.Length-2];
	}
}
