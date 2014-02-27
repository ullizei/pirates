using UnityEngine;
using System.Collections.Generic;

public class InputListener : MonoBehaviour {

	//events---
	public delegate void OnBeganTouch(SimTouch touch);
	public event OnBeganTouch onBeganTouch;

	public delegate void OnMovedTouch(SimTouch touch);
	public event OnMovedTouch onMovedTouch;

	public delegate void OnStationaryTouch(SimTouch touch);
	public event OnStationaryTouch onStationaryTouch;

	public delegate void OnEndedOrCanceledTouch(SimTouch touch);
	public event OnEndedOrCanceledTouch onEndedOrCaceledTouch;

	public delegate void OnAllInput(List<SimTouch> touches);
	public event OnAllInput onAllInput;
	//---

	private Rect screenRect;
	private List<SimTouch> lastTouches;

	private static InputListener instance;
	public static InputListener Instance {
		get
		{
			if (instance == null)
			{
				instance = (InputListener) GameStateManager.Instance.gameObject.AddComponent<InputListener>();
				instance.Init();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Init () {
		lastTouches = new List<SimTouch>();
		screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
		HandleMouseEvents();
		#else
		HandleTouchEvents();
		#endif

		ThrowEvents();
	}

	private void ThrowEvents() {

		if (lastTouches.Count > 0)
		{
			if (onAllInput != null)
				onAllInput(lastTouches);

			List<int> touchesToRemove = new List<int>();
			for (int i = 0; i < lastTouches.Count; i++)
			{
				switch (lastTouches[i].phase)
				{
				case TouchPhase.Began:
					if (onBeganTouch != null)
						onBeganTouch(lastTouches[i]);
					break;
				case TouchPhase.Moved:
					if (onMovedTouch != null)
						onMovedTouch(lastTouches[i]);
					break;
				case TouchPhase.Stationary:
					if (onStationaryTouch != null)
						onStationaryTouch(lastTouches[i]);
					break;
				case TouchPhase.Canceled:
					if (onEndedOrCaceledTouch != null)
						onEndedOrCaceledTouch(lastTouches[i]);
					touchesToRemove.Add(i);
					break;
				case TouchPhase.Ended:
					if (onEndedOrCaceledTouch != null)
						onEndedOrCaceledTouch(lastTouches[i]);
					touchesToRemove.Add(i);
					break;
				}
			}
			//remove ended or canceled touches
			if (touchesToRemove.Count > 0)
			{
				for (int i = touchesToRemove.Count-1; i >= 0; i--)
					lastTouches.RemoveAt(i);
			}
		}
	}

	private void HandleTouchEvents() {

		if (Input.touchCount > 0)
		{
			foreach (Touch touch in Input.touches) {

				SimTouch simTouch = new SimTouch();
				simTouch.fingerID = touch.fingerId;
				simTouch.phase = touch.phase;
				simTouch.deltaTime = touch.deltaTime;
				simTouch.screenPosition = touch.position;
				simTouch.deltaScreenPosition = touch.deltaPosition;
				simTouch.time = Time.time;
				simTouch.totalTime = 0f;

				if (simTouch.phase != TouchPhase.Began)
				{
					SimTouch lastTouch = GetLastTouchWithID(touch.fingerId);
					simTouch.totalTime = lastTouch.totalTime + simTouch.deltaTime;
					lastTouches.Remove(lastTouch);
				}
				lastTouches.Add(simTouch);
			}
		}
	}

	private bool trackMouse = false;
	private void HandleMouseEvents() {

		if (Input.GetMouseButtonDown(0))
		{
			trackMouse = true;
			SimTouch touch = new SimTouch();
			touch.fingerID = 1;
			touch.phase = TouchPhase.Began;
			touch.deltaTime = 0f;
			touch.time = Time.time;
			touch.screenPosition = Input.mousePosition;
			touch.deltaScreenPosition = Vector3.zero;
			lastTouches.Add(touch);
		}
		else if (trackMouse)
		{
			UpdateMouseTouch(1);
		}
	}

	private void UpdateMouseTouch(int id) {

		SimTouch lastTouch = GetLastTouchWithID(id);
		SimTouch touch = new SimTouch();

		touch.fingerID = id;
		touch.time = Time.time;
		touch.deltaTime = Time.time - lastTouch.time;
		touch.totalTime = lastTouch.totalTime + touch.deltaTime;
		touch.screenPosition = Input.mousePosition;

		touch.deltaScreenPosition = touch.screenPosition - lastTouch.screenPosition;

		if (Input.GetMouseButtonUp(0))
			touch.phase = TouchPhase.Ended;
		else if (Input.GetMouseButton(0))
		{
			if (screenRect.Contains(Input.mousePosition))
			{
				if (touch.deltaScreenPosition == Vector3.zero)
					touch.phase = TouchPhase.Stationary;
				else
					touch.phase = TouchPhase.Moved;
			}
			else
				touch.phase = TouchPhase.Canceled;
		}

		lastTouches.Add(touch);
		lastTouches.Remove(lastTouch);

		if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
			trackMouse = false;
	}

	private SimTouch GetLastTouchWithID(int id) {

		for (int i = 0; i < lastTouches.Count; i++)
		{
			if (lastTouches[i].fingerID == id)
				return lastTouches[i];
		}
		return null;
	}

	public static bool Exists() {
		return instance != null;
	}

	void OnDestroy() {
		instance = null;
	}
}
