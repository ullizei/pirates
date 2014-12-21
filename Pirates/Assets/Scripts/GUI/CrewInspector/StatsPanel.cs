using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatsPanel : MonoBehaviour {

	//colors!
	public static Color textColorRed = Color.red;
	public static Color textColorGreen = new Color(78f/255f, 178f/255f, 54f/255f, 1f);
	public static Color textColorNormal = Color.grey;

	private RectTransform rect;

	private StatsPanelMeterBar expMeter;
	private StatsPanelMeterBar hpMeter;
	
	private Text inspectedCharacterName;
	private Text inspectedCharacterJobLevel;

	private Dictionary<StatType, Stat> stats;

	//private CharacterData inspectedCharacterData;


	private string resourcePath = "GUI/StatsPanel/";

	// Use this for initialization
	public void Init () {
		rect = GetComponent<RectTransform>();
		LayoutElements();
	}

	public void ShowCharacterStats(Character selectedCharacter)
	{
		inspectedCharacterName.text = selectedCharacter.Name;

		StatType[] statTypes = (StatType[]) System.Enum.GetValues(typeof(StatType));
		int value;
		int modifier;
		for (int i = 0; i < statTypes.Length; i++)
		{
			value = selectedCharacter.GetBaseStat(statTypes[i]);
			modifier = selectedCharacter.GetStatModifier(statTypes[i]);
			stats[statTypes[i]].SetValueAndModifier(value+modifier, modifier);
		}

		expMeter.UpdateMeter(75, 200, 0f);
		hpMeter.UpdateMeter(23, 52, 0f);

		//		inspectedCharacterData = selecetdCharacter;
		//		inspectedCharacterName.text = selecetdCharacter.name;
		//		inspectedCharacterJobLevel.text = "Level "+selecetdCharacter.level+" "+selecetdCharacter.job;
	}

	public void ShowStatChanges() {

		Character selectedCharacter = CrewInspector.Instance.CurrentInspectedCharacter;
		CharacterStats altStats = selectedCharacter.GetStatsIfEquippedItem(CrewInspector.Instance.CurrentInspectedItem, CrewInspector.Instance.CurrentInspectedSlot);

		StatType[] statTypes = (StatType[]) System.Enum.GetValues(typeof(StatType));
		for (int i = 0; i < statTypes.Length; i++)
		{
			stats[statTypes[i]].CompareValue(altStats.GetStat(statTypes[i]));
		}
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
}
