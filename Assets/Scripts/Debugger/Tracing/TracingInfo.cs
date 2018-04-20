using UnityEngine;

public class TracingInfo {
	ABState state;

	public TracingInfo( ABState state ){
		this.state = state;
	}

	public ABState State {
		get {
			return state;
		}
	}

	public string toString(){
		//return "State [ " + state.Id + " ] = " + state.Name;
		return state.Name + " ("+state.Id+") ";
	}

	public Debugger_Node InfoProxy( ABModel model, Transform parent ){
		if (state.Action == null) {
			// State
			return MC_Debugger_Factory.instance.instantiateState (state, state.Id == model.InitStateId, parent);
		} else {
			// Action
			return MC_Debugger_Factory.instance.instantiateState (state, false, parent);
		}
	}

	public MC_Proxy_Debugger_Image InfoProxy_Image( ABModel model, Transform parent ){
		if (state.Action == null) {
			// State
			return MC_Proxy_Image_Factory.instance.instantiateStateImage (state, state.Id == model.InitStateId, parent);
		} else {
			// Action
			return MC_Proxy_Image_Factory.instance.instantiateActionImage (state, parent);
		}
	}
}
