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

    // Use this for initialization
    void Start () {
        CreateSwarmSelectionButons();
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
    }

    // Update is called once per frame
    void Update () {
		
	}


}
