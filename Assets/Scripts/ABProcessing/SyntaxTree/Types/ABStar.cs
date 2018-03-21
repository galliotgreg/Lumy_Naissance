using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABStar<T> : IABComplexType where T : IABSimpleType
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

	public static System.Type generateABStar( ParamType type ){
		switch (type) {
		case ParamType.Bool : return typeof( ABStar<ABBool> );
		case ParamType.Color : return typeof( ABStar<ABColor> );
		case ParamType.Ref : return typeof( ABStar<ABRef> );
		case ParamType.Scalar : return typeof( ABStar<ABScalar> );
		case ParamType.Text : return typeof( ABStar<ABText> );
		case ParamType.Vec : return typeof( ABStar<ABVec> );
		default : return null;
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
		if (type.IsGenericType && type.GetGenericArguments ().Length > 0) {
			List<System.Type> interfaces = new List<System.Type> (type.GetInterfaces ());
			if (interfaces.Contains (typeof(IABComplexType))) {
				return type.GetGenericArguments () [index];
			}
		}
		return null;
	}

	public static bool isStar( System.Type type ){
		return (type.IsGenericType && type.GetGenericTypeDefinition () == typeof(ABStar<>));
	}
}