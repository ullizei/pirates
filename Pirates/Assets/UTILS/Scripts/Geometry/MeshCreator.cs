using UnityEngine;
using System.Collections;

public class MeshCreator : MonoBehaviour {

	void Start() {

		CreatePlane(Vector2.one * 4f);
	}

	public static GameObject CreatePlane(Vector2 size) {

		size *= 0.5f;
		Mesh planeMesh = new Mesh();
		planeMesh.name = "CustomPlane";

		//set vertices
		Vector3[] vertices = {
			new Vector3(-size.x, -size.y, 0f),
			new Vector3(size.x, -size.y, 0f),
			new Vector3(size.x, size.y, 0f),
			new Vector3(-size.x, size.y, 0f)};
		planeMesh.vertices = vertices;
		//set uvs
		Vector2[] uvs = {Vector2.zero, Vector2.up, Vector2.one, Vector2.right};
		planeMesh.uv = uvs;
		//set triangles
		int[] triangles = {0, 1, 2, 0, 2, 3};
		planeMesh.triangles = triangles;
		planeMesh.RecalculateNormals();

		//Create gameobject
		GameObject plane = new GameObject("CustomPlane");
		MeshFilter _meshFilter = plane.AddComponent<MeshFilter>();
		_meshFilter.mesh = planeMesh;
		plane.AddComponent<MeshRenderer>();
		plane.AddComponent<MeshCollider>();
		plane.transform.position = Vector3.zero;

		return plane;
	}

}