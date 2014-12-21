using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentPanel : MonoBehaviour {

	private EquipmentSlot[] equipmentSlots;

	public GameObject equipment;

	// Use this for initialization
	public void Init () {

		equipmentSlots = GetComponentsInChildren<EquipmentSlot>() as EquipmentSlot[];
		for (int i = 0; i < equipmentSlots.Length; i++) {
			equipmentSlots[i].Init();
		}
	}

	public void ShowEquipment() {
		for (int i = 0; i < equipmentSlots.Length; i++)
			equipmentSlots[i].ShowContent();
	}
}
