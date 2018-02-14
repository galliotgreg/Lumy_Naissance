using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Objet permettant de d'impacter les actions effectuées par le joueur dans l'éditeur (copy/cut, delete transition, ...) sur le ABModèle
 * C'est le GameObject (dans le sens où il est afficher dans unity) représentant notre ABModèle.
     */
public class ProxyABModel : MonoBehaviour {
    private ABModel abModel;
    private List<ProxyABState> proxyStates;
    private List<ProxyABTransition> proxyTransitions;
    private List<Pin> pins; //ProxyABGateOperator
    //ProxyABParam
    //ProxyABOperator

    

    // Use this for initialization
    void Start () {
        this.proxyStates = new List<ProxyABState>();
        //CreateProxyStates();

        this.proxyTransitions = new List<ProxyABTransition>();
        this.pins = new List<Pin>();

        CreateProxyTransitions();               
	}
	
   /* void CreateProxyStates()
    {
        foreach (ABState state in this.abModel.States)
        {
            //ProxyABState proxyState = new ProxyABState(state);
            this.proxyStates.Add(proxyState);
            CreatePins(state.Outcomes);
        }
    }*/

    void CreateProxyTransitions()
    {
        foreach (ABTransition transition in this.abModel.Transitions)
        {
            ProxyABTransition proxyTransition = new ProxyABTransition(transition);
            this.proxyTransitions.Add(proxyTransition);
        }
    }

    void CreatePins(List<ABTransition> transitions)
    {
        foreach(ABTransition transition in transitions)
        {
            Pin pin = new Pin();
            //********
            // TODO : Caculer la position du prochain pin grâce à l'équation d'un cercle.
            pin.transform.position = new Vector3();
            //********
            this.pins.Add(pin);
        }
    }



    // Update is called once per frame
    void Update () {
		
	}
}
