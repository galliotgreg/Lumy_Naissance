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

	public MCEditor_Proxy InfoProxy( ABModel model ){
		if (state.Action == null) {
			// State
			return MCEditor_Proxy_Factory.instantiateState (state, state.Id == model.InitStateId);
		} else {
			// Action
			return MCEditor_Proxy_Factory.instantiateAction (state);
		}
	}
}
