using UnityEngine;
using System.Collections.Generic;



public class NodeNavigator {
	
	public static MapPath GetMapPath(MapNode fromNode, MapNode toNode) {

		//Debug.Log("GetMapPath!");

		//create a path to begin exploring
		MapPath bestPath = new MapPath();
		bestPath.AddNode(fromNode);

		//create a list of possible paths
		List<MapPath> paths = new List<MapPath>();
		paths.Add(bestPath);

		List<NodeConnection> newConnections;
		MapNode currentNode;
		MapNode tempNode;
		while (bestPath != null) {

			//Debug.Log ("-----------------------START LOOP!");

			if (bestPath.IsComplete(fromNode, toNode)) {
				//Debug.Log("Found Complete path!");
				break;
			}

			currentNode = bestPath.EndNode;
			newConnections = currentNode.GetConnections();

			Debug.Log("Current node "+currentNode.name);

			for (int i = 0; i < newConnections.Count; i++)
			{
				tempNode = newConnections[i].GetOppositeEnd(currentNode);
				if (!bestPath.path.Contains(tempNode)) {
					//Debug.Log("Added new path!");
					MapPath newPath = new MapPath(bestPath);
					newPath.AddNode(tempNode);
					paths.Add(newPath);
				}
			}

			paths.Remove(bestPath);
			bestPath = GetBestPath(paths);
		}

		return bestPath;
	}

	private static MapPath GetBestPath(List<MapPath> paths) {

		//Debug.Log (string.Format("Get best of {0} paths...", paths.Count));

		if (paths.Count > 0)
		{
			int indexOfBestPath = 0;
			float shortestLength = paths[indexOfBestPath].length;

			for (int i = 0; i < paths.Count; i++)
			{
				if (paths[i].length < shortestLength) {
					indexOfBestPath = i;
					shortestLength = paths[i].length;
				}
			}

			return paths[indexOfBestPath];
		}
		else
			return null;
	}
}

