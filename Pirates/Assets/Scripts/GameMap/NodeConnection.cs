using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NodeConnection : MonoBehaviour {

	public MapNode point1, point2;
    public float risk;
	public List<Vector3> path;

	private float distance;

	// Use this for initialization
	void Start () {

		distance = CalculateDistance();

		if (point1 !=  null && point2 != null)
		{
			point1.AddConnection(this);
			point2.AddConnection(this);
		
			DrawConnection();
		}
	}

	public void DrawConnection() {

		CR_Spline crSpline = GetCRSpline(point2);
		int segments = 20;

		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetVertexCount(segments);
		lineRenderer.SetWidth(5f, 5f);
		lineRenderer.material = (Material) Resources.Load("MapConnectionMaterial");

		float t;
		for (int i = 0; i < segments; i++)
		{
			t = (float)i/(float)(segments-1);
			lineRenderer.SetPosition(i, crSpline.Interp(t));
		}
	}

	private float CalculateDistance() {

		float dist = 0f;
		if (point1 !=  null && point2 != null)
		{
			Vector3 currentPoint = point1.transform.position;
			float dY, dX;
			foreach (Vector3 pos in path)
			{
				dX = Mathf.Abs(pos.x-currentPoint.x);
				dY = Mathf.Abs(pos.y - currentPoint.y);
				dist += Mathf.Sqrt(Mathf.Pow(dX, 2f) + Mathf.Pow(dY, 2f));
				currentPoint = pos;
			}
			dX = Mathf.Abs(point2.transform.position.x - currentPoint.x);
			dY = Mathf.Abs(point2.transform.position.y - currentPoint.y);
			dist += Mathf.Sqrt(Mathf.Pow(dX, 2f) + Mathf.Pow(dY, 2f));
		}
		return dist;
	}

	public MapNode GetOppositeEnd(MapNode point) {

		if (point == point1)
			return point2;
		else
			return point1;
	}

	public bool ConnectsToNode(MapNode node) {

		if (node == point1 || node == point2)
			return true;
		else
			return false;
	}

	public CR_Spline GetCRSpline(MapNode endPoint) {

		Vector3[] points = new Vector3[path.Count + 2];

		if (endPoint == point2)
		{
			points[0] = point1.transform.position;

			for (int i = 0; i < path.Count; i++)
				points[i+1] = path[i];

			points[points.Length-1] = point2.transform.position;
		}
		else if (endPoint == point1)
		{
			points[0] = point2.transform.position;

			for (int i = 0; i < path.Count; i++)
				points[i+1] = path[path.Count-1-i];

			points[points.Length-1] = point1.transform.position;
		}
		return new CR_Spline(points);
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

	public float Heuristic() {
		return distance;
	}

	public float GetDistance() {
		return distance;
	}
}
