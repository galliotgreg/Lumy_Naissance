using System.Collections;
using System.Collections.Generic;

public class ABTableParam<T> : ABParam<ABTable<T>> where T : IABSimpleType {
    public ABTableParam(string identifier, ABTable<T> value) : base(identifier, value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}
