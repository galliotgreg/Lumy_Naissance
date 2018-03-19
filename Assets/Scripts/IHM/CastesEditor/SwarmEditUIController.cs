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

    public void SelectSwarm()
    {
        Debug.Log("SelectSwarm");
    }

    public void CopySwarm()
    {
        Debug.Log("CopySwarm");
    }

    public void DeleteSwarm()
    {
        Debug.Log("DeleteSwarm");
    }

    public void NewSwarm()
    {
        Debug.Log("NewSwarm");
    }

    public void OpenEditSwarmDialog()
    {
        Debug.Log("OpenEditSwarmDialog");
    }

    public void OpenImportSwarmDialog()
    {
        Debug.Log("OpenImportSwarmDialog");
    }

    public void OpenExportSwarmDialog()
    {
        Debug.Log("OpenExportSwarmDialog");
    }

    public void SelectLumy()
    {
        Debug.Log("SelectLumy");
    }

    public void CopyLumy()
    {
        Debug.Log("CopyLumy");
    }

    public void DeleteLumy()
    {
        Debug.Log("DeleteLumy");
    }

    public void NewLumy()
    {
        Debug.Log("NewLumy");
    }

    public void EditLumyMC()
    {
        Debug.Log("EditLumyMC");
    }

    public void EditPrysmMC()
    {
        Debug.Log("EditPrysmMC");
    }

    public void IncrVitality()
    {
        Debug.Log("IncrVitality");
    }

    public void DecrVitality()
    {
        Debug.Log("DecrVitality");
    }

    public void IncrStamina()
    {
        Debug.Log("IncrStamina");
    }

    public void DecrStamina()
    {
        Debug.Log("DecrStamina");
    }

    public void IncrStrength()
    {
        Debug.Log("IncrStrength");
    }

    public void DecrStrength()
    {
        Debug.Log("DecrStrength");
    }

    public void IncrActSpeed()
    {
        Debug.Log("IncrActSpeed");
    }

    public void DecrActSpeed()
    {
        Debug.Log("DecrActSpeed");
    }

    public void IncrMoveSpeed()
    {
        Debug.Log("IncrMoveSpeed");
    }

    public void DecrMoveSpeed()
    {
        Debug.Log("DecrMoveSpeed");
    }

    public void IncrVisionRange()
    {
        Debug.Log("IncrVisionRange");
    }

    public void DecrVisionRange()
    {
        Debug.Log("DecrVisionRange");
    }

    public void IncrAtkRange()
    {
        Debug.Log("IncrAtkRange");
    }

    public void DecrAtkRange()
    {
        Debug.Log("DecrAtkRange");
    }

    public void IncrPickRange()
    {
        Debug.Log("IncrPickRange");
    }

    public void DecrPickRange()
    {
        Debug.Log("DecrPickRange");
    }
}
