using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrewInspectorButton : MonoBehaviour {

	private Button button;

	// Use this for initialization
	void Start () {

		button = GetComponent<Button>();
		button.onClick.AddListener(this.OpenCrewInspector);
	}
	
	private void OpenCrewInspector() {
		if (CrewInspector.Open(transform.parent)) {
			CrewInspector.Instance.closeButton.onClick.AddListener(this.OnClosedCrewInspector);
			CameraControl.Instance.AllowScrolling = false;
			gameObject.SetActive(false);
		}
	}

	private void OnClosedCrewInspector() {
		gameObject.SetActive(true);
		CameraControl.Instance.AllowScrolling = true;
	}
}
