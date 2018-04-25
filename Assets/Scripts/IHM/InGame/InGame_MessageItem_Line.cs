using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_MessageItem_Line : MonoBehaviour {

	string line;

	[SerializeField]
	Text text;

	public string Line {
		get {
			return line;
		}
		set {
			line = value;
			text.text = line;
            text.color = Color.red;
		}
	}

	//Rect thisRect;

	// Use this for initialization
	void Start () {
		//thisRect = GetComponent<RectTransform> ().rect;
	}
	
	// Update is called once per frame
	void Update () {
		// adjust size to TEXT
//		Rect textRect = text.gameObject.GetComponent<RectTransform>().rect;
		/*thisRect.Set( thisRect.x, thisRect.y, thisRect.width, textRect.height );
		Debug.LogError (textRect.height);
		Debug.LogError (thisRect.height);*/
//		text.gameObject.GetComponent<LayoutElement> ().preferredHeight = textRect.height;
	}

	#region INSTANTIATE
	public static InGame_MessageItem_Line instantiate( string value, Transform parent, InGame_MessageItem_Line prefab ){
		InGame_MessageItem_Line result = Instantiate<InGame_MessageItem_Line> (prefab, parent);
		result.Line = value;
		return result;
	}
	#endregion
}
