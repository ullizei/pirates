using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum MeterType {
	EXP,
	HP
}

public class StatsPanelMeterBar : MonoBehaviour {

	public Text label;
	public Text modifierInfoLabel;
	public Image meterBar;

	private string header;
	private int maxValue;
	private int currentValue;
	private float valueModifier = 0f;

	private MeterType meterType;

	private Color meterColor;
	private Color orgModifierColor;

	private Button button;
	private RectTransform rectTransform;

	public static StatsPanelMeterBar Create(string meterBarHeader, Color color, MeterType type) {

		GameObject meterBarObject = (GameObject) Instantiate(Resources.Load("GUI/StatsPanel/MeterBar"));
		StatsPanelMeterBar statsPanelMeterBar = meterBarObject.GetComponent<StatsPanelMeterBar>();

		statsPanelMeterBar.header = meterBarHeader;
		statsPanelMeterBar.label.text = meterBarHeader;
		statsPanelMeterBar.meterBar.color = color;
		statsPanelMeterBar.meterType = type;
		//statsPanelMeterBar.modifierInfoLabel.text = "";
		statsPanelMeterBar.Init();

		return statsPanelMeterBar;
	}

	public void Init() {

		orgModifierColor = modifierInfoLabel.color;
	}


	// Use this for initialization
	void Start () {

		rectTransform = GetComponent<RectTransform>();
		button = GetComponent<Button>();
		button.onClick.AddListener(this.ShowInfo);

	}

	void ShowInfo() {
		ToolTipPanel toolTip = ToolTipPanel.Show(CrewInspector.Instance.transform, rectTransform, new Vector3(rectTransform.rect.width, 0f, 0f));
		toolTip.SetText(ToolTipStrings.MeterBarInfo(meterType), ToolTipStrings.MeterBarLabel(meterType));
	}

	public void UpdateMeter(int _currentValue, int _maxValue, float _valueModifier) {

		currentValue = _currentValue;
		maxValue = _maxValue;
		valueModifier = _valueModifier;

		float fill = ((float) currentValue) / ((float) maxValue);
		meterBar.fillAmount = fill;

		label.text = header + ": "+currentValue+"/"+maxValue;

		modifierInfoLabel.color = orgModifierColor;
		if (valueModifier != 0) {
			if (valueModifier > 0)
				modifierInfoLabel.text = "(+"+valueModifier+"%)";
			else
				modifierInfoLabel.text = "("+valueModifier+"%)";
		}
		else
			modifierInfoLabel.text = "";
	}

	public void ShowMaxValueUpdate(int newMaxValue) {

		float fill = ((float) currentValue) / ((float) newMaxValue);
		meterBar.fillAmount = fill;

		label.text = header + ": "+currentValue+"/"+newMaxValue;

		float valueChange = 1f - ((float) newMaxValue) / ((float) maxValue);

		if (valueChange >= 0f)
			modifierInfoLabel.text = "(+"+valueChange+"%)";
		else
			modifierInfoLabel.text = "("+valueChange+"%)";

		modifierInfoLabel.text = "("+valueModifier+")";
		if (valueChange > 0f)
			modifierInfoLabel.color = Color.green;
		else if (valueChange < 0f)
			modifierInfoLabel.color = Color.red;
		else
			modifierInfoLabel.color = orgModifierColor;
	}
}
