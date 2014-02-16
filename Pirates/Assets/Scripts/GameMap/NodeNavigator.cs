using UnityEngine;
using System.Collections.Generic;

public class NodeNavigator : MonoBehaviour {

	// Use this for initialization
	/*void Start () {
	
	}
	
	public List<MapNode> GetRoute(MapNode fromNode, MapNode toNode) {

		List<MapNode> closedSet = new List<MapNode>();
		List<MapNode> openSet = new List<MapNode>();
		openSet.Add(fromNode);

		return FindShortestPath(to, openSet, closedSet);
	}

	//open set: nodes to search
	//closed set: nodes already seraced
	private List<MapNode> FindShortestPath(MapNode goal, List<MapNode> openSet, List<MapNode> closedSet) {

		MapNode currentNode = GetNextNode(openSet, closedSet);
	}

	private MapNode GetNextNode(List<MapNode> openSet, List<MapNode> closedSet, MapNode goal) {

		if (openSet.Count == 0)
			return null;
		else if (openSet.Count == 1)
			return openSet[0];
		else
		{
			MapNode bestNode = null;
			for (int i = 1; i < openSet.Count; i++)
			{
				bestNode = GetBestNode(bestNode, onpenSet[i]);
			}
			return bestNode;
		}
	}
	
	private MapNode GetBestNode(MapNode node1, MapNode node2, MapNode goalNode) {

		if (node1 == null) return node2;
		if (node2 == null) return node1;

		NodeConnection c1 = node1.GetConnectionToNode(goalNode);
		NodeConnection c2 = node2.GetConnectionToNode(goalNode);

		float dist1 = node1.GetDistanceToNode(goalNode);
		float dist2 = node2.GetDistanceToNode(goalNode);

		if ((c1 == null && c2 == null) || (c1 != null && c2 != null))
		{
			if (dist1 < dist2)
				return node1;
			else
				return node2;
		}
		else
		{
			if (c1 != null)
				return node1;
			else
				return node2;
		}
	}*/
	


	/*private List<MapNode> FindShortestPath(MapNode start, MapNode end, MapNode current, List<MapNode> path) {

		NodeConnection connection = current.GetConnectionToNode(end);
		if (connection != null)
		{
			path.Add(current);
			return path;
		}
		else
		{
			List<NodeConnection> connections = from.GetConnections();
			if (connections != null)
			{
				foreach (NodeConnection c in connections)
				{
					FindShortestPath(start, end, c.GetOppositeEnd(current), path);
				}
			}
		}
	}*/
}

