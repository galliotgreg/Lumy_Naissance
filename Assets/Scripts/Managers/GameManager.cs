using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static GameManager instance = null;

    //Timer 
    [SerializeField]
    private float timerLeft;
    private bool gameNotOver = true;

    //Queen Ref
    private GameObject p1_queen;
    private GameObject p2_queen;

    public enum Winner { Player1Q, Player2Q, Player1E, Player2E, Equality, None };
    private Winner winnerPlayer;

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

    string readFile(string path)
    {
        System.IO.StreamReader reader = new System.IO.StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }

    //Paths
    public string SPECIE_FILE_SUFFIX = "specie.csv";

    private string intputsFolderPath = "Inputs/";
    public string PLAYER1_SPECIE_FOLDER = "Player1/";
    public string PLAYER2_SPECIE_FOLDER = "Player2/";

    //Templates & Prefabs
    [SerializeField]
    private GameObject emptyAgentPrefab;
    [SerializeField]
    private GameObject emptyComponentPrefab;
    [SerializeField]
    private GameObject homePrefab;
    [SerializeField]
    private GameObject gameParamsPrefab;
    [SerializeField]
    private GameObject[] p1_unitTemplates;
    [SerializeField]
    private GameObject[] p2_unitTemplates;
    [SerializeField]
    private GameObject gameParam;

    //Instancied Game Objects
    [SerializeField]
    private GameObject p1_home;
    [SerializeField]
    private GameObject p2_home;

    private Specie p1_specie;
    private Specie p2_specie;

    #region Accesseur
    public GameObject P1_home
    {
        get
        {
            return p1_home;
        }

        set
        {
            p1_home = value;
        }
    }

    public GameObject P2_home
    {
        get
        {
            return p2_home;
        }

        set
        {
            p2_home = value;
        }
    }

    public float TimerLeft
    {
        get
        {
            return timerLeft;
        }

        set
        {
            timerLeft = value;
        }
    }

    public GameObject GameParam
    {
        get
        {
            return gameParam;
        }

        set
        {
            gameParam = value;
        }
    }

    public Winner WinnerPlayer
    {
        get
        {
            return winnerPlayer;
        }
        set
        {
            winnerPlayer = value;
        }
    }

    public string IntputsFolderPath
    {
        get
        {
            return Application.dataPath + "/" + intputsFolderPath;
        }

        set
        {
            intputsFolderPath = value;
        }
    }

    public Specie P1_specie {
        get {
            return p1_specie;
        }
    }

    public Specie P2_specie {
        get {
            return p2_specie;
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        ABManager.instance.Reset(true);
        SetupMatch();
    }

    private void Flush()
    {
        foreach (GameObject template in p1_unitTemplates)
        {
            Destroy(template);
        }
        foreach (GameObject template in p2_unitTemplates)
        {
            Destroy(template);
        }
        Destroy(gameParam);
    }

    public void ResetGame()
    {
        Flush();
        Init();
    }

    public void Update()
    {
        if (gameNotOver)
        {
            WinCondition();
        }

        //Pause Method 
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            //Can Pause Game Only if gameNotOver and PanelDebug is not active
            if (gameNotOver && InGameUIController.instance.PanelOptionsDebug.activeSelf == false)
            {
                InGameUIController.instance.PauseGame();
            }
         
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameNotOver)
            {
                InGameUIController.instance.OpenOptionsDebug();
                if (InGameUIController.instance.StatePlayPause == true)
                {
                    InGameUIController.instance.PauseGame();
                } 
                else if(InGameUIController.instance.PanelOptionsDebug.activeSelf == false)
                {
                    InGameUIController.instance.PauseGame(); 
                }
             
            }
          
        }

    }


    #endregion

    private void WinCondition()
    {
        //WIN CONDITION PRYSME
        if (p1_queen == null)
        {
            gameNotOver = false;
            winnerPlayer = Winner.Player2Q;
            return;
        }
        else if (p2_queen == null)
        {
            gameNotOver = false;
            winnerPlayer = Winner.Player1Q;
            return;
        }

        //WIN CONDITION RESOURCES 
        timerLeft -= Time.deltaTime;

        if (timerLeft <= 0) {
            gameNotOver = false;
            int scoreJ1 = (int) Score(PlayerAuthority.Player1);
            int scoreJ2 = (int) Score(PlayerAuthority.Player2);
            Debug.Log(scoreJ1 + " | " + scoreJ2);
            if (scoreJ1 > scoreJ2)
            {
                winnerPlayer = Winner.Player1E;
            }
            else if (scoreJ2 > scoreJ1)
            {
                winnerPlayer = Winner.Player2E;
            }
            else
            {
                winnerPlayer = Winner.Equality;
            }

        }

    }

    public float Score(PlayerAuthority authority) {
       
        return sumResources(authority) + SumProdCost(authority);

    }

    public float SumProdCost(PlayerAuthority authority) {

        float costProd = 0; 
        GameObject[] units = GameObject.FindGameObjectsWithTag("Agent");
        List<GameObject> playerUnits = new List<GameObject>();

        foreach (GameObject lumy in units) {
            if (lumy.GetComponent<AgentEntity>().Authority == authority) {
                playerUnits.Add(lumy);
            }
        }

        foreach (GameObject unit in playerUnits) {

            Dictionary<string, int> costs = unit.transform.FindChild("Self").GetComponent<AgentScript>().ProdCost;
            foreach (KeyValuePair<string, int> cost in costs) {
                costProd += cost.Value;
            }
        }
        return costProd; 
    }

    private void SetupMatch()
    {

        SetupGameParams();
        ParseSpecies();
        CreateUnitTemplates();
        InitGameObjects();
        winnerPlayer = Winner.None;
    }


    private void SetupGameParams()
    {
        timerLeft = SwapManager.instance.GetPlayerTimer();
        gameParam = Instantiate(gameParamsPrefab);
        gameParam.transform.parent = this.transform;
    }

    private void ParseSpecies()
    {
        //Init p1_specie
        //Get file name
        string[] filenames = Directory.GetFiles(IntputsFolderPath + PLAYER1_SPECIE_FOLDER);
        string p1_filename = null;
        foreach (string filename in filenames)
        {
            string[] toks = filename.Split('_');
            if (toks[toks.Length - 1] == SPECIE_FILE_SUFFIX) {
                p1_filename = filename;
                break;
            }
        }
        //Read file
        StreamReader reader = new StreamReader(p1_filename);
        List<string> lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }
        reader.Close();
        //Parse file
        SpecieParser parser = new SpecieParser();
        p1_specie = parser.Parse(lines);

        //Init p2_specie
        //Get file name
        filenames = Directory.GetFiles(IntputsFolderPath + PLAYER2_SPECIE_FOLDER);
        string p2_filename = null;
        foreach (string filename in filenames)
        {
            string[] toks = filename.Split('_');
            if (toks[toks.Length - 1] == SPECIE_FILE_SUFFIX) {
                p2_filename = filename;
                break;
            }
        }
        //Read file
        reader = new StreamReader(p2_filename);
        lines = new List<string>();
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }
        reader.Close();
        //Parse file
        parser = new SpecieParser();
        p2_specie = parser.Parse(lines);
    }

    #region Create Unit Templates
    private void CreateUnitTemplates()
    {
        p1_unitTemplates = createTemplates(P1_specie, PlayerAuthority.Player1);
        p2_unitTemplates = createTemplates(P2_specie, PlayerAuthority.Player2);
    }

    GameObject[] createTemplates(Specie specie, PlayerAuthority authority) {
        GameObject[] unitTemplates = new GameObject[specie.Casts.Values.Count + 1];

        //queen first
        unitTemplates[0] = createPrysmeTemplate();

        int ind = 1;
        foreach (string key in specie.Casts.Keys)
        {
            if (key != specie.QueenCastName)
            {
                unitTemplates[ind++] = createTemplate(specie.Casts[key], key);
            }
        }

        foreach (GameObject template in unitTemplates) {
            template.GetComponent<AgentEntity>().Context.setModelValues(authority);
        }

        return unitTemplates;
    }

    private GameObject createPrysmeTemplate()
    {
        GameObject template = Instantiate(emptyAgentPrefab);
        template.transform.parent = gameObject.transform;

        //Disable physic
        template.GetComponentInChildren<PhySkeleton>().enabled = false; ;

        template.SetActive(false);
        UnitTemplateInitializer.InitPrysmeTemplate(
            template, emptyComponentPrefab
        );

        template.GetComponent<AgentEntity>().Context.Model.Cast = "prysme";

        return template;
    }

    GameObject createTemplate(Cast cast, string castName) {
        GameObject template = Instantiate(emptyAgentPrefab);
        template.transform.parent = this.transform;

        template.SetActive(false);
        UnitTemplateInitializer.InitTemplate(
            cast, template, emptyComponentPrefab
        );

        template.GetComponent<AgentEntity>().Context.Model.Cast = castName;
    
        return template;
    }
    #endregion

    private void InitGameObjects()
    {
        //Hives
        GameObject[] hives = GameObject.FindGameObjectsWithTag("Hive");
        if (hives.Length == 2)
        {
            p1_home = Instantiate(homePrefab, hives[0].GetComponent<Transform>().position, Quaternion.identity);
            p2_home = Instantiate(homePrefab, hives[1].GetComponent<Transform>().position, Quaternion.identity);

        }
        else
        {
            p1_home = Instantiate(homePrefab, new Vector3(-30f, -0.45f, 0f), Quaternion.identity);
            p2_home = Instantiate(homePrefab, new Vector3(30f, -0.45f, 0f), Quaternion.identity);
        }
        p1_home.transform.parent = gameObject.transform;
        p2_home.transform.parent = gameObject.transform;
        p1_home.name = "p1_hive";
        p2_home.name = "p2_hive";
        HomeScript p1_hiveScript = p1_home.GetComponent<HomeScript>();
        p1_hiveScript.Authority = PlayerAuthority.Player1;
        HomeScript p2_hiveScript = p2_home.GetComponent<HomeScript>();
        p2_hiveScript.Authority = PlayerAuthority.Player2;
        p1_hiveScript.RedResAmout = P1_specie.RedResAmount;
        p1_hiveScript.GreenResAmout = P1_specie.GreenResAmount;
        p1_hiveScript.BlueResAmout = P1_specie.BlueResAmount;
        p2_hiveScript.RedResAmout = P2_specie.RedResAmount;
        p2_hiveScript.GreenResAmout = P2_specie.GreenResAmount;
        p2_hiveScript.BlueResAmout = P2_specie.BlueResAmount;
        foreach (string key in P1_specie.Casts.Keys)
        {
            p1_hiveScript.Population.Add(key, 0);
        }
        foreach (string key in P2_specie.Casts.Keys)
        {
            p2_hiveScript.Population.Add(key, 0);
        }
		// Set costs in Homes
		p1_hiveScript.fillCost( new List<GameObject>( p1_unitTemplates ) );
		p2_hiveScript.fillCost( new List<GameObject>( p2_unitTemplates ) );
        Unit_GameObj_Manager.instance.Homes = new List<HomeScript>() { p1_hiveScript, p2_hiveScript };

        //Queens
        p1_queen = Instantiate(p1_unitTemplates[0], p1_home.transform.position, Quaternion.identity);
        //   p1_queen.GetComponent<AgentEntity>().
        p1_queen.name = "p1_queen";
        p1_queen.transform.parent = gameObject.transform;
        Light [] light = p1_queen.GetComponentsInChildren<Light>();
        foreach(Light li in light)
        {
            li.enabled = false;
        }
        p2_queen = Instantiate(p2_unitTemplates[0], p2_home.transform.position, Quaternion.identity);
        p2_queen.name = "p2_queen";
        p2_queen.transform.parent = gameObject.transform;
        light = p2_queen.GetComponentsInChildren<Light>();
        foreach (Light li in light)
        {
            li.enabled = false;
        }
        p1_queen.SetActive(true);
        p2_queen.SetActive(true);
        p1_queen.GetComponent<AgentEntity>().GameParams =
            gameParam.GetComponent<GameParamsScript>();
        p2_queen.GetComponent<AgentEntity>().GameParams =
            gameParam.GetComponent<GameParamsScript>();
		
		Unit_GameObj_Manager.instance.addPrysme( p1_queen.GetComponent<AgentEntity>(), p1_hiveScript ); 
		Unit_GameObj_Manager.instance.addPrysme( p2_queen.GetComponent<AgentEntity>(), p2_hiveScript ); 

        InitResources();
        SetResources();
    }

    private void InitResources() {

        List<ResourceScript> listResources = new List<ResourceScript>();
        ResourceScript[] list;
        list = FindObjectsOfType<ResourceScript>();
        if (list.Length <= 0 || list == null) {
            return;
        }
        for (int i = 0; i < list.Length; i++) {
            list[i].Stock = SwapManager.instance.GetPlayerStock();
            listResources.Add(list[i]);
        }

        Unit_GameObj_Manager.instance.Resources = listResources;
    }

    public GameObject GetUnitTemplate(PlayerAuthority authority, string castName)
    {
        // Select template list
        GameObject[] unitTemplates = null;
        if (authority == PlayerAuthority.Player1)
        {
            unitTemplates = p1_unitTemplates;
        } else if (authority == PlayerAuthority.Player2)
        {
            unitTemplates = p2_unitTemplates;
        }

        //Find unit template
        foreach (GameObject unitTemplate in unitTemplates)
        {
            AgentEntity unitTemplateEntity =
                unitTemplate.GetComponent<AgentEntity>();
            if (unitTemplateEntity.CastName == castName)
            {
                return unitTemplate;
            }
        }
        return null;
    }

    public HomeScript GetHome(PlayerAuthority authority)
    {
        if (authority == PlayerAuthority.Player1)
        {
            return p1_home.GetComponent<HomeScript>();
        } else
        {
            return p2_home.GetComponent<HomeScript>();
        }
    }

    /// <summary>
    /// GetAllResources of the player passed in parameters 
    /// </summary>
    /// <returns>Return a tab[3] of floats : 0 is RedResources, 1 is GreenResources, 2 is BlueResources</returns>
    public float[] GetResources(PlayerAuthority authority)
    {
        float[] resourcesAmount = { 0, 0, 0 };
        if (authority == PlayerAuthority.Player1)
        {
            HomeScript p1_hiveScript = p1_home.GetComponent<HomeScript>();
            p1_hiveScript.Authority = PlayerAuthority.Player1;
            resourcesAmount[0] = p1_hiveScript.RedResAmout;
            resourcesAmount[1] = p1_hiveScript.GreenResAmout;
            resourcesAmount[2] = p1_hiveScript.BlueResAmout;
            return resourcesAmount;
        }
        else if (authority == PlayerAuthority.Player2)
        {
            HomeScript p2_hiveScript = p2_home.GetComponent<HomeScript>();
            p2_hiveScript.Authority = PlayerAuthority.Player2;
            resourcesAmount[0] = p2_hiveScript.RedResAmout;
            resourcesAmount[1] = p2_hiveScript.GreenResAmout;
            resourcesAmount[2] = p2_hiveScript.BlueResAmout;
            return resourcesAmount;
        }
        return null;
    }

    /// <summary>
    /// Called from the GameParams in the PartiePersoScene
    /// Init all the resources of the players same amount for each resources 
    /// </summary>
    public void SetResources()
    {
        int nbResources = SwapManager.instance.GetPlayerResources();
        HomeScript p1_hiveScript = p1_home.GetComponent<HomeScript>();
        p1_hiveScript.Authority = PlayerAuthority.Player1;
        p1_hiveScript.RedResAmout = nbResources;
        p1_hiveScript.GreenResAmout = nbResources;
        p1_hiveScript.BlueResAmout = nbResources;

        HomeScript p2_hiveScript = p2_home.GetComponent<HomeScript>();
        p2_hiveScript = p2_home.GetComponent<HomeScript>();
        p2_hiveScript.Authority = PlayerAuthority.Player2;
        p2_hiveScript.RedResAmout = nbResources;
        p2_hiveScript.GreenResAmout = nbResources;
        p2_hiveScript.BlueResAmout = nbResources;

    }

    public float sumResources(PlayerAuthority authority)
    {
        float resSum = 0;
        if (authority == PlayerAuthority.Player1)
        {
            HomeScript p1_hiveScript = p1_home.GetComponent<HomeScript>();
            p1_hiveScript.Authority = PlayerAuthority.Player1;
            resSum += p1_hiveScript.RedResAmout;
            resSum += p1_hiveScript.GreenResAmout;
            resSum += p1_hiveScript.BlueResAmout;
        }
        else if (authority == PlayerAuthority.Player2)
        {
            HomeScript p2_hiveScript = p2_home.GetComponent<HomeScript>();
            p2_hiveScript.Authority = PlayerAuthority.Player2;
            resSum += p2_hiveScript.RedResAmout;
            resSum += p2_hiveScript.GreenResAmout;
            resSum += p2_hiveScript.BlueResAmout;
        }
        return resSum;
    }
    

    public HomeScript GetEnemyHome(PlayerAuthority authority)
    {
        if (authority == PlayerAuthority.Player1)
        {
            return p2_home.GetComponent<HomeScript>();
        } else
        {
            return p1_home.GetComponent<HomeScript>();
        }
    }

    /// <summary>
    /// Pause Method : Switch the Time.timeScale to 0 or 1
    /// </summary>
    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }

   

}
