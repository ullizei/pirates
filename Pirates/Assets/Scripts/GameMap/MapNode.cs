using UnityEngine;
using System.Collections.Generic;


public class MapNode : InputHandler {

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
			if (ShipPositionMarker.Instance.TravelToPort(this))
			{
				SoundManager.PlaySfx(Sfx.Click);
			}
		}
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

	void OnDestroy() {
		ListenForInput(false);
	}
}
