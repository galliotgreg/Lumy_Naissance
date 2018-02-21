using UnityEngine;
using System.Collections;

public class TransitionUpdate : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 posDepart = GetComponent<LineRenderer>().GetPosition(0);
        Vector3 posArrivee = GetComponent<LineRenderer>().GetPosition(1);
        GetComponent<LineRenderer>().SetPosition(0, posDepart);
        GetComponent<LineRenderer>().SetPosition(1, posArrivee);
    }
}
