using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimeDrawer : MonoBehaviour {

	private static GameTimeDrawer instance;

	private Text gameTimeText;

	// Use this for initialization
	void Start () {
		instance = this;
		gameTimeText = GetComponent<Text>();
		UpdateTime();
	}

	public static void UpdateTime() {

		if (instance != null) {
			instance.gameTimeText.text = "Mars 27, 1394";
		}
	}
	
	void Destroy() {
		instance = null;
	}
}
