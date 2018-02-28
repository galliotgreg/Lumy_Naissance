using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseAnnuler : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClosePanel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClosePanel()
    {
        transform.parent.parent.gameObject.SetActive(!transform.parent.parent.gameObject.activeSelf);
    }
}
