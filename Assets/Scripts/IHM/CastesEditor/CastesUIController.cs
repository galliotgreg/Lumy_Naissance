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

    void Update()
    {
    }

    [SerializeField]
    private float TREE_H_MARGIN = 4;
    [SerializeField]
    private float TREE_V_MARGIN = 2;

    [SerializeField]
    private GameObject swarmSelectionBtnPrefab;

    [SerializeField]
    private GameObject swarmSelectionPanel;

    [SerializeField]
    private GameObject nueePrincipalePanel;

    [SerializeField]
    private GameObject UICastPrefab;

    [SerializeField]
    private GameObject UICastPlaceHoldePrefab;

    [SerializeField]
    private GameObject rootUICast;

    private IList<Cast> castLeaves;

    private IList<GameObject> UICasts;

    // Use this for initialization
    void Start () {
        CreateSwarmSelectionButons();
        LoadEditedLumy();
        CreateTree();
	}

    public void CreateTree()
    {
        if (rootUICast != null)
        {
            Destroy(rootUICast);
        }
        castLeaves = new List<Cast>();
        UICasts = new List<GameObject>();

        UICastPrefab.transform.position = new Vector3(0f, 295f, 0);
        UICastPrefab.transform.localScale = new Vector3(100f, 100f, 1f);
        rootUICast = Instantiate(UICastPrefab, UICastPrefab.transform.position, transform.rotation);
        rootUICast.transform.SetParent(nueePrincipalePanel.transform, false); 

        Cast origin = AppContextManager.instance.ActiveSpecie.Casts["origin"];
        RecCreateTree(origin, rootUICast);
        LayoutTree(rootUICast);
    }

    private void RecCreateTree(Cast node, GameObject nodeUICast)
    {
        //Register node
        UICasts.Add(nodeUICast);

        //Configure Node
        GameObject lumyBtnTxtObj = nodeUICast.transform.Find("PanelLumy/btn_Lumy/Text").gameObject;
        Text lumyBtnTxt = lumyBtnTxtObj.GetComponent<Text>();
        lumyBtnTxt.text = node.Name;

        if (node.Childs.Count == 2)
        {
            GameObject buttonObject = nodeUICast.transform.Find("PanelLumy/PanelAction/btn_Fork").gameObject;
            ForkLumy btnCallBack = buttonObject.GetComponent<ForkLumy>();
            btnCallBack.Init();
            btnCallBack.UIFork();

            GameObject leftChild = nodeUICast.transform.GetChild(1).gameObject;
            GameObject rightChild = nodeUICast.transform.GetChild(2).gameObject;

            RecCreateTree(node.Childs[0], leftChild);
            RecCreateTree(node.Childs[1], rightChild);
        } else
        {
            castLeaves.Add(node);
        }
    }

    private void LayoutTree(GameObject rootUICast)
    {
        //Compute tree height
        int treeHeight = 0;
        foreach (Cast cast in castLeaves)
        {
            int curHeight = ComputeLeaveHeight(cast);
            if (curHeight > treeHeight)
            {
                treeHeight = curHeight;
            }
        }

        //Fill tree step tables
        IList<IList<GameObject>> treeSteps = new List<IList<GameObject>>();
        for (int i = 0; i < treeHeight; i++)
        {
            treeSteps.Add(new List<GameObject>());
        }
        foreach (GameObject UICast in UICasts)
        {
            string key = GetCastKeyFromUICast(UICast);
            Cast curCast = AppContextManager.instance.ActiveSpecie.Casts[key];
            treeSteps[ComputeLeaveHeight(curCast) - 1].Add(UICast);
        }

        //Layout
        for (int step = 0; step < treeSteps.Count; step++)
        {
            for (int offset = 0; offset < treeSteps[step].Count; offset++)
            {
                GameObject UICast = treeSteps[step][offset];
                float stepLenght = (treeSteps[step].Count - 1) * TREE_H_MARGIN;
                float leftBound = -stepLenght / 2f;
                float x = leftBound + offset * TREE_H_MARGIN;
                float y = 295 - step * TREE_V_MARGIN;
                UICast.transform.position = new Vector3(x, y, 0f);
            }
        }
        rootUICast.transform.localPosition = new Vector3(734, -132, 0);
    }

    private static string GetCastKeyFromUICast(GameObject UICast)
    {
        GameObject lumyBtnTxtObj = UICast.transform.Find("PanelLumy/btn_Lumy/Text").gameObject;
        Text lumyBtnTxt = lumyBtnTxtObj.GetComponent<Text>();
        string key = lumyBtnTxt.text;
        return key;
    }

    private int ComputeLeaveHeight(Cast cast)
    {
        int height = 1;
        while (cast.Parent != null)
        {
            height++;
            cast = cast.Parent;
        }
        return height;
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

    public void CreateSwarmSelectionButons()
    {

        string[] speciesNames = AppContextManager.instance.GetSpeciesFolderNames();

        // Set ScrollRect sizes
        RectTransform rec = swarmSelectionPanel.transform.GetComponent<RectTransform>();
        rec.sizeDelta = new Vector2(rec.sizeDelta.x, speciesNames.Length * (swarmSelectionBtnPrefab.GetComponent<RectTransform>().sizeDelta.y + 20f) + 20f);

        foreach (Transform child in swarmSelectionPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i=0; i < speciesNames.Length; i++)
        {
            GameObject swarmSelectionButton = Instantiate(swarmSelectionBtnPrefab);
            Button button = swarmSelectionButton.GetComponent<Button>();
            Text text = swarmSelectionButton.GetComponentInChildren<Text>();
            RectTransform rectTransform = swarmSelectionButton.GetComponent<RectTransform>();
            swarmSelectionButton.transform.SetParent(swarmSelectionPanel.transform);

            //Set Text
            text.text = speciesNames[i];

            //Set Position
            rectTransform.localPosition = new Vector3(
                0,
                -i * (rectTransform.rect.height + 20f) - 20f,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            button.onClick.AddListener(delegate { SelectActiveSwarm(text.text); });
        }

    }

    public void SelectActiveSwarm(string swarmName)
    {
        AppContextManager.instance.SwitchActiveSpecie(swarmName);
        LoadEditedLumy();
        CreateTree();
    }
}
