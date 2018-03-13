public class GeneralException : System.Exception {
	public GeneralException( string msg )
		: base( msg ){
	}
	public GeneralException()
		: this( "" ){
	}
}
