using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentSlot : MonoBehaviour {

	public int slotID;
	
	private ItemType contentType;

	private Button button;
	private Image defaultImage;
	private Image alternativeDefaultImage = null;
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

		gameObject.SetActive(true);

		ItemData itemData;
		if (CrewInspector.Instance.CurrentInspectedCharacter.GetItemInSlot(slotID, out itemData)) {
			itemImage.gameObject.SetActive(true);
			itemImage.sprite = itemData.LoadItemIcon();

			defaultImage.gameObject.SetActive(false);
			if (alternativeDefaultImage != null)
				alternativeDefaultImage.gameObject.SetActive(false);
		}
		else if (CrewInspector.Instance.CurrentInspectedCharacter.CanUseItemOfType(contentType))
		{
			itemImage.gameObject.SetActive(false);
			defaultImage.gameObject.SetActive(true);
		}
		else
			gameObject.SetActive(false);
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
