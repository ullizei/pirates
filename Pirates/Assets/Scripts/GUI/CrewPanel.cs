using UnityEngine;
using System.Collections.Generic;

public class CrewPanel : MonoBehaviour {

	public RectTransform characterButtonsParent;
	
	private List<CharacterSelectButton> characterButtons;


	// Use this for initialization
	void Start () {
		LoadCharacterButtonList();
	}

	private void LoadCharacterButtonList() {

		float spacing = 30f;
		float offsetX = spacing;

		CharacterSelectButton button;
		RectTransform buttonRect;
		
		characterButtons = new List<CharacterSelectButton>();
		for (int i = 0; i < Crew.Instance.CrewMembers.Count; i++)
		{
			button = CharacterSelectButton.Create(Crew.Instance.CrewMembers[i]);
			characterButtons.Add(button);
			buttonRect = button.GetComponent<RectTransform>();
			buttonRect.SetParent(characterButtonsParent, false);
			buttonRect.localPosition = new Vector3(offsetX, 0f, 0f);

	
			//buttonRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, offsetX, buttonRect.sizeDelta.x);
			//buttonRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, buttonRect.sizeDelta.y);
			offsetX += buttonRect.sizeDelta.x + spacing;
		}

		characterButtonsParent.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, offsetX);
		//characterButtonsParent.anchorMax = Vector2.one;
		//characterButtonsParent.anchorMin = Vector2.zero;
	}
}
