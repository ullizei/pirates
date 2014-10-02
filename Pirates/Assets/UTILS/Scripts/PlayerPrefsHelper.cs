using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


// This class saves data objects as strings in player prefs.
// Mark the class you want to save objects from with [System.Serializable]. 
// The class must be a regular C# class and members must be simple types or marked as [System.NonSerialized]. Classes extending UnityEngine.Object can not be serialized (=no monobehaviours, vector3, etc D-:).


public static class PlayerPrefsHelper {
	
	//private static System.Collections.Generic.Dictionary<string, object> hash;
	
	public static void SaveObject<T>(T _object, string playerPrefsKey) {
		#if UNITY_IPHONE
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
		
		System.Type objectType = typeof(T);
		
		if (objectType.IsSerializable) {
			
			if (_object != null) {
				
				if (playerPrefsKey != "") {
					BinaryFormatter formatter = new BinaryFormatter();
					MemoryStream memStream = new MemoryStream();
					
					formatter.Serialize(memStream, _object);
					
					PlayerPrefs.SetString(playerPrefsKey, System.Convert.ToBase64String(memStream.GetBuffer()));
					memStream.Close();
				}
				else
					Debug.Log("Could not save object. Keystring was empty.");
			}
			else
				Debug.Log("Could not save object. Object was null.");
		}
		else
			Debug.Log("Could not save object. "+objectType.ToString()+" is not serializable.");
	}
	
	public static object LoadObject(string playerPrefsKey) {
		#if UNITY_IPHONE
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
		
		if (PlayerPrefs.HasKey(playerPrefsKey)) {
			string data = PlayerPrefs.GetString(playerPrefsKey);
			
			if (data != "") {
				BinaryFormatter formatter = new BinaryFormatter();
				MemoryStream memStream = new MemoryStream(System.Convert.FromBase64String(data));
				
				return formatter.Deserialize(memStream);
				memStream.Close();
			}
			else
				Debug.Log("Could not load object. String matching key "+playerPrefsKey+" was empty");
		}
		else
			Debug.Log("Could not load object. Key "+playerPrefsKey+" does not exist in PlayerPrefs");
		
		return null;
	}
}
