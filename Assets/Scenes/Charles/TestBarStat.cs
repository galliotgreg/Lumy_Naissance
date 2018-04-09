using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBarStat : MonoBehaviour {
    [SerializeField]
    private Button btn_plus;
    [SerializeField]
    private Button btn_moins;
    [SerializeField]
    private GameObject statBarPrefab;
    [SerializeField]
    private GameObject bckStatBarPrefab;
    [SerializeField]
    private float statBarHSpacing;
    [SerializeField]
    private float statBarVertSpacing;

    private List<GameObject> bckBarStatsList;
    private List<GameObject> barStatsList;

    private int compteur = 0;
    // Use this for initialization
    void Start () {
        PlaceBars();
        btn_plus.onClick.AddListener(Add);
        btn_moins.onClick.AddListener(Sub);
	}

    private void PlaceBars()
    {
        bckBarStatsList = new List<GameObject>();
        barStatsList = new List<GameObject>();

        float barWidth = statBarPrefab.GetComponent<RectTransform>().rect.width;
        float barHeight = statBarPrefab.GetComponent<RectTransform>().rect.height;

        //Instantiate 4 rows
        for (int i =0; i < 4; i++)
        {   //Instantiate 1 Prefab
            for (int j = 0; j < 3; j++)
            {
                GameObject bckStatBar = Instantiate(bckStatBarPrefab, new Vector3(j * (barWidth + statBarHSpacing), i* statBarVertSpacing, 0f), Quaternion.identity);
                bckStatBar.transform.SetParent(this.gameObject.transform, false);
                bckBarStatsList.Add(bckStatBar);

                GameObject statBar = Instantiate(statBarPrefab, new Vector3(j * (barWidth + statBarHSpacing), i* (barHeight + statBarVertSpacing), 0f), Quaternion.identity);
                statBar.transform.SetParent(this.gameObject.transform, false);
                barStatsList.Add(statBar);
            }
        }
       
        //Clear View
        foreach (GameObject statBar in barStatsList)
        {
            statBar.SetActive(false);
        }

    }

    private void Add()
    {
        compteur += 1;
        RefreshBars();  
    }

    private void RefreshBars()
    {
        foreach (GameObject statBar in barStatsList)
        {
            statBar.SetActive(false);
        }

        switch (compteur)
        {
            case 1:
                barStatsList[0].SetActive(true);
                break;
            case 2:
                barStatsList[0].SetActive(true);
                barStatsList[1].SetActive(true);
                break;
            case 3:
                barStatsList[0].SetActive(true);
                barStatsList[1].SetActive(true);
                barStatsList[2].SetActive(true);
                break;
            default:
                break;
        }
    }

    private void Sub()
    {
       compteur -=1;
       RefreshBars();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
