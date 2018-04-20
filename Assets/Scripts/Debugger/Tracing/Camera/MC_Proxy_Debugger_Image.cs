using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Proxy_Debugger_Image : MonoBehaviour {

	MC_Proxy_Camera cam;
	MCEditor_Proxy proxy;

	[SerializeField]
	UnityEngine.UI.RawImage image;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setCam( MCEditor_Proxy _proxy ){
		proxy = _proxy;
		proxy.GetComponentInChildren<MC_Proxy_Camera> ().setImage (image);
	}

	void OnDestroy(){
		Destroy (proxy.gameObject);
	}
}
