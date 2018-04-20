using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomingScript : MonoBehaviour
{

    public float minSize = 1f;
    public float maxSize = 40f;
    private float sensitivity = 10f;
    private GameObject dialogBox;
    private bool isZoomable = true;
    private int fingerID = -1;
    [SerializeField]
    private DropArea zommingZone;

	[SerializeField]
	Camera zoomingCam;
	[SerializeField]
	float initialHeight;

	#region Properties

	private Camera ZoomingCam {
		set{
			zoomingCam = value;
			initialHeight = CurrentHeight;
		}
	}

	public float InitialHeight {
		get {
			return initialHeight;
		}
	}

	public float CurrentHeight {
		get {
			return zoomingCam.orthographicSize*2;
		}
	}

	public float CurrentWidth {
		get {
			return CurrentHeight * zoomingCam.aspect;
		}
	}

	#endregion

	void Start(){
		ZoomingCam = GameObject.Find("Camera").GetComponent<Camera>();
	}

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MCEditor_DialogBox"))
        {
            isZoomable = false; 
        }
        else isZoomable = true;

		if (isZoomable && zommingZone.IsHover)
        {
			float prev_size = zoomingCam.orthographicSize;
            float new_size = prev_size - Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            new_size = Mathf.Clamp(new_size, minSize, maxSize);
			zoomingCam.orthographicSize = new_size;
        }

        /*dialogBox = GameObject.FindGameObjectWithTag("MCEditor_DialogBox");
        Vector3 posBox = dialogBox.GetComponent<RectTransform>().position;
        if (prev_size < new_size) //dezooming
        {
            posBox = new Vector3(posBox.x - (new_size - prev_size) / 10, posBox.y - (new_size - prev_size) / 10, 0.0f);
        }
        else //zooming
        {
            posBox = new Vector3(posBox.x + (prev_size - new_size) / 10, posBox.y + (prev_size - new_size) / 10, 0.0f);
        }

        dialogBox.GetComponent<RectTransform>().SetPositionAndRotation(posBox, new Quaternion() );*/
    }
}
