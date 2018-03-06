using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABOperator<T> : ABNode, IABOperator
{
    protected ABNode[] inputs;

    public ABNode[] Inputs
    {
        get
        {
            return inputs;
        }

        set
        {
            inputs = value;
        }
    }

    public abstract T Evaluate(ABContext context);

	public System.Type getOutcomeType ()
	{
		return typeof(T);
		//return ((ABOperator<T>)this).GetType().GetGenericArguments () [0];
	}

	public System.Type getIncomeType( int index ){
		return ABModel.ParamTypeToType( getIncomeParamType(index) );
	}

	ParamType getIncomeParamType( int index ){
		int indexPlusStart = index + 3;
		string[] terms = this.GetType ().ToString ().Split ('_');
		try{
			foreach( ParamType t in System.Enum.GetValues( typeof( ParamType ) ) ){
				if( t.ToString() == terms[indexPlusStart] ){
					return t;
				}else if( t.ToString() == terms[indexPlusStart] + "le" ){// ScalTab + le
					return t;
				}
			}
		}catch(System.Exception ex){
			return ParamType.None;
		}
		return ParamType.None;
	}
}
