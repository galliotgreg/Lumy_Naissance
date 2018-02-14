using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallSaveLumy : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SaveLumy);
    }

    void SaveLumy()
    {
        LumyEditorManager.instance.SaveLumy();
    }
}
