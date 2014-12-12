using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentSlot : MonoBehaviour {

	private ItemType contentType;
	private Button button;

	// Use this for initialization
	void Start () {

		button = GetComponent<Button>();
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

	public void OnClick() {
		Debug.Log("Clicked slot: "+contentType.ToString());

		//ToolTipPanel.Show(transform.parent.GetComponent<RectTransform>(), transform.localPosition);
	}
}
