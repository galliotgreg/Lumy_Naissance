using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPrysmeScript : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SwapScene);
    }

    void SwapScene()
    {
        AppContextManager.instance.PrysmeEdit = true;
        NavigationManager.instance.SwapScenesWithPanel("EditeurMCScene", "", this.gameObject.transform.position);
    }
}
