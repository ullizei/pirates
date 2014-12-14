using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectButton : MonoBehaviour {

	public Text nameTag;
	public GameObject selectionFrame;

	private Button button;
	private CharacterData character;

	public static CharacterSelectButton Create(CharacterData characterData) {

		GameObject obj = (GameObject) Instantiate(Resources.Load("GUI/CharacterSelectBar/CharacterSelectButton"));
		CharacterSelectButton charSelectButton = obj.GetComponent<CharacterSelectButton>();
		charSelectButton.Init(characterData);
		return charSelectButton;
	}

	private void Init(CharacterData characterData) {
		character = characterData;
		selectionFrame.SetActive(false);
		nameTag.text = character.name;
		button = GetComponent<Button>();
		button.onClick.AddListener(this.OnClick);
	}

	public void OnClick() {
		Select(true);
	}

	public void Select(bool select) {
		selectionFrame.SetActive(select);
	}

}
