using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchPPCallback : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(LaunchGame);
    }

    void LaunchGame()
    {
        PartiePersoUIController.instance.LaunchGame();
    }
}
