using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecTermProdVecVec_Op_TEST : MonoBehaviour {
    [SerializeField]
    private string symbol = "V*VV";
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
        float x1 = Random.value * 100 - 50;
        float x2 = Random.value * 100 - 50;
        float y1 = Random.value * 100 - 50;
        float y2 = Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Vec_TermProd_Vec_Vec_Operator ope = (AB_Vec_TermProd_Vec_Vec_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABVec> arg1 = ABParamFactory.CreateVecParam("const", x1, y1);
        ABParam<ABVec> arg2 = ABParamFactory.CreateVecParam("const", x2, y2);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABVec testValue = ope.Evaluate(ctx);
        ABVec expected = TypeFactory.CreateEmptyVec();
        expected.X = x1 * x2;
        expected.Y = y1 * y2;
        if (testValue.X == expected.X && testValue.Y == expected.Y)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + x1 + ", " + y1 + ") ," + "(" + x2 + ", " + y2 + ")" +"should be '" + expected + "' but it is '" + testValue + "'");
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
