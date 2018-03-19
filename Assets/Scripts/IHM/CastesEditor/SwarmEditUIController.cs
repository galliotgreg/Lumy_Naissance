using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEditUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static SwarmEditUIController instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        RefreshView();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RefreshView()
    {
        Debug.Log("RefreshView");
    }

    public void CopySwarm()
    {

    }

    public void DeleteSwarm()
    {

    }

    public void NewSwarm()
    {

    }

    public void OpenEditSwarmDialog()
    {

    }

    public void OpenImportSwarmDialog()
    {
        
    }

    public void OpenExportSwarmDialog()
    {

    }

    public void CopyLumy()
    {

    }

    public void DeleteLumy()
    {

    }

    public void NewLumy()
    {

    }

    public void IncrVitality()
    {

    }

    public void DecrVitality()
    {

    }

    public void IncrStamina()
    {

    }

    public void DecrStamina()
    {

    }

    public void IncrStrength()
    {

    }

    public void DecrStrength()
    {

    }

    public void IncrActSpeed()
    {

    }

    public void DecrActSpeed()
    {

    }

    public void IncrMoveSpeed()
    {

    }

    public void DecrMoveSpeed()
    {

    }

    public void IncrVisionRange()
    {

    }

    public void DecrVisionRange()
    {

    }

    public void IncrAtkRange()
    {

    }

    public void DecrAtkRange()
    {

    }

    public void IncrPickRange()
    {

    }

    public void DecrPickRange()
    {

    }
}
