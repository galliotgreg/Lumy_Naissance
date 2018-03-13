using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour {

    public static OptionManager instance = null;

    [SerializeField]
    private Toggle visionRange;
    [SerializeField]
    private Toggle atkRange;
    [SerializeField]
    private Toggle pickRange;
    [SerializeField]
    private Toggle lifeBar;
    [SerializeField]
    private Toggle lumyName;
    [SerializeField]
    private Toggle directionLumy;
    [SerializeField]
    private Toggle lumyCost;
    [SerializeField]
    private Toggle lumycComponents;
    [SerializeField]
    private Toggle lumyAction;
    [SerializeField]
    private Toggle trace;
    [SerializeField]
    private Toggle gisements;
    [SerializeField]
    private Toggle suiviRessources;

    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        DebugGisement();

    }

    private void DebugGisement()
    {

       GameObject[] minerais = GameObject.FindGameObjectsWithTag("minerais");

        if (gisements.isOn == true)
        {
            foreach (GameObject ressource in minerais)
            {
                ressource.transform.GetChild(0).gameObject.SetActive(true);
                string stock = ressource.GetComponent<ResourceScript>().Stock.ToString();
                Text stockText = ressource.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
                stockText.text = stock;
            }
        }
        else
        {
            foreach (GameObject ressource in minerais)
            {
                ressource.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
   

    private void DebugLifebar()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");
        if (lifeBar.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {
                /*lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                health = GetComponentInParent<AgentScript>().Vitality;
                maxHealth = 
                healthBar.fillAmount = health / maxHealth;*/

            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {
                lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
