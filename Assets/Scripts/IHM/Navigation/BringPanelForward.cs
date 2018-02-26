using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringPanelForward : MonoBehaviour {

    public string sourcePanelName;
    public string targetPanelName;

    public float zoomStep;

    // Use this for initialization
    void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(MovePanel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MovePanel()
    {
        StartCoroutine(MovePanelCo());
    }

    IEnumerator MovePanelCo()
    {
        GameObject sourcePanel = GameObject.Find(sourcePanelName);
        GameObject targetPanel = GameObject.Find(targetPanelName);
        Vector3 sourcePanelPos = sourcePanel.transform.position;

        // Pour tester sans Navigation Manager, commenter la première ligne et décommenter la seconde.
        //Camera camera = GameObject.Find("NavigationManager").GetComponent<NavigationManager>().GetCurrentCamera();
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        Vector3 dir = (sourcePanel.transform.position - camera.transform.position).normalized;

        /*while (Vector3.Dot(dir, sourcePanel.transform.position - camera.transform.position) > 0)
        {
            Vector3 towards = Vector3.MoveTowards(
                sourcePanel.transform.position,
                camera.transform.position + sourcePanel.transform.position,
                zoomStep);
            sourcePanel.transform.position = towards;
            yield return true;
        }*/

        Vector3 dir2 = (targetPanel.transform.position - sourcePanelPos).normalized;

        while (Vector3.Dot(dir2, targetPanel.transform.position - sourcePanelPos) > 0)
        {
            Vector3 towards1 = Vector3.MoveTowards(
                sourcePanel.transform.position,
                camera.transform.position + sourcePanel.transform.position,
                zoomStep);
            sourcePanel.transform.position = towards1;

            Vector3 towards2 = Vector3.MoveTowards(
                targetPanel.transform.position,
                sourcePanelPos,
                zoomStep);
            targetPanel.transform.position = towards2;
            yield return true;
        }

        yield return true;
    }
}
