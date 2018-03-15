using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static InGameUIController instance = null;

    private float startTime = 2.0f;
    private bool winState = false; 
    
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

    [Header("Main Menu")]
    [SerializeField]
    private Button Menu_MainMenu;
    [SerializeField]
    private Button Menu_PersonnalizedMap;
    [SerializeField]
    private Button Menu_Caste;
    [SerializeField]
    private Button Menu_OptionsDebug;

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
    private GameObject unitGo;

    Dictionary<string, int> castsJ1;
    Dictionary<string, int> castsJ2; 

    /// <summary>
    /// Variables for CastUIDisplay
    /// </summary>
    private List<GameObject> castUiList = new List<GameObject>();
    private Dictionary<string, int> popJ1 = new Dictionary<string, int>();
    private Dictionary<string, int> popJ2 = new Dictionary<string, int>();


    private List<GameObject> queens = new List<GameObject>();

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

    GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        Init();
        if(!isNotNull())
            return;
        popJ1 = new Dictionary<string, int>(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
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
        getAllUnit(PlayerAuthority.Player1); 
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
        if(OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        NavigationManager.instance.SwapScenesWithoutZoom("PartiePersoScene"); 
    }
    private void GoToCasteMenu()
    {
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        NavigationManager.instance.SwapScenesWithoutZoom("CastesScene");
    }

    private void GoToMainMenu()
    {
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        NavigationManager.instance.SwapScenesWithoutZoom("MenuPrincipalScene");
      
    }

    private void GoToPersonnalizedMap()
    {
        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        NavigationManager.instance.SwapScenesWithoutZoom("PartiePersoScene");

    }

    private void OpenOptionsDebug() {

        if (OperatorHelper.Instance != null)
        {
            OperatorHelper.Instance.transform.parent = GameManager.instance.transform;
        }
        panelOptionsDebug.SetActive(!panelOptionsDebug.activeSelf);

    }

    void SwitchMenu() {
        subMenu.SetActive(!subMenu.activeSelf);
    }
    #endregion

    private void UnitStats()
    {
        //TODO CREATE VISUALS 
        Camera camera = NavigationManager.instance.GetCurrentCamera(); 
        if (camera == null) {
            Debug.LogError("ERROR: CAMERA NOT SET"); 
            return; 
        }
        CameraRay cameraRay = camera.GetComponent<CameraRay>();
        if (cameraRay == null) {
            Debug.LogError("ERROR : SCRIPT NOT SET ON CAMERA");
            return;
        }
        AgentScript self = cameraRay.Self;
        if(self == null)
        {
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
            
            return; 
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

    private void getCurAction()
    {
        //Warning Real State from the Action.
        //Maybe make a traduction for more visibility.
        Camera camera = NavigationManager.instance.GetCurrentCamera();
        string action = camera.GetComponent<CameraRay>().Action;
    }
    //TODO REMOVE once implemented in UI 
    private void test ()
    {
        Dictionary<string, int> units =  getAllUnit(PlayerAuthority.Player1);
        int nbLignes = units.Count;
        foreach (KeyValuePair<string, int> unit in units)
        {
            string casteName = unit.Key;
            int castePop = unit.Value; 
        }
    }

    private Dictionary<string,int> getAllUnit(PlayerAuthority player)
    {
        if(PlayerAuthority.Player1 == player)
        {
            if (!CheckDicoEquality(popJ1, GameObject.Find("p1_hive").GetComponent<HomeScript>().Population)){
                DisplayUnits(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
                popJ1 = new Dictionary<string, int>(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
            }

            //if (!popJ1.Equals(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population)) {
            //    DisplayUnits(GameObject.Find("p1_hive").GetComponent<HomeScript>().Population);
            //    popJ1 = GameObject.Find("p1_hive").GetComponent<HomeScript>().Population;
            //}
        }
        //else if(PlayerAuthority.Player2 == player)
        //{
        //    if (!popJ2.Equals(GameObject.Find("p2_hive").GetComponent<HomeScript>().Population)) {
        //        DisplayUnits(GameObject.Find("p2_hive").GetComponent<HomeScript>().Population);
        //        popJ2 = GameObject.Find("p2_hive").GetComponent<HomeScript>().Population;
        //    }
        //}


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





    private void DisplayUnits(Dictionary<string,int> units) {
        //Dictionary<string, int> units = getAllUnit(PlayerAuthority.Player1);
        //Clean list if element in it
        foreach(GameObject go in castUiList) {
            castUiList.Remove(go);
            Destroy(go);
        }
        //Create UI cast and add them to the list 
        foreach(KeyValuePair<string, int> unit in units) {
            if (unit.Value!=0) {
                GameObject go = Instantiate(unitGo);
                castUiList.Add(go); 
                go.transform.GetChild(0).GetComponent<Text>().text = unit.Key;
                go.transform.GetChild(1).GetComponent<Text>().text = unit.Value.ToString();
                go.transform.SetParent(unitGo.transform.parent.gameObject.transform);
            }
        }
    }

}

