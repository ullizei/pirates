using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourcePanel : MonoBehaviour {
	
	public Text amountOfWater;
	public Text amountOfRum;
	public Text amountOfOranges;
	public Text amountOfProvisions;
	public Text amountOfGold;

	private static ResourcePanel instance;

	// Use this for initialization
	void Start () {
		instance = this;

		UpdateAmounts();
	}
	
	public static void UpdateAmounts() {

		if (instance != null) 
		{
			instance.amountOfGold.text = CrewInventory.Instance.GetResourceAmount(ResourceType.Gold).ToString();
			instance.amountOfWater.text = "x"+CrewInventory.Instance.GetResourceAmount(ResourceType.Water);
			instance.amountOfRum.text = "x"+CrewInventory.Instance.GetResourceAmount(ResourceType.Rum);
			instance.amountOfOranges.text = "x"+CrewInventory.Instance.GetResourceAmount(ResourceType.Oranges);
			instance.amountOfProvisions.text = "x"+CrewInventory.Instance.GetResourceAmount(ResourceType.Provisions);
		}
	}

	void OnDestroy() {
		instance = null;
	}
}
