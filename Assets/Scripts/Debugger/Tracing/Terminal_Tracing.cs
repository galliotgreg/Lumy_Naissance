using System.Collections.Generic;

public class Terminal_Tracing : Tracing {
	
	public Terminal_Tracing( TracingInfo info )
		:base( info ){
	}

	#region implemented abstract members of Tracing

	public override List<TracingInfo> getTracingInfo ()
	{
		return new List<TracingInfo>(){ this.info };
	}

	public override TracingInfo getLastTracingInfo ()
	{
		return this.info;
	}

	public override string toString ()
	{
		return this.info.toString ();
	}
		
	#endregion
}