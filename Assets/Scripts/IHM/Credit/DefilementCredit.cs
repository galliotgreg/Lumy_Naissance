using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefilementCredit : MonoBehaviour {

    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private GameObject panelDeroulant;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        DeplacementPanel();
        InitialPosition();
	}

    #region Panel positions
    //Reset Panel Position 
    private void InitialPosition()
    {
        if(transform.position.y >= 47)
        {
            transform.position = new Vector3(0, -40, 1);
        }
    }
    //Move panel to the top
    private void DeplacementPanel()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }
    #endregion

}
