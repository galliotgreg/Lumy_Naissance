using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static HelpManager instance = null;
    public HelpDatabase help;

    [SerializeField]
    string JSON_name = "parametres";


    [Header("Explanation Panel")]
    [SerializeField]
    private Text mainPanelHelpTitle;
    [SerializeField]
    private Text mainPanelHelpContent;

    /// <summary>
    /// The help scroll selection content
    /// </summary>
    [Header("Help List Panel")]
    [SerializeField]
    private GameObject helpScrollContent;

    /// <summary>
    /// The btn prefab for help selection 
    /// </summary>
    [SerializeField]
    private GameObject helpSelectionBtnPrefab;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
            help = new HelpDatabase();
            help.LoadDatabase(JSON_name);
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public string JSON_Name
    {
        get
        {
            return JSON_name;
        }

        set
        {
            JSON_name = value;
        }
    }

    public void UpdatePanel(string JSON_name)
    {
        this.JSON_name = JSON_name;
        help.LoadDatabase(JSON_name);
    }


    private string[] GetListHelpTitle()
    {
        string[] listTitle = new string[help.GetLength()];
        for(int i = 0; i< help.GetLength();i++)
        {
            listTitle[i] = help.FetchHelpByID(i).Title;
        }
        return listTitle;
    }

    public void RefreshHelpScroll()
    {
        //Remove old buttons
        IList<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < helpScrollContent.transform.childCount; i++)
        {
            childs.Add(helpScrollContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }

        string[] listNames = GetListHelpTitle();

        // Set ScrollRect sizes
        RectTransform rec = helpScrollContent.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, listNames.Length * (helpSelectionBtnPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) + 20f);

        for (int i = 0; i < listNames.Length; i++)
        {
            GameObject helpSelectionButton = Instantiate(helpSelectionBtnPrefab);
            Button button = helpSelectionButton.GetComponent<Button>();
            Text button_text = helpSelectionButton.GetComponentInChildren<Text>();

            //Text button_text = helpSelectionButton.transform.Find("Text").GetComponent<Text>();
            RectTransform rectTransform = helpSelectionButton.GetComponent<RectTransform>();
            helpSelectionButton.transform.SetParent(helpScrollContent.transform);
            
            //Set Text
            button_text.text = listNames[i];
            //Set Position
            rectTransform.localPosition = new Vector3(
                30f,
                -i * (rectTransform.rect.height + 10f) - 10f,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            button.onClick.AddListener(delegate { SelectHelp(button_text.text); });
        }
    }

    public void SelectHelp(string title)
    {
        //AppContextManager.instance.SwitchActiveSpecie(swarmName);
        mainPanelHelpTitle.text = help.FetchHelpByTitle(title).Title;
        mainPanelHelpContent.text = help.FetchHelpByTitle(title).Content;
    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
