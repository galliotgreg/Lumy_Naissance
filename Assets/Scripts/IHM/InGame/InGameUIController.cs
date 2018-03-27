using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour {

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static InGameUIController instance = null;

    private float startTime = 2.0f;
    private bool winState = false;

    #region UIVariables
    #region PlayerInfosPanel
    /// <summary>
    /// Resources 
    /// </summary>
    [Header("Player Infos Panel")]
    [SerializeField]
    private Text J1_Red_Resources;
    [SerializeField]
    private Text J1_Green_Resources;
    [SerializeField]
    private Text J1_Blue_Resources;
    [SerializeField]
    private Text J1_Pop;
    [SerializeField]
    private Text J1_Species;
    [SerializeField]
    private Text J1_PrysmeLife;
    [SerializeField]
    private Text J2_Red_Resources;
    [SerializeField]
    private Text J2_Green_Resources;
    [SerializeField]
    private Text J2_Blue_Resources;
    [SerializeField]
    private Text J2_Pop;
    [SerializeField]
    private Text J2_Species;
    [SerializeField]
    private Text J2_PrysmeLife;

    #endregion

    #region MainMenu
    [Header("Main Menu")]
    [SerializeField]
    private Button Menu_MainMenu;
    [SerializeField]
    private Button Menu_PersonnalizedMap;
    [SerializeField]
    private Button Menu_Caste;
    [SerializeField]
    private Button Menu_OptionsDebug;
    #endregion

    #region VictoryScreen
    [Header("Victory Screen")]
    [SerializeField]
    private GameObject victoryMenu;
    [SerializeField]
    private Text victory;
    [SerializeField]
    private Text J1_Resources;
    [SerializeField]
    private Text J2_Resources;
    [SerializeField]
    private Button Caste_Menu;
    [SerializeField]
    private Button quitVictory;

    #endregion

    [SerializeField]
    private Button Menu;
    [SerializeField]
    private GameObject subMenu;
    [SerializeField]
    private GameObject panelOptionsDebug;

    /// <summary>
    /// Timer 
    /// </summary>
    [Header("Timer")]
    [SerializeField]
    private Text timer;
    [SerializeField]
    private Button playPause;

    #region ExitMenu
    /// <summary>
    /// Exit Menu
    /// </summary>
    [Header("Exit Menu")]
    [SerializeField]
    private GameObject exitMenu;
    [SerializeField]
    private Button quit_ExitMenu;
    [SerializeField]
    private Button cancel_ExitMenu;
    [SerializeField]
    private Canvas canvas;

    #endregion

    #region StatsLumy
    [Header("Stats Lumy")]
    [SerializeField]
    private Text vitalityText;
    [SerializeField]
    private Text strenghtText;
    [SerializeField]
    private Text staminaText;
    [SerializeField]
    private Text moveSpeedText;
    [SerializeField]
    private Text actionSpeedText;
    [SerializeField]
    private Text visionText;
    [SerializeField]
    private Text pickupRangeText;
    [SerializeField]
    private Text strikeRangeText;
    [SerializeField]
    private Text curPosText;
    [SerializeField]
    private Text trgPosText;
    [SerializeField]
    private Text LayTimeText;
    [SerializeField]
    private Text castText;
    [SerializeField]
    private Text item;
    [SerializeField]
    private GameObject unitGoJ1;
    [SerializeField]
    private GameObject unitGoJ2;
    [SerializeField]
    private GameObject contentParentJ1;
    [SerializeField]
    private GameObject contentParentJ2;
    [SerializeField]
    private Text unitCostRedText;
    [SerializeField]
    private Text unitCostGreenText;
    [SerializeField]
    private Text unitCostBlueText;
    [SerializeField]
    private Text alliesInSightText;
    [SerializeField]
    private Text ennemiesInSightText;
    [SerializeField]
    private Text ressourcesInSightText;
    [SerializeField]
    private Text tracesInSightText;
    #endregion


    Dictionary<string, int> castsJ1;
    Dictionary<string, int> castsJ2;

    /// <summary>
    /// Variables for CastUIDisplay
    /// </summary>
    private List<GameObject> castUiList = new List<GameObject>();
    private List<GameObject> castUiListJ2 = new List<GameObject>();
    private Dictionary<string, int> popJ1 = new Dictionary<string, int>();
    private Dictionary<string, int> popJ2 = new Dictionary<string, int>();


    private List<GameObject> queens = new List<GameObject>();

    private float newAmount = 0f;
    private float newAmountJ2 = 0f;
    private float[] newAmountColor = { 0, 0, 0 };
    private float[] newAmountColorJ2 = { 0, 0, 0 };
    private float depenseLigne = 0;
    private float gainLigne = 0;

    [Header("Prysme Text")]
    [SerializeField]
    private Text positiveRedText;
    [SerializeField]
    private Text negativeRedText;
    [SerializeField]
    private Text positiveBlueText;
    [SerializeField]
    private Text negativeBlueText;
    [SerializeField]
    private Text positiveGreenText;
    [SerializeField]
    private Text negativeGreenText;
    [SerializeField]
    private Text positiveRedTextJ2;
    [SerializeField]
    private Text negativeRedTextJ2;
    [SerializeField]
    private Text positiveBlueTextJ2;
    [SerializeField]
    private Text negativeBlueTextJ2;
    [SerializeField]
    private Text positiveGreenTextJ2;
    [SerializeField]
    private Text negativeGreenTextJ2;
    [SerializeField]
    private float waitingTime = 1f;

    //button valid debugg params
    [SerializeField]
    private Button valider;

    private bool isDisplayingNegativeResJ1 =false;
    private bool isDisplayingPositiveResJ1 = false;
    private bool isDisplayingNegativeResJ2 = false;
    private bool isDisplayingPositiveResJ2 = false;
    private DateTime tsNegativeJ1;
    private DateTime tsPositiveJ1;
    private DateTime tsNegativeJ2;
    private DateTime tsPositiveJ2;

    #region Instance
    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
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
    #endregion
    #endregion
    GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        Init();
        if(!isNotNull())
            return;
        popJ1 = new Dictionary<string, int>(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
        popJ2 = new Dictionary<string, int>(GameObject.Find("p2_hive").GetComponent<HomeScript>().Population);
    }

    /// <summary>
    /// Init the Controller 
    /// </summary>
	private void Init()
    {
        //Get gameManager Instance 
        gameManager = GameManager.instance;

        Camera camera = NavigationManager.instance.GetCurrentCamera();
        canvas.worldCamera = camera;
        //Init all Listener
        //Exit Menu
        cancel_ExitMenu.onClick.AddListener(CloseExitMenu);
        quit_ExitMenu.onClick.AddListener(ExitGame);
        //Victory Menu 
        Caste_Menu.onClick.AddListener(GoToCasteMenu);
        quitVictory.onClick.AddListener(ExitGame);
        Menu_MainMenu.onClick.AddListener(GoToMainMenu);
        Menu_PersonnalizedMap.onClick.AddListener(GoToPersonnalizedMap);
        Menu_Caste.onClick.AddListener(GoToCasteMenu);
        Menu_OptionsDebug.onClick.AddListener(OpenOptionsDebug);

        Menu.onClick.AddListener(SwitchMenu);

        valider.onClick.AddListener(OptionManager.instance.setPlayerPreferencesDebug);


        playPause.onClick.AddListener(PauseGame);

        //Player Species 
        J1_Species.text = SwapManager.instance.GetPlayer1Name();
        J2_Species.text = SwapManager.instance.GetPlayer2Name();

        GameObject[] lumys = GameObject.FindGameObjectsWithTag("Agent");

        foreach (GameObject lumy in lumys) {
            if (lumy.gameObject.name == "p1_queen" || lumy.gameObject.name == "p2_queen") {
                queens.Add(lumy);
            }
        }
    }

    private void PauseGame()
    {
        GameManager.instance.PauseGame(); 
    }


    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        CheckKeys(); 
        UpdateUI();
    }

    /// <summary>
    /// Check all the buttons on the Scene 
    /// </summary>
    private void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            exitMenu.SetActive(!exitMenu.activeSelf);
    }

    #region WinConditions
    /// <summary>
    /// Check if the winner variable is on a Win State 
    /// </summary>
    private void CheckWinCondition()
    {
        //Wait until Game is Initialized 
        //All the Hive to be instantiate for example
        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            return; 
        }

        //Check Win Conditions 
        GameManager.Winner winner = gameManager.WinnerPlayer;
        if (!winState)
        {
            if (winner != GameManager.Winner.None)
            {
                winState = true;
                victoryMenu.SetActive(true);
                if (winner == GameManager.Winner.Player1R)
                {
                    victory.text = "Victoire du Joueur 1 : Avantage à la ressources";
                }
                if (winner == GameManager.Winner.Player2R)
                {
                    victory.text = " Victoire du Joueur 2 : Avantage à la ressources";
                }
                if (winner == GameManager.Winner.Player1Q) {
                    victory.text = "Victoire du Joueur 1 : Destruction du prysme adverse";
                }
                if (winner == GameManager.Winner.Player2Q) {
                    victory.text = " Victoire du Joueur 2 : Destruction du prysme adverse";
                }
                if (winner == GameManager.Winner.Equality)
                {
                    victory.text = "Egalité ! ";
                }
                J1_Resources.text = "Resources : " + gameManager.sumResources(PlayerAuthority.Player1);
                J2_Resources.text = "Resources : " + gameManager.sumResources(PlayerAuthority.Player2);
            }
        }
       
    }
    #endregion

    /// <summary>
    /// Update the UI with the parameters : Resources and Timer
    /// </summary>
    private void UpdateUI()
    {
        //Get Resources values in game Manager
        if (gameManager == null)
            return; 

        float[] res = gameManager.GetResources(PlayerAuthority.Player1);  

        if (CheckRes(res))
        {
            J1_Red_Resources.text = "" + res[0];
            J1_Green_Resources.text = "" + res[1];
            J1_Blue_Resources.text = "" + res[2];
        }

        res = gameManager.GetResources(PlayerAuthority.Player2); 
        if(CheckRes(res))
        {
            J2_Red_Resources.text = "" + res[0];
            J2_Green_Resources.text = "" + res[1];
            J2_Blue_Resources.text = "" + res[2];
        }

        if (gameManager.TimerLeft != null)
        {
            TimeSpan t = TimeSpan.FromSeconds(gameManager.TimerLeft); 
            timer.text = t.Minutes + " : " + t.Seconds;
            if (t.Seconds <10)
            {
                timer.text = t.Minutes + " : 0" + t.Seconds;  
            }
            if (gameManager.TimerLeft <=30)
            {
                timer.color = new Color(255, 0, 0,255);
                timer.fontStyle = FontStyle.Bold; 
            }
        }
        
        J1_Pop.text = "" +gameManager.GetHome(PlayerAuthority.Player1).getPopulation().Count;    
        J2_Pop.text = "" + gameManager.GetHome(PlayerAuthority.Player2).getPopulation().Count;




        J1_PrysmeLife.text = queens[0].transform.GetChild(1).GetComponent<AgentScript>().Vitality.ToString() + " / " + queens[0].transform.GetChild(1).GetComponent<AgentScript>().VitalityMax.ToString();
        J2_PrysmeLife.text = queens[1].transform.GetChild(1).GetComponent<AgentScript>().Vitality.ToString() + " / " + queens[1].transform.GetChild(1).GetComponent<AgentScript>().VitalityMax.ToString();

        UnitStats();
        DisplayInSight();
        unitCost();
        displayRessourceJ1();
        displayRessourceJ2();
        getAllUnit(PlayerAuthority.Player1);
        getAllUnit(PlayerAuthority.Player2);
    }


    /// <summary>
    /// Check is Resources from GameManager is ok
    /// </summary>
    /// <param name="res"></param>
    /// <returns></returns>
    private bool CheckRes(float[] res)
    {
        if (res == null)
        {
            Debug.LogError("Res null in GUI");
            return false;

        }
        if (res.Length != 3)
        {
            Debug.LogError("Not 3 Resources for player in UI");
            return false ;
        }
        return true;
    }

    #region Validator
    /// <summary>
    /// Check is UI gameobjetcs are not null 
    /// </summary>
    /// <returns>Return a boolean</returns>
    private bool isNotNull()
    {
        if (J1_Red_Resources == null)
        {
            Debug.LogError("InGame UI Error : Red Resource not set for J1");
            return false; 
        }
        if (J2_Red_Resources == null)
        {
            Debug.LogError("InGame UI Error : Red Resource not set for J2");
            return false;
        }
        if (J1_Green_Resources == null)
        {
            Debug.LogError("InGame UI Error : Green Resource not set for J1");
            return false;
        }
        if (J2_Green_Resources == null)
        {
            Debug.LogError("InGame UI Error : Green Resource not set for J2");
            return false;
        }
        if (J1_Blue_Resources == null)
        {
            Debug.LogError("InGame UI Error : Blue Resource not set for J1");
            return false;
        }
        if (J2_Blue_Resources == null)
        {
            Debug.LogError("InGame UI Error : Blue Resource not set for J2");
            return false;
        }
        if(timer == null)
        {
            Debug.LogError("InGame UI Error : Timer not set ");
            return false; 
        }
        if(exitMenu == null)
        {
            Debug.LogError("Ingame UI Error : Exit Menu not set");
            return false; 
        }
        if(quit_ExitMenu == null)
        {
            Debug.LogError("Ingame UI Error : Exit Button Menu not set");
            return false; 
        }
        if(cancel_ExitMenu == null)
        {
            Debug.LogError("Ingame UI Error : Cancel Button Menu not set");
            return false; 
        }
        if(canvas == null)
        {
            Debug.LogError("Ingame UI Error : Canvas not set");
            return false; 
        }
        if(victoryMenu == null)
        {
            Debug.LogError("Ingame UI Error : Victory Menu not Set");
            return false;
        }
        if(victory == null )
        {
            Debug.LogError("Ingame UI Error : Victory field not set");
            return false; 
        }
        if(J1_Resources == null)
        {
            Debug.LogError("Ingame UI Error : J1 resources not set");
            return false;
        }
        if(J2_Resources == null)
        {
            Debug.LogError("Ingame UI Error : J2 resources not set ");
            return false; 
        }
        if(Caste_Menu == null)
        {
            Debug.LogError("Ingame UI Error : Menu Victory Caste button not set");
            return false; 
        }
        if(quitVictory == null)
        {
            Debug.LogError("Ingame UI Error : Menu Victory quit buttton not set");
            return false; 
        }

        if(Menu_Caste == null)
        {
            Debug.LogError("InGame UI Error : Menu/Caste Menu button not set"); 
        }
        if (Menu_MainMenu == null)
        {
            Debug.LogError("InGame UI Error : Menu/Principal Menu button not set");
        }
        if (Menu_PersonnalizedMap == null)
        {
            Debug.LogError("InGame UI Error : Menu/Personnalized Menu button not set");
        }


        return true; 
    }
    #endregion

    #region BtnListener
    private void CloseExitMenu()
    {
        exitMenu.SetActive(false);
    }

    private void ExitGame()
    
    {
      
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        CheckPause();
        NavigationManager.instance.SwapScenesWithoutZoom("PartiePersoScene"); 
    }
    private void GoToCasteMenu()
    {
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        CheckPause();
        NavigationManager.instance.SwapScenesWithoutZoom("EditeurCastesScene");
    }

    private void GoToMainMenu()
    {
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        CheckPause();
        NavigationManager.instance.SwapScenesWithoutZoom("MenuPrincipalScene");
      
    }

    private void GoToPersonnalizedMap()
    {
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        CheckPause();
        NavigationManager.instance.SwapScenesWithoutZoom("PartiePersoScene");

    }

    private void OpenOptionsDebug() {

        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        panelOptionsDebug.SetActive(!panelOptionsDebug.activeSelf);

    }

    private void CheckPause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1; 
        }
    }

    void SwitchMenu() {
        subMenu.SetActive(!subMenu.activeSelf);
    }
    #endregion

    private void UnitStats()
    {
        AgentScript self = getUnitSelf();
        GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Agent");

        if (self == null)
        {
            vitalityText.color = Color.white;
            strenghtText.color = Color.white;
            staminaText.color = Color.white;
            moveSpeedText.color = Color.white;
            actionSpeedText.color = Color.white;
            visionText.color = Color.white;
            pickupRangeText.color = Color.white;
            strikeRangeText.color = Color.white;
            item.color = Color.white;
            LayTimeText.color = Color.white;
            castText.color = Color.white;

            vitalityText.text = "-";
            strenghtText.text = "-";
            staminaText.text = "-";
            moveSpeedText.text = "-";
            actionSpeedText.text = "-";
            visionText.text = "-";
            pickupRangeText.text = "-";
            strikeRangeText.text = "-";
            item.text = "-";
            LayTimeText.text = "-";
            castText.text = "-";

            foreach (GameObject agent in allUnits)
            {
                agent.gameObject.transform.GetChild(1).GetComponent<AgentScript>().gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }

            return; 
        }

        foreach (GameObject agent in allUnits)
        {
            if(agent.gameObject.transform.GetChild(1).GetComponent<AgentScript>() != self)
            {
                agent.gameObject.transform.GetChild(1).GetComponent<AgentScript>().gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(agent.gameObject.transform.GetChild(1).GetComponent<AgentScript>() == self)
            {
                agent.gameObject.transform.GetChild(1).GetComponent<AgentScript>().gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        string vitality = self.Vitality.ToString();
        string visionRange = self.VisionRange.ToString();
        string vitalityMax = self.VitalityMax.ToString();
        string strength = self.Strength.ToString();
        string pickRange = self.PickRange.ToString();
        string atkRange = self.AtkRange.ToString();
        string actSpeed = self.ActSpd.ToString();
        string moveSpeed = self.MoveSpd.ToString();
        string nbItemMax = self.NbItemMax.ToString();
        string nbItem = self.NbItem.ToString();
        string layTimeCost = self.LayTimeCost.ToString();
        string stamina = self.Stamina.ToString();
        string cast = self.Cast;
        
        if(self.GetComponentInParent<AgentContext>().Home.gameObject.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
            vitalityText.color = Color.blue;
            strenghtText.color = Color.blue;
            staminaText.color = Color.blue;
            moveSpeedText.color = Color.blue;
            actionSpeedText.color = Color.blue;
            visionText.color = Color.blue;
            pickupRangeText.color = Color.blue;
            strikeRangeText.color = Color.blue;
            item.color = Color.blue;
            LayTimeText.color = Color.blue;
            castText.color = Color.blue;
        }
        if (self.GetComponentInParent<AgentContext>().Home.gameObject.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
            vitalityText.color = Color.red;
            strenghtText.color = Color.red;
            staminaText.color = Color.red;
            moveSpeedText.color = Color.red;
            actionSpeedText.color = Color.red;
            visionText.color = Color.red;
            pickupRangeText.color = Color.red;
            strikeRangeText.color = Color.red;
            item.color = Color.red;
            LayTimeText.color = Color.red;
            castText.color = Color.red;
        }
        vitalityText.text = vitality + " / " + self.VitalityMax.ToString();
        strenghtText.text = strength;
        staminaText.text = stamina.ToString();
        moveSpeedText.text = moveSpeed;
        actionSpeedText.text = actSpeed;
        visionText.text = visionRange;
        pickupRangeText.text = pickRange;
        strikeRangeText.text = atkRange;
        item.text = nbItem + " / " + nbItemMax;
        LayTimeText.text = layTimeCost;
        castText.text = cast;
        
        

    }

   
    private AgentScript getUnitSelf()
    {
        Camera camera = NavigationManager.instance.GetCurrentCamera();
        if (camera != null)
        {
            CameraRay cameraRay = camera.GetComponent<CameraRay>();
            if (cameraRay != null)
            {
                return cameraRay.Self;
            }
        }
        return null; 
    }

    private void getCurAction()
    {
        Camera camera = NavigationManager.instance.GetCurrentCamera();
        string action = camera.GetComponent<CameraRay>().Action;
    }

    //TODO REMOVE once implemented in UI 

    private Dictionary<string, int> getAllUnit(PlayerAuthority player) {
        if (PlayerAuthority.Player1 == player) {
            if (!CheckDicoEquality(popJ1, GameObject.Find("p1_hive").GetComponent<HomeScript>().Population)) {
                DisplayUnits(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
                popJ1 = new Dictionary<string, int>(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
            }

        }
        if (PlayerAuthority.Player2 == player) {
            if (!CheckDicoEquality(popJ2, GameObject.Find("p2_hive").GetComponent<HomeScript>().Population)) {
                DisplayUnitsJ2(GameObject.Find("p2_hive").GetComponent<HomeScript>().Population);
                popJ2 = new Dictionary<string, int>(GameObject.Find("p2_hive").GetComponent<HomeScript>().Population);
            }
        }
        return null; 
    }

   
    private bool CheckDicoEquality (Dictionary<string, int> dico1, Dictionary<string, int> dico2) {
        // check keys are the same
        
        foreach (string str in dico1.Keys) {
            if (!dico2.ContainsKey(str)) {
                return false;
            }      
        }
        // check values are the same
        foreach (string str in dico1.Keys) {
            if (!dico1[str].Equals(dico2[str])) {
                return false;
            } 
        }
        return true; 
    }





    private void DisplayUnits(Dictionary<string, int> units)
    {
        //Dictionary<string, int> units = getAllUnit(PlayerAuthority.Player1);
        //Clean list if element in it
        foreach (GameObject go in castUiList)
        {
            Destroy(go);
        }
        castUiList.Clear(); 
        //Create UI cast and add them to the list 
        foreach (KeyValuePair<string, int> unit in units)
        {
            if (unit.Value != 0)
            {
                GameObject go = Instantiate(unitGoJ1);
                castUiList.Add(go);
                go.transform.GetChild(0).GetComponent<Text>().color = Color.blue;
                go.transform.GetChild(1).GetComponent<Text>().color = Color.blue;
                go.transform.GetChild(0).GetComponent<Text>().text = unit.Key;
                go.transform.GetChild(1).GetComponent<Text>().text = unit.Value.ToString();
                //go.transform.SetParent(unitGoJ1.transform.parent.gameObject.transform);
                go.transform.SetParent(contentParentJ1.transform);
            }
        }
    }

    
    private void DisplayUnitsJ2(Dictionary<string, int> units) {
        //Dictionary<string, int> units = getAllUnit(PlayerAuthority.Player1);
        //Clean list if element in it
        foreach (GameObject go in castUiListJ2) {
            
            Destroy(go);
        }
        castUiListJ2.Clear();
        //Create UI cast and add them to the list 
        foreach (KeyValuePair<string, int> unit in units) {
            if (unit.Value != 0) {
                GameObject go = Instantiate(unitGoJ2);
                castUiListJ2.Add(go);
                go.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                go.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                go.transform.GetChild(0).GetComponent<Text>().text = unit.Key;
                go.transform.GetChild(1).GetComponent<Text>().text = unit.Value.ToString();
                //go.transform.SetParent(unitGoJ2.transform.parent.gameObject.transform);
                go.transform.SetParent(contentParentJ2.transform);
            }
        }
    }

    private void DisplayInSight() {
        AgentScript self = getUnitSelf();

        if(self == null) {
            alliesInSightText.color = Color.white;
            ressourcesInSightText.color = Color.white;
            ennemiesInSightText.color = Color.white;
            tracesInSightText.color = Color.white;
            alliesInSightText.text = "-";
            ressourcesInSightText.text = "-";
            ennemiesInSightText.text = "-";
            tracesInSightText.text = "-";
            return;
        }

        if(self.GetComponentInParent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
            alliesInSightText.color = Color.blue;
            ressourcesInSightText.color = Color.blue;
            ennemiesInSightText.color = Color.blue;
            tracesInSightText.color = Color.blue;
        }
        if (self.GetComponentInParent<AgentContext>().Home.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
            alliesInSightText.color = Color.red;
            ressourcesInSightText.color = Color.red;
            ennemiesInSightText.color = Color.red;
            tracesInSightText.color = Color.red;
        }

        alliesInSightText.text = self.GetComponentInParent<AgentContext>().Allies.Length.ToString();
        ressourcesInSightText.text = self.GetComponentInParent<AgentContext>().Resources.Length.ToString();
        ennemiesInSightText.text = self.GetComponentInParent<AgentContext>().Enemies.Length.ToString();
        tracesInSightText.text = self.GetComponentInParent<AgentContext>().Traces.Length.ToString();


    }

    public void unitCost() {
        AgentScript self = getUnitSelf();
        if (self == null) {
            //Set values to "-" like for stats
            unitCostRedText.color = Color.white;
            unitCostGreenText.color = Color.white;
            unitCostBlueText.color = Color.white;
            unitCostRedText.text = "-";
            unitCostGreenText.text = "-";
            unitCostBlueText.text = "-";
            return;
        }

        Dictionary<string, int> costs = self.ProdCost;
        foreach (KeyValuePair<string, int> cost in costs) {
            //Set color and count like stats for the lumy 
            string color = cost.Key;
            int count = cost.Value;

            if (self.GetComponentInParent<AgentContext>().Home.gameObject.GetComponent<HomeScript>().Authority == PlayerAuthority.Player1) {
                unitCostRedText.color = Color.blue;
                unitCostGreenText.color = Color.blue;
                unitCostBlueText.color = Color.blue;
            }
            if (self.GetComponentInParent<AgentContext>().Home.gameObject.GetComponent<HomeScript>().Authority == PlayerAuthority.Player2) {
                unitCostRedText.color = Color.red;
                unitCostGreenText.color = Color.red;
                unitCostBlueText.color = Color.red;
            }

            if (color == "Red") {
                unitCostRedText.text = count.ToString();
            }
            if (color == "Green") {
                unitCostGreenText.text = count.ToString();
            }
            if (color == "Blue") {
                unitCostBlueText.text = count.ToString();
            }
        }
        //Enjoy this incredible code ;) 

    }

    private void displayRessourceJ1() {
        GameObject homeP1 = GameManager.instance.P1_home;

        HomeScript p1_hiveScript = homeP1.GetComponent<HomeScript>();
        float oldAmount = p1_hiveScript.RedResAmout + p1_hiveScript.BlueResAmout + p1_hiveScript.GreenResAmout;
        float[] oldAmountColor = { p1_hiveScript.RedResAmout, p1_hiveScript.GreenResAmout, p1_hiveScript.BlueResAmout };
        int depense = (int) (oldAmount -  newAmount);
        int depenseB = (int)( oldAmountColor[2] - newAmountColor[2]);
        int depenseG = (int) (oldAmountColor[1] - newAmountColor[1]);
        int depenseR = (int) (oldAmountColor[0] - newAmountColor[0]);

        if (isDisplayingNegativeResJ1) {
            if (DateTime.Now > tsNegativeJ1) {
                isDisplayingNegativeResJ1 = false;
                negativeRedText.text = "-";
                negativeBlueText.text = "-";
                negativeGreenText.text = "-";
            }
        }
        else if (!isDisplayingNegativeResJ1) {
            if (depense < 0) {
                negativeRedText.text = depenseR.ToString();
                negativeBlueText.text = depenseB.ToString();
                negativeGreenText.text = depenseG.ToString();
                tsNegativeJ1 = DateTime.Now + TimeSpan.FromSeconds(waitingTime);
                isDisplayingNegativeResJ1 = true;
            }

        }
        if (isDisplayingPositiveResJ1) {
            if (DateTime.Now > tsPositiveJ1) {
                isDisplayingPositiveResJ1 = false;
                positiveRedText.text = "-";
                positiveBlueText.text = "-";
                positiveGreenText.text = "-";
            }
        }
        else if (!isDisplayingPositiveResJ1) {
            if (depense > 0) {
                positiveRedText.text = "+" + depenseR.ToString();
                positiveBlueText.text = "+" + depenseB.ToString();
                positiveGreenText.text = "+" + depenseG.ToString();
                tsPositiveJ1 = DateTime.Now + TimeSpan.FromSeconds(waitingTime);
                isDisplayingPositiveResJ1 = true;
            }

        }
        newAmount = oldAmount;
        newAmountColor[0] = oldAmountColor[0];
        newAmountColor[1] = oldAmountColor[1];
        newAmountColor[2] = oldAmountColor[2];
    }

    private void displayRessourceJ2() {
        GameObject homeP2 = GameManager.instance.P2_home;

        HomeScript p2_hiveScript = homeP2.GetComponent<HomeScript>();
        float oldAmount = p2_hiveScript.RedResAmout + p2_hiveScript.BlueResAmout + p2_hiveScript.GreenResAmout;
        float[] oldAmountColorJ2 = { p2_hiveScript.RedResAmout, p2_hiveScript.GreenResAmout, p2_hiveScript.BlueResAmout };
        int depense = (int)(oldAmount - newAmountJ2);
        int depenseB = (int)(oldAmountColorJ2[2] - newAmountColorJ2[2]);
        int depenseG = (int)(oldAmountColorJ2[1] - newAmountColorJ2[1]);
        int depenseR = (int)(oldAmountColorJ2[0] - newAmountColorJ2[0]);

        if (isDisplayingNegativeResJ2) {
            if (DateTime.Now > tsNegativeJ2) {
                isDisplayingNegativeResJ2 = false;
                negativeRedTextJ2.text = "-";
                negativeBlueTextJ2.text = "-";
                negativeGreenTextJ2.text = "-";
            }
        }
        else if (!isDisplayingNegativeResJ2) {
            if (depense < 0) {
                negativeRedTextJ2.text = depenseR.ToString();
                negativeBlueTextJ2.text = depenseB.ToString();
                negativeGreenTextJ2.text = depenseG.ToString();
                tsNegativeJ2 = DateTime.Now + TimeSpan.FromSeconds(waitingTime);
                isDisplayingNegativeResJ2 = true;
            }

        }
        if (isDisplayingPositiveResJ2) {
            if (DateTime.Now > tsPositiveJ2) {
                isDisplayingPositiveResJ2 = false;
                positiveRedTextJ2.text = "-";
                positiveBlueTextJ2.text = "-";
                positiveGreenTextJ2.text = "-";
            }
        }
        else if (!isDisplayingPositiveResJ2) {
            if (depense > 0) {
                positiveRedTextJ2.text = "+" + depenseR.ToString();
                positiveBlueTextJ2.text = "+" + depenseB.ToString();
                positiveGreenTextJ2.text = "+" + depenseG.ToString();
                tsPositiveJ2 = DateTime.Now + TimeSpan.FromSeconds(waitingTime);
                isDisplayingPositiveResJ2 = true;
            }

        }
        newAmountJ2 = oldAmount;
        newAmountColorJ2[0] = oldAmountColorJ2[0];
        newAmountColorJ2[1] = oldAmountColorJ2[1];
        newAmountColorJ2[2] = oldAmountColorJ2[2];
    }




}

