using UnityEngine;
using System.Collections;

public class StatsPanel : MonoBehaviour {

	public TextMesh inspectedCharacterName;
	public TextMesh inspectedCharacterJobLevel;

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
