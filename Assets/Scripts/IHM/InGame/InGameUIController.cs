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

    [SerializeField]
    private Text timer;



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
        {
            return; 
        }
    }

    /// <summary>
    /// Init the Controller 
    /// </summary>
	private void Init()
    {
        gameManager = GameManager.instance;
    }


    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        UpdateUI();
    }

    /// <summary>
    /// Check if the winner variable is on a Win State 
    /// </summary>
    private void CheckWinCondition()
    {
        GameManager.Winner winner = gameManager.WinnerPlayer;
        if (winner != GameManager.Winner.None)
        {
            if (winner == GameManager.Winner.Player1)
            {
                Debug.LogWarning("TODO: Implement Winner1 Won");
            }
            if (winner == GameManager.Winner.Player2)
            {
                Debug.LogWarning("TODO: Implement Winner2 Won");
            }
            if (winner == GameManager.Winner.Equality)
            {
                Debug.LogWarning("TODO: Implement Equality");
            }
        }
    }

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
            if(gameManager.TimerLeft <=175)
            {
                timer.color = new Color(255, 0, 0,255);
                timer.fontStyle = FontStyle.Bold; 
            }
        }
      

    }

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
        return true; 
    }
}

   