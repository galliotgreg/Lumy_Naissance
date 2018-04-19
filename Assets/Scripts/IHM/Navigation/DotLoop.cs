using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotLoop : MonoBehaviour {
    
    bool isDots1 = true;
    bool isDots2 = false;
    bool isDots3 = false;

    string loadingString = "Chargement";

    IEnumerator loop;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallDotLoop(string loadText)
    {
        loadingString = loadText;
        loop = LoopDotsCo();
        StartCoroutine(loop);
    }

    public void StopDotLoop()
    {
        StopCoroutine(loop);
        this.gameObject.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        this.gameObject.GetComponent<Text>().text = "Chargement...";
    }

    IEnumerator LoopDotsCo()
    {
        while (true)
        {
            this.gameObject.GetComponent<Text>().text = loadingString + dotLoadingText();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private string dotLoadingText()
    {
        string dots1 = ".<color=grey>..</color>";
        string dots2 = "<color=grey>.</color>.<color=grey>.</color>";
        string dots3 = "<color=grey>..</color>.";

        if (isDots1)
        {
            isDots1 = false;
            isDots2 = true;
            isDots3 = false;

            return dots1;
        }
        else if (isDots2)
        {
            isDots1 = false;
            isDots2 = false;
            isDots3 = true;

            return dots2;
        }
        else if (isDots3)
        {
            isDots1 = true;
            isDots2 = false;
            isDots3 = false;

            return dots3;
        }
        else
        {
            return "";
        }
    }
}
