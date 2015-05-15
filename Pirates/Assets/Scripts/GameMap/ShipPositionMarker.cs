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
	}

	public static ShipPositionMarker Instance {
		get {return instance;}
	}

	public void TravelToPort(MapPath mapPath) {
		StartCoroutine(TravelAlongMapPathRoutine(mapPath));
	}

	private IEnumerator TravelAlongMapPathRoutine(MapPath mapPath) {

		isSailing = true;
		CameraControl.Instance.AllowScrolling = false;

		NodeConnection connection;
		for (int i = 1; i < mapPath.path.Count; i++) {
			connection = currentPort.GetConnectionToNode(mapPath.path[i]);
			yield return StartCoroutine(TravelToPortRoutine(mapPath.path[i], connection));
			yield return new WaitForSeconds(0.1f);
		}

		isSailing = false;
		CameraControl.Instance.AllowScrolling = true;
	}

	private IEnumerator TravelToPortRoutine(MapNode newPort, NodeConnection connection) {

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
	}

	void OnDestroy() {
		instance = null;
	}
}
