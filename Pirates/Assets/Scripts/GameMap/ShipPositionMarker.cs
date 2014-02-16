using UnityEngine;
using System.Collections;

public class ShipPositionMarker : MonoBehaviour {

	private static ShipPositionMarker instance;

	public MapNode currentPort;

	private Transform _transform;
	private bool isSailing = false;

	void Awake() {

		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}

	void Start () {

		_transform = transform;
		_transform.position = new Vector3(currentPort.transform.position.x, currentPort.transform.position.y, _transform.position.z);
		CameraControl.Instance.MoveToPosition(_transform.position);
		GameStateManager.Instance.GetCurrentState();
	}

	public static ShipPositionMarker Instance {
		get {return instance;}
	}

	public bool TravelToPort(MapNode newPort) {

		if (isSailing || newPort == currentPort)
			return false;

		NodeConnection connection = currentPort.GetConnectionToNode(newPort);
		if (connection != null)
		{
			StartCoroutine(TravelToPortRoutine(newPort, connection));
			return true;
		}
		else
			return false;
	}

	private IEnumerator TravelToPortRoutine(MapNode newPort, NodeConnection connection) {

		isSailing = true;

		CameraControl.Instance.AllowScrolling = false;
		float cameraLerpTime = CameraControl.Instance.LerpToPosition(_transform.position);
		yield return new WaitForSeconds(cameraLerpTime+0.1f);

		SoundManager.PlaySfx(Sfx.Wave);

		CR_Spline crSpline = connection.GetCRSpline(newPort);
		float travelTime = 1.25f;
		float elapsedTime = 0f;

		Vector3 pos;
		while (elapsedTime < travelTime)
		{
			pos = crSpline.Interp(elapsedTime/travelTime);
			pos.z = _transform.position.z;
			_transform.position = pos;
			CameraControl.Instance.MoveToPosition(pos);
			yield return null;
			elapsedTime += Time.smoothDeltaTime;
		}

		currentPort = newPort;
		isSailing = false;
		CameraControl.Instance.AllowScrolling = true;

	}

	void OnDestroy() {
		instance = null;
	}
}
