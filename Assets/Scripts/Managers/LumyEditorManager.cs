using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumyEditorManager : MonoBehaviour
{
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static LumyEditorManager instance = null;

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

    public enum Part
    {
        Head,
        Tail
    }

    [SerializeField]
    private GameObject editedLumy;
    private ComponentInfo selectedLibraryCompo;
    [SerializeField]
    private AgentComponent selectedLumyCompo;
    [SerializeField]
    private AgentComponent hoveredLumyCompo;
    private IList<ComponentInfo> compoLibrary;

    [SerializeField]
    private GameObject emptyAgentPrefab;
    [SerializeField]
    private GameObject emptyComponentPrefab;

    public GameObject EditedLumy
    {
        get
        {
            return editedLumy;
        }

        set
        {
            editedLumy = value;
        }
    }

    public ComponentInfo SelectedLibraryCompo
    {
        get
        {
            return selectedLibraryCompo;
        }

        set
        {
            selectedLibraryCompo = value;
        }
    }

    public AgentComponent SelectedLumyCompo
    {
        get
        {
            return selectedLumyCompo;
        }

        set
        {
            selectedLumyCompo = value;
        }
    }

    public AgentComponent HoveredLumyCompo
    {
        get
        {
            return hoveredLumyCompo;
        }

        set
        {
            hoveredLumyCompo = value;
        }
    }

    public IList<ComponentInfo> CompoLibrary
    {
        get
        {
            return compoLibrary;
        }

        set
        {
            compoLibrary = value;
        }
    }

    void Start()
    {
        LoadLibrary();
    }

    void Update()
    {
        hoveredLumyCompo = FindCompoOnCursor();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            selectedLumyCompo = FindCompoOnCursor();
        }
        if (Input.GetKeyUp(KeyCode.Delete))
        {
            RemoveSelected();
        }
    }

    private AgentComponent FindCompoOnCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject pickedObject = hitInfo.collider.gameObject;
            AgentComponent agentComponent = pickedObject.GetComponent<AgentComponent>();
            if (agentComponent != null)
            {
                return agentComponent;
            }
        }

        return null;
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
    }

    /// <summary>
    /// Remove the selected Component
    /// </summary>
    public void RemoveSelected()
    {
        if (selectedLumyCompo == null)
        {
            Debug.Log("No component selected");
            return;
        }

        //Destroy compo then rebuild bones
        Destroy(selectedLumyCompo.gameObject);
        selectedLumyCompo.gameObject.transform.parent = null;
        selectedLumyCompo = null;
        GameObject skeleton = editedLumy.transform.Find("Skeleton").gameObject;
        PhySkeleton skeletonScript = skeleton.GetComponent<PhySkeleton>();
        skeletonScript.BuildSkeleton();
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

        //Add Component
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
    }

    /// <summary>
    /// Set the selectedLibraryCompo
    /// </summary>
    /// <param name="compoRank">The position on the component Library</param>
    public void SelectLibraryCompo(int compoRank)
    {
        Debug.Log("TODO : implement LumyEditorManager.SelectLibraryCompo");
    }

    /// <summary>
    /// Load the selected lumy given its cast name
    /// </summary>
    /// <param name=""></param>
    public void LoadLumy(string castName)
    {
        //Instanciate
        Cast lumyCast = AppContextManager.instance.ActiveCast;
        editedLumy = Instantiate(emptyAgentPrefab);
        editedLumy.SetActive(false);
        UnitTemplateInitializer.InitTemplate(
            lumyCast, editedLumy, emptyComponentPrefab);

        //Disable InGame Logics
        AgentBehavior agentBehavior = editedLumy.GetComponent<AgentBehavior>();
        agentBehavior.enabled = false;
        AgentContext agentContext = editedLumy.GetComponent<AgentContext>();
        agentContext.enabled = false;
        AgentEntity agentEntity = editedLumy.GetComponent<AgentEntity>();
        agentEntity.enabled = false;

        //Set lumy to Forward Kinematic
        GameObject skeleton = editedLumy.transform.Find("Skeleton").gameObject;
        PhySkeleton skeletonScript = skeleton.GetComponent<PhySkeleton>();
        skeletonScript.IsIK = false;
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
    /// Load Components list frome Component Factory
    /// Filters can be applied here.
    /// </summary>
    public void LoadLibrary()
    {
        compoLibrary = new List<ComponentInfo>();
        int i = 1;
        ComponentInfo component = null;
        do
        {
            component = ComponentFactory.instance.CreateComponent(i++);
            if (component != null)
            {
                compoLibrary.Add(component);
            }
        } while (component != null);
    }
}
