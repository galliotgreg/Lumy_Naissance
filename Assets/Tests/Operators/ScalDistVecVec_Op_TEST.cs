using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScalDistVecVec_Op_TEST : MonoBehaviour {
    [SerializeField]
    private string symbol = "SdistVV";
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
        float x1 = UnityEngine.Random.value * 100 - 50;
        float y1 = UnityEngine.Random.value * 100 - 50;
        float x2 = UnityEngine.Random.value * 100 - 50;
        float y2 = UnityEngine.Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Dist_Vec_Vec_Operator ope = (AB_Scal_Dist_Vec_Vec_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABVec> arg1 = ABParamFactory.CreateVecParam("const", x1, y1);
        ABParam<ABVec> arg2 = ABParamFactory.CreateVecParam("const", x2, y2);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        float testValue = ope.Evaluate(ctx).Value;
        float expected = (float)Math.Sqrt((x2-x1) * (x2-x1) + (y2-y1) * (y2-y1));
        //if (testValue == expected)
        if(Math.Abs(testValue - expected) < 0.0001)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + x1 + ", " + y1 + "), "+ "(" + x2 + ", " + y2 + ")" + " should be '" + expected + "' but it is '" + testValue + "'");
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
