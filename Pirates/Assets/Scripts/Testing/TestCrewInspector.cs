using UnityEngine;
using System.Collections;

public class TestCrewInspector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.O)) {
			Debug.Log ("O");
			CrewInspector.Open();
		}
	}
}
