using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMacroOperator<T> : ABOperator<T>
{
    private string className;
    private string viewName;
    private string symbolName;

	private List<System.Type> argTypes;

    private ABOperator<T> wrappedTree;

    public override string ClassName
    {
        get
        {
            return className;
        }

        set
        {
            className = value;
        }
    }

    public override string ViewName
    {
        get
        {            
            return viewName;
        }

        set
        {
            viewName = value;
        }
    }

    public string SymbolName
    {
        get
        {
            return symbolName;
        }

        set
        {
            symbolName = value;
        }
    }

    public ABOperator<T> WrappedTree
    {
        get
        {
            return wrappedTree;
        }

        set
        {
            wrappedTree = value;
        }
    }

	public List<System.Type> ArgTypes {
		get {
			return argTypes;
		}
		set {
			argTypes = value;
		}
	}

    protected override T Evaluate(ABContext context)
    {
        ABContext subContext = ComputeSubContext(context);
        return wrappedTree.EvaluateOperator(subContext);
    }

    private ABContext ComputeSubContext(ABContext context)
    {
        ABContext subContext = context.Copy();
        IList<IABParam> args = new List<IABParam>();
        int id = 0;
        foreach (ABNode input in inputs)
        {
            IABParam param;
            if (input is ABOperator<ABBool>
                || input is ABParam<ABBool>)
            {
                ABBool var = OperatorHelper.Instance.getBoolParam(context, input);
                param = new ABBoolParam((id++).ToString(), var);
            } else if (input is ABOperator<ABScalar>
                || input is ABParam<ABScalar>)
            {
                ABScalar var = OperatorHelper.Instance.getScalarParam(context, input);
                param = new ABScalParam((id++).ToString(), var);
            }
            else if (input is ABOperator<ABText>
              || input is ABParam<ABText>)
            {
                ABText var = OperatorHelper.Instance.getTextParam(context, input);
                param = new ABTextParam((id++).ToString(), var);
            }
            else if (input is ABOperator<ABColor>
              || input is ABParam<ABColor>)
            {
                ABColor var = OperatorHelper.Instance.getColorParam(context, input);
                param = new ABColorParam((id++).ToString(), var);
            }
            else if (input is ABOperator<ABVec>
              || input is ABParam<ABVec>)
            {
                ABVec var = OperatorHelper.Instance.getVecParam(context, input);
                param = new ABVecParam((id++).ToString(), var);
            }
            else if (input is ABOperator<ABRef>
               || input is ABParam<ABRef>)
            {
                ABRef var = OperatorHelper.Instance.getRefParam(context, input);
                param = new ABRefParam((id++).ToString(), var);
            }
            else if (input is ABOperator<ABTable<ABBool>>
                || input is ABParam<ABTable<ABBool>>)
            {
                ABTable<ABBool> var = OperatorHelper.Instance.getTabBoolParam(context, input);
                param = new ABTableParam<ABBool>((id++).ToString(), var);
            }
            else if (input is ABOperator<ABTable<ABScalar>>
                || input is ABParam<ABTable<ABScalar>>)
            {
                ABTable<ABScalar> var = OperatorHelper.Instance.getTabScalarParam(context, input);
                param = new ABTableParam<ABScalar>((id++).ToString(), var);
            }
            else if (input is ABOperator<ABTable<ABText>>
                || input is ABParam<ABTable<ABText>>)
            {
                ABTable<ABText> var = OperatorHelper.Instance.getTabTxtParam(context, input);
                param = new ABTableParam<ABText>((id++).ToString(), var);
            }
            else if (input is ABOperator<ABTable<ABColor>>
                || input is ABParam<ABTable<ABColor>>)
            {
                ABTable<ABColor> var = OperatorHelper.Instance.getTabColorParam(context, input);
                param = new ABTableParam<ABColor>((id++).ToString(), var);
            }
            else if (input is ABOperator<ABTable<ABVec>>
                || input is ABParam<ABTable<ABVec>>)
            {
                ABTable<ABVec> var = OperatorHelper.Instance.getTabVecParam(context, input);
                param = new ABTableParam<ABVec>((id++).ToString(), var);
            }
            else if (input is ABOperator<ABTable<ABRef>>
                || input is ABParam<ABTable<ABRef>>)
            {
                ABTable<ABRef> var = OperatorHelper.Instance.getTabRefParam(context, input);
                param = new ABTableParam<ABRef>((id++).ToString(), var);
            } else
            {
                throw new NotImplementedException();
            }

            subContext.SetParam(param);
        }

        return subContext;
    }

    public void AllocInputs(int nbInputs)
    {
        inputs = new ABNode[nbInputs];
    }

	#region Implementation IABOperator

	public override System.Type getIncomeType( int index ){
		try{
			return argTypes [index];
		}
		catch(System.Exception ex){}

		return ABModel.ParamTypeToType( ParamType.None );
	}

	#endregion
}
