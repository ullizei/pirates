using UnityEngine;
using System.Collections;


//andeeee from the Unity forum's steller Catmull-Rom class ( http://forum.unity3d.com/viewtopic.php?p=218400#218400 ):
public class CR_Spline {

	private Vector3[] pts;
	
	public CR_Spline(Vector3[] path) {

		pts = new Vector3[path.Length+2];
		pts[0] = path[0];
		pts[pts.Length-1] = path[path.Length-1];

		for (int i = 0; i < path.Length; i++)
			pts[i+1] = path[i];
	}
	
	
	public Vector3 Interp(float t) {
		int numSections = pts.Length - 3;
		int currPt = Mathf.Min(Mathf.FloorToInt(t * (float) numSections), numSections - 1);
		float u = t * (float) numSections - (float) currPt;
		Vector3 a = pts[currPt];
		Vector3 b = pts[currPt + 1];
		Vector3 c = pts[currPt + 2];
		Vector3 d = pts[currPt + 3];
		return .5f*((-a+3f*b-3f*c+d)*(u*u*u)+(2f*a-5f*b+4f*c-d)*(u*u)+(-a+c)*u+2f*b);
	}	
	
	public int GetCurrentPoint(float t) {
		
		int numSections = pts.Length - 3;
		return Mathf.Min(Mathf.FloorToInt(t * (float) numSections), numSections - 1);
	}
	
	//makes the path "circular" by adding nodes from the start at the end and vice versa
	/*public void ClosePath() {
		
		Vector3[] nodes = new Vector3[pts.Length+2];
		
		System.Array.Copy(pts, 0, nodes, 1, pts.Length);
		nodes[0] = pts[pts.Length-2];
		nodes[nodes.Length-1] = pts[1];
		
		pts = nodes;
	}*/
}
