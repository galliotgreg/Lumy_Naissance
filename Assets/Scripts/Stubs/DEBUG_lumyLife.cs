using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_lumyLife : MonoBehaviour {

    private Image healthBar;
    private float maxHealth = 0f;
    private float health = 0f;
    private Toggle tog;
    // Use this for initialization
    void Start () {
        Transform child = GetComponentInChildren<Image>().transform.GetChild(0);
        healthBar = child.GetComponent<Image>();
        maxHealth = GetComponentInParent<AgentScript>().VitalityMax;
	}
	
	// Update is called once per frame
	void Update () {

        if (DEBUG_Manager.instance.debuglumyLife == true) {

            gameObject.SetActive(true);

            health = GetComponentInParent<AgentScript>().Vitality;
            healthBar.fillAmount = health / maxHealth;
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
