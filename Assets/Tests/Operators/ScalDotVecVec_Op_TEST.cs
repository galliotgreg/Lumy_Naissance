using UnityEngine;

using System;
public class ScalDotVecVec_Op_TEST : MonoBehaviour {

    [SerializeField]
    private string symbol = "S.VV";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < nbTests; i++) {
            RandomizeTest();
        }
    }

    private void RandomizeTest() {
        //Generate random values
        ABVec v1 = new ABVec();
        ABVec v2 = new ABVec();
        v1.X = UnityEngine.Random.value * 100 - 50;
        v1.Y = UnityEngine.Random.value * 100 - 50;
        v2.X = UnityEngine.Random.value * 100 - 50;
        v2.Y = UnityEngine.Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Dot_Vec_Vec_Operator ope = (AB_Scal_Dot_Vec_Vec_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABVec> arg1 = ABParamFactory.CreateVecParam("const", v1.X, v1.Y);
        ABParam<ABVec> arg2 = ABParamFactory.CreateVecParam("const", v2.X, v2.Y);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABScalar testValue = new ABScalar();
        testValue = ope.Evaluate(ctx);
        ABScalar expected = new ABScalar();
        expected.Value = (v1.X * v2.X) + (v1.Y * v2.Y);
        if (Math.Abs(testValue.Value - expected.Value) < 0.0003) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + v1 + ", " + v2 + ") should be '" + expected + "' but it is '" + testValue + "'");
        }
    }

    // Update is called once per frame
    void Update() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Destroy(this);
#endif
    }
}
