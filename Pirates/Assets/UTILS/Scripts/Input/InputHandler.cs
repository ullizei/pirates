using UnityEngine;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour {

	protected bool isListening = false;
	public void ListenForInput(bool listen) {

		if (listen && !isListening)
		{
			InputListener.Instance.onBeganTouch += this.OnBeganTouch;
			InputListener.Instance.onMovedTouch += this.OnMovedTouch;
			InputListener.Instance.onStationaryTouch += this.OnStationaryTouch;
			InputListener.Instance.onEndedOrCaceledTouch += this.OnEndedOrCanceledTouch;
			InputListener.Instance.onAllInput += this.OnAllInput;
			isListening = true;
		}
		else if (!listen && isListening)
		{
			InputListener.Instance.onBeganTouch -= this.OnBeganTouch;
			InputListener.Instance.onMovedTouch -= this.OnMovedTouch;
			InputListener.Instance.onStationaryTouch -= this.OnStationaryTouch;
			InputListener.Instance.onEndedOrCaceledTouch -= this.OnEndedOrCanceledTouch;
			InputListener.Instance.onAllInput -= this.OnAllInput;
			isListening = false;
		}
	}
	
	public virtual void OnBeganTouch(SimTouch touch) {}

	public virtual void OnMovedTouch(SimTouch touch) {}

	public virtual void OnStationaryTouch(SimTouch touch) {}

	public virtual void OnEndedOrCanceledTouch(SimTouch touch) {}

	public virtual void OnAllInput(List<SimTouch> touches) {}

	public static bool TouchHitTarget(SimTouch touch, GameObject target, float rayCastDepth = 1500f) {

		//Raycast 3D
		Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, rayCastDepth))
		{
			if (hit.collider != null && hit.collider.gameObject == target)
				return true;
		}

		//Raycast 2D
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.screenPosition);
		RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, rayCastDepth);

		if (hit2D.collider != null && hit2D.collider.gameObject == target)
			return true;

		//no cigar
		return false;
	}

	public static bool TouchHitGameObject(SimTouch touch, out GameObject result, float rayCastDepth = 1500f) {

		result = null;

		//Raycast 3D
		Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, rayCastDepth))
		{
			if (hit.collider != null)
			{
				result = hit.collider.gameObject;
				return true;
			}
		}
		
		//Raycast 2D
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.screenPosition);
		RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, rayCastDepth);//(new Vector2(touch.screenPosition.x, touch.screenPosition.y), Vector2.zero, rayCastDepth);
		if (hit2D.collider != null)
		{
			result = hit2D.collider.gameObject;
			return true;
		}
		
		//no cigar
		return false;
	}


}
