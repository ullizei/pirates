using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTipPanel : MonoBehaviour {

	public static void Show(RectTransform parentRect, Vector3 position) {

		GameObject toolTip = (GameObject) Instantiate(Resources.Load("GUI/StatsPanel/ToolTipPanel"));
		ToolTipPanel panel = toolTip.GetComponent<ToolTipPanel>();

		toolTip.transform.SetParent(parentRect);
		toolTip.transform.localPosition = position;

	}
}
