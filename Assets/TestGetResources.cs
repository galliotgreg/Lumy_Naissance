using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetResources : MonoBehaviour {


    private GameManager gameManager; 
	// Use this for initialization
	void Start () {
        GameManager gameManager = GameManager.instance; 
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Player 1" + gameManager.GetResources(PlayerAuthority.Player1));
        Debug.Log("Player 2" + gameManager.GetResources(PlayerAuthority.Player2));
	}
}
