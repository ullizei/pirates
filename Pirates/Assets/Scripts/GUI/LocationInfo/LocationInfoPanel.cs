using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationInfoPanel : MonoBehaviour {

	private static LocationInfoPanel currentOpenPanel = null;

	public Text label;
	public Text info;
	public Image image;

	public Text travelInfoLabel;
	public Text travelInfo;

	public Button closeButton;
	public Button travelButton;

	private MapLocationData locationData;
	private MapNode mapNode;
	private MapPath path;

    private const float LOW_RISK = 2f;
    private const float MEDIUM_RISK = 4f;
    private const float HIGH_RISK = 7f;

    public static void Open(MapNode node) {

		GameObject obj = (GameObject) Instantiate(Resources.Load("GUI/LocationInfoPanel"));
		obj.transform.SetParent(GameObject.Find("ScreenSpaceCanvas").transform, false);

		LocationInfoPanel panel = obj.GetComponent<LocationInfoPanel>();
		panel.locationData = MapLocationDatabase.Instance.GetLocationData(node.locationID);
		panel.mapNode = node;

		if (currentOpenPanel != null)
			currentOpenPanel.Close();

		currentOpenPanel = panel;
	}

	// Use this for initialization
	void Start () {

		if (locationData != null) {
			label.text = locationData.name;
			info.text = locationData.info;
			image.sprite = Resources.Load<Sprite>(MapLocationDatabase.Instance.imageResourcePath+locationData.imageResourceName);
		}

		if (mapNode != null) {
			ShowTravelInfo();
		}

		closeButton.onClick.AddListener(this.OnClickedCloseButton);
		travelButton.onClick.AddListener(this.OnClickedTravelButton);
	}

	private void ShowTravelInfo() {

		path = NodeNavigator.GetMapPath(ShipPositionMarker.Instance.currentPort, mapNode);

		//label
		travelInfoLabel.text = (locationData != null && path != null) ? "TRAVEL TO "+locationData.name.ToUpper() : "";

		if (path != null)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

			//distance & route info
			stringBuilder.AppendLine(string.Format("DISTANCE:\t{0} nautical miles / {1} days", Mathf.CeilToInt(path.length), Mathf.CeilToInt(path.length / 50f)));
			stringBuilder.AppendLine(string.Format("ROUTE:\t{0}", path.ToString()));
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("RISK:\t{0}", ConvertRiskValueToColoredText(path.GetRiskAverage())));

			travelInfo.text = stringBuilder.ToString();
		}
		else
			travelInfo.text = "";
	}

    private string ConvertRiskValueToColoredText(float risk)
    {
        string riskLevel = "";
        string color = "black";

        if (risk <= LOW_RISK)
        {
            riskLevel = "LOW";
            color = "green";
        }
        else if (risk <= MEDIUM_RISK)
        {
            riskLevel = "MEDIUM";
            color = "orange";
        }
        else
        {
            riskLevel = "HIGH";
            color = "red";
        }
        return string.Format("<color={0}>{1}</color>", color, riskLevel);
    }

	public void OnClickedCloseButton() {
		Close ();
	}

	public void OnClickedTravelButton() {
		ShipPositionMarker.Instance.TravelToPort(path);
		Close();
	}

	public void Close() {

		if (currentOpenPanel == this)
			currentOpenPanel = null;

		Destroy(gameObject);
	}
}
