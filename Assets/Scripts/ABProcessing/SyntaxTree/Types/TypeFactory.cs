using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeFactory {
    public static ABBool CreateEmptyBool()
    {
        return new ABBool();
    }

    public static ABScalar CreateEmptyScalar()
    {
        return new ABScalar();
    }

    public static ABText CreateEmptyText()
    {
        return new ABText();
    }

    public static ABVec CreateEmptyVec()
    {
        return new ABVec();
    }

    public static ABColor CreateEmptyColor()
    {
        return new ABColor();
    }

    public static ABRef CreateEmptyRef()
    {
        return new ABRef();
    }

    public static ABTable<T> CreateEmptyTable<T>() where T : IABSimpleType
    {
        ABTable<T> tab = new ABTable<T>();
        tab.Values = new T[0];
        return tab;
    }
}
