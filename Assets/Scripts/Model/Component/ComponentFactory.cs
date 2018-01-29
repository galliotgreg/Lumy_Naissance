using System;
using System.Collections.Generic;

public class ComponentFactory {

	protected static Dictionary<int,AgentComponent> loadedComponents = null;

	public class ComponentFactoryNotCreated : System.Exception{};

	public static bool IsLoaded {
		get {
			return loadedComponents != null;
		}
	}

	public static void CreateFactory( string fileContent ){
		try{
			loadedComponents = ComponentParser.parse( fileContent );
		}
		catch( Exception ex ){
			UnityEngine.Debug.Log( ex.ToString() );
			throw new ComponentFactory.ComponentFactoryNotCreated();
		}
	}

    public static AgentComponent CreateComponent(int id)
    {
		if( !IsLoaded ){
			throw new ComponentFactory.ComponentFactoryNotCreated();
		}

		if( loadedComponents.ContainsKey( id ) ){
			return loadedComponents[id];
			// TODO clone
		}
		else{
			return null;
		}
    }

}
