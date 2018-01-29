using UnityEngine;
using System.Collections;


class BoolIsSetVec_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "BisSetV";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < nbTests; i++) {
            RandomizeTest();
        }
    }

    private void RandomizeTest() {
        
        ABVec r1 = new ABVec();
        r1.X = Random.value * 100 - 50;
        r1.Y = Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_IsSet_Vec_Operator ope = (AB_Bool_IsSet_Vec_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABVec> arg1 = ABParamFactory.CreateVecParam("const", r1.X, r1.Y);
        ope.Inputs[0] = arg1;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected;
        if (r1!= null) {
            expected = true;
        }
        else {
            expected = false;
        }
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + r1 +  ") should be '" + expected + "' but it is '" + testValue + "'");
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

