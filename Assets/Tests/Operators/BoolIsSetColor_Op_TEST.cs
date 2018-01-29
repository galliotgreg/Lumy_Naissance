using UnityEngine;
using System.Collections;
class BoolIsSetColor_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "BisSetC";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < nbTests; i++) {
            RandomizeTest();
        }
    }

    private void RandomizeTest() {

        ABColor r1 = new ABColor();

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_IsSet_Color_Operator ope = (AB_Bool_IsSet_Color_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABColor> arg1 = (ABParam<ABColor>)ABParamFactory.CreateColorParam("const", 255,0,0);
        ope.Inputs[0] = arg1;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected;
        if (r1 != null) {
            expected = true;
        }
        else {
            expected = false;
        }
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + r1 + ") should be '" + expected + "' but it is '" + testValue + "'");
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

