using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour {

	//colors!
	public static readonly Color textColorRed = Color.red;
	public static readonly Color textColorGreen = Color.green;
	public static readonly Color textColorNormal = Color.grey;

	private RectTransform rect;

	private StatsPanelMeterBar expMeter;
	private StatsPanelMeterBar hpMeter;
	
	private Text inspectedCharacterName;
	private Text inspectedCharacterJobLevel;

	private Dictionary<StatType, Stat> stats;

	private CharacterData inspectedCharacterData;


	private string resourcePath = "GUI/StatsPanel/";

	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform>();
		LayoutElements();
	}

	private void LayoutElements() {

		float margin = 10f;
		float offsetY = margin;

		RectTransform elemRect;

		//Character name
		elemRect = LayoutElement("CharacterName", margin, offsetY);
		inspectedCharacterName = elemRect.GetComponent<Text>();
		offsetY += elemRect.sizeDelta.y + 5f;

		//Character job and level
		elemRect = LayoutElement("CharacterJobLevel", margin+5f, offsetY);
		inspectedCharacterJobLevel = elemRect.GetComponent<Text>();
		offsetY += elemRect.sizeDelta.y + 5f;

		//Experience meter
		expMeter = StatsPanelMeterBar.Create("EXP", Color.blue);
		elemRect = expMeter.GetComponent<RectTransform>();
		LayoutElement(elemRect, margin+5f, offsetY);
		offsetY += elemRect.sizeDelta.y + 5f;

		//HP meter
		hpMeter = StatsPanelMeterBar.Create("HP", Color.green);
		elemRect = hpMeter.GetComponent<RectTransform>();
		LayoutElement(elemRect, margin+5f, offsetY);
		offsetY += elemRect.sizeDelta.y + 10f;

		//Stats label
		elemRect = LayoutElement("StatsLabel", margin, offsetY);
		offsetY += elemRect.sizeDelta.y + 5f;

		//Stats...
		stats = new Dictionary<StatType, Stat>();
		Stat stat;

		StatType[] statTypes = (StatType[]) System.Enum.GetValues(typeof(StatType));
		for (int i = 0; i < statTypes.Length; i++)
		{
			stat = Stat.Create(statTypes[i]);
			stats.Add(statTypes[i], stat);
			elemRect = stat.GetComponent<RectTransform>();
			LayoutElement(elemRect, margin+5f, offsetY);
			offsetY += elemRect.sizeDelta.y + 3f;
		}
	}

	private RectTransform LayoutElement(string elementPrefabName, float offsetX, float offsetY) {

		GameObject element = (GameObject) Instantiate(Resources.Load(resourcePath+elementPrefabName));
		RectTransform elemRect = element.GetComponent<RectTransform>();
		elemRect.SetParent(rect, false);
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, offsetX, elemRect.sizeDelta.x);
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offsetY, elemRect.sizeDelta.y);

		return elemRect;
	}

	private void LayoutElement(RectTransform elemRect, float offsetX, float offsetY) {

		elemRect.SetParent(rect, false);
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, offsetX, elemRect.sizeDelta.x);
		elemRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offsetY, elemRect.sizeDelta.y);
	}
	
	void ShowStats(CharacterData selecetdCharacter)
	{
//		inspectedCharacterData = selecetdCharacter;
//		inspectedCharacterName.text = selecetdCharacter.name;
//		inspectedCharacterJobLevel.text = "Level "+selecetdCharacter.level+" "+selecetdCharacter.job;
	}
}
