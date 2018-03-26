using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour {

    public static OptionManager instance = null;

    [Header("Joueur 1")]
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
    private Toggle trace;

    [Header("Joueur 2")]
    [SerializeField]
    private Toggle visionRangeJ2;
    [SerializeField]
    private Toggle atkRangeJ2;
    [SerializeField]
    private Toggle pickRangeJ2;
    [SerializeField]
    private Toggle lifeBarJ2;
    [SerializeField]
    private Toggle lumyNameJ2;
    [SerializeField]
    private Toggle directionLumyJ2;
    [SerializeField]
    private Toggle traceJ2;

    [Header("General")]
    [SerializeField]
    private Toggle gisements;


    private float rangeVision;
    private float rangePick;
    private float rangeAttack;


   

    public Toggle DirectionLumy
    {
        get
        {
            return directionLumy;
        }
    }

    public Toggle DirectionLumyJ2 {
        get {
            return directionLumyJ2;
        }
    }

    public Toggle Trace {
        get {
            return trace;
        }
    }

    public Toggle TraceJ2 {
        get {
            return traceJ2;
        }
    }

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

        OptionManager.instance.getPlayerPreferencesDebug();
    }
	
	// Update is called once per frame
	void Update () {

        DebugGisement();
        DebugLifebarJ1();
        DebugLifebarJ2();
        DebugAtkRangeJ1();
        DebugAtkRangeJ2();
        DebugVisionRangeJ1();
        DebugVisionRangeJ2();
        DebugPickRangeJ1();
        DebugPickRangeJ2();
        DebugLumyNameJ1();
        DebugLumyNameJ2();

        DebugDirectionJ1();
        DebugDirectionJ2();
        DebugTraceJ1();
        DebugTraceJ2();
        
    }



    private void DebugDirectionJ1() {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Alpha1)) {
            directionLumy.isOn = !directionLumy.isOn;
        }
    }

    private void DebugDirectionJ2() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Alpha2)) {
            directionLumyJ2.isOn = !directionLumyJ2.isOn;
        }
    }

    private void DebugTraceJ1() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.Alpha1)) {
            trace.isOn = !trace.isOn;
        }
    }

    private void DebugTraceJ2() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.Alpha2)) {
            traceJ2.isOn = !traceJ2.isOn;
        }
    }

    private void DebugGisement()
    {

       GameObject[] minerais = GameObject.FindGameObjectsWithTag("minerais");

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.G)) {
            gisements.isOn = !gisements.isOn;
        }

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
   

    private void DebugLifebarJ1()
    {
        float health;
        float maxHealth;
        Image healthbar;
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.L) && Input.GetKeyDown(KeyCode.Alpha1)) {
            lifeBar.isOn = !lifeBar.isOn;
        }

        if (lifeBar.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {
                if(lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {

                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    maxHealth = lumy.transform.GetChild(1).GetComponent<AgentScript>().VitalityMax;
                    healthbar = lumy.transform.GetChild(1).transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
                    health = lumy.transform.GetChild(1).GetComponent<AgentScript>().Vitality;
                    healthbar.fillAmount = health / maxHealth;
                }

            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugLifebarJ2() {
        float health;
        float maxHealth;
        Image healthbar;
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.L) && Input.GetKeyDown(KeyCode.Alpha2)) {
            lifeBarJ2.isOn = !lifeBarJ2.isOn;
        }

        if (lifeBarJ2.isOn == true) {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {

                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    maxHealth = lumy.transform.GetChild(1).GetComponent<AgentScript>().VitalityMax;
                    healthbar = lumy.transform.GetChild(1).transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
                    health = lumy.transform.GetChild(1).GetComponent<AgentScript>().Vitality;
                    healthbar.fillAmount = health / maxHealth;
                }

            }
        }
        else {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugLumyNameJ1() {

        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.Alpha1)) {
            lumyName.isOn = !lumyName.isOn;
        }

        if (lumyName.isOn == true) {
            foreach (GameObject lumy in lumys) {


                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {

                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    string name = lumy.transform.GetChild(1).GetComponent<AgentScript>().Cast;
                    Text castName = lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
                    castName.text = name;
                }
            }
        }
        else {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugLumyNameJ2() {

        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.Alpha2)) {
            lumyNameJ2.isOn = !lumyNameJ2.isOn;
        }

        if (lumyNameJ2.isOn == true) {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    string name = lumy.transform.GetChild(1).GetComponent<AgentScript>().Cast;
                    Text castName = lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
                    castName.text = name;
                }

            }
        }
        else {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    lumy.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugVisionRangeJ1()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.V) && Input.GetKeyDown(KeyCode.Alpha1)) {
            visionRange.isOn = !visionRange.isOn;
        }


        if (visionRange.isOn == true)
        {
            foreach(GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    Transform vRange = lumy.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
                    vRange.gameObject.SetActive(true);
                    rangeVision = lumy.transform.GetChild(1).GetComponent<AgentScript>().VisionRange;

                    //vRange.localScale = new Vector3(rangeVision * 2, rangeVision * 2, 1f);
                    vRange.localScale = new Vector3(rangeVision * 2, 0.01f, rangeVision * 2);
                }
            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    Transform vRange = lumy.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
                    vRange.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugVisionRangeJ2() {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.V) && Input.GetKeyDown(KeyCode.Alpha2)) {
            visionRangeJ2.isOn = !visionRangeJ2.isOn;
        }


        if (visionRangeJ2.isOn == true) {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    Transform vRange = lumy.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
                    vRange.gameObject.SetActive(true);
                    rangeVision = lumy.transform.GetChild(1).GetComponent<AgentScript>().VisionRange;

                    //vRange.localScale = new Vector3(rangeVision * 2, rangeVision * 2, 1f);
                    vRange.localScale = new Vector3(rangeVision * 2,0.01f, rangeVision * 2);
                }
            }
        }
        else {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    Transform vRange = lumy.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.transform;
                    vRange.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugAtkRangeJ1()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Alpha1)) {
            atkRange.isOn = !atkRange.isOn;
        }

        if (atkRange.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    Transform attackRange = lumy.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
                    attackRange.gameObject.SetActive(true);
                    rangeAttack = lumy.transform.GetChild(1).GetComponent<AgentScript>().AtkRange;

                    //attackRange.localScale = new Vector3(rangeAttack * 2, rangeAttack * 2, 1f);
                    attackRange.localScale = new Vector3(rangeAttack * 2, 0.01f, rangeAttack * 2);
                }
            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    Transform attackRange = lumy.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
                    attackRange.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugAtkRangeJ2() {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Alpha2)) {
            atkRangeJ2.isOn = !atkRangeJ2.isOn;
        }

        if (atkRangeJ2.isOn == true) {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    Transform attackRange = lumy.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
                    attackRange.gameObject.SetActive(true);
                    rangeAttack = lumy.transform.GetChild(1).GetComponent<AgentScript>().AtkRange;

                    //attackRange.localScale = new Vector3(rangeAttack * 2,rangeAttack * 2, 1f);
                    attackRange.localScale = new Vector3(rangeAttack * 2, 0.01f, rangeAttack * 2);
                }
            }
        }
        else {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    Transform attackRange = lumy.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
                    attackRange.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugPickRangeJ1()
    {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.Alpha1)) {
            pickRange.isOn = !pickRange.isOn;
        }


        if (pickRange.isOn == true)
        {
            foreach (GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    Transform pkRange = lumy.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
                    pkRange.gameObject.SetActive(true);
                    rangePick = lumy.transform.GetChild(1).GetComponent<AgentScript>().PickRange;

                    //pkRange.localScale = new Vector3(rangePick * 2,rangePick * 2, 1f);
                    pkRange.localScale = new Vector3(rangePick * 2, 0.01f, rangePick * 2);
                }
            }
        }
        else
        {
            foreach (GameObject lumy in lumys)
            {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                    Transform pkRange = lumy.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
                    pkRange.gameObject.SetActive(false);
                }
            }
        }
    }

    private void DebugPickRangeJ2() {
        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.Alpha2)) {
            pickRangeJ2.isOn = !pickRangeJ2.isOn;
        }


        if (pickRangeJ2.isOn == true) {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    Transform pkRange = lumy.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
                    pkRange.gameObject.SetActive(true);
                    rangePick = lumy.transform.GetChild(1).GetComponent<AgentScript>().PickRange;

                    //pkRange.localScale = new Vector3(rangePick * 2,rangePick * 2, 1f);
                    pkRange.localScale = new Vector3(rangePick * 2, 0.01f, rangePick * 2);
                }
            }
        }
        else {
            foreach (GameObject lumy in lumys) {

                if (lumy.GetComponent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                    Transform pkRange = lumy.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.transform;
                    pkRange.gameObject.SetActive(false);
                }
            }
        }
    }

    //Getter/setters Player preferences debug
    public void setPlayerPreferencesDebug()
    {
        SwapManager.instance.setPlayerGisementKey(gisements.isOn);
        #region set Player J1 preference Debug
        SwapManager.instance.setPlayerAtkKey(PlayerAuthority.Player1, atkRange.isOn);
        SwapManager.instance.setPlayerVisionKey(PlayerAuthority.Player1, visionRange.isOn);
        SwapManager.instance.setPlayerPickKey(PlayerAuthority.Player1, pickRange.isOn);
        SwapManager.instance.setPlayerLifeBarKey(PlayerAuthority.Player1, lifeBar.isOn);
        SwapManager.instance.setPlayerLumyNameKey(PlayerAuthority.Player1, lumyName.isOn);
        SwapManager.instance.setPlayerDirectionLumyKey(PlayerAuthority.Player1, directionLumy.isOn);
        SwapManager.instance.setPlayerTraceKey(PlayerAuthority.Player1, trace.isOn);
        #endregion
        #region set Player J2 preference Debug
        SwapManager.instance.setPlayerAtkKey(PlayerAuthority.Player2, OptionManager.instance.atkRangeJ2);
        SwapManager.instance.setPlayerVisionKey(PlayerAuthority.Player2, OptionManager.instance.visionRangeJ2);
        SwapManager.instance.setPlayerPickKey(PlayerAuthority.Player2, OptionManager.instance.pickRangeJ2);
        SwapManager.instance.setPlayerLifeBarKey(PlayerAuthority.Player2, OptionManager.instance.lifeBarJ2);
        SwapManager.instance.setPlayerLumyNameKey(PlayerAuthority.Player2, OptionManager.instance.lumyNameJ2);
        SwapManager.instance.setPlayerDirectionLumyKey(PlayerAuthority.Player2, OptionManager.instance.directionLumyJ2);
        SwapManager.instance.setPlayerTraceKey(PlayerAuthority.Player2, OptionManager.instance.traceJ2);
        #endregion
        
    }

    public void getPlayerPreferencesDebug()
    {
        gisements.isOn = SwapManager.instance.getPlayerGisementKey();
        #region set Player J1 preference Debug
        atkRange.isOn = SwapManager.instance.getPlayerAtkKey(PlayerAuthority.Player1);
        visionRange.isOn = SwapManager.instance.getPlayerVisionKey(PlayerAuthority.Player1);
        pickRange.isOn= SwapManager.instance.getPlayerPickKey(PlayerAuthority.Player1);
        lifeBar.isOn = SwapManager.instance.getPlayerLifeBarKey(PlayerAuthority.Player1);
        lumyName.isOn = SwapManager.instance.getPlayerLumyNameKey(PlayerAuthority.Player1);
        directionLumy.isOn = SwapManager.instance.getPlayerDirectionLumyKey(PlayerAuthority.Player1);
        trace.isOn = SwapManager.instance.getPlayerTraceKey(PlayerAuthority.Player1);
        #endregion
        #region set Player J2 preference Debug
        atkRangeJ2.isOn = SwapManager.instance.getPlayerAtkKey(PlayerAuthority.Player2);
        visionRangeJ2.isOn = SwapManager.instance.getPlayerVisionKey(PlayerAuthority.Player2);
        pickRangeJ2.isOn = SwapManager.instance.getPlayerPickKey(PlayerAuthority.Player2);
        lifeBarJ2.isOn = SwapManager.instance.getPlayerLifeBarKey(PlayerAuthority.Player2);
        lumyNameJ2.isOn = SwapManager.instance.getPlayerLumyNameKey(PlayerAuthority.Player2);
        directionLumyJ2.isOn = SwapManager.instance.getPlayerDirectionLumyKey(PlayerAuthority.Player2);
        traceJ2.isOn = SwapManager.instance.getPlayerTraceKey(PlayerAuthority.Player2);
        #endregion
    }

}
