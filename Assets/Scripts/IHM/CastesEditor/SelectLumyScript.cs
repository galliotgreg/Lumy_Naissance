using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLumyScript : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SelectLumy);
    }

    void SelectLumy()
    {
        SwarmEditUIController.instance.SelectLumy();
    }
}
