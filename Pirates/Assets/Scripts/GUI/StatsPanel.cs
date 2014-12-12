using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsPanel : MonoBehaviour {

	private RectTransform rect;
	private Bounds panelBounds;


	private Text inspectedCharacterName;
	private Text inspectedCharacterJobLevel;

	private CharacterData inspectedCharacterData;


	private string resourcePath = "GUI/StatsPanel/";

	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform>();
		//panelBounds = rect.
		LayoutElements();
	}

	private void LayoutElements() {

		float margin = 10f;
		float offset = 0f;
		GameObject element;
		RectTransform elemRect;

		//Character name
		element = (GameObject) Instantiate(Resources.Load(resourcePath+"CharacterName"));
		elemRect = element.GetComponent<RectTransform>();
		elemRect.SetParent(rect, false);

		//Character job and level
		element = (GameObject) Instantiate(Resources.Load(resourcePath+"CharacterJobLevel"));
		elemRect = element.GetComponent<RectTransform>();
		elemRect.SetParent(rect, false);
		offset += 60f;
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offset, 10f);

		//Experience meter
		//element = (GameObject) Instantiate(Resources.Load (resourcePath+"MeterBar"));
		elemRect = StatsPanelMeterBar.Create("EXP", Color.blue); //element.GetComponent<RectTransform>();
		elemRect.SetParent(rect, false);
		offset += 45f;
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offset, 10f);
		//elemRect.GetComponent<StatsPanelMeterBar>().UpdateMeter(78, 254, -9);

		//HP meter
		//element = (GameObject) Instantiate(Resources.Load (resourcePath+"MeterBar"));
		elemRect = StatsPanelMeterBar.Create("HP", Color.green); //element.GetComponent<RectTransform>();
		elemRect.SetParent(rect, false);
		offset += 50f;
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offset, 10f);

		//Stats label
		element = (GameObject) Instantiate(Resources.Load (resourcePath+"StatsLabel"));
		elemRect = element.GetComponent<RectTransform>();
		elemRect.SetParent(rect, false);
		offset += 40f;
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offset, 10f);

		//Stats...
		offset += 20f;
		for (int i = 0; i < 5; i++)
		{
			element = (GameObject) Instantiate(Resources.Load (resourcePath+"Stat"));
			elemRect = element.GetComponent<RectTransform>();
			elemRect.SetParent(rect, false);
			offset += 25f;
			elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offset, 10f);
		}


		//elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, margin, 100f);
		//elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, margin, 100f);
	}
	
	void ShowStats(CharacterData selecetdCharacter)
	{
//		inspectedCharacterData = selecetdCharacter;
//		inspectedCharacterName.text = selecetdCharacter.name;
//		inspectedCharacterJobLevel.text = "Level "+selecetdCharacter.level+" "+selecetdCharacter.job;
	}
}
