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

		valueText.text = value.ToString();
		Color textColor = StatsPanel.textColorNormal;

		if (diff >= 0) {
			modifierText.text = "(+"+modifier+")";
			textColor = StatsPanel.textColorGreen;
		}
		else {
			modifierText.text = "("+modifier+")";
			textColor = StatsPanel.textColorRed;
		}

		valueText.color = textColor;
		modifierText.color = textColor;
	}
}
