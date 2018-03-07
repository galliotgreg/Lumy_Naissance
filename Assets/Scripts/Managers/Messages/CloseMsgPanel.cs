using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMsgPanel : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClosePanel);
    }

    void ClosePanel()
    {
        MessagesManager.instance.CloseMsgPanel();
    }
}
