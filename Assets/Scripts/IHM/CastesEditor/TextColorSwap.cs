using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TextColorSwap : EventTrigger {

    private Text text;

    private void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        text.color = new Color32 (150,150,150,255);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        text.color = new Color32(255, 255, 255, 255);
    }

}
