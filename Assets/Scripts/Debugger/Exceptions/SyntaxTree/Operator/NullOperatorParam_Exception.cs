public class NullOperatorParam_Exception : Operator_Exception {
	public NullOperatorParam_Exception(IABOperator _operator, ABContext context, string msg = "")
		:base( _operator, context, msg ) {
	}
}