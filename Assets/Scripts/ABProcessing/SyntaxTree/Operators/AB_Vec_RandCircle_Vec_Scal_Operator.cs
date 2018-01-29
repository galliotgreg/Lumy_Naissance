using System;

public class AB_Vec_RandCircle_Vec_Scal_Operator : ABOperator<ABVec>
{
    public static Random random = new Random(42);

    public AB_Vec_RandCircle_Vec_Scal_Operator()
    {
        this.Inputs = new ABNode[2];
    }

    public override ABVec Evaluate(ABContext context)
    {
        //Get center
        ABVec center = null;
        ABNode input1 = Inputs[0];
        center = OperatorHelper.Instance.getVecParam(context, input1);

        //Get radius
        ABScalar radius = null;
        ABNode input2 = Inputs[1];
        radius = OperatorHelper.Instance.getScalarParam(context, input2);

        //Randomize point
        
        float teta = ((float) random.NextDouble()) * 2f * (float) Math.PI;
        teta -= (float)Math.PI;

        //Return
        ABVec result = TypeFactory.CreateEmptyVec();
        result.X = center.X + radius.Value * (float) Math.Cos(teta);
        result.Y = center.Y + radius.Value * (float)Math.Sin(teta);
        return result;
    }
}