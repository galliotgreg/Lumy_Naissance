using System.Collections.Generic;

public class Intermediate_Tracing : Tracing {
	Tracing internalTracing;

	public Intermediate_Tracing( Tracing internalTracing, TracingInfo info )
		: base( info ) {
		this.internalTracing = internalTracing;
	}

	#region implemented abstract members of Tracing

	public override List<TracingInfo> getTracingInfo ()
	{
		List<TracingInfo> result = new List<TracingInfo>(){ this.info };
		if (internalTracing != null) {
			result.AddRange( internalTracing.getTracingInfo () );
		}
		return result;
	}

	public override TracingInfo getLastTracingInfo ()
	{
		if (internalTracing != null) {
			return internalTracing.getLastTracingInfo ();
		} else {
			return this.info;
		}
	}

	public override string toString ()
	{
		string result = this.info.toString();
		if (internalTracing != null) {
			result += " => " + internalTracing.toString ();
		}
		return result;
	}

	#endregion
}
