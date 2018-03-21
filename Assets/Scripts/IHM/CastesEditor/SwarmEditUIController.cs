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

    //Max number of points per Lumy attribute
    private int statLimit = 3;

    /// <summary>
    /// Active Lumy Stats
    /// </summary>
    private LumyStatsInfo lumyStats = new LumyStatsInfo();

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
    /// Instancied prefab for each lumys on the lumy scroll content
    /// </summary>
    [SerializeField]
    private GameObject lumyButtonPrefab;

    /// <summary>
    /// Set the margin beetween each lumy select button
    /// </summary>
    [SerializeField]
    private float lumyYMarginLayout = 35f;

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

    // Use this for initialization
    void Start()
    {
        RefreshView();
    }

    private void RefreshView()
    {
        RefreashSwarmScroll();
        RefreashLumysScroll();
        RefreshLumyAppearenceFromData();
        RefreshLumyInfo();
        RefreashLumyStats();
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
            vitality += (int) compo.VitalityBuff;
            stamina += (int)compo.StaminaBuff;
            strength += (int)compo.StrengthBuff;
            actSpeed += (int)compo.ActionSpeedBuff;
            moveSpeed += (int)compo.MoveSpeedBuff;
            visionRange += (int)compo.VisionRangeBuff;
            //TODO manage atkRange & pick range
            pickRange = -1;
            atkRange = -1;
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
        prodTime = GetProdTime();
        redCost = GetRedCost();
        greenCost = GetGreenCost();
        blueCost = GetBlueCost();
    }

    private int GetBlueCost()
    {
        return CostManager.instance.ComputeBlueCost(editedLumy);
    }

    private int GetGreenCost()
    {
        return CostManager.instance.ComputeGreenCost(editedLumy);
    }

    private int GetRedCost()
    {
        return CostManager.instance.ComputeRedCost(editedLumy);
    }

    private float GetProdTime()
    {
        AgentContext agentContext = editedLumy.GetComponent<AgentContext>();
        AgentComponent[] agentComponent = agentContext.Entity.getAgentComponents();
        return CostManager.instance.ComputeProdTime(agentComponent);
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

    private void RefreshLumyAppearenceFromStats()
    {
        //Destroy last Lumy
        if (editedLumy != null)
        {
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
        
        Debug.Log("CopySwarm");
    }

    public void DeleteSwarm()
    {

        Debug.Log("DeleteSwarm");
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
        Debug.Log("OpenEditSwarmDialog");
    }

    public void OpenImportSwarmDialog()
    {
        ImportController.ImportSpecie();
        Debug.Log("OpenImportSwarmDialog");
    }

    public void OpenExportSwarmDialog()
    {
        ExportController.ExportSpecie();
        Debug.Log("OpenExportSwarmDialog");
    }

    public void SelectLumy(string lumyName)
    {
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

    private void PersistStatChange()
    {
        RefreshLumyAppearenceFromStats();
        SaveLumy();
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
