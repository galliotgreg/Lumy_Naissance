using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTable<T> : IABComplexType where T : IABSimpleType
{
    private T[] values;

    public T[] Values
    {
        get
        {
            return values;
        }

        set
        {
            this.values = value;
        }
    }

	#region IABComplexType implementation

	System.Type IABComplexType.getInternalType (int index = 0)
	{
		// ignore index
		return typeof(T);
	}

	#endregion

	public static System.Type getInternalIfComplexType (System.Type type, int index = 0)
	{
		if (type.IsGenericType && type.GetGenericTypeDefinition () == typeof(ABTable<>) && type.GetGenericArguments ().Length > 0) {
			return type.GetGenericArguments()[index];
		}
		return null;
	}
}
