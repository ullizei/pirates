using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CrewInspectorState {
	InspectingCrew,
	InspectingInventory
}


public class CrewInspector : MonoBehaviour {

	public Button closeButton;

	private CrewInspectorState state = CrewInspectorState.InspectingCrew;

	private static CrewInspector instance = null;
	public static CrewInspector Instance {
		get { return instance; }
	}

	private StatsPanel statsPanel;
	private EquipmentPanel equipmentPanel;
	private CrewPanel crewPanel;
	private ItemInfoPanel itemInfoPanel;
	private InventoryPanel inventoryPanel;
	private PopupPanel popupPanel; 

	private Character currentInspectedCharacter = null;
	public Character CurrentInspectedCharacter {
		get {return currentInspectedCharacter;}
	}

	private ItemData currentInspectedItem = null;
	public ItemData CurrentInspectedItem {
		get {return currentInspectedItem;}
	}

	private int currentInspectedSlot;
	public int CurrentInspectedSlot {
		get {return currentInspectedSlot;}
	}

	public static bool Open(Transform parentCanvas) {

		if (instance == null) {
			GameObject obj = (GameObject) Instantiate(Resources.Load("GUI/CrewInspector"));
			obj.transform.SetParent(parentCanvas, false);
			instance = obj.GetComponentInChildren<CrewInspector>();
			instance.Init();
			return true;
		}
		else
			return false;
	}

	private void Close() {
		Destroy(gameObject);
		instance = null;
	}

	private void Init() {

		closeButton.onClick.AddListener(this.Close);

		statsPanel = GetComponentInChildren<StatsPanel>();
		statsPanel.Init();
		crewPanel = GetComponentInChildren<CrewPanel>();
		crewPanel.Init();
		equipmentPanel = GetComponentInChildren<EquipmentPanel>();
		equipmentPanel.Init();
		itemInfoPanel = GetComponentInChildren<ItemInfoPanel>();
		inventoryPanel = GetComponentInChildren<InventoryPanel>();
		popupPanel = GetComponentInChildren<PopupPanel>();

		crewPanel.SelectDefaultCrewMember();
	}

	public void OnSelectedCharacter(Character character) {
		currentInspectedCharacter = character;
		statsPanel.ShowCharacterStats(character);
		equipmentPanel.ShowEquipment();
		crewPanel.OnSelectedCharacter(character);
	}	
	

	public void ShowItemInfoPanel(bool show) {

		itemInfoPanel.gameObject.SetActive(show);
		equipmentPanel.gameObject.SetActive(!show);
	}

	public void OnSelectedItem(ItemData itemData) {
		currentInspectedItem = itemData;
		itemInfoPanel.ShowItem(currentInspectedItem);
		inventoryPanel.OnSelectedItem();
		statsPanel.ShowStatChanges();
	}

	public void OnSelectedEquipmentSlot(int slotId, ItemType itemType) {
		currentInspectedSlot = slotId;
		currentInspectedCharacter.GetItemInSlot(slotId, out currentInspectedItem);
		SwitchState(CrewInspectorState.InspectingInventory);
		inventoryPanel.LoadItemButtonList(itemType);
	}

	public void OnEquippedOrUnequippedItem() {

		SwitchState(CrewInspectorState.InspectingCrew);
		equipmentPanel.ShowEquipment();
		statsPanel.ShowCharacterStats(currentInspectedCharacter);
	}

	public void OnCancelledEquipment() {

		SwitchState(CrewInspectorState.InspectingCrew);
		statsPanel.ShowCharacterStats(currentInspectedCharacter);
	}

	public void ShowInfoPopup(string info, string header) {
		popupPanel.Show(info, header);
	}

	private void SwitchState(CrewInspectorState newState) {
		
		if (newState != state)
		{
			//end current state
			switch (state) {
			case CrewInspectorState.InspectingCrew:
				equipmentPanel.gameObject.SetActive(false);
				crewPanel.gameObject.SetActive(false);
				break;
				
			case CrewInspectorState.InspectingInventory:
				itemInfoPanel.gameObject.SetActive(false);
				inventoryPanel.gameObject.SetActive(false);
				break;
			}
			
			//start new state
			state = newState;
			switch (state) {
			case CrewInspectorState.InspectingCrew:
				equipmentPanel.gameObject.SetActive(true);
				crewPanel.gameObject.SetActive(true);
				break;
				
			case CrewInspectorState.InspectingInventory:
				itemInfoPanel.gameObject.SetActive(true);
				inventoryPanel.gameObject.SetActive(true);
				break;
			}
		}
	}
}


//	public StatsPanel CharacterStatsPanel {
//		get {return statsPanel;}
//	}
//
//	public EquipmentPanel CharacterEquipmentPanel {
//		get {return equipmentPanel;}
//	}
