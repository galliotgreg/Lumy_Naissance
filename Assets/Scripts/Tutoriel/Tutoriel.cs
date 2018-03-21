using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutoriel : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button previous;
    [SerializeField]
    private Button close;
    [SerializeField]
    private Button tuto;
    [SerializeField]
    private GameObject panelInfobulles;
    [SerializeField]
    private Text txt_currentInfo;
    [SerializeField]
    private List<GameObject> panelList = new List<GameObject>();

    private int currentInfo = 0;

    private bool isTutoOpen;

    #endregion

    // Use this for initialization
    void Start()
    {
        
        //Check is all panel are not visible
        foreach (GameObject info in panelList)
        {
            info.SetActive(false);
            //info.transform.position = panelList[0].transform.position;
        }

        ButtonListener(); 

        //Enable Tuto 
        //TODO Implement PlayerPrefs
        OpenTuto(); 
    }

    private void ButtonListener()
    {
        //Listener
        next.GetComponent<Button>().onClick.AddListener(Next);
        previous.GetComponent<Button>().onClick.AddListener(Prev);
        close.GetComponent<Button>().onClick.AddListener(Close);
        tuto.GetComponent<Button>().onClick.AddListener(OpenTuto);
    }

    #region NavigationTuto
    void Next()
    {
        if (currentInfo < panelList.Count -1)
        {
            panelList[currentInfo].SetActive(false);
            panelList[currentInfo + 1].SetActive(true);
            currentInfo += 1;
            txt_currentInfo.text = (currentInfo + 1).ToString() + " / " + panelList.Count.ToString();
        }

        else
        {
            panelList[currentInfo].SetActive(false);
            panelInfobulles.SetActive(false);
            isTutoOpen = false; 
        }

    }

    void Prev()
    {
        if (currentInfo > 0)
        {
            panelList[currentInfo].SetActive(false);
            panelList[currentInfo - 1].SetActive(true);
            currentInfo -= 1;
            txt_currentInfo.text = (currentInfo + 1).ToString() + " / " + panelList.Count.ToString();
        }
        else
        {
            panelList[currentInfo].SetActive(false);
            panelInfobulles.SetActive(false);
            isTutoOpen = false;
        }

    }

    void Close()
    {
        panelList[currentInfo].SetActive(false);
        panelInfobulles.SetActive(false);
        
        isTutoOpen = false;

    }

    void OpenTuto()
    {
        
        currentInfo = 0;
        panelList[currentInfo].SetActive(true);
        panelInfobulles.SetActive(true);
        txt_currentInfo.text = (currentInfo + 1).ToString() + " / " + panelList.Count.ToString();

    }
#endregion
}

