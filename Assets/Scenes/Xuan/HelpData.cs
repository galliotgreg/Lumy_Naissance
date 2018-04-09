using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class HelpData : MonoBehaviour{
    public Help help;
   // public HelpDatabase helps = new HelpDatabase();
   /*
    private Tooltip tooltip;
    private Vector2 offset;
	// Use this for initialization
	void Start () {
        /*ListHelp = GameObject.Find("ListHelp").GetComponent<ListHelp>();
        tooltip = ListHelp.GetComponent<Tooltip>();
        //helps.LoadDatabase();
        //help = helps.FetchHelpByID(1);
        GameObject.Find("ListHelp").GetComponent<ListHelp>().LoadDatabase();
        help = GameObject.Find("ListHelp").GetComponent<ListHelp>().FetchHelpByID(1);
        Debug.Log(help);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        if (help != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(help);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }*/
}
