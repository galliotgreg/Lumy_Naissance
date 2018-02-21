using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MCEditor_Proxy : MonoBehaviour {

	public List<Pin> AllPins{
		get{
			return new List<Pin> (this.gameObject.GetComponentsInChildren<Pin> ());
		}
	}

	public List<Pin> getPins( Pin.PinType pinType ){
		List<Pin> result = new List<Pin> ();

		Pin[] pins = this.gameObject.GetComponentsInChildren<Pin>();

		foreach (Pin p in pins) {
			if (p.Pin_Type == pinType) {
				result.Add ( p );
			}
		}

		return result;
	}

	#region VIEW METHODS
	public void SetNodeName(ABNode node)
	{
		UnityEngine.UI.Text operatorName = this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
		operatorName.text = getNodeName( node );
	}

	public static string getNodeName( ABNode node ){
		string opeName = node.ToString();
		char splitter = '_';
		string[] newName = opeName.Split(splitter);
		string newOpeName = "";

		for (int i = 1; i < newName.Length - 1; i++)
		{
			newOpeName += newName[i];
		}

		return newOpeName;
	}
	#endregion
}
