using UnityEngine;
using System.Collections;

public enum GameState {

	GameMap
}

public class GameStateManager : MonoBehaviour {

	private static GameStateManager instance;

	private GameState currentState;

	public static GameStateManager Instance {

		get
		{
			if (instance == null)
			{
				GameObject gsm = new GameObject("GameStateManager");
				DontDestroyOnLoad(gsm);
				instance = gsm.AddComponent<GameStateManager>();
				instance.Init();
			}
			return instance;
		}
	}

	private void Init() {

		if (Application.loadedLevelName == "TestScene")
		{
			currentState = GameState.GameMap;
			SoundManager.Instance.PlayMusic(Music.piratloop2);
		}
	}

	public GameState GetCurrentState() {
		return currentState;
	}

	public void ComeAlive() {}

	void OnDestroy() {
		instance = null;
	}
}
