public class NullActionParam_Exception : Action_Exception {
	public NullActionParam_Exception(ABAction action, ABContext context, string msg = "")
		:base( action, context, msg ) {
	}
}