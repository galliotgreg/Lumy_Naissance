using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Close : MonoBehaviour {

    // Use this for initialization
    void Start() {

        this.gameObject.GetComponent<Button>().onClick.AddListener(ClosePanel);
    }

    // Update is called once per frame
    void Update() {

    }

    void ClosePanel()
    {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
    }
}
