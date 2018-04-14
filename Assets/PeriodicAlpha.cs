using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeriodicAlpha : MonoBehaviour {
    private float time;

    // Use this for initialization
    private void OnEnable()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update () {
        time += Time.fixedDeltaTime;
        ChangeColor();
	}

    private void ChangeColor()
    {
        Image image = this.gameObject.GetComponent<Image>();
        float alphaF =  Mathf.Sin(time)*Mathf.Sin(time);
        byte alpha = (byte) ((int)(alphaF * 255) % 256);
        image.color = new Color32(255,255,255,alpha);
        Debug.Log(alpha);
    }
}
