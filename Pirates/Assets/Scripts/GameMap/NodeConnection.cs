using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NodeConnection : MonoBehaviour {

	public MapNode point1, point2;
	public List<Vector3> path;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnDrawGizmos() {

		if (point1 != null && point2 != null)
		{
			Gizmos.color = Color.red;
			Vector3 from = point1.transform.position;

			foreach (Vector3 node in path)
			{
				Gizmos.DrawLine(from, node);
				from = node;
			}
			Gizmos.DrawLine(from, point2.transform.position);
		}
	}
}
