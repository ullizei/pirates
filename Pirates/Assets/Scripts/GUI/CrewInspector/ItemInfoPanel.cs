using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoPanel : MonoBehaviour {

	public Text itemName;
	public Text itemDescription;
	public Text itemProperties;
	public Image itemImage;
	public Button equipUnequipButton;
	public Button cancelButton;

	private bool showingEquippedItem;

	// Use this for initialization
	void Start () {

		equipUnequipButton.onClick.AddListener(this.OnClickedEquipUnequipButton);
		cancelButton.onClick.AddListener(this.OnClickedCancelButton);

		gameObject.SetActive(false);
	}
	
	public void ShowItem(ItemData item) {

		ItemData itemInCurrentSlot;
		CrewInspector.Instance.CurrentInspectedCharacter.GetItemInSlot(CrewInspector.Instance.CurrentInspectedSlot, out itemInCurrentSlot);
		showingEquippedItem = itemInCurrentSlot == item;
		gameObject.SetActive(true);

		itemName.text = item.itemName;
		itemDescription.text = item.itemDescription;
		itemImage.sprite = item.LoadItemIcon();
		string itemProps = item.GetItemStatModifiersList();
		if (string.IsNullOrEmpty(itemProps))
			itemProperties.text = "No properties";
		else
			itemProperties.text = itemProps;

		if (showingEquippedItem)
			equipUnequipButton.GetComponentInChildren<Text>().text = "Unequip";
		else
			equipUnequipButton.GetComponentInChildren<Text>().text = "Equip";
	}

	public void OnClickedEquipUnequipButton() {

		if (showingEquippedItem) { //act as unequip button
			CrewInspector.Instance.CurrentInspectedCharacter.UnequipItem(CrewInspector.Instance.CurrentInspectedItem, CrewInspector.Instance.CurrentInspectedSlot);
		}
		else { //equip item
			CrewInspector.Instance.CurrentInspectedCharacter.EquipItem(CrewInspector.Instance.CurrentInspectedItem, CrewInspector.Instance.CurrentInspectedSlot);
		}
		CrewInspector.Instance.OnEquippedOrUnequippedItem();
	}

	public void OnClickedCancelButton() {
		CrewInspector.Instance.OnCancelledEquipment();
	}
}
