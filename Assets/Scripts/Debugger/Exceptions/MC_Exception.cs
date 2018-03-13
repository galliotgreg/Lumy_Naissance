public abstract class MC_Exception : GeneralException {
	ABContext contextSource;

	public ABContext ContextSource {
		get {
			return contextSource;
		}
	}

	public MC_Exception( ABContext context, string msg = "" )
		: base( msg ) {
		this.contextSource = context;
	}
}
