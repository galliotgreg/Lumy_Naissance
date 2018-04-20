using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Proxy_Camera : MonoBehaviour {

	[SerializeField]
	Camera cam;
	[SerializeField]
	RenderTexture texturePrefab;

	//RenderTexture texture;
	UnityEngine.UI.RawImage image;

	/*public RenderTexture Texture {
		get {
			return texture;
		}
	}*/

	// Use this for initialization
	void Start () {
		//cam.targetTexture = new RenderTexture (texturePrefab.descriptor); // texturePrefab. Instantiate<RenderTexture>( texturePrefab );
		cam.targetTexture = new RenderTexture ( texturePrefab );
		cam.targetTexture.Create();
	}
	
	// Update is called once per frame
	void Update () {
		if (image != null) {
			//image.texture = GetRTPixels( cam.targetTexture );
			image.texture = cam.targetTexture;
		}
	}

	public void setImage( UnityEngine.UI.RawImage _image ){
		//_image.texture = cam.targetTexture;
		image = _image;
	}

	static public Texture2D GetRTPixels(RenderTexture rt)
	{
		// Remember currently active render texture
		RenderTexture currentActiveRT = RenderTexture.active;

		// Set the supplied RenderTexture as the active one
		RenderTexture.active = rt;

		// Create a new Texture2D and read the RenderTexture image into it
		Texture2D tex = new Texture2D(rt.width, rt.height);
		tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

		// Restorie previously active render texture
		RenderTexture.active = currentActiveRT;
		return tex;
	}
}
