public class Action_Exception : GeneralException {
	ABAction actionSource;

	public Action_Exception( ABAction action, string msg = "" ){
		actionSource = action;
	}
}
