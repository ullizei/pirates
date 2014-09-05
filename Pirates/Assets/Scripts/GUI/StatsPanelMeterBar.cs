using UnityEngine;
using System.Collections;

public class StatsPanelMeterBar : MonoBehaviour {

	private GameObject meterBar;
	private TextMesh headerText;
	private TextMesh modifierText;

	private int maxValue;
	private int currentValue;
	private int valueModifier = 0;
	private string header;
	private Color meterColor;


	public static StatsPanelMeterBar Create(int _maxValue, int _currentValue, string meterBarHeader, Color color) {

		GameObject meterBarObject = (GameObject) Instantiate(Resources.Load("GUI/StatsPanelMeterBar"));
		StatsPanelMeterBar statsPanelMeterBar = meterBarObject.GetComponent<StatsPanelMeterBar>();

		statsPanelMeterBar.maxValue = _maxValue;
		statsPanelMeterBar.currentValue = _currentValue;
		statsPanelMeterBar.header = meterBarHeader;
		statsPanelMeterBar.meterColor = color;
		statsPanelMeterBar.Init();

		return statsPanelMeterBar;
	}

	// Use this for initialization
	void Start () {



	}

	public void Init() {


	}

	public void UpdateValueModifier(int amount) {

		valueModifier += amount;
		ResizeMeter();
		SetText();
	}

	private void SetupMeterBar() {

		meterBar = gameObject.GetChildByName("MeterBar");
		headerText = gameObject.GetChildByName("InfoText").GetComponent<TextMesh>();
		modifierText = gameObject.GetChildByName("ModifierText").GetComponent<TextMesh>();

		meterBar.renderer.material.color = meterColor;
		ResizeMeter();
		SetText();
	}

	private void SetText() {

		int modifiedMaxValue = maxValue + valueModifier;
		headerText.text = header+": "+currentValue+"/"+modifiedMaxValue;

		if (valueModifier != 0)
		{
			modifierText.text = "("+valueModifier+")";
			if (valueModifier < 0)
				modifierText.color = Color.red;
			else
				modifierText.color = Color.green;
		}
		else
			modifierText.text = "";
	}
	
	private void ResizeMeter() {

		int modifiedMaxValue = maxValue + valueModifier;
		float metersize = (float)currentValue / (float)modifiedMaxValue;
		float fullMeterSize = meterBar.transform.localScale.x;
		meterBar.transform.localScale = new Vector3(fullMeterSize*metersize, meterBar.transform.localScale.y, 1f);
		meterBar.transform.localPosition = Vector3.right * (meterBar.transform.localScale.x / 2f);
	}	
}
