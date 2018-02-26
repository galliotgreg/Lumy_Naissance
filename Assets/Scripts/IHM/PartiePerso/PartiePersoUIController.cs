using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartiePersoUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static PartiePersoUIController instance = null;

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

    [SerializeField]
    private string player1SpecieName;
    [SerializeField]
    private string player2SpecieName;

    [SerializeField]
    private GameObject swarmSelectionBtnPrefab;

    [SerializeField]
    private GameObject player1SwarmSelectionPanel;

    [SerializeField]
    private GameObject player2SwarmSelectionPanel;


    [Header("UI")]
    [SerializeField]
    Dropdown resourcesStock;
    [SerializeField]
    Dropdown startResources;
    [SerializeField]
    Dropdown gameTimer;
    [SerializeField]
    Dropdown lumyLimit;

    [Header("Gisement")]
    [SerializeField]
    private int minGisement = 125;
    [SerializeField]
    private int mediumGisement = 250;
    [SerializeField]
    private int maxGisement = 500;

    [Header("Resources Start")]
    [SerializeField]
    private int minRes = 10;
    [SerializeField]
    private int mediumRes = 20;
    [SerializeField]
    private int maxRes = 30;


    [Header("Timer")]
    [SerializeField]
    private int minTimer = 100;
    [SerializeField]
    private int mediumTimer = 300;
    [SerializeField]
    private int maxTimer = 420;

    [Header("Number of Lumy")]
    [SerializeField]
    private int minLumy = 50;
    [SerializeField]
    private int mediumLumy = 100;
    [SerializeField]
    private int maxLumy = 250;

    // Use this for initialization
    void Start () {
        CreateP1SwarmSelectionButons();
        CreateP2SwarmSelectionButons();
        CheckParams();
        InitMenu();
    }

    private void CreateP1SwarmSelectionButons()
    {
        CreateSwarmSelectionButons(0);
    }

    private void CreateP2SwarmSelectionButons()
    {
        CreateSwarmSelectionButons(1);
    }

    private void CreateSwarmSelectionButons(int playerId)
    {
        string[] speciesNames = AppContextManager.instance.GetSpeciesFolderNames();
        for (int i = 0; i < speciesNames.Length; i++)
        {
            GameObject swarmSelectionButton = Instantiate(swarmSelectionBtnPrefab);
            Button button = swarmSelectionButton.GetComponent<Button>();
            Text text = swarmSelectionButton.GetComponentInChildren<Text>();
            RectTransform rectTransform = swarmSelectionButton.GetComponent<RectTransform>();
            if (playerId == 0)
            {
                swarmSelectionButton.transform.parent = player1SwarmSelectionPanel.transform;
            } else
            {
                swarmSelectionButton.transform.parent = player2SwarmSelectionPanel.transform;
            }

            //Set Text
            text.text = speciesNames[i];

            //Set Position
            rectTransform.localPosition = new Vector3(
                -275 + i * (rectTransform.rect.width + 20f),
                0,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            if (playerId == 0)
            {
                button.onClick.AddListener(delegate { SelectP1ActiveSwarm(text.text); });
            } else
            {
                button.onClick.AddListener(delegate { SelectP2ActiveSwarm(text.text); });
            }
        }
    }

    public void LaunchGame()
    {
        //Set the settings of the game and save them in the PlayerPrefs
        SetPrefsGame(); 
        
        //Load Species
        AppContextManager.instance.LoadPlayerSpecies(player1SpecieName, player2SpecieName);
        
        //Launch
        NavigationManager.instance.SwapScenes("MapPersonnalise", Vector3.zero);
    }

    private void SelectP1ActiveSwarm(string swarmName)
    {
        player1SpecieName = swarmName;
    }

    private void SelectP2ActiveSwarm(string swarmName)
    {
        player2SpecieName = swarmName;
    }

    /// <summary>
    /// Save the settings for the personnalised game in the PlayerPref and apply them 
    /// </summary>
    public void SetPrefsGame()
    {
        switch (resourcesStock.value)
        {
            case 0:
                SwapManager.instance.SetPlayerStock(minGisement);
                break;
            case 1:
                SwapManager.instance.SetPlayerStock(mediumGisement);
                break;
            case 2:
                SwapManager.instance.SetPlayerStock(maxGisement);
                break;
        }
        switch (startResources.value)
        {
            case 0:
                SwapManager.instance.SetPlayerResources(minRes);
                break;
            case 1:
                SwapManager.instance.SetPlayerResources(mediumRes);
                break;
            case 2:
                SwapManager.instance.SetPlayerResources(maxRes);
                break;
        }
        switch (gameTimer.value)
        {
            case 0:
                SwapManager.instance.SetPlayerTimer(minTimer);
                break;
            case 1:
                SwapManager.instance.SetPlayerTimer(mediumTimer);
                break;
            case 2:
                SwapManager.instance.SetPlayerTimer(maxTimer);
                break;
        }

        switch (lumyLimit.value)
        {
            case 0:
                SwapManager.instance.SetPlayerNbLumy(minLumy);
                break;
            case 1:
                SwapManager.instance.SetPlayerNbLumy(mediumLumy);
                break;
            case 2:
                SwapManager.instance.SetPlayerNbLumy(maxLumy);
                break;
        }

    }
    
    
    /// <summary>
    ///   Init the menu with the values keep in the save
    /// </summary>
    private void InitMenu()
    {
        if (SwapManager.instance.GetPlayerResources() == minRes)
            startResources.value = 0;
        else if (SwapManager.instance.GetPlayerResources() == mediumRes)
            startResources.value = 1;
        else if (SwapManager.instance.GetPlayerResources() == maxRes)
            startResources.value = 2;

        if (SwapManager.instance.GetPlayerStock() == minGisement)
            resourcesStock.value = 0;
        else if (SwapManager.instance.GetPlayerStock() == mediumGisement)
            resourcesStock.value = 1;
        else if (SwapManager.instance.GetPlayerStock() == maxGisement)
            resourcesStock.value = 2;

        if (SwapManager.instance.GetPlayerNbLumy() == minLumy)
            lumyLimit.value = 0;
        else if (SwapManager.instance.GetPlayerNbLumy() == mediumLumy)
            lumyLimit.value = 1;
        else if (SwapManager.instance.GetPlayerNbLumy() == maxLumy)
            lumyLimit.value = 2;

        if (SwapManager.instance.GetPlayerTimer() == minTimer)
            gameTimer.value = 0;
        else if (SwapManager.instance.GetPlayerNbLumy() == mediumTimer)
            gameTimer.value = 1;
        else if (SwapManager.instance.GetPlayerNbLumy() == maxTimer)
            gameTimer.value = 2;
    }


    /// <summary>
    /// Check if each value UI and SwapManager is initialized
    /// </summary>
    private void CheckParams()
    {
        if (resourcesStock == null)
        {
            Debug.LogError("Resources Size not implemented in UI");
        }

        if (startResources == null)
        {
            Debug.LogError("Start Resources not implemented in UI");
        }

        if (gameTimer == null)
        {
            Debug.LogError("Game Timer not implemented in UI");
        }

        if (lumyLimit == null)
        {
            Debug.LogError("Lumy limit not implemented in UI");
        }

        if (SwapManager.instance == null)
        {
            Debug.LogError("SwapManager is not instantiate");
        }
    }
}
