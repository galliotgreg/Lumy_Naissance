using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour {

    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static InGameUIController instance = null;

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
    void Start () {

        Init(); 
	}

    /// <summary>
    /// Init the Controller 
    /// </summary>
	private void Init()
    {
        gameManager = GameManager.instance;
    }


	// Update is called once per frame
	void Update () {
        CheckWinCondition(); 	    	
	}

    /// <summary>
    /// Check if the winner variable is on a Win State 
    /// </summary>
    private void CheckWinCondition()
    {
        GameManager.Winner winner = gameManager.WinnerPlayer; 
        if (winner != GameManager.Winner.None)
        {
            if(winner == GameManager.Winner.Player1)
            {
                Debug.LogWarning("TODO: Implement Winner1 Won"); 
            }
            if(winner == GameManager.Winner.Player2)
            {
                Debug.LogWarning("TODO: Implement Winner2 Won"); 
            }
            if(winner == GameManager.Winner.Equality)
            {
                Debug.LogWarning("TODO: Implement Equality"); 
            }
        }
    }

    private void GetResources(PlayerAuthority player)
    {
        gameManager.GetResources(player); 
    }



}
