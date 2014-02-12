using UnityEngine;
using System.Collections;

public class InputTest : InputHandler {


	void Start () {
		ListenForInput(true);
	}

	public override void OnBeganTouch (SimTouch touch)
	{
		Debug.Log ("ON BEGAN TOUCH "+touch.time);

		GameObject target;
		if (InputHandler.TouchHitGameObject(touch, out target))
			Debug.Log("Hit object: "+target.name);
	}

	/*public override void OnMovedTouch (SimTouch touch)
	{
		Debug.Log("ON MOVED TOUCH "+touch.deltaScreenPosition);
	}

	public override void OnStationaryTouch (SimTouch touch)
	{
		Debug.Log ("ON STATIONARY TOUCH");
	}

	public override void OnEndedOrCanceledTouch (SimTouch touch)
	{
		Debug.Log("ON ENDED TOUCH "+touch.totalTime);
	}*/
	

}
