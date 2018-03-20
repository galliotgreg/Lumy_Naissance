using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoMC : MonoBehaviour
{

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
    private GameObject infobulleA;
    [SerializeField]
    private GameObject infobulleB;
    [SerializeField]
    private GameObject infobulleC;
    [SerializeField]
    private GameObject infobulleD;
    [SerializeField]
    private GameObject infobulleE;

    private int currentInfo = 0;

    private bool isTutoOpen;

    List<GameObject> listeInfos = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

        isTutoOpen = true;

        listeInfos.Add(infobulleA);
        listeInfos.Add(infobulleB);
        listeInfos.Add(infobulleC);
        listeInfos.Add(infobulleD);
        listeInfos.Add(infobulleE);

        Button tut = tuto.GetComponent<Button>();
        Button nxt = next.GetComponent<Button>();
        Button prev = previous.GetComponent<Button>();
        Button cls = close.GetComponent<Button>();
        /*
        if(currentInfo != 0)
        {

        }*/
        nxt.onClick.AddListener(NextInfo);
        prev.onClick.AddListener(PrevInfo);
        cls.onClick.AddListener(CloseInfo);
        tut.onClick.AddListener(OpenTuto);

    }


    // Update is called once per frame
    void Update()
    {
       
        /*
        if (currentInfo == 0)
        {
            prev.gameObject.SetActive(false);
        }
        else
        {
            prev.gameObject.SetActive(true);
        }*/

    }


    void NextInfo()
    {
        if (currentInfo != 4)
        {
            listeInfos[currentInfo].SetActive(false);
            listeInfos[currentInfo + 1].SetActive(true);
            currentInfo += 1;
           
            Debug.Log("current" + currentInfo);

        }

        else
        {
            listeInfos[currentInfo].SetActive(false);
            panelInfobulles.SetActive(false);
            isTutoOpen = false;

        }

    }

    void PrevInfo()
    {
        if (currentInfo != 0)
        {
            listeInfos[currentInfo].SetActive(false);
            listeInfos[currentInfo - 1].SetActive(true);
            currentInfo -= 1;
            Debug.Log("current" + currentInfo);
        }
        else
        {
            listeInfos[currentInfo].SetActive(false);
            panelInfobulles.SetActive(false);
            isTutoOpen = false;

        }

    }

    void CloseInfo()
    {
        listeInfos[currentInfo].SetActive(false);
        panelInfobulles.SetActive(false);
        isTutoOpen = false;

    }

    void OpenTuto()
    {
        if (isTutoOpen == false)
        {
            currentInfo = 0;
            listeInfos[currentInfo].SetActive(true);
            panelInfobulles.SetActive(true);

        }

    }
}

