using UnityEngine;
using System.Collections;

public class VecProdVecScal_Op_TEST : MonoBehaviour
{
    [SerializeField]
    private string symbol = "V*VS";
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
        ABScalar r2 = new ABScalar();
        r1.X = Random.value * 100 - 50;
        r1.Y = Random.value * 100 - 50;
        r2.Value = Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Vec_Prod_Vec_Scal_Operator ope = (AB_Vec_Prod_Vec_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABVec> arg1 = ABParamFactory.CreateVecParam("const", r1.X, r1.Y);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2.Value);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABVec testValue = new ABVec();
        testValue = ope.Evaluate(ctx);
        ABVec expected = new ABVec();
        expected.X = r1.X + r2.Value;
        expected.Y = r1.Y + r2.Value;
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
