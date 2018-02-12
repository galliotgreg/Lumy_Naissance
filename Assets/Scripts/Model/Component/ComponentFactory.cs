using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentFactory : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static ComponentFactory instance = null;

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

    [SerializeField]
    private string COMPO_DATA_PATH = "Assets/Inputs/Components/components.csv";

    protected static Dictionary<int, ComponentInfo> loadedComponents = null;

	public class ComponentFactoryNotCreated : System.Exception{};

	public static bool IsLoaded {
		get {
			return loadedComponents != null;
		}
	}

	private void CreateFactory(){
		try{
			loadedComponents = ComponentParser.parse(readFile(COMPO_DATA_PATH));
		}
		catch( Exception ex ){
			UnityEngine.Debug.Log( ex.ToString() );
			throw new ComponentFactory.ComponentFactoryNotCreated();
		}
	}

    public ComponentInfo CreateComponent(int id)
    {
		if( !IsLoaded ){
            CreateFactory();
		}

		if( loadedComponents.ContainsKey( id ) ){
			return loadedComponents[id];
			// TODO clone
		}
		else{
			return null;
		}
    }

    private string readFile(string path)
    {
        System.IO.StreamReader reader = new System.IO.StreamReader(path);
        return reader.ReadToEnd();
    }
}
