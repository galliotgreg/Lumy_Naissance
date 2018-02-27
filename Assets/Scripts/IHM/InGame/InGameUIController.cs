using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("Resources Panel")]
    [SerializeField]
    private Text J1_Red_Resources;
    [SerializeField]
    private Text J1_Green_Resources;
    [SerializeField]
    private Text J1_Blue_Resources;
    [SerializeField]
    private Text J2_Red_Resources;
    [SerializeField]
    private Text J2_Green_Resources;
    [SerializeField]
    private Text J2_Blue_Resources;

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

    /// <summary>
    /// Check if the winner variable is on a Win State 
    /// </summary>
    private void CheckWinCondition()
    {
        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            return; 
        }
        GameManager.Winner winner = gameManager.WinnerPlayer;
        if (!winState)
        {
            if (winner != GameManager.Winner.None)
            {
                winState = true;
                victoryMenu.SetActive(true);
                if (winner == GameManager.Winner.Player1)
                {
                    victory.text = "Victoire du Joueur 1";
                }
                if (winner == GameManager.Winner.Player2)
                {
                    victory.text = " Victoire du Joueur 2 ";
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


    /// <summary>
    /// Update the UI with the parameters : Resources and Timer
    /// </summary>
    private void UpdateUI()
    {
        //Get Resources values in game Manager
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
            timer.text = "TIMER : " + t.Minutes + " : " + t.Seconds;
            if(gameManager.TimerLeft <=30)
            {
                timer.color = new Color(255, 0, 0,255);
                timer.fontStyle = FontStyle.Bold; 
            }
        }
      

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
        return true; 
    }

    private void CloseExitMenu()
    {
        exitMenu.SetActive(false);
    }

    private void ExitGame()
    {
        NavigationManager.instance.SwapScenesWithoutZoom("PartiePersoScene"); 
    }
    private void GoToCasteMenu()
    {
        NavigationManager.instance.SwapScenes("CastesScene", new Vector3(0, 0, 0)); 
    }
}

   