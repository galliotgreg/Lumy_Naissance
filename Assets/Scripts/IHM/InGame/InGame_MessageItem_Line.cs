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
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region INSTANTIATE
	public static InGame_MessageItem_Line instantiate( string value, Transform parent, InGame_MessageItem_Line prefab ){
		InGame_MessageItem_Line result = Instantiate<InGame_MessageItem_Line> (prefab, parent);
		result.Line = value;
		return result;
	}
	#endregion
}
