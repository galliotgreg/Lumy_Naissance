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

	public abstract string Title{ get; }

	public abstract string getMessage ();

	public string Cast{
		get{
			string cast = "";
			IABParam selfParam = ContextSource.GetParam ("self");
			if(selfParam != null){
				IABType castParam = ((ABRefParam)selfParam).Value.GetAttr ("cast");
				if (castParam != null) {
					cast = ((ABText)castParam).Value;
				}
			}
			return cast;
		}
	}
}
