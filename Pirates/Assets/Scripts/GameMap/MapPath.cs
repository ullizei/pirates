using UnityEngine;
using System.Collections.Generic;

public class MapPath {

	public List<MapNode> path;
	public float length;
    public float combinedRisk;


	public MapPath() {

		path = new List<MapNode>();
		length = 0f;
        combinedRisk = 0f;
	}

	public MapPath(MapPath toCopy) {

		path = new List<MapNode>();
		for (int i = 0; i < toCopy.path.Count; i++) {
			path.Add(toCopy.path[i]);
		}
		length = toCopy.length;
        combinedRisk = toCopy.combinedRisk;
	}

	public MapNode EndNode {
		get {
			if (path.Count > 0)
				return path[path.Count-1];
			else
				return null;
		}
	}

	public void AddNode(MapNode node) {

		path.Add(node);

		if (path.Count > 1) {
			NodeConnection connection = path[path.Count-2].GetConnectionToNode(path[path.Count-1]);
			length += connection.GetDistance();
            combinedRisk += connection.risk;
		}
	}

	public bool IsComplete(MapNode start, MapNode end) {

		if (path.Count > 0) {
			if (path[0] == start && path[path.Count-1] == end)
				return true;
		}
		return false;
	}

    public float GetRiskAverage()
    {
        if (path.Count > 0)
            return combinedRisk / ((float)path.Count);
        else
            return 0f;
    }

	public override string ToString()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		MapLocationData locationData;

		for (int i = 0; i < path.Count; i++)
		{
			locationData = MapLocationDatabase.Instance.GetLocationData(path[i].locationID);
			stringBuilder.Append(locationData.name);

			if (i < path.Count-1)
				stringBuilder.Append(" – ");
		}

		return stringBuilder.ToString();
	}
}
