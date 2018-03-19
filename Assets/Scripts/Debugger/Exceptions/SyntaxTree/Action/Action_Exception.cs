public class Action_Exception : Terminal_SyntaxTree_MC_Exception {
	ABAction actionSource;

	public Action_Exception( ABAction action, ABContext context, string msg )
		: base( context, msg ){
		actionSource = action;
	}

	#region implemented abstract members of SyntaxTree_MC_Exception

	protected override string getNodeMessage ()
	{
		return actionSource.Type.ToString ()+" => "+this.Message;
	}

	#endregion
}
