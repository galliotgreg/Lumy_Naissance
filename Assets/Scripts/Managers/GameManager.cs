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
    private float timerLeft = 10 ;
    private bool gameNotOver = true; 

    //Queen Ref
    private GameObject p1_queen;
    private GameObject p2_queen; 


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
        return reader.ReadToEnd();
    }

    //Paths
    public string SPECIE_FILE_SUFFIX = "specie.csv";

    public string INPUTS_FOLDER_PATH = "Assets/Inputs/";
    public string PLAYER1_SPECIE_FOLDER = "Player1/";
    public string PLAYER2_SPECIE_FOLDER = "Player2/";

    //Templates & Prefabs
    [SerializeField]
    private GameObject prysmePrefab;
    [SerializeField]
    private GameObject emptyAgentPrefab;
    [SerializeField]
    private GameObject emptyComponentPrefab;
    [SerializeField]
    private GameObject homePrefab;
    [SerializeField]
    private GameObject[] p1_unitTemplates;
    [SerializeField]
    private GameObject[] p2_unitTemplates;

    //Instancied Game Objects
    [SerializeField]
    private GameObject p1_home;
    [SerializeField]
    private GameObject p2_home;

    private Specie p1_specie;
    private Specie p2_specie;

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

    // Use this for initialization
    void Start()
    {
        SetupMatch();
    }

    public void Update()
    {
        //First Win Condition Timer 
        if(gameNotOver)
        {
            timerLeft -= Time.deltaTime;
            Debug.Log(timerLeft);
            if (timerLeft <= 0)
            {
                gameNotOver = false;
                if (sumResources(PlayerAuthority.Player1) > sumResources(PlayerAuthority.Player2)) 
                    Debug.Log("Player 1 Won with : " + sumResources(PlayerAuthority.Player1));
                else if (sumResources(PlayerAuthority.Player2) > sumResources(PlayerAuthority.Player1))
                    Debug.Log("Player 2 Won with : " + sumResources(PlayerAuthority.Player2));
                else
                    Debug.Log("Equality ? Player1 : " + sumResources(PlayerAuthority.Player1) + "Player2: " + sumResources(PlayerAuthority.Player2));
                return; 
            }
            //second win condition prysme
            if (p1_queen == null)
            {
                Debug.Log("Game Over Player2 Won, Player 1 lost the Prysme");
                gameNotOver = false;
                return;
            }
            else if (p2_queen == null)
            {
                Debug.Log("Game Over Player1 Won, Player 2 lost the Prysme");
                gameNotOver = false;
                return; 
            }

        }

    }


    private void SetupMatch()
    {
        ParseSpecies();
        CreateUnitTemplates();
        InitGameObjects();
    }

    private void ParseSpecies()
    {
        //Init p1_specie
        //Get file name
        string[] filenames = Directory.GetFiles(INPUTS_FOLDER_PATH + PLAYER1_SPECIE_FOLDER);
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
        //Parse file
        SpecieParser parser = new SpecieParser();
        p1_specie = parser.Parse(lines);

        //Init p2_specie
        //Get file name
        filenames = Directory.GetFiles(INPUTS_FOLDER_PATH + PLAYER2_SPECIE_FOLDER);
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
        //Parse file
        parser = new SpecieParser();
        p2_specie = parser.Parse(lines);
    }

	#region Create Unit Templates
    private void CreateUnitTemplates()
    {
		p1_unitTemplates = createTemplates( p1_specie );
		p2_unitTemplates = createTemplates( p2_specie );
    }

	GameObject[] createTemplates( Specie specie ){
		GameObject[] unitTemplates = new GameObject[specie.Casts.Values.Count + 1];

        //queen first
        unitTemplates[0] = createPrysmeTemplate();
            //createTemplate( specie.Casts[ specie.QueenCastName ], specie.QueenCastName );

        int ind = 1;
		foreach (string key in specie.Casts.Keys)
		{
			if (key != specie.QueenCastName)
			{
				unitTemplates[ind++] = createTemplate( specie.Casts[key], key );
			}
		}

		foreach (GameObject template in unitTemplates) {
			template.GetComponent<AgentEntity> ().Context.setModelValues ();
		}

		return unitTemplates;
	}

    private GameObject createPrysmeTemplate()
    {
        GameObject template = Instantiate(emptyAgentPrefab);

        //Disable physic
        template.GetComponentInChildren<PhySkeleton>().enabled = false; ;

        template.SetActive(false);
        UnitTemplateInitializer.InitPrysmeTemplate(
            template, emptyComponentPrefab
        );

        template.GetComponent<AgentEntity>().Context.Model.Cast = "Prysme";

        return template;
    }

    GameObject createTemplate( Cast cast, string castName ){
		GameObject template = Instantiate(emptyAgentPrefab);

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
        if (hives.Length ==2)
        {
            p1_home = Instantiate(homePrefab,hives[0].GetComponent<Transform>().position, Quaternion.identity);
            p2_home = Instantiate(homePrefab,hives[1].GetComponent<Transform>().position, Quaternion.identity);
           
        }
        else
        {
            p1_home = Instantiate(homePrefab, new Vector3(-30f, -0.45f, 0f), Quaternion.identity);
            p2_home = Instantiate(homePrefab, new Vector3(30f, -0.45f, 0f), Quaternion.identity);
        }
      
        p1_home.name = "p1_hive";
        p2_home.name = "p2_hive";
        HomeScript p1_hiveScript = p1_home.GetComponent<HomeScript>();
		p1_hiveScript.Authority = PlayerAuthority.Player1;
        HomeScript p2_hiveScript = p2_home.GetComponent<HomeScript>();
		p2_hiveScript.Authority = PlayerAuthority.Player2;
        p1_hiveScript.RedResAmout = p1_specie.RedResAmount;
        p1_hiveScript.GreenResAmout = p1_specie.GreenResAmount;
        p1_hiveScript.BlueResAmout = p1_specie.BlueResAmount;
        p2_hiveScript.RedResAmout = p2_specie.RedResAmount;
        p2_hiveScript.GreenResAmout = p2_specie.GreenResAmount;
        p2_hiveScript.BlueResAmout = p2_specie.BlueResAmount;
        foreach (string key in p1_specie.Casts.Keys)
        {
            p1_hiveScript.Population.Add(key, 0);
        }
        foreach (string key in p2_specie.Casts.Keys)
        {
            p2_hiveScript.Population.Add(key, 0);
        }
		Unit_GameObj_Manager.instance.Homes = new List<HomeScript>(){ p1_hiveScript, p2_hiveScript };

        //Queens
        p1_queen = Instantiate(p1_unitTemplates[0], p1_home.transform.position, Quaternion.identity);
        p1_queen.name = "p1_queen";
        p2_queen = Instantiate(p2_unitTemplates[0], p2_home.transform.position, Quaternion.identity);
        p2_queen.name = "p2_queen";
        p1_queen.SetActive(true);
        p2_queen.SetActive(true);

		p1_hiveScript.addUnit( p1_queen.GetComponent<AgentEntity>() );
		p2_hiveScript.addUnit( p2_queen.GetComponent<AgentEntity>() );
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
        if(authority == PlayerAuthority.Player1)
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
            HomeScript p2_hiveScript = p1_home.GetComponent<HomeScript>();
            p2_hiveScript.Authority = PlayerAuthority.Player2;
            resourcesAmount[0] = p2_hiveScript.RedResAmout;
            resourcesAmount[1] = p2_hiveScript.GreenResAmout;
            resourcesAmount[2] = p2_hiveScript.BlueResAmout;
            return resourcesAmount;  
        }
        return null; 
    }

    private float sumResources(PlayerAuthority authority)
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
            p2_hiveScript.Authority = PlayerAuthority.Player1;
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
}
