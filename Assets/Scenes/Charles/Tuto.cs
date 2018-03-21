using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour {

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
    private int currentInfo =0;

    private bool isTutoOpen;

    List<GameObject> listeInfos = new List<GameObject>();

    // Use this for initialization
    void Start () {

        isTutoOpen = true;

        listeInfos.Add(infobulleA);
        listeInfos.Add(infobulleB);
        listeInfos.Add(infobulleC);

        Button tut = tuto.GetComponent<Button>();
        Button nxt = next.GetComponent<Button>();
        Button prev = previous.GetComponent<Button>();
        Button cls = close.GetComponent<Button>();

        nxt.onClick.AddListener(NextInfo);
        prev.onClick.AddListener(PrevInfo);
        cls.onClick.AddListener(CloseInfo);
        tut.onClick.AddListener(OpenTuto);


    }

    void NextInfo()
    {
        if(currentInfo != 2)
        {
            listeInfos[currentInfo].SetActive(false);
            listeInfos[currentInfo + 1].SetActive(true);
            currentInfo += 1;
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
        if(currentInfo != 0)
        {
            listeInfos[currentInfo].SetActive(false);
            listeInfos[currentInfo - 1].SetActive(true);
            currentInfo -= 1;
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

    // Update is called once per frame
    void Update () {

    }
}
