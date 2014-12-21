using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupPanel : MonoBehaviour {

	public Button okButton;
	public Text infoText;
	public Text headerText;

	private GameObject _gameObject;

	// Use this for initialization
	void Start () {
		_gameObject = gameObject;
		okButton.onClick.AddListener(this.OnClickedOkButton);

		_gameObject.SetActive(false);
	}

	public void Show(string info, string header) {
		_gameObject.SetActive(true);
		infoText.text = info;
		headerText.text = header;
	}
	
	private void OnClickedOkButton() {
		_gameObject.SetActive(false);
	}
}
