using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastesUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static CastesUIController instance = null;

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
    private GameObject swarmSelectionBtnPrefab;

    [SerializeField]
    private GameObject swarmSelectionPanel;

    [SerializeField]
    private GameObject nueePrincipalePanel;

    [SerializeField]
    private GameObject UICastPrefab;

    [SerializeField]
    private GameObject rootUICast;

    // Use this for initialization
    void Start () {
        CreateSwarmSelectionButons();
        LoadEditedLumy();
        CreateTree();
	}

    private void CreateTree()
    {
        if (rootUICast != null)
        {
            Destroy(rootUICast);
        }

        UICastPrefab.transform.position = new Vector3(0f, 295f, 0);
        UICastPrefab.transform.localScale = new Vector3(100f, 100f, 1f);
        rootUICast = Instantiate(UICastPrefab, UICastPrefab.transform.position, transform.rotation);
        rootUICast.transform.SetParent(nueePrincipalePanel.transform, false); 

        Cast origin = AppContextManager.instance.ActiveSpecie.Casts["origin"];
        RecCreateTree(origin, rootUICast);

    }

    private void RecCreateTree(Cast node, GameObject nodeUICast)
    {
        //Configure Node
        GameObject lumyBtnTxtObj = nodeUICast.transform.Find("PanelLumy/btn_Lumy/Text").gameObject;
        Text lumyBtnTxt = lumyBtnTxtObj.GetComponent<Text>();
        lumyBtnTxt.text = node.Name;

        if (node.Childs.Count == 2)
        {
            GameObject buttonObject = nodeUICast.transform.Find("PanelLumy/PanelAction/btn_Fork").gameObject;
            ForkLumy btnCallBack = buttonObject.GetComponent<ForkLumy>();
            btnCallBack.Init();
            btnCallBack.Fork();

            GameObject leftChild = nodeUICast.transform.GetChild(1).gameObject;
            GameObject rightChild = nodeUICast.transform.GetChild(2).gameObject;

            RecCreateTree(node.Childs[0], leftChild);
            RecCreateTree(node.Childs[1], rightChild);
        }
    }

    public void LoadEditedLumy()
    {
        if (LumyEditorManager.instance.EditedLumy != null)
        {
            Destroy(LumyEditorManager.instance.EditedLumy);
        }
        string castName = AppContextManager.instance.ActiveCast.Name;
        LumyEditorManager.instance.LoadLumy(castName);
    }

    private void CreateSwarmSelectionButons()
    {
        string[] speciesNames = AppContextManager.instance.GetSpeciesFolderNames();
        for (int i=0; i < speciesNames.Length; i++)
        {
            GameObject swarmSelectionButton = Instantiate(swarmSelectionBtnPrefab);
            Button button = swarmSelectionButton.GetComponent<Button>();
            Text text = swarmSelectionButton.GetComponentInChildren<Text>();
            RectTransform rectTransform = swarmSelectionButton.GetComponent<RectTransform>();
            swarmSelectionButton.transform.parent = swarmSelectionPanel.transform;

            //Set Text
            text.text = speciesNames[i];

            //Set Position
            rectTransform.localPosition = new Vector3(
                -300f + i * (rectTransform.rect.width + 20f),
                25f,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            button.onClick.AddListener(delegate { SelectActiveSwarm(text.text); });
        }
    }

    private void SelectActiveSwarm(string swarmName)
    {
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        LoadEditedLumy();
        CreateTree();
    }

    // Update is called once per frame
    void Update () {
		
	}


}
