using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsPanel : MonoBehaviour {

	public Text inspectedCharacterName;
	public Text inspectedCharacterJobLevel;

	private CharacterData inspectedCharacterData;

	// Use this for initialization
	void Start () {
	
	}
	
	void ShowStats(CharacterData selecetdCharacter)
	{
//		inspectedCharacterData = selecetdCharacter;
//		inspectedCharacterName.text = selecetdCharacter.name;
//		inspectedCharacterJobLevel.text = "Level "+selecetdCharacter.level+" "+selecetdCharacter.job;
	}
}
