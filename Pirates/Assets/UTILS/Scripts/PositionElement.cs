using UnityEngine;
using System.Collections;


public enum AlignmentX {

	Left,
	Right,
	Middle
} 

public enum AlignmentY {

	Upper,
	Lower,
	Middle
}


public class PositionElement : MonoBehaviour {

	public AlignmentX alignmentX;
	public AlignmentY alignmentY;

	public float offsetX = 0f;
	public float offsetY = 0f;

	private bool inited = false;

	void Awake() {

		if (!inited)
			Init();
	}

	public void Init() {

		transform.position = GetPosition();
		inited = true;
	}

	public void Init(AlignmentX alignX, AlignmentY alignY) {

		alignmentX = alignX;
		alignmentY = alignY;
		Init();
	}

	public void Init(AlignmentX alignX, AlignmentY alignY, float xOffset, float yOffset) {

		alignmentX = alignX;
		alignmentY = alignY;
		offsetX = xOffset;
		offsetY = yOffset;
		Init();
	}

	private Vector3 GetPosition() {

		Vector3 viewPortPoint = Vector3.zero;
		viewPortPoint.z = transform.position.z - Camera.main.transform.position.z;
		switch (alignmentX)
		{
		case AlignmentX.Left: 
			viewPortPoint.x = 0f;
			break;

		case AlignmentX.Right:
			viewPortPoint.x = 1f;
			break;

		case AlignmentX.Middle:
			viewPortPoint.x = 0.5f;
			break;
		}

		switch (alignmentY)
		{
		case AlignmentY.Lower:
			viewPortPoint.y = 0f;
			break;

		case AlignmentY.Upper:
			viewPortPoint.y = 1f;
			break;

		case AlignmentY.Middle:
			viewPortPoint.y = 0.5f;
			break;
		}

		viewPortPoint.x += offsetX;
		viewPortPoint.y += offsetY;

		return Camera.main.ViewportToWorldPoint(viewPortPoint);
	}
}
