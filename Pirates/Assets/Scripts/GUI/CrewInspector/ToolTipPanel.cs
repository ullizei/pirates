using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTipPanel : MonoBehaviour {

	public Text infoText;
	public Text labelText;
	public Button closeButton;

	private static ToolTipPanel currentOpenPanel = null;

	public static ToolTipPanel Show(Transform parentTransform, RectTransform positionRelativeTo, Vector3 position) {

		GameObject toolTip = (GameObject) Instantiate(Resources.Load("GUI/StatsPanel/ToolTipPanel"));
		ToolTipPanel panel = toolTip.GetComponent<ToolTipPanel>();

		toolTip.transform.SetParent(positionRelativeTo);
		toolTip.transform.localPosition = position;
		toolTip.transform.SetParent(parentTransform);
		toolTip.transform.SetAsLastSibling();
		toolTip.transform.localScale = Vector3.one;

		if (currentOpenPanel != null)
			currentOpenPanel.Close();

		currentOpenPanel = panel;
		return panel;
	}

	void Start() {
		closeButton.onClick.AddListener(this.Close);
		PositionWithinBounds();
	}

	private void PositionWithinBounds() {

		RectTransform rectTransform = GetComponent<RectTransform>();
		Vector3[] corners = new Vector3[4];
		rectTransform.GetWorldCorners(corners);

		Vector3 offset = Vector3.zero;
		for (int i = 0; i < corners.Length; i++) {
			if (corners[i].y > Screen.height)
				offset.y = Screen.height - corners[i].y - 10;
			else if (corners[i].y < 0)
				offset.y = Mathf.Abs(corners[i].y) + 10;

			if (corners[i].x > Screen.width)
				offset.x = Screen.width - corners[i].x - 10;
			else if (corners[i].x < 0)
				offset.x = Mathf.Abs(corners[i].x) + 10;
		}

		transform.localPosition += offset;
	}

	public void SetText(string info, string label = "") {

		labelText.text = label;
		infoText.supportRichText = true;
		infoText.text = info;
	}

	public void Close() {
		if (currentOpenPanel == this)
			currentOpenPanel = null;

		Destroy(gameObject);
	}
}
