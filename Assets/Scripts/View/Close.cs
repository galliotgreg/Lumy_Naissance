using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close : MonoBehaviour {

    GameObject canvas;
    // Use this for initialization
    void Start() {

        this.gameObject.GetComponent<Button>().onClick.AddListener(ClosePanel);
        canvas = transform.parent.parent.parent.Find("CastesSceneCanvas").gameObject;
    }

    // Update is called once per frame
    void Update() {

    }

    void ClosePanel()
    {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
        canvas.SetActive(true);
    }
}
