using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>
    /// The swarm scroll selection content
    /// </summary>
    [Header("Lumy Appearence")]
    [SerializeField]
    private GameObject editedLumy;

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

    /// <summary>
    /// The canvas listing aull lumys from the active swarm
    /// </summary>
    [Header("Select Lumy Scroll")]
    [SerializeField]
    private GameObject lumysScrollContent;

    /// <summary>
    /// Instancied prefab for each lumys on the lumy scroll content
    /// </summary>
    [SerializeField]
    private GameObject lumyButtonPrefab;

    /// <summary>
    /// Set the margin beetween each lumy select button
    /// </summary>
    [SerializeField]
    private float lumyYMarginLayout = 35f;

    // Use this for initialization
    void Start()
    {
        RefreshView();
    }

    private void RefreshView()
    {
        RefreashSwarmScroll();
        RefreashLumysScroll();
        RefreshLumyInfo();
        RefreshLumyAppearence();
    }

    private void RefreshLumyAppearence()
    {
        LoadLumy(AppContextManager.instance.ActiveCast.Name);
    }

    private void RefreashSwarmScroll()
    {
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
                0,
                -i * (rectTransform.rect.height + 20f) - 20f,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            button.onClick.AddListener(delegate { SelectSwarm(text.text); });
        }
    }

    private void SelectSwarm(string swarmName)
    {
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        RefreshView();
    }

    private void RefreshLumyInfo()
    {
        mainPanelLumyName.text = AppContextManager.instance.ActiveCast.Name;
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

        //Create new buttons
        float y = -5f;
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
            button.transform.parent = lumysScrollContent.transform;

            //Set Name
            Text btnText = button.GetComponentInChildren<Text>();
            btnText.text = lumy.Key;
        }
    }

    /// <summary>
    /// Load the selected lumy given its cast name
    /// </summary>
    /// <param name=""></param>
    public void LoadLumy(string castName)
    {
        //Destroy last Lumy
        if (editedLumy != null)
        {
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
        PhyJoin[] headJoins = head.GetComponentsInChildren<PhyJoin>();
        PhyJoin[] tailJoins = tail.GetComponentsInChildren<PhyJoin>();
        for (int i = 0; i < headJoins.Length; i++)
        {
            headJoins[i].Init();
            headJoins[i].Frame();
        }
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
        for (int i = 0; i < tailJoins.Length; i++)
        {
            tailJoins[i].enabled = false;
        }

        //Layout
        editedLumy.transform.position = new Vector3(-1.5f, -3f, 0f);
        editedLumy.transform.rotation = Quaternion.Euler(0f, 90f, 90f);
    }

    /// <summary>
    /// Persist the changes on the selected lumy
    /// </summary>
    public void SaveLumy()
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
        AppContextManager.instance.SaveCast();
    }

    public void OpenSelectSwarmDialog()
    {
        RefreshView();
    }

    public void CopySwarm()
    {
        Debug.Log("CopySwarm");
    }

    public void DeleteSwarm()
    {
        Debug.Log("DeleteSwarm");
    }

    public void NewSwarm()
    {
        Debug.Log("NewSwarm");
    }

    public void OpenEditSwarmDialog()
    {
        Debug.Log("OpenEditSwarmDialog");
    }

    public void OpenImportSwarmDialog()
    {
        Debug.Log("OpenImportSwarmDialog");
    }

    public void OpenExportSwarmDialog()
    {
        Debug.Log("OpenExportSwarmDialog");
    }

    public void SelectLumy(string lumyName)
    {
        AppContextManager.instance.SwitchActiveCast(lumyName);
        RefreshView();
    }

    public void CopyLumy()
    {
        Debug.Log("CopyLumy");
    }

    public void DeleteLumy()
    {
        Debug.Log("DeleteLumy");
    }

    public void NewLumy()
    {
        Debug.Log("NewLumy");
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

    public void IncrVitality()
    {
        Debug.Log("IncrVitality");
    }

    public void DecrVitality()
    {
        Debug.Log("DecrVitality");
    }

    public void IncrStamina()
    {
        Debug.Log("IncrStamina");
    }

    public void DecrStamina()
    {
        Debug.Log("DecrStamina");
    }

    public void IncrStrength()
    {
        Debug.Log("IncrStrength");
    }

    public void DecrStrength()
    {
        Debug.Log("DecrStrength");
    }

    public void IncrActSpeed()
    {
        Debug.Log("IncrActSpeed");
    }

    public void DecrActSpeed()
    {
        Debug.Log("DecrActSpeed");
    }

    public void IncrMoveSpeed()
    {
        Debug.Log("IncrMoveSpeed");
    }

    public void DecrMoveSpeed()
    {
        Debug.Log("DecrMoveSpeed");
    }

    public void IncrVisionRange()
    {
        Debug.Log("IncrVisionRange");
    }

    public void DecrVisionRange()
    {
        Debug.Log("DecrVisionRange");
    }

    public void IncrAtkRange()
    {
        Debug.Log("IncrAtkRange");
    }

    public void DecrAtkRange()
    {
        Debug.Log("DecrAtkRange");
    }

    public void IncrPickRange()
    {
        Debug.Log("IncrPickRange");
    }

    public void DecrPickRange()
    {
        Debug.Log("DecrPickRange");
    }
}
