using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stat : MonoBehaviour {

	public Text labelText;
	public Text valueText;
	public Text modifierText;

	private int value;
	private int modifier;

	private StatType statType;
	private Button button;
	private RectTransform rectTransform;

	private ToolTipPanel statInfo = null;

	public static Stat Create(StatType type) {

		GameObject obj = (GameObject) Instantiate(Resources.Load("GUI/StatsPanel/Stat"));
		Stat stat = obj.GetComponent<Stat>();
		stat.Init(type);
		return stat;
	}

	// Use this for initialization
	void Init(StatType type) {

		statType = type;
		labelText.text = statType.ToString() +":";
	}

	void Start() {
		rectTransform = GetComponent<RectTransform>();
		button = GetComponent<Button>();
		button.onClick.AddListener(this.ShowInfo);
	}

	public void ShowInfo() {
		if (statInfo == null) {
			statInfo = ToolTipPanel.Show(CrewInspector.Instance.transform, rectTransform, new Vector3(rectTransform.rect.width, 0f, 0f));
			statInfo.SetText(ToolTipStrings.StatInfo(statType), GetToolTipInfoLabel());
		}
	}

	public string GetToolTipInfoLabel() {
		return statType.ToString();
	}

	public void SetValueAndModifier(int _value, int _modifier) {

		value = _value;
		modifier = _modifier;

		valueText.text = value.ToString();

		if (modifier > 0)
			modifierText.text = "(+"+modifier+")";
		else if (modifier < 0)
			modifierText.text = "("+modifier+")";
		else
			modifierText.text = "";

		valueText.color = StatsPanel.textColorNormal;
		modifierText.color = StatsPanel.textColorNormal;
	}	

	public void CompareValue(int newValue) {

		int diff = newValue - value;

		valueText.text = newValue.ToString();
		Color textColor = StatsPanel.textColorNormal;

		if (diff == 0) {
			modifierText.text = "";
			textColor = StatsPanel.textColorNormal;
		}
		if (diff > 0) {
			modifierText.text = "(+"+diff+")";
			textColor = StatsPanel.textColorGreen;
		}
		else if (diff < 0) {
			modifierText.text = "("+diff+")";
			textColor = StatsPanel.textColorRed;
		}

		valueText.color = textColor;
		modifierText.color = textColor;
	}
}
