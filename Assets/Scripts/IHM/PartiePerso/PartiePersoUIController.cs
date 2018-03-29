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
    private Specie player1Specie;
    [SerializeField]
    private Specie player2Specie;

    [SerializeField]
    private GameObject swarmSelectionBtnPrefab;

    [SerializeField]
    private GameObject player1SwarmSelectionPanel;

    [SerializeField]
    private GameObject player2SwarmSelectionPanel;

    [SerializeField]
    private GameObject player1SelectedSwarmField;

    [SerializeField]
    private GameObject player2SelectedSwarmField;


    [Header("UI")]
    [SerializeField]
    Dropdown resourcesStock;
    [SerializeField]
    Dropdown startResources;
    [SerializeField]
    Dropdown gameTimer;
    [SerializeField]
    Dropdown lumyLimit;
    [SerializeField]
    Button swapSceneButton;

    [SerializeField]
    List<Image> imagesScenes; 

    [Header("Gisement")]
    [SerializeField]
    private int minGisement = 1000;
    [SerializeField]
    private int mediumGisement = 1500;
    [SerializeField]
    private int maxGisement = 2000;

    [Header("Resources Start")]
    [SerializeField]
    private int minRes = 100;
    [SerializeField]
    private int mediumRes = 150;
    [SerializeField]
    private int maxRes = 200;
    [SerializeField]
    private int DebugValueRes = 3000;


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

    private string sceneTxtField = "MapTutoInte";

    public Specie Player1Specie
    {
        get
        {
            return player1Specie;
        }

        set
        {
            player1Specie = value;
        }
    }

    public Specie Player2Specie
    {
        get
        {
            return player2Specie;
        }

        set
        {
            player2Specie = value;
        }
    }

    // Use this for initialization
    void Start () {
        CreateP1SwarmSelectionButons();
        CreateP2SwarmSelectionButons();
        CheckParams();
        InitMenu();
        ButtonListener(); 
    }

    private void ButtonListener ()
    {
        swapSceneButton.onClick.AddListener(swapSceneOnClick); 
    }

    private void swapSceneOnClick()
    {
        int activeIndex = 0; 
        for(int i=0; i<imagesScenes.Count; i++)
        {
            if(imagesScenes[i].IsActive())
            {
                activeIndex = i;
            }
        }
        imagesScenes[activeIndex].gameObject.SetActive(false); 
        if(activeIndex < imagesScenes.Count -1)
        {
            activeIndex ++;
            imagesScenes[activeIndex].gameObject.SetActive(true); 
        }
        else
        {
            activeIndex = 0;
            imagesScenes[activeIndex].gameObject.SetActive(true); 
        }
        sceneTxtField = imagesScenes[activeIndex].name;
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

        // Set ScrollRect sizes
        RectTransform rec = player1SwarmSelectionPanel.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, speciesNames.Length * (swarmSelectionBtnPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) +20f);
        rec = player2SwarmSelectionPanel.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, speciesNames.Length * (swarmSelectionBtnPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) + 20f);

        for (int i = 0; i < speciesNames.Length; i++)
        {
            GameObject swarmSelectionButton = Instantiate(swarmSelectionBtnPrefab);
            Button button = swarmSelectionButton.GetComponent<Button>();
            Text text = swarmSelectionButton.GetComponentInChildren<Text>();
            RectTransform rectTransform = swarmSelectionButton.GetComponent<RectTransform>();
            if (playerId == 0)
            {
                swarmSelectionButton.transform.SetParent(player1SwarmSelectionPanel.transform);
            } else
            {
                swarmSelectionButton.transform.SetParent(player2SwarmSelectionPanel.transform);
            }

            //Set Text
            text.text = speciesNames[i];

            //Set Position
            rectTransform.localPosition = new Vector3(
                0,
                -i * (rectTransform.rect.height + 20f) -20f,
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

    private void ChangeMap()
    {

    }

    public void LaunchGame()
    {
        //Set the settings of the game and save them in the PlayerPrefs
        SetPrefsGame(); 
        
        //Load Species
        if(player1SpecieName.Length != 0 || player2SpecieName.Length != 0)
        {
            AppContextManager.instance.LoadPlayerSpecies(player1SpecieName, player2SpecieName);
            //Launch
            NavigationManager.instance.SwapScenes(sceneTxtField, Vector3.zero);
        }
      
    }

    private void SelectP1ActiveSwarm(string swarmName)
    {
        player1SelectedSwarmField.GetComponent<Text>().text = swarmName;
        player1SpecieName = swarmName;

        //Save acive specie in order reload it later
        Specie tmp = AppContextManager.instance.ActiveSpecie;

        //Get selecte specie
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        player1Specie = AppContextManager.instance.ActiveSpecie;

        //Reload previous active specie
        AppContextManager.instance.SwitchActiveSpecie(tmp.Name);
    }

    private void SelectP2ActiveSwarm(string swarmName)
    {
        player2SelectedSwarmField.GetComponent<Text>().text = swarmName;
        player2SpecieName = swarmName;

        //Save acive specie in order reload it later
        Specie tmp = AppContextManager.instance.ActiveSpecie;

        //Get selecte specie
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        player2Specie = AppContextManager.instance.ActiveSpecie;

        //Reload previous active specie
        AppContextManager.instance.SwitchActiveSpecie(tmp.Name);
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
            case 3:
                SwapManager.instance.SetPlayerResources(DebugValueRes);
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

        SwapManager.instance.SetPlayer1Name(player1SpecieName);
        SwapManager.instance.SetPlayer2Name(player2SpecieName);

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
        else if (SwapManager.instance.GetPlayerResources() == DebugValueRes)
            resourcesStock.value = 3;

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

        if (swapSceneButton == null)
        {
            Debug.LogError("SwapSceneButton is not implemented in UI"); 
        }
    }
}
