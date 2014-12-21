using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectButton : MonoBehaviour {

	public Text nameTag;
	public GameObject selectionFrame;

	private Button button;
	private Character character;

	public static CharacterSelectButton Create(Character _character) {

		GameObject obj = (GameObject) Instantiate(Resources.Load("GUI/CharacterSelectBar/CharacterSelectButton"));
		CharacterSelectButton charSelectButton = obj.GetComponent<CharacterSelectButton>();
		charSelectButton.Init(_character);
		return charSelectButton;
	}

	private void Init(Character _character) {
		character = _character;
		selectionFrame.SetActive(false);
		nameTag.text = character.Name;
		button = GetComponent<Button>();
		button.onClick.AddListener(this.OnClick);
	}

	public void OnClick() {
		Select();
	}

	public void Select() {
		selectionFrame.SetActive(true);
		CrewInspector.Instance.OnSelectedCharacter(character);
	}
	
	public void Deselect() {
		if (character != CrewInspector.Instance.CurrentInspectedCharacter)
			selectionFrame.SetActive(false);
	}
}
