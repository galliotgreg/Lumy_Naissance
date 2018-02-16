using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartiePersoUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static PartiePersoUIController instance = null;

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
    private string player1SpecieName;
    [SerializeField]
    private string player2SpecieName;

    [SerializeField]
    private GameObject swarmSelectionBtnPrefab;

    [SerializeField]
    private GameObject player1SwarmSelectionPanel;

    [SerializeField]
    private GameObject player2SwarmSelectionPanel;

    // Use this for initialization
    void Start () {
        CreateP1SwarmSelectionButons();
        CreateP2SwarmSelectionButons();
    }

    private void CreateP1SwarmSelectionButons()
    {
        CreateSwarmSelectionButons(0);
    }

    private void CreateP2SwarmSelectionButons()
    {
        CreateSwarmSelectionButons(1);
    }

    private void CreateSwarmSelectionButons(int playerId)
    {
        string[] speciesNames = AppContextManager.instance.GetSpeciesFolderNames();
        for (int i = 0; i < speciesNames.Length; i++)
        {
            GameObject swarmSelectionButton = Instantiate(swarmSelectionBtnPrefab);
            Button button = swarmSelectionButton.GetComponent<Button>();
            Text text = swarmSelectionButton.GetComponentInChildren<Text>();
            RectTransform rectTransform = swarmSelectionButton.GetComponent<RectTransform>();
            if (playerId == 0)
            {
                swarmSelectionButton.transform.parent = player1SwarmSelectionPanel.transform;
            } else
            {
                swarmSelectionButton.transform.parent = player2SwarmSelectionPanel.transform;
            }

            //Set Text
            text.text = speciesNames[i];

            //Set Position
            rectTransform.localPosition = new Vector3(
                -275 + i * (rectTransform.rect.width + 20f),
                0,
                0f);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            //Set Callback
            if (playerId == 0)
            {
                button.onClick.AddListener(delegate { SelectP1ActiveSwarm(text.text); });
            } else
            {
                button.onClick.AddListener(delegate { SelectP2ActiveSwarm(text.text); });
            }
        }
    }

    public void LaunchGame()
    {
        //Load Species
        AppContextManager.instance.LoadPlayerSpecies(player1SpecieName, player2SpecieName);

        //Launch
        NavigationManager.instance.SwapScenes("MapPersonnalise", Vector3.zero);
    }

    private void SelectP1ActiveSwarm(string swarmName)
    {
        player1SpecieName = swarmName;
    }

    private void SelectP2ActiveSwarm(string swarmName)
    {
        player2SpecieName = swarmName;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
