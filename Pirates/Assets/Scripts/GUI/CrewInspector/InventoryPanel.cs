using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryPanel : MonoBehaviour {

	public RectTransform itemButtonsParent;
	
	private List<ItemSelectButton> itemSelectButtons;
	private ItemType currentInspectedItemType = ItemType.NONE;

	private float panelSize = 0f;

	void Start() {
		gameObject.SetActive(false);
	}

	public void LoadItemButtonList(ItemType itemType) {

		if (itemSelectButtons == null)
			itemSelectButtons = new List<ItemSelectButton>();

		UnloadItemButtonList();


		//if (itemType != currentInspectedItemType)
		//{


			float spacing = 30f;
			float offsetX = spacing;
			
			ItemSelectButton button;
			RectTransform buttonRect;

			currentInspectedItemType = itemType;
			List<ItemData> itemsInInventory = CrewInventory.Instance.GetItemsOfType(itemType);
			 

			for (int i = 0; i < itemsInInventory.Count; i++)
			{
				button = ItemSelectButton.Create(itemsInInventory[i]);
				itemSelectButtons.Add(button);
				buttonRect = button.GetComponent<RectTransform>();
				buttonRect.SetParent(itemButtonsParent, false);
				buttonRect.localPosition = new Vector3(offsetX, 0f, 0f);

				offsetX += buttonRect.sizeDelta.x + spacing;
			}
			
			itemButtonsParent.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, offsetX);
			panelSize = offsetX;
		//}

		SelectCurrentInspectedItem();
	}

	public void OnSelectedItem() {

		for (int i = 0; i < itemSelectButtons.Count; i++) {
			if (itemSelectButtons[i].itemName.text != CrewInspector.Instance.CurrentInspectedItem.itemName) {
				itemSelectButtons[i].Deselect();
			}
		}
	}

	private void SelectCurrentInspectedItem() {

		if (CrewInspector.Instance.CurrentInspectedItem != null)
		{
			for (int i = 0; i < itemSelectButtons.Count; i++) {
				if (itemSelectButtons[i].itemName.text == CrewInspector.Instance.CurrentInspectedItem.itemName) {
					itemSelectButtons[i].Select();
					break;
				}
			}
		}
		else
			itemSelectButtons[0].Select();

		//TODO: scroll if item is far back in list (use panelsize)
	}

	private void UnloadItemButtonList() {

		for (int i = 0; i < itemSelectButtons.Count; i++) {
			Destroy(itemSelectButtons[i].gameObject);
		}
		itemSelectButtons.Clear();
	}
}
