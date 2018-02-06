using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTestManager : MonoBehaviour {

	[SerializeField]
	string castName;

	[SerializeField]
	Vector2 pos;

	GameObject agent = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (agent == null) {
			GameObject obj = GameManager.instance.GetUnitTemplate (PlayerAuthority.Player1, castName);
			if (obj != null) {
				agent = Instantiate (obj, pos, Quaternion.identity);
				agent.name = agent.GetComponent<AgentEntity> ().CastName;
				agent.SetActive (true);
				GameManager.instance.GetHome (PlayerAuthority.Player1).addUnit (agent.GetComponent<AgentEntity> ());
			}
		}
	}
}
