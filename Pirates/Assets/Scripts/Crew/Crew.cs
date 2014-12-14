using UnityEngine;
using System.Collections.Generic;

public class Crew {

	private static Crew instance = null;
	public static Crew Instance {
		get
		{
			if (instance == null) {
				instance = new Crew();
				instance.Init();
			}
			return instance;
		}
	}

	private List<CharacterData> crewMembers;
	public List<CharacterData> CrewMembers {
		get { return crewMembers; }
	}


	private void Init() {
		LoadCrewMembers();
	}

	private void LoadCrewMembers() {

		crewMembers = new List<CharacterData>();

		crewMembers.Add(new CharacterData("Glenn", GetRandomStats()));
		crewMembers.Add(new CharacterData("Bob", GetRandomStats()));
		crewMembers.Add(new CharacterData("George", GetRandomStats()));
	}

	private CharacterStats GetRandomStats() {

		CharacterStats stats = new CharacterStats();
		stats.agility = Random.Range(5, 18);
		stats.health = Random.Range(5, 18);
		stats.mind = Random.Range(5, 18);
		stats.strength = Random.Range(5, 18);
		stats.swagger = Random.Range(5, 18);

		return stats;
	}
}
