using UnityEngine;
using System.Collections.Generic;


public class MapNode : InputHandler {

	public List<MapNode> connections;

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
			Debug.Log("Clicked a port!");
		}
	}
}
