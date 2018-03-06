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
		int indexPlusStart = index + 3;

		string[] terms = this.GetType ().ToString ().Split ('_');
		try{
			foreach( ParamType t in System.Enum.GetValues( typeof( ParamType ) ) ){
				if( t.ToString() == terms[indexPlusStart] ){
					return ABModel.ParamTypeToType( t );
				}else if( t.ToString() == terms[indexPlusStart] + "le" ){// ScalTab + le => Table
					return ABModel.ParamTypeToType( t );
				}else if( t.ToString() + "Star" == terms[indexPlusStart]  ){// Scal + Star => Star
					return ABStar<ABBool>.generateABStar( t );
				}
			}
		}catch(System.Exception ex){}
		return ABModel.ParamTypeToType( ParamType.None );
	}

	public bool acceptIncome( int index, System.Type income ){
		System.Type thisType = getIncomeType (index);

		if (thisType == income) {
			return true;
		} else {
			// Check Star Param
			if( thisType.GetGenericArguments().Length > 0 ){
				System.Type argType = thisType.GetGenericArguments()[0];

				if (argType == income) {
					// Simple Type
					return true;
				} else {
					// Complex Type
					if( income.GetGenericArguments().Length > 0 && income.GetGenericArguments()[0] == argType ){
						return true;
					}
				}
			}
		}

		return false;
	}
}
