using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(NodeConnection))]
public class NodeConnectionEditor : Editor {

	NodeConnection _target;

	void OnEnable() {
		_target = (NodeConnection) target;

		if (_target.path == null)
			_target.path = new List<Vector3>();
	}

	public override void OnInspectorGUI() {

		EditorGUILayout.PrefixLabel("Map node 1");
		_target.point1 = (MapNode) EditorGUILayout.ObjectField(_target.point1, typeof(MapNode), true);
		EditorGUILayout.PrefixLabel("Map node 2");
		_target.point2 = (MapNode) EditorGUILayout.ObjectField(_target.point2, typeof(MapNode), true);

		if (_target.point1 != null && _target.point2 != null)
		{
			if (GUILayout.Button("Add path node"))
				AddPathNode();

			if (GUILayout.Button("Remove path node"))
				RemovePathNode();
		}

		if(GUI.changed)
			EditorUtility.SetDirty(_target);			
	}

	private void AddPathNode() {

		Vector3 newNode =(_target.point2.transform.position - _target.point1.transform.position) /2f;
		newNode += _target.point1.transform.position;
		_target.path.Add(newNode);
	}

	private void RemovePathNode() {

		if (_target.path.Count > 0)
		{
			_target.path.RemoveAt(_target.path.Count-1);
		}
	}

	void OnSceneGUI() {

	//private void DrawHandles() {

		for (int i = 0; i < _target.path.Count; i++)
			_target.path[i] = Handles.PositionHandle(_target.path[i], Quaternion.identity);
	}
}
