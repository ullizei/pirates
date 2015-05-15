using UnityEngine;
using System.Collections.Generic;


public class MapNode : InputHandler {

	public string locationID = "Unknown location";

	private List<NodeConnection> connections;

	private GameObject _gameObject;

	// Use this for initialization
	void Start () {

		_gameObject = gameObject;
		_gameObject.AddComponent<CircleCollider2D>();
		ListenForInput(true);
	}
	
	public override void OnBeganTouch (SimTouch touch)
	{
		if (TouchHitTarget(touch, _gameObject))
		{
			if (ShipPositionMarker.Instance.currentPort != this)
				LocationInfoPanel.Open(this);
//			if (ShipPositionMarker.Instance.TravelToPort(this))
//			{
//				SoundManager.PlaySfx(Sfx.Click);
//			}
		}
	}

	public List<NodeConnection> GetConnections() {
		return connections;
	}

	public NodeConnection GetConnectionToNode(MapNode node) {

		foreach (NodeConnection connection in connections)
		{
			if (connection.ConnectsToNode(node))
				return connection;
		}
		return null;
	}

	public void AddConnection(NodeConnection connection) {

		if (connections == null)
			connections = new List<NodeConnection>();

		connections.Add (connection);
	}

	public float GetDistanceToNode(MapNode target) {

		float dX = target.transform.position.x - transform.position.x;
		float dY = target.transform.position.y - transform.position.y;
		return Mathf.Sqrt(dX*dX + dY*dY);
	}


	void OnDestroy() {
		ListenForInput(false);
	}
}
