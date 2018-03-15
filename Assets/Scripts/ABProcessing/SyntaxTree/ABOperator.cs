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

    public virtual string ClassName
    {
        get
        {
            return GetType().ToString();
        }

        set
        {
            throw new System.NotSupportedException();
        }
    }

	public T EvaluateOperator(ABContext context){
		try{
			return Evaluate( context );
		}
		catch( Operator_MC_Exception opEx ){
			throw new Operator_MC_Exception ( this, opEx );
		}
		catch( Param_MC_Exception paramEx ){
			throw new Operator_MC_Exception ( this, paramEx );
		}
		catch( System.Exception someEx ){
			throw new Operator_Exception ( this, context, someEx.Message );
		}
	}

	protected abstract T Evaluate(ABContext context);

	public System.Type getOutcomeType ()
	{
		return typeof(T);
		//return ((ABOperator<T>)this).GetType().GetGenericArguments () [0];
	}

	public System.Type getIncomeType( int index ){
		int indexPlusStart = index + 3;

		string[] terms = this.GetType ().ToString ().Split ('_');
		try{ // Txt; Scal; 
			foreach( ParamType t in System.Enum.GetValues( typeof( ParamType ) ) ){
				if( getTypeName( t ) == terms[indexPlusStart] ){
					return ABModel.ParamTypeToType( t );
				}else if( getTypeName( t ) == terms[indexPlusStart] + "le" ){// ScalTab + le => ScalTable
					return ABModel.ParamTypeToType( t );
				}else if( getTypeName( t ) + "Star" == terms[indexPlusStart]  ){// Scal + Star => Star
					return ABStar<ABBool>.generateABStar( t );
				}
			}
		}catch(System.Exception ex){return ABModel.ParamTypeToType( ParamType.None );}
		return ABModel.ParamTypeToType( ParamType.None );
	}

	public bool acceptIncome( int index, System.Type income ){
		System.Type thisType = getIncomeType (index);

		if (thisType == income) {
			return true;
		} else {
			// Check Star Param
			if( thisType.IsGenericType && thisType.GetGenericTypeDefinition() == typeof( ABStar<> ) && thisType.GetGenericArguments().Length > 0 ){
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

	public static string getTypeName( ParamType type ){
		if( type == ParamType.Scalar ){
			return "Scal";
		}
		else if( type == ParamType.Text ){
			return "Txt";
		}
		else if( type == ParamType.ScalarTable ){
			return "ScalTable";
		}
		else if( type == ParamType.TextTable ){
			return "TxtTable";
		}
		else{
			return type.ToString();
		}
	}
}
