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
    [SerializeField]
    private string keyTuto;
    [SerializeField]
    private Toggle dontShowAgain;

    private int currentInfo = 0;

    private bool isTutoOpen;

    #endregion

    // Use this for initialization
    void Start()
    {
         //Set key to initial value (for test)
         //SwapManager.instance.SetTutorielKey(false, keyTuto);
         Debug.Log(SwapManager.instance.getTutorielState(keyTuto));

        //Check if all panel are not visible
        panelInfobulles.SetActive(false);
        foreach (GameObject info in panelList)
        {
            info.SetActive(false);
           
        }

        ButtonListener(); 

        if(keyTuto.Length == 0)
        {
            Debug.LogError("Error : KeyTuto Not Initialized");
            return; 
        } 
        //Enable Tuto 
        if (!SwapManager.instance.getTutorielState(keyTuto)) {

            OpenTuto();
            Debug.Log("ok");
        }
  
    }

    /// <summary>
    /// Create all button listener for the GameObject 
    /// </summary>
    private void ButtonListener()
    {
        //Listener
        CheckButton();
        next.onClick.AddListener(Next);
        previous.onClick.AddListener(Prev);
        close.onClick.AddListener(Close);
        dontShowAgain.onValueChanged.AddListener(delegate{ToggleKey(dontShowAgain);});
        tuto.onClick.AddListener(OpenTuto);
   
    }
  

    private void CheckButton()
    {
        if (next == null)
        {
            Debug.LogError("NextButton not initialized"); 
        }
        if (previous == null)
        {
            Debug.LogError("previousButton not initialized");
        }
        if (close == null)
        {
            Debug.LogError("closeButton not initialized");
        }
        if (tuto == null)
        {
            Debug.LogError("TutoButton not initialized");
        }
       
    }

    /// <summary>
    /// Toggle keyTuto Value
    /// </summary>
    private void ToggleKey(Toggle dontShowAgain)
    {
        SwapManager.instance.SetTutorielKey(!SwapManager.instance.getTutorielState(keyTuto), keyTuto);

    }


    #region NavigationTuto
    void Next()
    {
        previous.GetComponent<Button>().gameObject.SetActive(true);

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
            if(currentInfo == 0)
            {
                previous.GetComponent<Button>().gameObject.SetActive(false);
            }
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

    public void OpenTuto()
    {
        panelInfobulles.SetActive(true);
        previous.GetComponent<Button>().gameObject.SetActive(false);
        currentInfo = 0;
        panelList[currentInfo].SetActive(true);    
        txt_currentInfo.text = (currentInfo + 1).ToString() + " / " + panelList.Count.ToString();

    }
#endregion

}

