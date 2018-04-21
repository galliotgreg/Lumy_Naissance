using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapManager : MonoBehaviour
{

    private string keyRes = "ResourceKey";
    private string keyStock = "StockKey";
    private string keyNbLumy = "NbLumyKey";
    private string keyTimer = "TimerKey";
    private string keyPlayer1Name = "Player1NameKey";
    private string keyPlayer2Name = "Player2NameKey";

    //player preferences debuger
    private string keyPlayerVisionRange = "VisionRangeKey";
    private string keyPlayerAtkRange = "AtkRangeKey";
    private string keyPlayerPickRange = "PickRangeKey";
    private string keyPlayerLifeBar = "LifeBarKey";
    private string keyPlayerLumyName = "LumyNameKey";
    private string keyPlayerDirectionLumy = "DirectionLumyKey";
    private string keyPlayerTrace = "TraceKey";
    private string KeyPlayerGisement = "GisementKey";
    private string KeyPlayerToutAfficher = "ToutAfficherKey";
    private string KeyPlayerToutDesactiver = "ToutDesactiverKey";
    private string KeyMouseSensitivity = "SensitivityKey";

    //Player preferences Sound
    private string keyPlayerSFXVolume = "SFXVolumeKey";
    private string keyPlayerMusicVolume = "MusicVolumeKey";
    private string keyPlayerGeneralVolume = "GeneralVolumeKey";

    //player preferences Video
    private string keyPlayerResolution = "ResolutionKey";
    private string keyWindowedPref = "WindowedPrefKey";
    private string keyQualityVideo = "QualityVideoKey";

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
    /// Set an Int in playerPrefs from a bool received in parameters with the associate Key
    /// For this function a key is needed in parameters to create custom Activator for tutorials
    /// </summary>
    /// <param name="state"></param>
    /// <param name="keyTutoriel"></param>
    public void SetTutorielKey(bool state, string keyTutoriel)
    {
        PlayerPrefs.SetInt(keyTutoriel, state ? 0 : 1);
    }

    /// <summary>
    /// Get the State of a tutorial with the given key in parameters 
    /// </summary>
    /// <param name="keyTutoriel"></param>
    /// <returns></returns>
    public bool getTutorielState(string keyTutoriel)
    {
        int value = PlayerPrefs.GetInt(keyTutoriel);
        if (value == 0)
        {
            return true;
        }
        else
        {
            return false;
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
    public void SetPlayerTimer(float timer)
    {
        PlayerPrefs.SetFloat(keyTimer, timer);
    }

    public void SetPlayer1Name(string playerName)
    {
        PlayerPrefs.SetString(keyPlayer1Name, playerName);
    }
    public void SetPlayer2Name(string playerName)
    {
        PlayerPrefs.SetString(keyPlayer2Name, playerName);
    }

    #region set Player preferences Debugger
    public void setPlayerVisionKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerVisionRange + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerVisionRange + "Player2", state ? 0 : 1);
        }
    }
    public void setPlayerAtkKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerAtkRange + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerAtkRange + "Player2", state ? 0 : 1);
        }
    }
    public void setPlayerPickKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerPickRange + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerPickRange + "Player2", state ? 0 : 1);
        }
    }

    public void setPlayerLifeBarKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerLifeBar + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerLifeBar + "Player2", state ? 0 : 1);
        }
    }
    public void setPlayerLumyNameKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerLumyName + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerLumyName + "Player2", state ? 0 : 1);
        }
    }

    public void setPlayerDirectionLumyKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerDirectionLumy + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerDirectionLumy + "Player2", state ? 0 : 1);
        }
    }

    public void setPlayerTraceKey(PlayerAuthority player, bool state)
    {
        if (player == PlayerAuthority.Player1)
        {
            PlayerPrefs.SetInt(keyPlayerTrace + "Player1", state ? 0 : 1);
        }
        else if (player == PlayerAuthority.Player2)
        {
            PlayerPrefs.SetInt(keyPlayerTrace + "Player2", state ? 0 : 1);
        }
    }

    public void setPlayerGisementKey( bool state)
    {
        PlayerPrefs.SetInt(KeyPlayerGisement, state ? 0 : 1);
    }

    public void setPlayerToutDesactiverKey(bool state)
    {
        PlayerPrefs.SetInt(KeyPlayerToutDesactiver, state ? 0 : 1);
    }
    public void setPlayerToutAfficherKey(bool state)
    {
        PlayerPrefs.SetInt(KeyPlayerToutAfficher, state ? 0 : 1);
    }
    #endregion


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
    public int GetPlayerNbLumy()
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

    public string GetPlayer1Name()
    {
        return PlayerPrefs.GetString(keyPlayer1Name);
    }

    public string GetPlayer2Name()
    {
        return PlayerPrefs.GetString(keyPlayer2Name);
    }

    
    public int getPlayerMouseSensitivity()
    {
        return PlayerPrefs.GetInt(KeyMouseSensitivity); 
    }

    public void setPlayerMouseSensitivity(int value)
    {
        PlayerPrefs.SetInt(KeyMouseSensitivity, value); 
    }

    #region Get player preferences Debugger
    public bool getPlayerDirectionLumyKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerDirectionLumy + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerDirectionLumy + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerVisionKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerVisionRange + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerVisionRange + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerAtkKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerAtkRange + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerAtkRange + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerPickKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerPickRange + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerPickRange + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerLifeBarKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerLifeBar + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerLifeBar + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerLumyNameKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerLumyName + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerLumyName + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerTraceKey(PlayerAuthority player)
    {
        if (player == PlayerAuthority.Player1)
        {
            int value = PlayerPrefs.GetInt(keyPlayerTrace + "Player1");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int value = PlayerPrefs.GetInt(keyPlayerTrace + "Player2");
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getPlayerGisementKey()
    {
        int value = PlayerPrefs.GetInt(KeyPlayerGisement);
        if (value == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool getPlayerToutAfficherKey()
    {
        int value = PlayerPrefs.GetInt(KeyPlayerToutAfficher);
        if (value == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool getPlayerToutDesactiverKey()
    {
        int value = PlayerPrefs.GetInt(KeyPlayerToutDesactiver);
        if (value == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region set player Video preferences
    public void setPlayerPrefResolution(int resolution)
    {
        PlayerPrefs.SetInt(keyPlayerResolution, resolution);
    }
    public void setPlayerPrefWindowed(bool state)
    {
        PlayerPrefs.SetInt(keyWindowedPref, state ? 0 : 1);
    }
    public void setPlayerPrefQuality(int quality)
    {
        PlayerPrefs.SetInt(keyQualityVideo, quality);
    }
    #endregion

    #region get player video preferences
    public int getPlayerPrefResolution()
    {
        return PlayerPrefs.GetInt(keyPlayerResolution);

    }
    public bool getPlayerPrefWindowed()
    {
        int value = PlayerPrefs.GetInt(keyWindowedPref);
        if (value == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int getPlayerPrefQuality()
    {
        return PlayerPrefs.GetInt(keyQualityVideo);
    }
    #endregion

    #region set Player Audio preferences
    public void setPlayerPrefMusic(float volume)
    {
        PlayerPrefs.SetFloat(keyPlayerMusicVolume, volume);
    }
    public void setPlayerPrefSFX(float volume)
    {
        PlayerPrefs.SetFloat(keyPlayerSFXVolume, volume);
    }
    public void setPlayerPrefVolumeGeneral(float volume)
    {
        PlayerPrefs.SetFloat(keyPlayerGeneralVolume, volume);
    }
    #endregion

    #region get Player Audio preferences
    public float getPlayerPrefMusic()
    {
        if(!PlayerPrefs.HasKey(keyPlayerMusicVolume))
        {
            PlayerPrefs.SetFloat(keyPlayerMusicVolume, 1); 
        }
        return PlayerPrefs.GetFloat(keyPlayerMusicVolume);
    }
    public float getPlayerPrefSFX()
    {
        if (!PlayerPrefs.HasKey(keyPlayerSFXVolume))
        {
            PlayerPrefs.SetFloat(keyPlayerSFXVolume, 1);
        }
        return PlayerPrefs.GetFloat(keyPlayerSFXVolume);
    }
    public float getPlayerPrefGeneral()
    {
        if (!PlayerPrefs.HasKey(keyPlayerGeneralVolume))
        {
            PlayerPrefs.SetFloat(keyPlayerGeneralVolume, 1);
        }
        return PlayerPrefs.GetFloat(keyPlayerGeneralVolume);
    }
    #endregion
}


