using System.Collections.Generic;

public abstract class Tracing {

	protected TracingInfo info;

	public Tracing( TracingInfo info ){
		this.info = info;
	}

	public abstract List<TracingInfo> getTracingInfo();
	public abstract TracingInfo getLastTracingInfo();
	public abstract string toString();
}
