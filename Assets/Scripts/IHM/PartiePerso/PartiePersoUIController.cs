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

    private Specie player1Specie = null;
    private Specie player2Specie = null;

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
    private Image statBarPrefab;
    [SerializeField]
    private Image bckStatBarPrefab;
    [SerializeField]
    private Text percentagePrefab;
    [SerializeField]
    List<Text> player1StatsNamesList;
    [SerializeField]
    List<Text> player2StatsNamesList;

    private List<Text> player1PercentageToDestroyList = new List<Text>();
    private List<Text> player2PercentageToDestroyList = new List<Text>();
    private List<GameObject> player1StatsBarToDestroyList = new List<GameObject>();
    private List<GameObject> player2StatsBarToDestroyList = new List<GameObject>();

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

    // Use this for initialization
    void Start () {
        CreateP1SwarmSelectionButons();
        CreateP2SwarmSelectionButons();
        CheckParams();
        InitMenu();
        ButtonListener();
        CheckView();
    }

    #region Compute and Display Stats

    private void CheckView()
    {
        //Clear names
       foreach (Text text in player1StatsNamesList)
        {
            text.gameObject.SetActive(false);
        }

       foreach (Text text in player2StatsNamesList)
        {
            text.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Compute and display player1 swarm stats
    /// </summary>
    private void ComputePlayer1Stats()
    {
        List<float> castStatsList = new List<float>();
        float vitalityGlobalSum = 0f;
        float staminaGlobalSum = 0f;
        float strengthGlobalSum = 0f;
        float actionSpeedGlobalSum = 0f;
        float moveSpeedGlobalSum = 0f;
        float visionRangeGlobalSum = 0f;
        float pickRangeGlobalSum = 0f;
        float attackRangeGlobalSum = 0f;

        float maxStat = 3 * player1Specie.Casts.Count;
        float barHeight = statBarPrefab.GetComponent<RectTransform>().rect.height;

        //Parse player1 casts
        foreach (KeyValuePair<string, Cast> cast in player1Specie.Casts)
        {
            float vitalitySum = 0f;
            float staminaSum = 0f;
            float strengthSum = 0f;
            float actionSpeedSum = 0f;
            float moveSpeedSum = 0f;
            float visionRangeSum = 0f; 
            float pickRangeSum = 0f;
            float attackRangeSum = 0f;

            //Get head stats
            for (int i = 0; i< cast.Value.Head.Count; i++)
            {
                //Get stats and exclude Base stats
                if (cast.Value.Head[i].Id != 1 && cast.Value.Head[i].Id != 2)
                {
                    strengthSum += cast.Value.Head[i].StrengthBuff; 
                    visionRangeSum += cast.Value.Head[i].VisionRangeBuff;
                    pickRangeSum += cast.Value.Head[i].PickRangeBuff;
                    attackRangeSum += cast.Value.Head[i].AtkRangeBuff;
                }   
            }
            //Get tail stats
            for (int i=0; i< cast.Value.Tail.Count; i++)
            {
                //Get stats and exclude Base stats
                if (cast.Value.Tail[i].Id != 1 && cast.Value.Tail[i].Id != 2)
                {
                    vitalitySum += cast.Value.Tail[i].VitalityBuff;
                    staminaSum += cast.Value.Tail[i].StaminaBuff;
                    actionSpeedSum += cast.Value.Tail[i].ActionSpeedBuff;
                    moveSpeedSum += cast.Value.Tail[i].MoveSpeedBuff;
                } 
            }
            //sum stats of all casts
            vitalityGlobalSum += vitalitySum;
            staminaGlobalSum += staminaSum;
            strengthGlobalSum += strengthSum;
            actionSpeedGlobalSum += actionSpeedSum;
            moveSpeedGlobalSum += moveSpeedSum;
            visionRangeGlobalSum += visionRangeSum;
            pickRangeGlobalSum += pickRangeSum;
            attackRangeGlobalSum += attackRangeSum;
        }
       
        castStatsList.Add(vitalityGlobalSum);
        castStatsList.Add(staminaGlobalSum);
        castStatsList.Add(strengthGlobalSum);
        castStatsList.Add(actionSpeedGlobalSum);
        castStatsList.Add(moveSpeedGlobalSum);
        castStatsList.Add(visionRangeGlobalSum);
        castStatsList.Add(pickRangeGlobalSum);
        castStatsList.Add(attackRangeGlobalSum);

        //Display stats
        for (int i=0; i< castStatsList.Count;i++)
        {
            //Bars
            Image bckStatBar = Instantiate(bckStatBarPrefab, new Vector3(160f, -i * barHeight, 0f), Quaternion.identity);
            bckStatBar.transform.SetParent(GameObject.Find("PanelJoueur1").transform, false);
            player1StatsBarToDestroyList.Add(bckStatBar.gameObject);

            Image statBar = Instantiate(statBarPrefab, new Vector3(160f, -i* barHeight, 0f), Quaternion.identity);
            statBar.transform.SetParent(GameObject.Find("PanelJoueur1").transform, false);
            statBar.fillAmount = castStatsList[i] / maxStat ;
            player1StatsBarToDestroyList.Add(statBar.gameObject);

            //Texts
            player1StatsNamesList[i].transform.localPosition = new Vector3(-150f, statBar.transform.localPosition.y,0f) ;
            player1StatsNamesList[i].gameObject.SetActive(true);
            
            //Percentage
            Text percentage = Instantiate(percentagePrefab, new Vector3(330f, statBar.transform.localPosition.y, 0f), Quaternion.identity);
            percentage.transform.SetParent(GameObject.Find("PanelJoueur1").transform, false);
            percentage.text = Mathf.Floor(statBar.fillAmount*100).ToString() + "%";
            player1PercentageToDestroyList.Add(percentage);
        }

    }
    /// <summary>
    /// Compute and display player2 swarm stats
    /// </summary>
    private void ComputePlayer2Stats()
    { 
        List<float> castStatsList = new List<float>();
        float vitalityGlobalSum = 0f;
        float staminaGlobalSum = 0f;
        float strengthGlobalSum = 0f;
        float actionSpeedGlobalSum = 0f;
        float moveSpeedGlobalSum = 0f;
        float visionRangeGlobalSum = 0f;
        float pickRangeGlobalSum = 0f;
        float attackRangeGlobalSum = 0f;

        float maxStat = 3 * player2Specie.Casts.Count;
        float barHeight = statBarPrefab.GetComponent<RectTransform>().rect.height;

        //Parse player2 casts
        foreach (KeyValuePair<string, Cast> cast in player2Specie.Casts)
        {
            float vitalitySum = 0f;
            float staminaSum = 0f;
            float strengthSum = 0f;
            float actionSpeedSum = 0f;
            float moveSpeedSum = 0f;
            float visionRangeSum = 0f;
            float pickRangeSum = 0f;
            float attackRangeSum = 0f;

            //Get head stats
            for (int i = 0; i < cast.Value.Head.Count; i++)
            {
                //Get stats and exclude Base stats
                if (cast.Value.Head[i].Id != 1 && cast.Value.Head[i].Id != 2)
                {
                    strengthSum += cast.Value.Head[i].StrengthBuff;
                    visionRangeSum += cast.Value.Head[i].VisionRangeBuff;
                    pickRangeSum += cast.Value.Head[i].PickRangeBuff;
                    attackRangeSum += cast.Value.Head[i].AtkRangeBuff;
                }
            }
            //Get tail stats
            for (int i = 0; i < cast.Value.Tail.Count; i++)
            {
                //Get stats and exclude Base stats
                if (cast.Value.Tail[i].Id != 1 && cast.Value.Tail[i].Id != 2)
                {
                    vitalitySum += cast.Value.Tail[i].VitalityBuff;
                    staminaSum += cast.Value.Tail[i].StaminaBuff;
                    actionSpeedSum += cast.Value.Tail[i].ActionSpeedBuff;
                    moveSpeedSum += cast.Value.Tail[i].MoveSpeedBuff;
                }
            }
            //sum stats of all casts
            vitalityGlobalSum += vitalitySum;
            staminaGlobalSum += staminaSum;
            strengthGlobalSum += strengthSum;
            actionSpeedGlobalSum += actionSpeedSum;
            moveSpeedGlobalSum += moveSpeedSum;
            visionRangeGlobalSum += visionRangeSum;
            pickRangeGlobalSum += pickRangeSum;
            attackRangeGlobalSum += attackRangeSum;
        }

        castStatsList.Add(vitalityGlobalSum);
        castStatsList.Add(staminaGlobalSum);
        castStatsList.Add(strengthGlobalSum);
        castStatsList.Add(actionSpeedGlobalSum);
        castStatsList.Add(moveSpeedGlobalSum);
        castStatsList.Add(visionRangeGlobalSum);
        castStatsList.Add(pickRangeGlobalSum);
        castStatsList.Add(attackRangeGlobalSum);

        //Display stats
        for (int i = 0; i < castStatsList.Count; i++)
        {
            //Bars
            Image bckStatBar = Instantiate(bckStatBarPrefab, new Vector3(160f, -i * barHeight, 0f), Quaternion.identity);
            bckStatBar.transform.SetParent(GameObject.Find("PanelJoueur2").transform, false);
            player2StatsBarToDestroyList.Add(bckStatBar.gameObject);

            Image statBar = Instantiate(statBarPrefab, new Vector3(160f, -i * barHeight, 0f), Quaternion.identity);
            statBar.transform.SetParent(GameObject.Find("PanelJoueur2").transform, false);
            statBar.fillAmount = castStatsList[i] / maxStat;
            player2StatsBarToDestroyList.Add(statBar.gameObject);
            //Texts
            player2StatsNamesList[i].transform.localPosition = new Vector3(-150f, statBar.transform.localPosition.y, 0f);
            player2StatsNamesList[i].gameObject.SetActive(true);
            
            //Percentage
            Text percentage = Instantiate(percentagePrefab, new Vector3(330f, statBar.transform.localPosition.y, 0f), Quaternion.identity);
            percentage.transform.SetParent(GameObject.Find("PanelJoueur2").transform, false);
            percentage.text = Mathf.Floor(statBar.fillAmount * 100).ToString() + "%";
            player2PercentageToDestroyList.Add(percentage);
        }

    }
    #endregion

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
                0f,
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


    public void LaunchGame()
    {
        //Set the settings of the game and save them in the PlayerPrefs
        SetPrefsGame(); 
        
        //Load Species
        if(player1SpecieName.Length != 0 || player2SpecieName.Length != 0)
        {
            AppContextManager.instance.LoadPlayerSpecies(player1SpecieName, player2SpecieName);
            //Launch
            NavigationManager.instance.ActivateFadeToBlack();
            NavigationManager.instance.SwapScenes(sceneTxtField, Vector3.zero);
        }
      
    }

    private void SelectP1ActiveSwarm(string swarmName)
    {
        ClearPlayer1View();

        player1SpecieName = swarmName;
        player1SelectedSwarmField.GetComponent<Text>().text = swarmName;

        //Register specie
        Specie tmpSpecie = AppContextManager.instance.ActiveSpecie;
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        player1Specie = AppContextManager.instance.ActiveSpecie;
        AppContextManager.instance.SwitchActiveSpecie(tmpSpecie.Name);
        
        //Compute and display stats
        ComputePlayer1Stats();
      
    }

    private void ClearPlayer1View()
    {   
        //Clear percentage
        if (player1PercentageToDestroyList != null)
        {
            for(int i = 0; i < player1PercentageToDestroyList.Count; i++)
            {
                Text percentToDestroy = player1PercentageToDestroyList[i];
                player1PercentageToDestroyList.RemoveAt(i);
                Destroy(percentToDestroy.gameObject);
                i--;
            }
        }
        //Clear stat bars
        if(player1StatsBarToDestroyList != null)
        {
            for (int i=0; i< player1StatsBarToDestroyList.Count; i++)
            {
                GameObject statBarToDestroy = player1StatsBarToDestroyList[i];
                player1StatsBarToDestroyList.RemoveAt(i);
                Destroy(statBarToDestroy);
                i--;
            }
        }
    }

    private void SelectP2ActiveSwarm(string swarmName)
    {
        ClearPlayer2View();

        player2SpecieName = swarmName;
        player2SelectedSwarmField.GetComponent<Text>().text = swarmName;

        //Register specie
        Specie tmpSpecie = AppContextManager.instance.ActiveSpecie;
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        player2Specie = AppContextManager.instance.ActiveSpecie;
        AppContextManager.instance.SwitchActiveSpecie(tmpSpecie.Name);

     
        //Compute and display stats
        ComputePlayer2Stats();
 
    }

    private void ClearPlayer2View()
    {
        //Clear percentage
        if (player2PercentageToDestroyList != null)
        {
            for (int i = 0; i < player2PercentageToDestroyList.Count; i++)
            {
                Text percentToDestroy = player2PercentageToDestroyList[i];
                player2PercentageToDestroyList.RemoveAt(i);
                Destroy(percentToDestroy.gameObject);
                i--;
            }

        }
        //Clear stat bars
        if (player2StatsBarToDestroyList != null)
        {
            for (int i = 0; i < player2StatsBarToDestroyList.Count; i++)
            {
                GameObject statBarToDestroy = player2StatsBarToDestroyList[i];
                player2StatsBarToDestroyList.RemoveAt(i);
                Destroy(statBarToDestroy);
                i--;
            }
        }
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
