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

    private float rangeVision;
    private float rangePick;
    private float rangeAttack;

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
        DebugLifebar();
        DebugAtkRange();
        DebugVisionRange();
        DebugPickRange();
        DebugLumyName();
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
        float health;
        float maxHealth;
        Image healthbar;
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");
        if (lifeBar.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {
                lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                maxHealth = lumy.transform.GetChild(1).GetComponent<AgentScript>().VitalityMax;
                healthbar = lumy.transform.GetChild(1).transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
                health = lumy.transform.GetChild(1).GetComponent<AgentScript>().Vitality;
                healthbar.fillAmount = health / maxHealth;

            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {
                lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    private void DebugLumyName() {

        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (lumyName.isOn == true) {
            foreach (GameObject lumy in lumys) {
                lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                string name = lumy.transform.GetChild(1).GetComponent<AgentScript>().Cast;
                Text castName = lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
                castName.text = name;

            }
        }
        else {
            foreach (GameObject lumy in lumys) {
                lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void DebugVisionRange()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");
        if(visionRange.isOn == true)
        {
            foreach(GameObject lumy in lumys)
            {
                Transform vRange = lumy.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
                vRange.gameObject.SetActive(true);
                rangeVision = lumy.transform.GetChild(1).GetComponent<AgentScript>().VisionRange;

                vRange.localScale = new Vector3(rangeVision * 2, 0.01f, rangeVision * 2);
            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {
                Transform vRange = lumy.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
                vRange.gameObject.SetActive(false);
            }
        }
    }

    private void DebugAtkRange()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");
        if (atkRange.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {
                Transform attackRange = lumy.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
                attackRange.gameObject.SetActive(true);
                rangeAttack = lumy.transform.GetChild(1).GetComponent<AgentScript>().AtkRange;

                attackRange.localScale = new Vector3(rangeAttack * 2, 0.01f, rangeAttack * 2);
            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {
                Transform attackRange = lumy.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
                attackRange.gameObject.SetActive(false);
            }
        }
    }

    private void DebugPickRange()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");
        if (pickRange.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {
                Transform pkRange = lumy.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
                pkRange.gameObject.SetActive(true);
                rangePick = lumy.transform.GetChild(1).GetComponent<AgentScript>().PickRange;

                pkRange.localScale = new Vector3(rangePick * 2, 0.01f, rangePick * 2);
            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {
                Transform pkRange = lumy.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
                pkRange.gameObject.SetActive(false);
            }
        }
    }
}
