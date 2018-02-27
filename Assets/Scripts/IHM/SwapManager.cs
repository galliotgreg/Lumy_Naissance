using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapManager : MonoBehaviour {

    private string keyRes = "ResourceKey";
    private string keyStock = "StockKey";
    private string keyNbLumy = "NbLumyKey";
    private string keyTimer = "TimerKey";

    // The static instance of the Singleton for external access
    public static SwapManager instance = null;

    // Enforce Singleton properties
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

    /// <summary>
    /// Set the amount of resources that each player will start
    /// </summary>
    /// <param name="res">The resources the player will have at the start of the game</param>
    public void SetPlayerResources(int res)
    {
        PlayerPrefs.SetInt(keyRes, res); 
    }

    /// <summary>
    /// Set the stock of each resources at the start of the game (each gameObject with a resource script will be affected)
    /// </summary>
    /// <param name="stock">the stock of each resources at the start of the game</param>
    public void SetPlayerStock(int stock)
    {
        PlayerPrefs.SetInt(keyStock, stock);
    }

    /// <summary>
    /// Set the max number of Lumy per player (for example if 25 then the max lumy on the map is 50).
    /// </summary>
    /// <param name="nbLumy">Maximum lumy per player</param>
    public void SetPlayerNbLumy(int nbLumy)
    {
        PlayerPrefs.SetInt(keyNbLumy, nbLumy);
    }

    /// <summary>
    /// Set the timer at the start of the game
    /// </summary>
    /// <param name="timer">the timer at the start of the game</param>
    public void SetPlayerTimer (float timer)
    {
        PlayerPrefs.SetFloat(keyTimer, timer); 
    }

    /// <summary>
    /// Get the resources at the start of the game for each Player
    /// </summary>
    /// <returns>The player resources (int)</returns>
    public int GetPlayerResources()
    {
        return PlayerPrefs.GetInt(keyRes); 
    }

    /// <summary>
    /// Get the stock of each resources (gameObject which contain a resourceScript) at the start of the game
    /// </summary>
    /// <returns>The stock of each resources script</returns>
    public int GetPlayerStock()
    {
        return PlayerPrefs.GetInt(keyStock);
    }

    /// <summary>
    /// Get the number of lumy that each player can have (max)
    /// </summary>
    /// <returns>Number of Lumy that a player can have (max)</returns>
    public int GetPlayerNbLumy ()
    {
        return PlayerPrefs.GetInt(keyNbLumy); 
    }

    /// <summary>
    /// Get the timer of the game
    /// </summary>
    /// <returns>the timer of the game</returns>
    public float GetPlayerTimer()
    {
        return PlayerPrefs.GetFloat(keyTimer); 
    }


}
