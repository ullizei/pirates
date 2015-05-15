using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MapLocationDatabase : ScriptableObject {

	public string imageResourcePath;
	public List<MapLocationData> locations;

	private Dictionary<string, MapLocationData> locationIndex;

	private static MapLocationDatabase instance;
	public static MapLocationDatabase Instance {
		get 
		{
			if (instance == null) {
				instance = Resources.Load("MapLocationDatabase") as MapLocationDatabase;
				instance.BuildLocationDictionary();
			}
			
			return instance;
		}
	}

	private void BuildLocationDictionary() {

		locationIndex = new Dictionary<string, MapLocationData>();

		for (int i = 0; i < locations.Count; i++) {
			locationIndex.Add(locations[i].name, locations[i]);
		}
	}

	public MapLocationData GetLocationData(string locationID) {
		if (locationIndex.ContainsKey(locationID))
			return locationIndex[locationID];
		else
			return null;
	}	
}
