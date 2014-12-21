using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemSelectButton : MonoBehaviour {
	
	public Text itemName;
	public Text itemCounter;
	public Image itemImage;
	public GameObject itemSelectFrame;

	private ItemData itemData;
	private Button button;

	public static ItemSelectButton Create(ItemData data) {

		GameObject button = Instantiate(Resources.Load("GUI/CrewInspector/ItemSelectButton")) as GameObject;
		ItemSelectButton buttonScript = button.GetComponent<ItemSelectButton>();
		buttonScript.Init(data);
		return buttonScript;
	}

	public void Init(ItemData data) {

		itemData = data;
		itemName.text = itemData.itemName;
		itemImage.sprite = data.LoadItemIcon();
		SetItemCounter();
		itemSelectFrame.SetActive(false);
		button = GetComponent<Button>();
		button.onClick.AddListener(this.Select);
	}

	public void Select() {
		itemSelectFrame.SetActive(true);
		CrewInspector.Instance.OnSelectedItem(itemData);
	}

	public void Deselect() {
		if (itemData != CrewInspector.Instance.CurrentInspectedItem)
			itemSelectFrame.SetActive(false);
	}

	private void SetItemCounter() {

		int totalAmount;
		int freeAmount;
		CrewInventory.Instance.GetItemAmounts(itemData.itemName, out totalAmount, out freeAmount);
		itemCounter.text = freeAmount.ToString() + "/" + totalAmount.ToString();
	}
}
