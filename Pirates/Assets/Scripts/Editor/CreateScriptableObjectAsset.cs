using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

class CreateScriptableObjectAsset : EditorWindow
{
	[MenuItem ("MyTools/Create ScriptableObject asset")]
	public static void  ShowWindow ()
	{
		var assembly = System.Reflection.Assembly.Load (new System.Reflection.AssemblyName ("Assembly-CSharp"));
		var allScriptableObjects = (from t in assembly.GetTypes ()
		                            where t.IsSubclassOf (typeof(ScriptableObject))
		                            select t).ToArray ();

		CreateScriptableObjectAsset window = (CreateScriptableObjectAsset)EditorWindow.GetWindow (typeof(CreateScriptableObjectAsset));
		window.Types = allScriptableObjects;
	}

	private int selectedTypeIndex;
	private string assetName = "";

	private string[] names;
	private Type[] types;
	
	public Type[] Types { 
		get { return types; }
		set {
			types = value;
			names = types.Select (t => t.FullName).ToArray ();
		}
	}


	void OnGUI ()
	{
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PrefixLabel ("Asset name");
		assetName = GUILayout.TextField (assetName, GUILayout.Width (300f));
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PrefixLabel ("Asset type");
		selectedTypeIndex = EditorGUILayout.Popup (selectedTypeIndex, names, GUILayout.Width (200f));
		EditorGUILayout.EndHorizontal ();

		GUILayout.Space (10f);

		bool showButton = !string.IsNullOrEmpty (assetName) && (selectedTypeIndex >= 0 && selectedTypeIndex < types.Length);

		GUI.enabled = showButton;
		if (GUILayout.Button ("Create asset", GUILayout.Width (100f))) {
			ScriptableObject asset = ScriptableObject.CreateInstance (types [selectedTypeIndex]);
			AssetDatabase.CreateAsset (asset, "Assets/" + assetName + ".asset");
			AssetDatabase.SaveAssets ();
			Debug.Log (string.Format ("Created {0} asset!", names [selectedTypeIndex]));
		}
		GUI.enabled = true;
	}
}
