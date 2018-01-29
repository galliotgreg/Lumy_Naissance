using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayAction : MonoBehaviour {
    // Workaround for script enabling issues
    public bool activated;

    [SerializeField]
    private string castName;

    private bool coolDownElapsed = true;

    private AgentEntity agentEntity;

    public string CastName
    {
        get
        {
            return castName;
        }

        set
        {
            castName = value;
        }
    }

    // Use this for initialization
    void Start () {
        agentEntity = GetComponent<AgentEntity>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!activated)
        {
            return;
        }

        if (coolDownElapsed)
        {
            Lay();
            coolDownElapsed = false;
            Invoke("EndCooldown", 0.1f);
        }
    }

    private void Lay()
    {
        GameObject childTemplate = GameManager.instance.GetUnitTemplate(
            agentEntity.Authority, castName);
        HomeScript home = GameManager.instance.GetHome(agentEntity.Authority);
        GameObject child = Instantiate(
            childTemplate, this.transform.position, this.transform.rotation);
        child.SetActive(true);
        home.Population[castName]++;
    }

    private void EndCooldown()
    {
        coolDownElapsed = true;
    }
}
