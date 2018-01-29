using UnityEngine;
using System.Collections;

public class VecSumVecVec_Op_TEST : MonoBehaviour
{
    [SerializeField]
    private string symbol = "V+VV";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < nbTests; i++)
        {
            RandomizeTest();
        }
    }
    private void RandomizeTest()
    {
        //Generate random values
        ABVec r1 = new ABVec();
        ABVec r2 = new ABVec();
        r1.X = Random.value * 100 - 50;
        r1.Y = Random.value * 100 - 50;
        r2.X = Random.value * 100 - 50;
        r2.Y = Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Vec_Sum_Vec_Vec_Operator ope = (AB_Vec_Sum_Vec_Vec_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABVec> arg1 = ABParamFactory.CreateVecParam("const", r1.X, r1.Y);
        ABParam<ABVec> arg2 = ABParamFactory.CreateVecParam("const", r2.X, r2.Y);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABVec testValue = new ABVec();
        testValue = ope.Evaluate(ctx);
        ABVec expected = new ABVec();
        expected.X = r1.X + r2.X;
        expected.Y = r1.Y + r2.Y;
        if (testValue.X == expected.X && testValue.Y == expected.Y)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + r1 + ", " + r2 + ") should be '" + expected + "' but it is '" + testValue + "'");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Destroy(this);
#endif
    }
}
