public class AB_Bool_Equals_Txt_Txt_Operator : ABOperator<ABBool>
{
    public AB_Bool_Equals_Txt_Txt_Operator() {

        this.Inputs = new ABNode[2];
    }

    public override ABBool Evaluate(ABContext context) {

        //Get s1
        ABText t1 = null;
        ABNode input1 = Inputs[0];
        t1 = OperatorHelper.Instance.getTextParam(context, input1);

        //Get s2
        ABText t2 = null;
        ABNode input2 = Inputs[1];
        t2 = OperatorHelper.Instance.getTextParam(context, input2);

        //Return
        ABBool result = TypeFactory.CreateEmptyBool();

        if(t1.Value == t2.Value) {
            result.Value = true;
        }
        if (t1.Value != t2.Value) {
            result.Value = false;
        }

        return result;
    }
}