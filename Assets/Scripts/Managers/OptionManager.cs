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
    public Toggle gisements;
    [SerializeField]
    private Toggle suiviRessources;

    [SerializeField]
    public bool gisementsBool = false;


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

        gisementsBool = gisements.isOn;
    }

    public GameObject[] GetMinerals()
    {
       GameObject[] minerais;

        minerais = GameObject.FindGameObjectsWithTag("minerais");
        return minerais;
    }
}
