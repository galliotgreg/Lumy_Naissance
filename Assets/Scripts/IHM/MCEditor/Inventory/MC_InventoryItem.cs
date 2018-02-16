using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_InventoryItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform rect = GetComponent<RectTransform>();
		rect.localScale = new Vector3 (1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected ABNode item;

	public ABNode Item {
		get {
			return item;
		}
		set {
			item = value;
		}
	}

	[SerializeField]
	UnityEngine.UI.Text text;

	public UnityEngine.UI.Text Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}
}
