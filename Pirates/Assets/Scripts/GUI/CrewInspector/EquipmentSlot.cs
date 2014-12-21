using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentSlot : MonoBehaviour {

	public int slotID;

	private ItemType contentType;
	private Button button;
	private Image defaultImage;
	private Image itemImage;

	// Use this for initialization
	public void Init() {

		button = GetComponent<Button>();
		defaultImage = transform.GetChild(0).GetComponent<Image>();
		itemImage = transform.GetChild(1).GetComponent<Image>();
		button.onClick.AddListener(this.OnClick);

		//set content type
		string contentName = gameObject.name.Replace("Slot", "");
		ItemType[] itemTypes = (ItemType[]) System.Enum.GetValues(typeof(ItemType));
		for (int i = 0; i < itemTypes.Length; i++)
		{
			if (contentName.StartsWith(itemTypes[i].ToString())) {
				contentType = itemTypes[i];
				break;
			}
		}
	}

	public void ShowContent() {

		ItemData itemData;
		CrewInspector.Instance.CurrentInspectedCharacter.GetItemInSlot(slotID, out itemData);

		bool showDefaultImage = itemData == null;

		defaultImage.gameObject.SetActive(showDefaultImage);
		itemImage.gameObject.SetActive(!showDefaultImage);

		if (!showDefaultImage)
			itemImage.sprite = itemData.LoadItemIcon();
	}

	public void OnClick() {

		if (CrewInventory.Instance.HasAnyItemsOfType(contentType))
			CrewInspector.Instance.OnSelectedEquipmentSlot(slotID, contentType);
		else
		{
			string header = "Can't equip "+contentType.ToString().ToLower();
			string info = "You don't have any items of this type in your inventory!";
			CrewInspector.Instance.ShowInfoPopup(info, header);
		}
	}
}
