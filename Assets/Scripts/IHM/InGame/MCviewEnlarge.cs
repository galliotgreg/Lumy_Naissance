using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCviewEnlarge : MonoBehaviour {

    public GameObject frame;
    public float factor;
    public GameObject textToEdit;

    bool isEnlarged = false;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Button>().onClick.AddListener(EnlargeFrame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void EnlargeFrame()
    {
        Vector2 currentSize = frame.GetComponent<RectTransform>().sizeDelta;
        if (isEnlarged)
        {
            frame.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x / factor, currentSize.y);
            textToEdit.GetComponent<Text>().text = ">>";
            isEnlarged = false;
        }
        else
        {
            frame.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x * factor, currentSize.y);
            textToEdit.GetComponent<Text>().text = "<<";
            isEnlarged = true;
        }
       
        
    }

}
