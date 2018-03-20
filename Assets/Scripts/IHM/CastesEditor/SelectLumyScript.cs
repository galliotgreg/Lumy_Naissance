using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLumyScript : MonoBehaviour {
    [SerializeField]
    private string lumyName;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SelectLumy);
        lumyName = GetComponentInChildren<Text>().text;
    }

    void SelectLumy()
    {
        SwarmEditUIController.instance.SelectLumy(lumyName);
    }
}
