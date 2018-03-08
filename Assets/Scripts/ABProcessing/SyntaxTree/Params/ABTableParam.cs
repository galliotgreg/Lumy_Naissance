using System.Collections;
using System.Collections.Generic;

public class ABTableParam<T> : ABParam<ABTable<T>> where T : IABSimpleType {
    public ABTableParam(string identifier, ABTable<T> value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }

	#region implemented abstract members of ABParam

	protected override IABParam CloneParam ()
	{
		//throw new System.NotImplementedException ();
		ABTable<T> table = new ABTable<T>();
		table.Values = new T[this.value.Values.Length];
		for (int i = 0; i < this.value.Values.Length; i++) {
			table.Values [i] = ((T)this.value.Values.Clone ());
		}
		return new ABTableParam<T> (this.identifier, table);
	}

	#endregion
}
