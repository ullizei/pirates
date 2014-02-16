using UnityEngine;
using System.Collections;

public class CameraControl : InputHandler {

	private static CameraControl instance = null;
	private float zPos;
	private Transform _transform;
	private Rect scrollBounds;

	private bool isScrolling = false;
	private int scrollFinger;
	private bool allowScrolling = true;

	public static CameraControl Instance {
		get {return instance;}
	}

	public bool AllowScrolling {

		get {return allowScrolling;}
		set {allowScrolling = value;}
	}

	void Awake() {
		instance = this;
		_transform = transform;
		zPos = _transform.position.z;
	}

	// Use this for initialization
	void Start () {

		scrollBounds = GetScrollBounds();
		ListenForInput(true);
	}

	public override void OnBeganTouch (SimTouch touch)
	{
		if (allowScrolling)
		{
			if (!isScrolling && TouchHitNothing(touch))
			{
				isScrolling = true;
				scrollFinger = touch.fingerID;
			}
		}
	}

	public override void OnMovedTouch (SimTouch touch)
	{
		if (isScrolling && touch.fingerID == scrollFinger)
		{
			if (allowScrolling)
				Scroll(touch.deltaScreenPosition);
			else
				isScrolling = false;
		}
	}

	public override void OnEndedOrCanceledTouch (SimTouch touch)
	{
		if (isScrolling && touch.fingerID == scrollFinger)
			isScrolling = false;
	}


	public void Scroll(Vector3 deltaPos) {

		Vector3 nextPos = _transform.position - deltaPos;
		nextPos.z = zPos;

		//check so position doesn't overstep scroll bounds
		if (nextPos.x > scrollBounds.xMax)
			nextPos.x = scrollBounds.xMax;
		else if (nextPos.x < scrollBounds.xMin)
			nextPos.x = scrollBounds.xMin;

		if (nextPos.y > scrollBounds.yMax)
			nextPos.y = scrollBounds.yMax;
		else if (nextPos.y < scrollBounds.yMin)
			nextPos.y = scrollBounds.yMin;

		_transform.position = nextPos;
	}

	public void MoveToPosition(Vector3 targetPos) {

		targetPos.z = zPos;
		_transform.position = targetPos;
	}

	public float LerpToPosition(Vector3 targetPos) {

		targetPos.z = zPos;
		Vector3 distVector = targetPos - _transform.position;
		float distance = Mathf.Sqrt(Mathf.Pow(distVector.x, 2f) + Mathf.Pow(distVector.y, 2f));
		float lerpSpeed = 1000f; //= 100u/s
		float lerpTime = distance / lerpSpeed;

		StartCoroutine(LerpToPositionRoutine(targetPos, lerpTime));
		return lerpTime;
	}

	private IEnumerator LerpToPositionRoutine(Vector3 targetPos, float lerpTime) {

		float elapsedTime = 0f;
		Vector3 startPos = _transform.position;
		while (elapsedTime < lerpTime)
		{
			_transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime/lerpTime);
			yield return null;
			elapsedTime += Time.smoothDeltaTime;
		}
	}

	private Rect GetScrollBounds() {

		float maxY, minY;
		float maxX, minX;
		maxX = minX = maxY = minY = 0f;

		GameObject nodes = GameObject.Find("Nodes");
		if (nodes != null)
		{
			MapNode[] nodesList = nodes.GetComponentsInChildren<MapNode>();
			Vector3 pos;
			for (int i = 0; i < nodesList.Length; i++)
			{
				pos = nodesList[i].transform.position;
				if (pos.x > maxX)
					maxX = pos.x;
				else if (pos.x < minX)
					minX = pos.x;

				if (pos.y > maxY)
					maxY = pos.y;
				else if (pos.y < minY)
					minY = pos.y;
			}
		}
		else
			Debug.LogError("Camera control couldn't find GameObject Nodes to calculate scroll bounds!");

		//extend scroll area
		float scrollAreaPadding = 250f;

		minX -= scrollAreaPadding;
		maxX += scrollAreaPadding;
		minY -= scrollAreaPadding;
		maxY += scrollAreaPadding;


		return new Rect(minX, minY, maxX-minX, maxY-minY);
	}

	void OnDestroy() {
		ListenForInput(false);
	}

	void OnDrawGizmos() {

		Rect scrollArea = GetScrollBounds();

		Vector3 ul = new Vector3(scrollArea.xMin, scrollArea.yMax, 0f);
		Vector3 ur = new Vector3(scrollArea.xMax, scrollArea.yMax, 0f);
		Vector3 ll = new Vector3(scrollArea.xMin, scrollArea.yMin, 0f);
		Vector3 lr = new Vector3(scrollArea.xMax, scrollArea.yMin, 0f);

		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(ul, ur);
		Gizmos.DrawLine(ur, lr);
		Gizmos.DrawLine(lr, ll);
		Gizmos.DrawLine(ll, ul);
	}
}
