using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyDatabase : ScriptableObject {

	[SerializeField]
	public List<CharacterData> enemies;

	private static EnemyDatabase instance;
	public static EnemyDatabase Instance {
		get 
		{
			if (instance == null)
				instance = Resources.Load("EnemyDatabase") as EnemyDatabase;
			
			return instance;
		}
	}
}
