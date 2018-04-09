using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance = null;
    public HelpDatabase help;

    [SerializeField]
    string JSON_name = "";


    [Header("Explanation Panel")]
    [SerializeField]
    private Text mainPanelHelpTitle;
    [SerializeField]
    private Text mainPanelHelpContent;
    [SerializeField]
    private Image mainPanelHelpImage;

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
            AutomaticSelectHelp();
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void RefreshHelpScroll()
    {
        //Remove old buttons
        help = HelpManager.instance.help;
        IList<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < helpScrollContent.transform.childCount; i++)
        {
            childs.Add(helpScrollContent.transform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }

        string[] listNames = HelpManager.instance.GetListHelpTitle();

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
                10f,
                -i * (rectTransform.rect.height /3f) - 15f,
                0f);
            //rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            button.onClick.AddListener(delegate { SelectHelp(button_text.text); });
        }
    }

    public void SelectHelp(string title)
    {
        mainPanelHelpTitle.text = HelpManager.instance.help.FetchHelpByTitle(title).Title;
        mainPanelHelpContent.text = HelpManager.instance.help.FetchHelpByTitle(title).GetContentText();

        string imagename = HelpManager.instance.help.FetchHelpByTitle(title).Image;
        if (imagename != "")
        {
            //TODO Format image voir plus tard.
            mainPanelHelpImage.gameObject.SetActive(true);
            byte[] bytes = File.ReadAllBytes(Application.dataPath + @"/Inputs/HelpFiles/Resources/" + imagename);
            mainPanelHelpImage.sprite.texture.LoadImage(bytes);
        }
        else
        {
            mainPanelHelpImage.gameObject.SetActive(false);
        }
    }
    public void AutomaticSelectionHelp()
    {

        mainPanelHelpTitle.text = HelpManager.instance.help.GetFirstfromList().Title;
        mainPanelHelpContent.text = "lol";//HelpManager.instance.help.FetchHelpByTitle(title).Content;
        string imagename = HelpManager.instance.help.GetFirstfromList().Image;
        if (imagename != "")
        {
            //TODO Format image voir plus tard.
            mainPanelHelpImage.gameObject.SetActive(true);
            byte[] bytes = File.ReadAllBytes(Application.dataPath + @"/Inputs/HelpFiles/Resources/" + imagename);
            mainPanelHelpImage.sprite.texture.LoadImage(bytes);
        }
        else
        {
            mainPanelHelpImage.gameObject.SetActive(false);
        }
    }

    public void AutomaticSelectHelp()
    {

        mainPanelHelpTitle.text = HelpManager.instance.help.GetFirstfromList().Title;
        mainPanelHelpContent.text = HelpManager.instance.help.GetFirstfromList().GetContentText();
        string imagename = HelpManager.instance.help.GetFirstfromList().Image;
        if (imagename != "")
        {
            //TODO Format image voir plus tard.
            mainPanelHelpImage.gameObject.SetActive(true);
            byte[] bytes = File.ReadAllBytes(Application.dataPath + @"/Inputs/HelpFiles/Resources/" + imagename);
            mainPanelHelpImage.sprite.texture.LoadImage(bytes);
        }
        else
        {
            mainPanelHelpImage.gameObject.SetActive(false);
        }
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
