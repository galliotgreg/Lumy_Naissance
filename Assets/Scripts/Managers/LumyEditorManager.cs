using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumyEditorManager : MonoBehaviour {
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
    private ComponentInfo selectedLumyCompo;
    private ComponentInfo hoveredLumyCompo;
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

    public ComponentInfo SelectedLumyCompo
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

    public ComponentInfo HoveredLumyCompo
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
        Debug.Log("Update");
    }

    /// <summary>
    /// Push the composant on top of the selected lumy's head
    /// </summary>
    /// <param name="compoId">The id of the component on the component referencial</param>
    public void PushHead(int compoId)
    {
        Debug.Log("TODO : implement LumyEditorManager.PushHead");
    }

    /// <summary>
    /// Remove the component at the given position from the selected lumy's head
    /// </summary>
    /// <param name="compoRank">The position of the component to remove</param>
    public void RemoveHead(int compoRank)
    {
        Debug.Log("TODO : implement LumyEditorManager.RemoveHead");
    }

    /// <summary>
    /// Push the composant on top of the selected lumy's Tail
    /// </summary>
    /// <param name="compoId">The id of the component on the component referencial</param>
    public void PushTail(int compoId)
    {
        Debug.Log("TODO : implement LumyEditorManager.PushTail");
    }

    /// <summary>
    /// Remove the component at the given position from the selected lumy's Tail
    /// </summary>
    /// <param name="compoRank">The position of the component to remove</param>
    public void RemoveTail(int compoRank)
    {
        Debug.Log("TODO : implement LumyEditorManager.RemoveTail");
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
    /// Set the selectedLumyCompo
    /// </summary>
    /// <param name="compoRank">The position on the lumy specified part</param>
    /// <param name="part">The part from wich you want to select a component </param>
    public void SelectLumyCompo(int compoRank, Part part)
    {
        Debug.Log("TODO : implement LumyEditorManager.SelectLumyCompo");
    }

    /// <summary>
    /// Set the hoveredLumyCompo
    /// </summary>
    /// <param name="compoRank">The position on the lumy specified part</param>
    /// <param name="part">The part from wich you want to select a component </param>
    public void HoverLumyCompo(int compoRank, Part part)
    {
        Debug.Log("TODO : implement LumyEditorManager.HoverLumyCompo");
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
        editedLumy.SetActive(true);
        UnitTemplateInitializer.InitTemplate(
            lumyCast, editedLumy, emptyComponentPrefab);

        //Disable InGame Logics
        AgentBehavior agentBehavior = editedLumy.GetComponent<AgentBehavior>();
        agentBehavior.enabled = false;
        AgentContext agentContext = editedLumy.GetComponent<AgentContext>();
        agentContext.enabled = false;
        AgentEntity agentEntity = editedLumy.GetComponent<AgentEntity>();
        agentEntity.enabled = false;
    }

    /// <summary>
    /// Persist the changes on the selected lumy
    /// </summary>
    public void SaveLumy()
    {
        Debug.Log("TODO : implement LumyEditorManager.SaveLumy");
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
