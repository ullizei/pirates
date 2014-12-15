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

	private List<Character> crewMembers;
	public List<Character> CrewMembers {
		get { return crewMembers; }
	}


	private void Init() {
		LoadCrewMembers();
	}

	private void LoadCrewMembers() {

		crewMembers = new List<Character>();

		crewMembers.Add(new Character("Glenn"));
		crewMembers.Add(new Character("Bob"));
		crewMembers.Add(new Character("George"));
	}


}
