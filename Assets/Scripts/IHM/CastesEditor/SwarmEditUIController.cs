using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class SwarmEditUIController : MonoBehaviour
{
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static SwarmEditUIController instance = null;

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

    //Max number of points per Lumy attribute
    private int statLimit = 3;

    /// <summary>
    /// Active Lumy Stats
    /// </summary>
    private LumyStatsInfo lumyStats = new LumyStatsInfo();

    [Header("Confirmation panels")]
    [SerializeField]
    private GameObject resetConfirmationPanel;
    [SerializeField]
    private GameObject delSwarmConfirmationPanel;
    [SerializeField]
    private GameObject delCastConfirmationPanel;
    [SerializeField]
    private GameObject importSwarmConfirmationPanel;
    [SerializeField]
    private Text swarmName;
    [SerializeField]
    private Text castName;

    /// <summary>
    /// The swarm scroll selection content
    /// </summary>
    [Header("Lumy Appearence")]
    [SerializeField]
    private GameObject editedLumy;

    private GameObject editedInGameLumy;

    /// <summary>
    /// Empty lumy prefab
    /// </summary>
    [SerializeField]
    private GameObject emptyAgentPrefab;

    /// <summary>
    /// Empty Component Prefab
    /// </summary>
    [SerializeField]
    private GameObject emptyComponentPrefab;

    /// <summary>
    /// The swarm scroll selection content
    /// </summary>
    [Header("Swarm Panel")]
    [SerializeField]
    private GameObject swarmScrollContent;

    [SerializeField]
    private Text swarmNameInput;

    [SerializeField]
    private GameObject editSwarmDialog;

    [SerializeField]
    private Button editSwarmImageButton;

    /// <summary>
    /// The btn prefab for swarm selection 
    /// </summary>
    [SerializeField]
    private GameObject swarmSelectionBtnPrefab;

    /// <summary>
    /// The name of the current lumy on the main panel
    /// </summary>
    [Header("Main Panel")]
    [SerializeField]
    private Text mainPanelLumyName;

    [SerializeField]
    private GameObject lumyPanel;

    [SerializeField]
    private GameObject prysmePanel;

    [SerializeField]
    private GameObject lumyHublot;

    [SerializeField]
    private Button editCastImageButton;

    [SerializeField]
    private GameObject castImageDialog;

    [SerializeField]
    private Image imagePrysme;

    /// <summary>
    /// The name of the current lumy on the main panel
    /// </summary>
    [Header("Swarm Panel")]
    [SerializeField]
    private Text swarmPanelSwarmName;

    [SerializeField]
    private GameObject swarmSelectionPanel;

    /// <summary>
    /// The rename lumy panel
    /// </summary>
    [SerializeField]
    private GameObject renameLumyDialog;

    /// <summary>
    /// Swarm Image panel
    /// </summary>
    [SerializeField]
    private GameObject swarmImageDialog;

    /// <summary>
    /// The new name of the lumy
    /// </summary>
    [SerializeField]
    private GameObject renameLumyInput;

    /// <summary>
    /// The red resources cost of the active lumy
    /// </summary>
    [SerializeField]
    private int redCost;

    /// <summary>
    /// The green resources cost of the active lumy
    /// </summary>
    [SerializeField]
    private int greenCost;

    /// <summary>
    /// The blue resources cost of the active lumy
    /// </summary>
    [SerializeField]
    private int blueCost;

    /// <summary>
    /// The prodution time of the active lumy
    /// </summary>
    [SerializeField]
    private float prodTime;

    /// <summary>
    /// The canvas listing aull lumys from the active swarm
    /// </summary>
    [Header("Select Lumy Scroll")]
    [SerializeField]
    private GameObject lumysScrollContent;

    /// <summary>
    /// The canvas listing all images set in LumyPictFactory
    /// </summary>
    [Header("Select Swarm Images Scroll")]
    [SerializeField]
    private GameObject swarmImageScrollContent;

    [Header("Select Cast Images Scroll")]
    [SerializeField]
    private GameObject castImageScrollContent;

    /// <summary>
    /// Instancied prefab for each lumys on the lumy scroll content
    /// </summary>
    [SerializeField]
    private GameObject lumyButtonPrefab;

    /// <summary>
    /// Instancied prefab for image on the swarm images scroll content
    /// </summary>
    [SerializeField]
    private GameObject swarmImageButtonPrefab;

    /// <summary>
    /// Set the margin beetween each lumy select button
    /// </summary>
    [SerializeField]
    private float lumyYMarginLayout = 35f;

    /// <summary>
    /// Set the margin beetween each lumy select button
    /// </summary>
    [SerializeField]
    private float swarmImagesYMarginLayout;

    [Header("Basic Layer Prefabs")]
    [SerializeField]
    private GameObject swarmPrefabsToDestroyPanel;
    [SerializeField]
    private GameObject castPrefabsToDestroyPanel;
    /// <summary>
    /// Elements needed for stat bars display
    /// </summary>
    [Header("Stat Bars")]
    [SerializeField]
    private GameObject statBarPrefab;
    [SerializeField]
    private float statBarHSpacing;
    [SerializeField]
    private float statBarVertSpacing;
    [Header("Stat Bars Params")]
    [SerializeField]
    private float statBarLeftXpos;
    [SerializeField]
    private float statBarRightXpos;
    [SerializeField]
    private float statBarYPos;
    [SerializeField]
    private Color32 statsBlueColor;
    [SerializeField]
    private Color32 statsRedColor;
    [SerializeField]
    private Color32 statsGreenColor;
    [SerializeField]
    private Color32 statsBaseColor;
  
    private List<GameObject> barLeftStatsList;
    private List<GameObject> barRightStatsList;

    private GameObject[] swarmImagesArray;
    private GameObject[] castImagesArray;

    /// <summary>
    /// Increase / Decrease stats Button
    /// </summary>
    [SerializeField]
    private Button vitalityPlus;
    [SerializeField]
    private Button vitalityLess;
    [SerializeField]
    private Button staminaPlus;
    [SerializeField]
    private Button staminaLess;
    [SerializeField]
    private Button strengthPlus;
    [SerializeField]
    private Button strengthLess;
    [SerializeField]
    private Button actSpeedPlus;
    [SerializeField]
    private Button actSpeedLess;
    [SerializeField]
    private Button moveSpeedPlus;
    [SerializeField]
    private Button moveSpeedLess;
    [SerializeField]
    private Button visionRangePlus;
    [SerializeField]
    private Button visionRangeLess;
    [SerializeField]
    private Button pickRangePlus;
    [SerializeField]
    private Button pickRangeLess;
    [SerializeField]
    private Button  atkRangePlus;
    [SerializeField]
    private Button atkRangeLess;

    private bool isOnSwarmImagesPanel =false;
    private bool isOnCastImagesPanel = false;
    private bool isOnSwarmSelectionPanel = false;


    public int RedCost
    {
        get
        {
            return redCost;
        }

        set
        {
            redCost = value;
        }
    }

    public int GreenCost
    {
        get
        {
            return greenCost;
        }

        set
        {
            greenCost = value;
        }
    }

    public int BlueCost
    {
        get
        {
            return blueCost;
        }

        set
        {
            blueCost = value;
        }
    }

    public float ProdTime
    {
        get
        {
            return prodTime;
        }

        set
        {
            prodTime = value;
        }
    }

    public LumyStatsInfo LumyStats
    {
        get
        {
            return lumyStats;
        }

        set
        {
            lumyStats = value;
        }
    }

    private bool isInGameUnloaded = false;
    private bool isSceneLoaded = false;
    private bool isFirstRefreashed = false;

    private GameObject renderingCamera;

    private void Start()
    {
        DisplayStatBars();
        RefreshSwarmImageScroll();
        RefreshCastImageScroll();
        RefreshView();
    }


    private void OnDestroy()
    {
        ABManager.instance.Reset(false);
        SceneManager.UnloadScene("MapSwarmEdit");
    }

    #region Stats Button Listener
    private void StatsButtonListener()
    {
        //vitality
        vitalityLess.onClick.AddListener(RefreshVitality);
        vitalityPlus.onClick.AddListener(RefreshVitality);

        //stamina
        staminaLess.onClick.AddListener(RefreshStamina);
        staminaPlus.onClick.AddListener(RefreshStamina);

        //strength
        strengthLess.onClick.AddListener(RefreshStrength);
        strengthPlus.onClick.AddListener(RefreshStrength);

        //actionSpeed
        actSpeedLess.onClick.AddListener(RefreshActionSpeed);
        actSpeedPlus.onClick.AddListener(RefreshActionSpeed);

        //moveSpeed
        moveSpeedLess.onClick.AddListener(RefreshMoveSpeed);
        moveSpeedPlus.onClick.AddListener(RefreshMoveSpeed);

        //visionRange
        visionRangeLess.onClick.AddListener(RefreshVisionRange);
        visionRangePlus.onClick.AddListener(RefreshVisionRange);

        //pickRange
        pickRangeLess.onClick.AddListener(RefreshPickRange);
        pickRangePlus.onClick.AddListener(RefreshPickRange);

        //attackRange
        atkRangeLess.onClick.AddListener(RefreshAttackRange);
        atkRangePlus.onClick.AddListener(RefreshAttackRange);
    }
    #endregion

    #region Refresh Stat Bars
    private void RefreshStatBars()
    {
        RefreshStrength();
        RefreshVisionRange();
        RefreshPickRange();
        RefreshAttackRange();
        RefreshVitality();
        RefreshStamina();
        RefreshMoveSpeed();
        RefreshActionSpeed();

    }

    //Left Stats
    private void RefreshStrength()
    {
        //Clear stats
        for (int i = 0; i < 3; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.Strength)
        {
            case 1:
                barLeftStatsList[0].GetComponent<Image>().color = statsRedColor;
                break;
            case 2:
                barLeftStatsList[0].GetComponent<Image>().color = statsRedColor;
                barLeftStatsList[1].GetComponent<Image>().color = statsRedColor;
                break;
            case 3:
                barLeftStatsList[0].GetComponent<Image>().color = statsRedColor;
                barLeftStatsList[1].GetComponent<Image>().color = statsRedColor;
                barLeftStatsList[2].GetComponent<Image>().color = statsRedColor;
                break;
            default:
                break;
        }
    }

    private void RefreshVisionRange()
    {
        //Clear stats
        for (int i = 3; i < 6; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.VisionRange)
        {
            case 1:
                barLeftStatsList[3].GetComponent<Image>().color = statsGreenColor;
                break;
            case 2:
                barLeftStatsList[3].GetComponent<Image>().color = statsGreenColor;
                barLeftStatsList[4].GetComponent<Image>().color = statsGreenColor;
                break;
            case 3:
                barLeftStatsList[3].GetComponent<Image>().color = statsGreenColor;
                barLeftStatsList[4].GetComponent<Image>().color = statsGreenColor;
                barLeftStatsList[5].GetComponent<Image>().color = statsGreenColor;
                break;
            default:
                break;
        }
    }

    private void RefreshPickRange()
    {
        //Clear stats
        for (int i = 6; i < 9; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.PickRange)
        {
            case 1:
                barLeftStatsList[6].GetComponent<Image>().color = statsBlueColor;
                break;
            case 2:
                barLeftStatsList[6].GetComponent<Image>().color = statsBlueColor;
                barLeftStatsList[7].GetComponent<Image>().color = statsBlueColor;
                break;
            case 3:
                barLeftStatsList[6].GetComponent<Image>().color = statsBlueColor;
                barLeftStatsList[7].GetComponent<Image>().color = statsBlueColor;
                barLeftStatsList[8].GetComponent<Image>().color = statsBlueColor;
                break;
            default:
                break;
        }
    }

    private void RefreshAttackRange()
    {
        //Clear stats
        for (int i = 9; i < 12; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.AtkRange)
        {
            case 1:
                barLeftStatsList[9].GetComponent<Image>().color = statsRedColor;
                break;
            case 2:
                barLeftStatsList[9].GetComponent<Image>().color = statsRedColor;
                barLeftStatsList[10].GetComponent<Image>().color = statsRedColor;
                break;
            case 3:
                barLeftStatsList[9].GetComponent<Image>().color = statsRedColor;
                barLeftStatsList[10].GetComponent<Image>().color = statsRedColor;
                barLeftStatsList[11].GetComponent<Image>().color = statsRedColor;
                break;
            default:
                break;
        }
    }
    
    //Right Stats
    private void RefreshVitality()
    {
        //Clear stats
        for (int i = 0; i < 3; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.Vitality)
        {
            case 1:
                barRightStatsList[0].GetComponent<Image>().color = statsGreenColor;
                break;
            case 2:
                barRightStatsList[0].GetComponent<Image>().color = statsGreenColor;
                barRightStatsList[1].GetComponent<Image>().color = statsGreenColor;
                break;
            case 3:
                barRightStatsList[0].GetComponent<Image>().color = statsGreenColor;
                barRightStatsList[1].GetComponent<Image>().color = statsGreenColor;
                barRightStatsList[2].GetComponent<Image>().color = statsGreenColor;
                break;
            default:
                break;
        }
    }

    private void RefreshStamina()
    {
        //Clear stats
        for (int i = 3; i < 6; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.Stamina)
        {
            case 1:
                barRightStatsList[3].GetComponent<Image>().color = statsBlueColor;
                break;
            case 2:
                barRightStatsList[3].GetComponent<Image>().color = statsBlueColor;
                barRightStatsList[4].GetComponent<Image>().color = statsBlueColor;
                break;
            case 3:
                barRightStatsList[3].GetComponent<Image>().color = statsBlueColor;
                barRightStatsList[4].GetComponent<Image>().color = statsBlueColor;
                barRightStatsList[5].GetComponent<Image>().color = statsBlueColor;
                break;
            default:
                break;
        }
    }

    private void RefreshMoveSpeed()
    {
        //Clear stats
        for (int i = 6; i < 9; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.MoveSpeed)
        {
            case 1:
                barRightStatsList[6].GetComponent<Image>().color = statsGreenColor;
                break;
            case 2:
                barRightStatsList[6].GetComponent<Image>().color = statsGreenColor;
                barRightStatsList[7].GetComponent<Image>().color = statsGreenColor;
                break;
            case 3:
                barRightStatsList[6].GetComponent<Image>().color = statsGreenColor;
                barRightStatsList[7].GetComponent<Image>().color = statsGreenColor;
                barRightStatsList[8].GetComponent<Image>().color = statsGreenColor;
                break;
            default:
                break;
        }
    }

    private void RefreshActionSpeed()
    {
        //Clear stats
        for (int i = 9; i < 12; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (LumyStats.ActSpeed)
        {
            case 1:
                barRightStatsList[9].GetComponent<Image>().color = statsBlueColor;
                break;
            case 2:
                barRightStatsList[9].GetComponent<Image>().color = statsBlueColor;
                barRightStatsList[10].GetComponent<Image>().color = statsBlueColor;
                break;
            case 3:
                barRightStatsList[9].GetComponent<Image>().color = statsBlueColor;
                barRightStatsList[10].GetComponent<Image>().color = statsBlueColor;
                barRightStatsList[11].GetComponent<Image>().color = statsBlueColor;
                break;
            default:
                break;
        }
    }
    #endregion

    #region Display Stat Bars
    /// <summary>
    /// Display Stat Bars 
    /// </summary>
    ///
    private void DisplayStatBars()
    {
        barLeftStatsList = new List<GameObject>();
        barRightStatsList = new List<GameObject>();

        //Display Left Bars
        //Instantiate 4 rows
        for (int i = 0; i < 4; i++)
        {   //Instantiate 1 Prefab
            for (int j = 0; j < 3; j++)
            {
                GameObject statLeftBar = Instantiate(statBarPrefab, new Vector3(statBarLeftXpos + j * statBarHSpacing, statBarYPos - i * statBarVertSpacing, 0f), Quaternion.identity);
                statLeftBar.transform.SetParent(GameObject.Find("StatBars").transform, false);
                barLeftStatsList.Add(statLeftBar);
            }
        }
        
        //Display Right Bars
        //Instantiate 4 rows
        for (int i = 0; i < 4; i++)
        {   //Instantiate 1 Prefab
            for (int j = 0; j < 3; j++)
            {
                GameObject statBar = Instantiate(statBarPrefab, new Vector3(statBarRightXpos + j * statBarHSpacing, statBarYPos - i * statBarVertSpacing, 0f), Quaternion.identity);
                statBar.transform.SetParent(GameObject.Find("StatBars").transform, false);
                barRightStatsList.Add(statBar);
            }
        }
    }
    #endregion

    #region Set Swarm Image
    public void OpenSwarmImageDialog()
    {
        swarmImageDialog.SetActive(!swarmImageDialog.activeSelf);
        RefreshView();
    }

    private void RefreshSwarmImageScroll()
    {
        //Delete Prefabs
        IList<GameObject> prefabsChilds = new List<GameObject>();
        if (swarmPrefabsToDestroyPanel.transform.childCount != 0)
        {
            for (int i = 0; i < swarmPrefabsToDestroyPanel.transform.childCount; i++)
            {
                prefabsChilds.Add(swarmPrefabsToDestroyPanel.transform.GetChild(i).gameObject);
            }
            if (prefabsChilds != null)
            {
                foreach (GameObject child in prefabsChilds)
                {
                    Destroy(child);
                }
            }
        }
        
        //Instantiate Picts
        swarmImagesArray = LumyPictFactory.instance.InstanciateAllPicts();

        foreach (GameObject image in swarmImagesArray)
        {
            image.transform.SetParent(swarmPrefabsToDestroyPanel.transform);
        }

        //Remove old buttons
        IList<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < swarmImageScrollContent.transform.childCount; i++)
        {
            childs.Add(swarmImageScrollContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }

        //Set scrollRect size
        RectTransform rec = swarmImageScrollContent.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, swarmImagesArray.Length * (swarmImageButtonPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) + 20f);

        //Create new buttons
        float yLeft = -66f;
        float yMid = -66f;
        float yRight = -66f;
        float scalFactor = 0.01f;

        foreach (GameObject image in swarmImagesArray)
        {
            //Create Button
            GameObject button = Instantiate(swarmImageButtonPrefab, Vector3.zero, Quaternion.identity);

            //LButton Layout
            Vector3 pos = swarmImageScrollContent.transform.position;
            float nbImageLeftColumn = Mathf.Ceil(swarmImagesArray.Length / 3f);

            //Left Column
            if (System.Array.IndexOf(swarmImagesArray, image) < nbImageLeftColumn)
            {
                pos += new Vector3(-135f * scalFactor, yLeft * scalFactor, 0f);
                //button
                button.transform.position = pos;
                button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
                yLeft -= swarmImagesYMarginLayout;
                button.transform.SetParent(swarmImageScrollContent.transform);
            }
            //Mid Column
            else if (nbImageLeftColumn <= System.Array.IndexOf(swarmImagesArray, image) && System.Array.IndexOf(swarmImagesArray, image) < 2f * nbImageLeftColumn)
            {
                pos += new Vector3(25f * scalFactor, yMid * scalFactor, 0f);
                button.transform.position = pos;
                button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
                yMid -= swarmImagesYMarginLayout;
                button.transform.SetParent(swarmImageScrollContent.transform);
            }
            //RightColumn
            else
            {
                pos += new Vector3(183f * scalFactor, yRight * scalFactor, 0f);
                button.transform.position = pos;
                button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
                yRight -= swarmImagesYMarginLayout;
                button.transform.SetParent(swarmImageScrollContent.transform);
            }

            //Change button sprite
            Texture2D texturPicto = (Texture2D)image.GetComponent<MeshRenderer>().material.mainTexture;
            Sprite spritePicto = Sprite.Create(texturPicto, new Rect(0.0f, 0.0f, texturPicto.width, texturPicto.height), new Vector2(0.5f, 0.5f), 100.0f);
            button.GetComponent<Image>().sprite = spritePicto;

            //Set swarm image
            int index = System.Array.IndexOf(swarmImagesArray, image);
            button.GetComponent<Button>().onClick.AddListener(delegate {SetSwarmImage(index); });
            
        }

    }
    private void SetSwarmImage(int index)
    {
        AppContextManager.instance.ActiveSpecie.PictId = index;
        editSwarmImageButton.GetComponent<Image>().material = swarmImagesArray[index].GetComponent<MeshRenderer>().material;
        swarmImageDialog.SetActive(!swarmImageDialog.activeSelf);
        SaveSwarm();
        RefreshView();
    }
    #endregion

    #region Set Cast Image

    public void OpenCastImageDialog()
    {
        castImageDialog.SetActive(!castImageDialog.activeSelf);
        RefreshView();
    }

    private void RefreshCastImageScroll()
    {
        //Delete Prefabs
        IList<GameObject> prefabsChilds = new List<GameObject>();
        if (castPrefabsToDestroyPanel.transform.childCount != 0)
        {
            for (int i = 0; i < castPrefabsToDestroyPanel.transform.childCount; i++)
            {
                prefabsChilds.Add(castPrefabsToDestroyPanel.transform.GetChild(i).gameObject);
            }
            if (prefabsChilds != null)
            {
                foreach (GameObject child in prefabsChilds)
                {
                    Destroy(child);
                }
            }
        }
       
        //instantiate Picts
        castImagesArray = LumyPictFactory.instance.InstanciateAllPicts();
        foreach (GameObject image in castImagesArray)
        {
            image.transform.SetParent(castPrefabsToDestroyPanel.transform);
        }

        //Remove old buttons
        IList<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < castImageScrollContent.transform.childCount; i++)
        {
            childs.Add(castImageScrollContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }

        //Set scrollRect size
        RectTransform rec = castImageScrollContent.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, castImagesArray.Length * (swarmImageButtonPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) + 20f);

        //Create new buttons
        float yLeft = -66f;
        float yMid = -66f;
        float yRight = -66f;
        float scalFactor = 0.01f;

        foreach (GameObject image in castImagesArray)
        {
            //Create Button
            GameObject button = Instantiate(swarmImageButtonPrefab, Vector3.zero, Quaternion.identity);
            
            
            //LButton Layout
            Vector3 pos = castImageScrollContent.transform.position;
            float nbImageLeftColumn = Mathf.Ceil(castImagesArray.Length / 3f);
            //Left Column
            if (System.Array.IndexOf(castImagesArray, image) < nbImageLeftColumn)
            {
                pos += new Vector3(-135f * scalFactor, yLeft * scalFactor, 0f);
                //button
                button.transform.position = pos;
                button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
                yLeft -= swarmImagesYMarginLayout;
                button.transform.SetParent(castImageScrollContent.transform);
            }
            //Mid Column
            else if (nbImageLeftColumn <= System.Array.IndexOf(castImagesArray, image) && System.Array.IndexOf(castImagesArray, image) < 2f * nbImageLeftColumn)
            {
                pos += new Vector3(25f * scalFactor, yMid * scalFactor, 0f);
                button.transform.position = pos;
                button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
                yMid -= swarmImagesYMarginLayout;
                button.transform.SetParent(castImageScrollContent.transform);
            }
            //RightColumn
            else
            {
                pos += new Vector3(183f * scalFactor, yRight * scalFactor, 0f);
                button.transform.position = pos;
                button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
                yRight -= swarmImagesYMarginLayout;
                button.transform.SetParent(castImageScrollContent.transform);
            }

            //Change button sprite
            Texture2D texturPicto = (Texture2D)image.GetComponent<MeshRenderer>().material.mainTexture;
            Sprite spritePicto = Sprite.Create(texturPicto, new Rect(0.0f, 0.0f, texturPicto.width, texturPicto.height), new Vector2(0.5f, 0.5f), 100.0f);
            button.GetComponent<Image>().sprite = spritePicto;

            //Set cast image
            int index = System.Array.IndexOf(castImagesArray, image);
            button.GetComponent<Button>().onClick.AddListener(delegate { SetCastImage(index); });
        }
    }

    private void SetCastImage(int index)
    {
        AppContextManager.instance.ActiveCast.PictId = index;
        editCastImageButton.GetComponent<Image>().material = castImagesArray[index].GetComponent<MeshRenderer>().material;
        SaveLumy(AppContextManager.instance.ActiveCast);
        castImageDialog.SetActive(!castImageDialog.activeSelf);
        RefreshView();
    }
    #endregion

    #region CloseSelectionPanels
    //Swarm Panel
    public void CursorEntersSwarmPanel()
    {
        isOnSwarmImagesPanel = true;
    }
    public void CursorExitsSwarmPanel()
    {
        isOnSwarmImagesPanel = false;
    }
    //Cast Panel
    public void CursorEntersCastPanel()
    {
        isOnCastImagesPanel = true;
    }
    public void CursorExitsCastPanel()
    {
        isOnCastImagesPanel = false;
    }
    //Swarm Selection Panel
    public void CursorEntersSwarmSelectionPanel()
    {
        isOnSwarmSelectionPanel = true;
    }
    public void CursorExitsSwarmSelectionPanel()
    {
        isOnSwarmSelectionPanel = false;
    }
    #endregion

    void Update()
    {
        if (GameManager.instance == null)
        {
            SceneManager.LoadScene("MapSwarmEdit", LoadSceneMode.Additive);
            isInGameUnloaded = true;
        }

        if (isInGameUnloaded && GameManager.instance != null)
        {
            isSceneLoaded = true;
        }

        if (isSceneLoaded && !isFirstRefreashed)
        {
            isFirstRefreashed = true;
            renderingCamera = GameObject.FindGameObjectWithTag("RenderCamera");
            RefreshView();
        }

        //Close images panels on click
        if (Input.GetMouseButtonDown(0) && !isOnSwarmImagesPanel)
        {
            swarmImageDialog.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && !isOnCastImagesPanel)
        {
            castImageDialog.SetActive(false);
        }
        //Close Swarm selection panel on click
        if (Input.GetMouseButtonDown(0) && !isOnSwarmSelectionPanel)
        {
            swarmSelectionPanel.SetActive(false);
        }
    }

    #region Refresh view functions
    public void RefreshView()
    {
        RefreashSwarmScroll();
        RefreashLumysScroll();
        //RefreshSwarmImageScroll();
        //RefreshCastImageScroll();
        RefreshLumyAppearenceFromData();
        RefreshLumyInfo();
        RefreshSwarmInfo();
        RefreashLumyStats();
        RefreshStatBars();
        StatsButtonListener();

        if (!isFirstRefreashed)
        {
            return;
        }

        RefreashInGame();
    }

    private void RefreashInGame()
    {
        //Setup Player Folders
        string specieName = AppContextManager.instance.ActiveSpecie.Name;
        string castName = AppContextManager.instance.ActiveCast.Name;
        AppContextManager.instance.LoadPlayerSpecies(specieName, "XXX_specie");

        //Erase Player 1 prysme
        string p1PrismeFilePath = AppContextManager.instance.Player1FolderPath
            + AppContextManager.instance.PRYSME_FILE_NAME
            + AppContextManager.instance.CSV_EXT;
        string templatePrysmeFilePath = AppContextManager.instance.TemplateFolderPath
            + AppContextManager.instance.PRYSME_FILE_NAME
            + AppContextManager.instance.CSV_EXT;
        File.Delete(p1PrismeFilePath);
        File.Copy(templatePrysmeFilePath, p1PrismeFilePath);

        //ResetGame Manager
        GameManager.instance.ResetGame();

        //Replace old Lumy by new one
        GameObject oldEditedInGameLumy = editedInGameLumy;
        LayLumyInGame(castName);
        if (oldEditedInGameLumy != null)
        {
            Unit_GameObj_Manager.instance.KillUnit(oldEditedInGameLumy.GetComponent<AgentEntity>());
        }
    }

    private void LayLumyInGame(string castName)
    {
        //find spawn pos
        Vector3 spawnPos = new Vector3(10f, 0f, -4.5f);
        Quaternion spawnRot = Quaternion.identity;
        if (editedInGameLumy != null)
        {
            spawnPos = editedInGameLumy.transform.position;
            spawnRot = editedInGameLumy.transform.rotation;
        }

        //Retreive lumy prefab
        GameObject editedLumyPrefab = GameManager.instance.GetUnitTemplate(
                        PlayerAuthority.Player1, castName);

        //Pop Lumy in game
        editedInGameLumy = Instantiate(editedLumyPrefab, spawnPos, spawnRot);
        editedInGameLumy.SetActive(true);
        AgentEntity editedLumyEntity = editedInGameLumy.GetComponent<AgentEntity>();
        editedInGameLumy.transform.parent = GameManager.instance.transform;
        editedInGameLumy.name = editedLumyEntity.CastName;
        editedLumyEntity.GameParams =
        GameManager.instance.GameParam.GetComponent<GameParamsScript>();
        HomeScript homeScript = GameManager.instance.GetHome(PlayerAuthority.Player1);
        Unit_GameObj_Manager.instance.addUnit(editedLumyEntity, homeScript);

        //Hide lumy on menu scene
        int minimapLayer = LayerMask.NameToLayer("swarmeditminimap");
        GameObject hearth = editedInGameLumy.transform.Find("Hearth").gameObject;
        GameObject picto = hearth.transform.GetChild(0).gameObject;
        hearth.layer = minimapLayer;
        picto.layer = minimapLayer;
        GameObject head = editedInGameLumy.transform.Find("Head").gameObject;
        for (int i = 0; i < head.transform.childCount; i++)
        {
            GameObject compo = head.transform.GetChild(i).gameObject;
            compo.layer = minimapLayer;
        }
        GameObject tail = editedInGameLumy.transform.Find("Tail").gameObject;
        for (int i = 0; i < tail.transform.childCount; i++)
        {
            GameObject compo = tail.transform.GetChild(i).gameObject;
            compo.layer = minimapLayer;
        }
        GameObject canvas = editedInGameLumy.transform.Find("Self/Canvas").gameObject;
        Destroy(canvas);

        //Set camera
        renderingCamera.GetComponent<CameraSwarmEdit>().Target = editedInGameLumy;
    }

    /// <summary>
    /// Use the lumy appearence to update states
    /// </summary>
    private void RefreashLumyStats()
    {
        //Retreive buff components
        GameObject head = editedLumy.transform.Find("Head").gameObject;
        GameObject tail = editedLumy.transform.Find("Tail").gameObject;
        AgentComponent[] headCompos =
            head.GetComponentsInChildren<AgentComponent>();
        AgentComponent[] tailCompos =
            tail.GetComponentsInChildren<AgentComponent>();
        IList<AgentComponent> compos = new List<AgentComponent>();
        for (int i = 0; i < headCompos.Length; i++)
        {
            if (headCompos[i].Name != "Base Actions"
                && headCompos[i].Name != "Base Stats")
            {
                compos.Add(headCompos[i]);
            }
        }
        for (int i = 0; i < tailCompos.Length; i++)
        {
            if (tailCompos[i].Name != "Base Actions"
               && tailCompos[i].Name != "Base Stats")
            {
                compos.Add(tailCompos[i]);
            }
        }

        //Create Stats Info
        int vitality = 0;
        int stamina = 0;
        int strength = 0;
        int actSpeed = 0;
        int moveSpeed = 0;
        int visionRange = 0;
        int pickRange = 0;
        int atkRange = 0;
        foreach (AgentComponent compo in compos)
        {
            vitality += VitalityPointsFromCompoId(compo.Id);
            stamina += StaminaPointsFromCompoId(compo.Id);
            strength += StrengthPointsFromCompoId(compo.Id);
            actSpeed += ActionSpeedPointsFromCompoId(compo.Id);
            moveSpeed += MoveSpeedPointsFromCompoId(compo.Id);
            visionRange += VisionRangePointsFromCompoId(compo.Id);
            pickRange += PickRangePointsFromCompoId(compo.Id);
            atkRange += AtkRangePointsFromCompoId(compo.Id);
        }
        LumyStats.Vitality = vitality;
        LumyStats.Stamina = stamina;
        LumyStats.Strength = strength;
        LumyStats.ActSpeed = actSpeed;
        LumyStats.MoveSpeed = moveSpeed;
        LumyStats.VisionRange = visionRange;
        LumyStats.PickRange = pickRange;
        LumyStats.AtkRange = atkRange;
    }

    private void RefreshLumyAppearenceFromData()
    {
        LoadLumy(AppContextManager.instance.ActiveCast.Name);
    }

    private void RefreashSwarmScroll()
    {
        //Remove odl buttons
        IList<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < swarmScrollContent.transform.childCount; i++)
        {
            childs.Add(swarmScrollContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }

        string[] speciesNames = AppContextManager.instance.GetSpeciesFolderNames();

        // Set ScrollRect sizes
        RectTransform rec = swarmScrollContent.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, speciesNames.Length * (swarmSelectionBtnPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) + 20f);

        for (int i = 0; i < speciesNames.Length; i++)
        {
            GameObject swarmSelectionButton = Instantiate(swarmSelectionBtnPrefab);
            Button button = swarmSelectionButton.GetComponent<Button>();
            Text text = swarmSelectionButton.GetComponentInChildren<Text>();
            RectTransform rectTransform = swarmSelectionButton.GetComponent<RectTransform>();
            swarmSelectionButton.transform.SetParent(swarmScrollContent.transform);

            //Set Text
            text.text = speciesNames[i];

            //Set Position
            rectTransform.localPosition = new Vector3(
                200f,
                -i * (rectTransform.rect.height + 20f) - 70f,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            button.onClick.AddListener(delegate { SelectSwarm(text.text); });
        }
    }

    public void SelectSwarm(string swarmName)
    {
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        //End Prysme Button Glow
        imagePrysme.gameObject.SetActive(false);
        RefreshView();
    }

    private void RefreshLumyInfo()
    {
        mainPanelLumyName.text = AppContextManager.instance.ActiveCast.Name;
        prodTime = GetProdTime();
        getCost(); 
        LoadCastActions();

        //Cast Image
        editCastImageButton.GetComponent<Image>().material = castImagesArray[AppContextManager.instance.ActiveCast.PictId].GetComponent<MeshRenderer>().material;
    }

    private void RefreshSwarmInfo()
    {
        swarmPanelSwarmName.text = AppContextManager.instance.ActiveSpecie.Name;
        
        //Swarm Image
        editSwarmImageButton.GetComponent<Image>().material = swarmImagesArray[AppContextManager.instance.ActiveSpecie.PictId].GetComponent<MeshRenderer>().material;
    }

    private void RefreashLumysScroll()
    {
        //Remove odl buttons
        IList<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < lumysScrollContent.transform.childCount; i++)
        {
            childs.Add(lumysScrollContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }

        // Set ScrollRect sizes
        RectTransform rec = lumysScrollContent.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, AppContextManager.instance.ActiveSpecie.Casts.Count * (lumyButtonPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f));

        //Create new buttons
        float y = -40f;
        float scalFactor = 0.01f;
        foreach (KeyValuePair<string, Cast> lumy
            in AppContextManager.instance.ActiveSpecie.Casts)
        {
            //Create Button
            GameObject button = Instantiate(lumyButtonPrefab, Vector3.zero, Quaternion.identity);

            //Layout Button
            Vector3 pos = lumysScrollContent.transform.position;
            pos += new Vector3(0f, y * scalFactor, 0f);
            button.transform.position = pos;
            button.transform.localScale = new Vector3(scalFactor, scalFactor, scalFactor);
            y -= lumyYMarginLayout;
            button.transform.SetParent(lumysScrollContent.transform);

            //Set Name
            Text btnText = button.GetComponentInChildren<Text>();
            btnText.text = lumy.Key;
        }
    }

    private void RefreshLumyAppearenceFromStats()
    {
        //Destroy last Lumy
        if (editedLumy != null)
        {
            editedLumy.GetComponent<AgentEntity>().enabled = false;
            editedLumy.SetActive(false);
            Destroy(editedLumy);
        }

        //Create empty
        Cast lumyCast = AppContextManager.instance.ActiveCast;
        editedLumy = Instantiate(emptyAgentPrefab);
        editedLumy.transform.parent = this.transform;
        AgentEntity agentEntity = editedLumy.GetComponent<AgentEntity>();
        agentEntity.BehaviorModelIdentifier = lumyCast.BehaviorModelIdentifier;
        GameObject head = editedLumy.transform.Find("Head").gameObject;
        GameObject tail = editedLumy.transform.Find("Tail").gameObject;
        GameObject hearth = editedLumy.transform.Find("Hearth").gameObject;

        //Add sefault components
        //TODO Extract ID constants
        //All Actions
        GameObject componentObject = GameObject.Instantiate(emptyComponentPrefab);
        componentObject.transform.parent = head.transform;
        ComponentInfo compoInfo = ComponentFactory.instance.CreateComponent(1);
        UnitTemplateInitializer.CopyComponentValues(compoInfo, componentObject.GetComponent<AgentComponent>());
        //Default Stats
        componentObject = GameObject.Instantiate(emptyComponentPrefab);
        componentObject.transform.parent = tail.transform;
        compoInfo = ComponentFactory.instance.CreateComponent(2);
        UnitTemplateInitializer.CopyComponentValues(compoInfo, componentObject.GetComponent<AgentComponent>());

        //Add bonus components
        //TODO Extract ID constants
        //Head compos
        PushStrengthComp();
        PushAtkRangeComp();
        PushVisionRangeComp();
        PushPickRangeComp();

        //Tail compos
        PushVitalityComp();
        PushMoveSpeedComp();
        PushActionSpeedComp();
        PushStaminaComp();

        //Disable InGame Logics
        AgentBehavior agentBehavior = editedLumy.GetComponent<AgentBehavior>();
        agentBehavior.enabled = false;
        AgentContext agentContext = editedLumy.GetComponent<AgentContext>();
        agentContext.enabled = false;
        agentEntity.enabled = false;
        GameObject self = editedLumy.transform.Find("Self").gameObject;
        self.SetActive(false);

        //Set lumy to Forward Kinematic
        GameObject skeleton = editedLumy.transform.Find("Skeleton").gameObject;
        PhySkeleton skeletonScript = skeleton.GetComponent<PhySkeleton>();
        skeletonScript.IsIK = false;
        skeletonScript.Frame();
        PhyJoin[] headJoins = head.GetComponentsInChildren<PhyJoin>();
        PhyJoin[] tailJoins = tail.GetComponentsInChildren<PhyJoin>();
        PhyJoin hearthJoin = hearth.GetComponent<PhyJoin>();
        for (int i = 0; i < headJoins.Length; i++)
        {
            headJoins[i].Init();
            headJoins[i].Frame();
        }
        hearthJoin.Init();
        hearthJoin.Frame();
        for (int i = 0; i < tailJoins.Length; i++)
        {
            tailJoins[i].Init();
            tailJoins[i].Frame();
        }

        //Disable physic
        skeletonScript.gameObject.SetActive(false);
        for (int i = 0; i < headJoins.Length; i++)
        {
            headJoins[i].enabled = false;
        }
        hearthJoin.enabled = false;
        for (int i = 0; i < tailJoins.Length; i++)
        {
            tailJoins[i].enabled = false;
        }

        //Layout
        editedLumy.transform.position = new Vector3(-1.5f, -3f, 0f);
        editedLumy.transform.rotation = Quaternion.Euler(0f, 90f, 90f);

        //Unset Agent tag to be ignored by ABManager.UregisterAgent()
        editedLumy.tag = "Untagged";
    }
    #endregion

    #region Cost Functions

    private void getCost()
    {
        string path = AppContextManager.instance.ActiveBehaviorPath;
        ABModel behaviorModel = ABManager.instance.LoadABModelFromFile(path);
        AgentScript.ResourceCost res =  CostManager.instance.ComputeCost(editedLumy.GetComponentsInChildren<AgentComponent>(), behaviorModel);
        redCost = res.getResourceByColor(ABColor.Color.Red);
        greenCost = res.getResourceByColor(ABColor.Color.Green);
        blueCost = res.getResourceByColor(ABColor.Color.Blue);

    }

    private float GetProdTime()
    {
        AgentContext agentContext = editedLumy.GetComponent<AgentContext>();
        AgentComponent[] agentComponent = agentContext.Entity.getAgentComponents();
        return CostManager.instance.ComputeProdTime(agentComponent);
    }
    #endregion

    #region Display Actions
    /// <summary>
    /// Find actions used in the mc of the current cast and show this in the actions list canvas
    /// </summary>
    private void LoadCastActions()
    {
        List<string> actionsList = new List<string>();

        //Open .csv behavior of the current cast
        string behaviorPath = AppContextManager.instance.ActiveBehaviorPath;
        if (File.Exists(behaviorPath))
        {
            StreamReader reader = new StreamReader(behaviorPath);

            List<string> lines = new List<string>();
            while (reader.Peek() >= 0)
            {
                lines.Add(reader.ReadLine());
            }

            // Create a list with the actions used by the current cast
            foreach (string line in lines)
            {
                if (line.Contains("trigger"))
                {
                    string[] splitedLine = line.Split(',');
                    string[] splitedTrigger = splitedLine[2].Split('{');
                    string action = splitedTrigger[1].Substring(0, splitedTrigger[1].Length - 1);
                    if (!actionsList.Contains(action))
                    {
                        actionsList.Add(action);
                    }
                }
            }

            //int i = 1;

            /* Greg's code for text display :
             * 
            // Find Action Lumy canvas and put the right text in actions list
            GameObject listActionsCanvas = GameObject.Find("Liste_Actions");
            Text[] textAction = listActionsCanvas.GetComponentsInChildren<Text>();
            foreach (Text text in textAction)
            {
                //Do not erase the canvas title
                if (!text.text.Contains("Lumy"))
                {
                    text.text = "";
                }
            }
            foreach (string actionText in actionsList)
            {
                textAction[i].text = "- " + actionText.First().ToString().ToUpper() + actionText.Substring(1);
                i++;
            }
            */

            // Find Action Lumy canvas and put the right image in actions list
            if (lumyPanel.activeSelf)
            {
                GameObject listActionsCanvas = GameObject.Find("Liste_Actions");
                Image[] imageAction = listActionsCanvas.GetComponentsInChildren<Image>();
                foreach (Image image in imageAction)
                {
                    image.color = new Color32(70, 70, 70, 255);
                }

                //Set Panel Color
                imageAction[0].color = new Color32(0, 0, 0, 0);

                // Find Action Lumy canvas and put the right text in actions list
                Text[] textAction = listActionsCanvas.GetComponentsInChildren<Text>();
                foreach (Text text in textAction)
                {
                    text.color = new Color32(70, 70, 70, 255);
                }

                foreach (string actionText in actionsList)
                {

                    if (String.Compare(actionText, "goto") == 0)
                    {
                        imageAction[1].color = new Color32(255, 255, 255, 255);
                        textAction[0].color = new Color32(255, 255, 255, 255);
                    }
                    if (String.Compare(actionText, "strike") == 0)
                    {
                        imageAction[2].color = new Color32(255, 255, 255, 255);
                        textAction[1].color = new Color32(255, 255, 255, 255);
                    }
                    if (String.Compare(actionText, "pick") == 0)
                    {
                        imageAction[3].color = new Color32(255, 255, 255, 255);
                        textAction[2].color = new Color32(255, 255, 255, 255);
                    }
                    if (String.Compare(actionText, "roaming") == 0)
                    {
                        imageAction[4].color = new Color32(255, 255, 255, 255);
                        textAction[3].color = new Color32(255, 255, 255, 255);
                    }
                    if (String.Compare(actionText, "trace") == 0)
                    {
                        imageAction[5].color = new Color32(255, 255, 255, 255);
                        textAction[4].color = new Color32(255, 255, 255, 255);
                    }
                    if (String.Compare(actionText, "drop") == 0)
                    {
                        imageAction[6].color = new Color32(255, 255, 255, 255);
                        textAction[5].color = new Color32(255, 255, 255, 255);
                    }

                }
            }
            reader.Close();
        }
    }
    #endregion

    /// <summary>
    /// Load the selected lumy given its cast name
    /// </summary>
    /// <param name=""></param>
    public void LoadLumy(string castName)
    {
        //Destroy last Lumy
        if (editedLumy != null)
        {
            editedLumy.GetComponent<AgentEntity>().enabled = false;
            editedLumy.SetActive(false);
            Destroy(editedLumy);
        }

        //Instanciate
        Cast lumyCast = AppContextManager.instance.ActiveCast;
        editedLumy = Instantiate(emptyAgentPrefab);
        editedLumy.transform.parent = this.transform;

        //editedLumy.SetActive(false);
        UnitTemplateInitializer.InitTemplate(
            lumyCast, editedLumy, emptyComponentPrefab);

        //Disable InGame Logics
        AgentBehavior agentBehavior = editedLumy.GetComponent<AgentBehavior>();
        agentBehavior.enabled = false;
        AgentContext agentContext = editedLumy.GetComponent<AgentContext>();
        agentContext.enabled = false;
        AgentEntity agentEntity = editedLumy.GetComponent<AgentEntity>();
        agentEntity.enabled = false;
        GameObject self = editedLumy.transform.Find("Self").gameObject;
        self.SetActive(false);

        //Set lumy to Forward Kinematic
        GameObject skeleton = editedLumy.transform.Find("Skeleton").gameObject;
        PhySkeleton skeletonScript = skeleton.GetComponent<PhySkeleton>();
        skeletonScript.IsIK = false;
        skeletonScript.Frame();
        GameObject head = editedLumy.transform.Find("Head").gameObject;
        GameObject tail = editedLumy.transform.Find("Tail").gameObject;
        GameObject hearth = editedLumy.transform.Find("Hearth").gameObject;
        PhyJoin[] headJoins = head.GetComponentsInChildren<PhyJoin>();
        PhyJoin[] tailJoins = tail.GetComponentsInChildren<PhyJoin>();
        PhyJoin hearthJoin = hearth.GetComponent<PhyJoin>();
        for (int i = 0; i < headJoins.Length; i++)
        {
            headJoins[i].Init();
            headJoins[i].Frame();
        }
        hearthJoin.Init();
        hearthJoin.Frame();
        for (int i = 0; i < tailJoins.Length; i++)
        {
            tailJoins[i].Init();
            tailJoins[i].Frame();
        }

        //Disable physic
        skeletonScript.gameObject.SetActive(false);
        for (int i = 0; i < headJoins.Length; i++)
        {
            headJoins[i].enabled = false;
        }
        hearthJoin.enabled = false;
        for (int i = 0; i < tailJoins.Length; i++)
        {
            tailJoins[i].enabled = false;
        }

        //Layout
        editedLumy.transform.position = new Vector3(-6f, -3f, 0f);
        editedLumy.transform.rotation = Quaternion.Euler(0f, 90f, 90f);

        //Hide
        editedLumy.SetActive(false);
    }

    public void SaveSwarm()
    {
        AppContextManager.instance.SaveSpecie();
    }

    /// <summary>
    /// Persist the changes on the selected lumy
    /// </summary>
    public void SaveLumy(Cast lumyCast)
    {
        //Sync cast
        //Retreive Components Infos
        Cast cast = AppContextManager.instance.ActiveCast;
        GameObject head = editedLumy.transform.Find("Head").gameObject;
        GameObject tail = editedLumy.transform.Find("Tail").gameObject;
        List<ComponentInfo> headCompos = new List<ComponentInfo>();
        for (int i = 0; i < head.transform.childCount; i++)
        {
            GameObject compo = head.transform.GetChild(i).gameObject;
            ComponentInfo compoInfo = ComponentFactory.instance.CreateComponent(
                compo.GetComponent<AgentComponent>().Id);
            headCompos.Add(compoInfo);
        }
        List<ComponentInfo> tailCompos = new List<ComponentInfo>();
        for (int i = 0; i < tail.transform.childCount; i++)
        {
            GameObject compo = tail.transform.GetChild(i).gameObject;
            ComponentInfo compoInfo = ComponentFactory.instance.CreateComponent(
                compo.GetComponent<AgentComponent>().Id);
            tailCompos.Add(compoInfo);
        }
        //Insert Component Infos
        cast.Head.Clear();
        cast.Head.AddRange(headCompos);
        cast.Tail.Clear();
        cast.Tail.AddRange(tailCompos);

        //Persist changes
        AppContextManager.instance.SaveCast(lumyCast);
    }

    /// <summary>
    /// Push the composant on top of the selected lumy's head
    /// </summary>
    /// <param name="compoId">The id of the component on the component referencial</param>
    public void PushHead(int compoId)
    {
        //Add Component
        ComponentInfo compoInfo = ComponentFactory.instance.CreateComponent(compoId);
        GameObject compo = Instantiate(emptyComponentPrefab);
        AgentComponent compoScript = compo.GetComponent<AgentComponent>();
        UnitTemplateInitializer.CopyComponentValues(compoInfo, compoScript);
        GameObject head = editedLumy.transform.Find("Head").gameObject;
        compo.transform.parent = head.transform;
        compo.transform.SetAsFirstSibling();

        //Update bone links
        GameObject skeleton = editedLumy.transform.Find("Skeleton").gameObject;
        PhySkeleton skeletonScript = skeleton.GetComponent<PhySkeleton>();
        skeletonScript.BuildSkeleton();

        compo.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
    }

    /// <summary>
    /// Push the composant on top of the selected lumy's Tail
    /// </summary>
    /// <param name="compoId">The id of the component on the component referencial</param>
    public void PushTail(int compoId)
    {
        //Reopen tail
        GameObject tail = editedLumy.transform.Find("Tail").gameObject;
        GameObject lastCompo = tail.transform.GetChild(tail.transform.childCount - 1).gameObject;
        PhyJoin lastJoin = lastCompo.GetComponent<PhyJoin>();
        lastJoin.DstBones = new PhyBone[1];


        ComponentInfo compoInfo = ComponentFactory.instance.CreateComponent(compoId);
        GameObject compo = Instantiate(emptyComponentPrefab);
        AgentComponent compoScript = compo.GetComponent<AgentComponent>();
        UnitTemplateInitializer.CopyComponentValues(compoInfo, compoScript);
        compo.transform.parent = tail.transform;
        compo.transform.SetAsLastSibling();

        //Update bone links
        GameObject skeleton = editedLumy.transform.Find("Skeleton").gameObject;
        PhySkeleton skeletonScript = skeleton.GetComponent<PhySkeleton>();
        skeletonScript.BuildSkeleton();

        compo.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
    }

    public void OpenSelectSwarmDialog()
    {
        RefreshView();
    }

    public void CopySwarm()
    {
        int nbSpecies = AppContextManager.instance.GetSpeciesFolderNames().Length + 1;
        string copyName = AppContextManager.instance.ActiveSpecie.Name + "(" + nbSpecies + ")";
        copyName = Char.ToLowerInvariant(copyName[0]) + copyName.Substring(1);
        AppContextManager.instance.CopySpecie(copyName);
        string specieFolderName = Char.ToUpperInvariant(copyName[0]) + copyName.Substring(1);
        SelectSwarm(specieFolderName);
        RefreshView();
    }

    public void DeleteSwarm()
    {
        //Prevent removing last
        if (AppContextManager.instance.GetSpeciesFolderNames().Length < 2)
        {
            MessagesManager.instance.LogMsg("You cannot remove the last specie");
            return;
        }

        AppContextManager.instance.DeleteSpecie();
        string defaultSpecie = AppContextManager.instance.GetSpeciesFolderNames()[0];
        SelectSwarm(defaultSpecie);
        RefreshView();
    }

    public void NewSwarm()
    {
        int nbSpecies = AppContextManager.instance.GetSpeciesFolderNames().Length + 1;
        string defaultName = AppContextManager.instance.DEFAULT_SPECIE_NAME + nbSpecies;
        AppContextManager.instance.CreateSpecie(defaultName);
        string specieFolderName = Char.ToUpperInvariant(defaultName[0]) + defaultName.Substring(1);
        SelectSwarm(specieFolderName);
        RefreshView();
    }

    public void OpenEditSwarmDialog()
    {
        editSwarmDialog.SetActive(!editSwarmDialog.activeSelf);
    }

    public void ApplySwarmUpdates()
    {
        //Validate data
        string newName = swarmNameInput.text;
        if (!ValidateSwarmInfo(newName))
        {
            MessagesManager.instance.LogMsg("Swarm info are not valide !");
            return;
        }

        //Apply changes
        string folderName = Char.ToUpperInvariant(newName[0]) + newName.Substring(1);
        string specieFileName = Char.ToLowerInvariant(newName[0]) + newName.Substring(1);
        string src = AppContextManager.instance.ActiveSpecieFilePath;
        string dst = AppContextManager.instance.ActiveSpecieFolderPath
            + specieFileName + AppContextManager.instance.SPECIE_FILES_SUFFIX
            + AppContextManager.instance.CSV_EXT;
        File.Move(src, dst);
        src = AppContextManager.instance.ActiveSpecieFolderPath;
        dst = AppContextManager.instance.SpeciesFolderPath
            + folderName;
        Directory.Move(src, dst);

        SelectSwarm(folderName);

        //Update IHM
        RefreshView();
        OpenEditSwarmDialog();
    }

    private bool ValidateSwarmInfo(string newName)
    {
        foreach (string curName in AppContextManager.instance.GetSpeciesFolderNames())
        {
            string lowerCurName = curName.ToLower();
            string lowerNewName = newName.ToLower();
            if (lowerCurName == lowerNewName)
            {
                return false;
            }
        }
        return true;
    }

    public void OpenImportSwarmDialog()
    {
        ImportController.ImportSpecie();
        RefreshView();
    }

    public void OpenResetSwarmDialog()
    {
        ImportController.ResetSpecie();
        RefreshView();
    }

    public void OpenExportSwarmDialog()
    {
        ExportController.ExportSpecie();
    }

    public void SelectPrysme()
    {
        SetPrysmePanel();
    }

    private void SetPrysmePanel()
    {
        editedLumy.SetActive(false);
        lumyPanel.SetActive(false);
        prysmePanel.SetActive(true);
        lumyHublot.SetActive(false);
        //End Prysme Button Glow
        imagePrysme.gameObject.SetActive(false);
    }

    private void SetLumyPanel()
    {
        editedLumy.SetActive(true);
        lumyPanel.SetActive(true);
        prysmePanel.SetActive(false);
        lumyHublot.SetActive(true);
    }

    //Confirmation panels
    public void ToggleResetConfirmationPanel()
    {
        List<GameObject> confirmationPanelsList = new List<GameObject>();
        confirmationPanelsList.Add(resetConfirmationPanel);
        confirmationPanelsList.Add(delSwarmConfirmationPanel);
        confirmationPanelsList.Add(delCastConfirmationPanel);
        //Active Reset Panel
        confirmationPanelsList[0].SetActive(!confirmationPanelsList[0].activeSelf);
        //Desactive other confirmation panels
        confirmationPanelsList[1].SetActive(false);
        confirmationPanelsList[2].SetActive(false);
    }

    public void ToggleDelSwarmConfirmationPanel()
    {
        List<GameObject> confirmationPanelsList = new List<GameObject>();
        confirmationPanelsList.Add(resetConfirmationPanel);
        confirmationPanelsList.Add(delSwarmConfirmationPanel);
        confirmationPanelsList.Add(delCastConfirmationPanel);
        //Active Delete Swarm Panel
        confirmationPanelsList[1].SetActive(!confirmationPanelsList[1].activeSelf);
        //Desactive other confirmation panels
        confirmationPanelsList[0].SetActive(false);
        confirmationPanelsList[2].SetActive(false);
        confirmationPanelsList[3].SetActive(false);

        //Get Swarm name
        swarmName.text = AppContextManager.instance.ActiveSpecie.Name;
    }

    public void ToggleDelCastConfirmationPanel()
    {
        List<GameObject> confirmationPanelsList = new List<GameObject>();
        confirmationPanelsList.Add(resetConfirmationPanel);
        confirmationPanelsList.Add(delSwarmConfirmationPanel);
        confirmationPanelsList.Add(delCastConfirmationPanel);
        confirmationPanelsList.Add(importSwarmConfirmationPanel);

        //Active Delete Cast Panel
        confirmationPanelsList[2].SetActive(!confirmationPanelsList[2].activeSelf);
        //Desactive other confirmation panels
        confirmationPanelsList[0].SetActive(false);
        confirmationPanelsList[1].SetActive(false);
        confirmationPanelsList[3].SetActive(false);
        //Get Cast name
        castName.text = AppContextManager.instance.ActiveCast.Name;
    }

    public void ToggleImportSwarmConfirmationPanel()
    {
        List<GameObject> confirmationPanelsList = new List<GameObject>();
        confirmationPanelsList.Add(resetConfirmationPanel);
        confirmationPanelsList.Add(delSwarmConfirmationPanel);
        confirmationPanelsList.Add(delCastConfirmationPanel);
        confirmationPanelsList.Add(importSwarmConfirmationPanel);
        
        //Active Delete Cast Panel
        confirmationPanelsList[3].SetActive(!confirmationPanelsList[3].activeSelf);
        //Desactive other confirmation panels
        confirmationPanelsList[0].SetActive(false);
        confirmationPanelsList[1].SetActive(false);
        confirmationPanelsList[2].SetActive(false);
        //Get Cast name
    }

    public void SelectLumy(string lumyName)
    {
        SetLumyPanel();
        AppContextManager.instance.SwitchActiveCast(lumyName);
        RefreshView();
    }

    public void CopyLumy()
    {
        AppContextManager.instance.CloneCast();
        RefreshView();
    }

    public void DeleteLumy()
    {
        //Prevent removing last
        if (AppContextManager.instance.ActiveSpecie.Casts.Count < 2)
        {
            MessagesManager.instance.LogMsg("You cannot remove the last lumy");
            return;
        }

        AppContextManager.instance.DeleteCast();
        Cast firstCast = null;
        foreach (Cast cast in AppContextManager.instance.ActiveSpecie.Casts.Values)
        {
            firstCast = cast;
            break;
        }
        AppContextManager.instance.SwitchActiveCast(firstCast.Name);
        RefreshView();
    }

    public void NewLumy()
    {
        AppContextManager.instance.CreateCast();
        RefreshView();
    }

    public void OpenRenameDialog()
    {
        renameLumyDialog.SetActive(!renameLumyDialog.activeSelf);
    }

    public void RenameLumy()
    {
        //Validate name
        string newName = renameLumyInput.GetComponent<Text>().text;
        if (!ValidateName(newName))
        {
            MessagesManager.instance.LogMsg("The new name is not valide !");
            return;
        }

        //Rename behavior file
        string src = AppContextManager.instance.ActiveBehaviorPath;
        string dst = AppContextManager.instance.ActiveSpecieFolderPath +
            newName + AppContextManager.instance.CAST_FILES_SUFFIX +
            AppContextManager.instance.CSV_EXT;
        File.Move(src, dst);
        Cast trgCast = AppContextManager.instance.ActiveCast.Clone();
        Cast activeCast = AppContextManager.instance.ActiveCast;
        string curName = activeCast.Name;
        AppContextManager.instance.ActiveSpecie.Casts.Remove(curName);
        activeCast.Name = newName;
        activeCast.BehaviorModelIdentifier = 
            newName + AppContextManager.instance.CAST_FILES_SUFFIX;
        AppContextManager.instance.ActiveSpecie.Casts.Add(newName, activeCast);
        AppContextManager.instance.SwitchActiveCast(newName);

        //Rename position file if any
        string posFilePath = AppContextManager.instance.ActiveSpecieFolderPath +
            curName + AppContextManager.instance.POSITION_FILES_SUFFIX +
            AppContextManager.instance.CSV_EXT;
        if (File.Exists(posFilePath))
        {
            src = posFilePath;
            dst = AppContextManager.instance.ActiveSpecieFolderPath +
            newName + AppContextManager.instance.POSITION_FILES_SUFFIX +
            AppContextManager.instance.CSV_EXT;

            File.Move(src, dst);
        }

        //Update specie file
        SaveLumy(trgCast);
        RefreshView();
        OpenRenameDialog();

        //Prysme Button Glow
        imagePrysme.gameObject.SetActive(true);
    }

    private bool ValidateName(string newName)
    {
        string pattern = "[^a-zA-Z0-9\\(\\)_]";
        Regex r = new Regex(pattern);
        Match m = r.Match(newName);
        if (m.Success)
        {
            return false;
        }
        foreach (string curName in AppContextManager.instance.ActiveSpecie.Casts.Keys)
        {
            string lowerCurName = curName.ToLower();
            string lowerNewName = newName.ToLower();
            if (lowerCurName == lowerNewName)
            {
                return false;
            }
        }
        return true;
    }

    public void EditLumyMC()
    {
        NavigationManager.instance.SwapScenesWithoutZoom("EditeurMCScene");
    }

    public void EditPrysmMC()
    {
        AppContextManager.instance.PrysmeEdit = true;
        NavigationManager.instance.SwapScenesWithoutZoom("EditeurMCScene");
    }

    private void PersistStatChange()
    {
        RefreshLumyAppearenceFromStats();
        SaveLumy(AppContextManager.instance.ActiveCast);
        RefreshView();
    }

    private void PushStrengthComp()
    {
        if (lumyStats.Strength == 1)
        {
            PushHead(9);
        }
        else if (lumyStats.Strength == 2)
        {
            PushHead(10);
        }
        else if (lumyStats.Strength == 3)
        {
            PushHead(11);
        }
    }

    private int StrengthPointsFromCompoId(int id)
    {
        if (id == 9)
        {
            return 1;
        }
        else if (id == 10)
        {
            return 2;
        }
        else if (id == 11)
        {
            return 3;
        }
        return 0;
    }

    private void PushAtkRangeComp()
    {
        if (lumyStats.AtkRange == 1)
        {
            PushHead(21);
        }
        else if (lumyStats.AtkRange == 2)
        {
            PushHead(22);
        }
        else if (lumyStats.AtkRange == 3)
        {
            PushHead(23);
        }
    }

    private int AtkRangePointsFromCompoId(int id)
    {
        if (id == 21)
        {
            return 1;
        }
        else if (id == 22)
        {
            return 2;
        }
        else if (id == 23)
        {
            return 3;
        }
        return 0;
    }

    private void PushVisionRangeComp()
    {
        if (lumyStats.VisionRange == 1)
        {
            PushHead(18);
        }
        else if (lumyStats.VisionRange == 2)
        {
            PushHead(19);
        }
        else if (lumyStats.VisionRange == 3)
        {
            PushHead(20);
        }
    }

    private int VisionRangePointsFromCompoId(int id)
    {
        if (id == 18)
        {
            return 1;
        }
        else if (id == 19)
        {
            return 2;
        }
        else if (id == 20)
        {
            return 3;
        }
        return 0;
    }

    private void PushPickRangeComp()
    {
        if (lumyStats.PickRange == 1)
        {
            PushHead(24);
        }
        else if (lumyStats.PickRange == 2)
        {
            PushHead(25);
        }
        else if (lumyStats.PickRange == 3)
        {
            PushHead(26);
        }
    }

    private int PickRangePointsFromCompoId(int id)
    {
        if (id == 24)
        {
            return 1;
        }
        else if (id == 25)
        {
            return 2;
        }
        else if (id == 26)
        {
            return 3;
        }
        return 0;
    }

    private void PushVitalityComp()
    {
        if (lumyStats.Vitality == 1)
        {
            PushTail(3);
        }
        else if (lumyStats.Vitality == 2)
        {
            PushTail(4);
        }
        else if (lumyStats.Vitality == 3)
        {
            PushTail(5);
        }
    }

    private int VitalityPointsFromCompoId(int id)
    {
        if (id == 3)
        {
            return 1;
        }
        else if (id == 4)
        {
            return 2;
        }
        else if (id == 5)
        {
            return 3;
        }
        return 0;
    }

    private void PushMoveSpeedComp()
    {
        if (lumyStats.MoveSpeed == 1)
        {
            PushTail(15);
        }
        else if (lumyStats.MoveSpeed == 2)
        {
            PushTail(16);
        }
        else if (lumyStats.MoveSpeed == 3)
        {
            PushTail(17);
        }
    }

    private int MoveSpeedPointsFromCompoId(int id)
    {
        if (id == 15)
        {
            return 1;
        }
        else if (id == 16)
        {
            return 2;
        }
        else if (id == 17)
        {
            return 3;
        }
        return 0;
    }

    private void PushActionSpeedComp()
    {
        if (lumyStats.ActSpeed == 1)
        {
            PushTail(12);
        }
        else if (lumyStats.ActSpeed == 2)
        {
            PushTail(13);
        }
        else if (lumyStats.ActSpeed == 3)
        {
            PushTail(14);
        }
    }

    private int ActionSpeedPointsFromCompoId(int id)
    {
        if (id == 12)
        {
            return 1;
        }
        else if (id == 13)
        {
            return 2;
        }
        else if (id == 14)
        {
            return 3;
        }
        return 0;
    }

    private void PushStaminaComp()
    {
        if (lumyStats.Stamina == 1)
        {
            PushTail(6);
        }
        else if (lumyStats.Stamina == 2)
        {
            PushTail(7);
        }
        else if (lumyStats.Stamina == 3)
        {
            PushTail(8);
        }
    }

    private int StaminaPointsFromCompoId(int id)
    {
        if (id == 6)
        {
            return 1;
        }
        else if (id == 7)
        {
            return 2;
        }
        else if (id == 8)
        {
            return 3;
        }
        return 0;
    }

    public void IncrVitality()
    {
        if (CanIncrVitality()) {
            LumyStats.Vitality++;
            PersistStatChange();
        } 
    }

    public bool CanIncrVitality()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.Vitality < statLimit;
    }

    public void DecrVitality()
    {
        if (CanDecrVitality())
        {
            LumyStats.Vitality--;
            PersistStatChange();
        }
    }

    public bool CanDecrVitality()
    {
        return LumyStats.Vitality > 0;
    }

    public void IncrStamina()
    {
        if (CanIncrStamina())
        {
            LumyStats.Stamina++;
            PersistStatChange();
        }
    }

    public bool CanIncrStamina()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.Stamina < statLimit;
    }

    public void DecrStamina()
    {
        if (CanDecrStamina())
        {
            LumyStats.Stamina--;
            PersistStatChange();
        }
    }

    public bool CanDecrStamina()
    {
        return LumyStats.Stamina > 0;
    }

    public void IncrStrength()
    {
        if (CanIncrStrength())
        {
            LumyStats.Strength++;
            PersistStatChange();
        }
    }

    public bool CanIncrStrength()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.Strength < statLimit;
    }

    public void DecrStrength()
    {
        if (CanDecrStrength())
        {
            LumyStats.Strength--;
            PersistStatChange();
        }
    }

    public bool CanDecrStrength()
    {
        return LumyStats.Strength > 0;
    }

    public void IncrActSpeed()
    {
        if (CanIncrActSpeed())
        {
            LumyStats.ActSpeed++;
            PersistStatChange();
        }
    }

    public bool CanIncrActSpeed()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.ActSpeed < statLimit;
    }

    public void DecrActSpeed()
    {
        if (CanDecrActSpeed())
        {
            LumyStats.ActSpeed--;
            PersistStatChange();
        }
    }

    public bool CanDecrActSpeed()
    {
        return LumyStats.ActSpeed > 0;
    }

    public void IncrMoveSpeed()
    {
        if (CanIncrMoveSpeed())
        {
            LumyStats.MoveSpeed++;
            PersistStatChange();
        }
    }

    public bool CanIncrMoveSpeed()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.MoveSpeed < statLimit;
    }

    public void DecrMoveSpeed()
    {
        if (CanDecrMoveSpeed())
        {
            LumyStats.MoveSpeed--;
            PersistStatChange();
        }
    }

    public bool CanDecrMoveSpeed()
    {
        return LumyStats.MoveSpeed > 0;
    }

    public void IncrVisionRange()
    {
        if (CanIncrVisionRange())
        {
            LumyStats.VisionRange++;
            PersistStatChange();
        }
    }

    public bool CanIncrVisionRange()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.VisionRange < statLimit;
    }

    public void DecrVisionRange()
    {
        if (CanDecrVisionRange())
        {
            LumyStats.VisionRange--;
            PersistStatChange();
        }
    }

    public bool CanDecrVisionRange()
    {
        return LumyStats.VisionRange > 0;
    }

    public void IncrAtkRange()
    {
        if (CanIncrAtkRange())
        {
            LumyStats.AtkRange++;
            PersistStatChange();
        }
    }

    public bool CanIncrAtkRange()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.AtkRange < statLimit;
    }

    public void DecrAtkRange()
    {
        if (CanDecrAtkRange())
        {
            LumyStats.AtkRange--;
            PersistStatChange();
        }
    }

    public bool CanDecrAtkRange()
    {
        return LumyStats.AtkRange > 0;
    }

    public void IncrPickRange()
    {
        if (CanIncrPickRange())
        {
            LumyStats.PickRange++;
            PersistStatChange();
        }
    }

    public bool CanIncrPickRange()
    {
        return LumyStats.PointsLeft > 0 && LumyStats.PickRange < statLimit;
    }

    public void DecrPickRange()
    {
        if (CanDecrPickRange())
        {
            LumyStats.PickRange--;
            PersistStatChange();
        }
    }

    public bool CanDecrPickRange()
    {
        return LumyStats.PickRange > 0;
    }
}
