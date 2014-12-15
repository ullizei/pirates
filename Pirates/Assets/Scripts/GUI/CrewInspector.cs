using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrewInspector : MonoBehaviour {

	private static CrewInspector instance = null;
	public static CrewInspector Instance {
		get { return instance; }
	}

	private StatsPanel statsPanel;
	private EquipmentPanel equipmentPanel;
	private CrewPanel crewPanel;

	private Character currentInspectedCharacter = null;
	public Character CurrentInspectedCharacter {
		get {return currentInspectedCharacter;}
	}

	public static bool Open() {

		Debug.Log("Try open...");

		if (instance == null) {
			GameObject obj = (GameObject) Instantiate(Resources.Load("GUI/CrewInspector"));
			obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
			instance = obj.GetComponentInChildren<CrewInspector>();
			instance.Init();
			return true;
		}
		else
			return false;
	}

	private void Init() {
		statsPanel = GetComponentInChildren<StatsPanel>();
		statsPanel.Init();

		equipmentPanel = GetComponentInChildren<EquipmentPanel>();
		crewPanel = GetComponentInChildren<CrewPanel>();
	}

	public void OnSelectedCharacter(Character character) {
		currentInspectedCharacter = character;
		statsPanel.ShowCharacterStats(character);
		crewPanel.OnSelectedCharacter(character);
	}
}


//	public StatsPanel CharacterStatsPanel {
//		get {return statsPanel;}
//	}
//
//	public EquipmentPanel CharacterEquipmentPanel {
//		get {return equipmentPanel;}
//	}
