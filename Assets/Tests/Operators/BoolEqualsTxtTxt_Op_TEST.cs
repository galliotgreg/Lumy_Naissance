using UnityEngine;
using System.Collections;

public class BoolEqualsTxtTxt_Op_TEST : MonoBehaviour {

    [SerializeField]
    private string symbol = "B==TT";
    [SerializeField]
    private int nbTests = 100;

    // Use this for initialization
    void Start() {
        string t1 = "text1";
        string t2 = "text2";

        Test(t1, t1);
        Test(t1, t2);
        Test(t2, t1);
        Test(t2, t2);
    }


    private void Test(string t1, string t2) {
        //Generate rabdom values

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_Equals_Txt_Txt_Operator ope = (AB_Bool_Equals_Txt_Txt_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABText> arg1 = ABParamFactory.CreateTextParam("const", t1);
        ABParam<ABText> arg2 = ABParamFactory.CreateTextParam("const", t2);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected;
        if (t1 == t2) {
            expected = true;
        }
        else {
            expected = false;
        }
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + t1 + ", " + t2 + ") should be '" + expected + "' but it is '" + testValue + "'");
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

