using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCEditor_LateralBar : MonoBehaviour {

	public enum Side{ Right, Left };

	[SerializeField]
	float initialAnchorX;
	bool activated = false;

	[SerializeField]
	Side side = Side.Left;

	[SerializeField]
	RectTransform parent;
	[SerializeField]
	RectTransform bar;
	[SerializeField]
	UnityEngine.UI.Button button;
	[SerializeField]
	UnityEngine.UI.Image arrow;

	// Use this for initialization
	void Start () {
		switch (side) {
		case Side.Left:
			initialAnchorX = parent.anchorMin.x;
			break;
		case Side.Right:
			initialAnchorX = parent.anchorMax.x;
			break;
		}

		button.onClick.AddListener (activate);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void activate(){
		if (!activated) {
			
			switch (side) {
			case Side.Left:
				parent.anchorMin = new Vector2 (0, parent.anchorMin.y);
				arrow.transform.Rotate ( new Vector3(0,0,180) );
				break;
			case Side.Right:
				parent.anchorMax = new Vector2( 1, parent.anchorMax.y );
				//bar.anchorMin = new Vector2( 1, bar.anchorMin.y );
				arrow.transform.Rotate ( new Vector3(0,0,180) );
				break;
			}
			bar.gameObject.SetActive (false);

		} else {
			
			switch (side) {
			case Side.Left:
				parent.anchorMin = new Vector2( initialAnchorX, parent.anchorMin.y );
				arrow.transform.Rotate ( new Vector3(0,0,-180) );
				break;
			case Side.Right:
				parent.anchorMax = new Vector2( initialAnchorX, parent.anchorMax.y );
				arrow.transform.Rotate ( new Vector3(0,0,-180) );
				break;
			}
			bar.gameObject.SetActive (true);
		}

		//parent.rect.Set (0, 0, 0, 0);
		//bar.rect.Set (0, 0, 0, 0);

		activated = !activated;
	}
}
