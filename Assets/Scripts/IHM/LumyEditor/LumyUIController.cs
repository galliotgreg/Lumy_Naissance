using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumyUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static LumyUIController instance = null;

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
    private GameObject libraryCompPrefab;
    [SerializeField]
    private GameObject voletGauche;

    // Use this for initialization
    void Start () {
        if (LumyEditorManager.instance.EditedLumy != null)
        {
            LumyEditorManager.instance.EditedLumy.SetActive(true);
        }
        RetreiveData();
    }

    private void RetreiveData()
    {
        int nbColumns = 4;
        IList<ComponentInfo> compos = LumyEditorManager.instance.CompoLibrary;
        for (int i = 0; i < compos.Count; i++)
        {
            //Instanciate Compo button
            GameObject libComp = Instantiate(libraryCompPrefab);
            LibraryCompoData compoData = libComp.GetComponent<LibraryCompoData>();
            compoData.ComponentInfo = compos[i];

            //Set position
            int x = i % nbColumns;
            int y = i / nbColumns;
            RectTransform rectTransform = libComp.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(-160f + x * 110f, 380f - y * 140f, 0f);
            libComp.transform.SetParent(voletGauche.transform, false);

            //Congigure Button
            Text text = libComp.transform.Find("Text").gameObject.GetComponent<Text>();
            text.text = compoData.ComponentInfo.Name;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnDestroy()
    {
        if (LumyEditorManager.instance.EditedLumy != null)
        {
            LumyEditorManager.instance.EditedLumy.SetActive(false);
        }
    }
}
