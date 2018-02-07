using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTestManager : MonoBehaviour {

	[SerializeField]
	string castName;

	[SerializeField]
	PlayerAuthority authority = PlayerAuthority.Player1;

	[SerializeField]
	Vector2 pos;

	GameObject agent = null;
	bool initialized = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (agent == null && !initialized) {
			GameObject obj = GameManager.instance.GetUnitTemplate (authority, castName);
			if (obj != null) {
				agent = Instantiate (obj, pos, Quaternion.identity);
				agent.SetActive (true);
				HomeScript home = GameManager.instance.GetHome (authority);
				home.addUnit (agent.GetComponent<AgentEntity> ());
				agent.name = agent.GetComponent<AgentEntity> ().CastName + "-" + home.Authority.ToString();

				initialized = true;
			}
		}
	}
}
