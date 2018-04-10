using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenClosePanel : MonoBehaviour
{

    public GameObject panelInfo;
    public Button btn_panelInfo;

    bool isOpen = true;

    public Vector2 closed = new Vector2(0f, 0f);
    public Vector2 open = new Vector2(-300f, 0f);


    // Use this for initialization
    void Start()
    {
        isOpen = panelInfo.activeSelf;
        btn_panelInfo.onClick.AddListener(OpenClose);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Close()
    {
        panelInfo.SetActive(false);
        isOpen = !isOpen;
    }

    void Open()
    {
        panelInfo.SetActive(true);
        isOpen = !isOpen;
    }

   public void OpenClose()
    {
        if (isOpen)
        {
            Close();
            btn_panelInfo.GetComponent<RectTransform>().localPosition = closed;
        }
        else
        {
            Open();
            btn_panelInfo.GetComponent<RectTransform>().localPosition = open;
        }
    }


}
