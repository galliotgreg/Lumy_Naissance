using UnityEngine;
using System.Collections;

class ScalGetRefTxt_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "SgetRT";
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
        string s = "pos";

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Get_Ref_Txt_Operator ope = (AB_Scal_Get_Ref_Txt_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABRef> ref1 = CreateRef(1);
        ABParam<ABText> arg2 = ABParamFactory.CreateTextParam("const", s);
        ope.Inputs[0] = ref1;
        ope.Inputs[1] = arg2;

        //Test
        Debug.Log("ctx" + ctx);
        Debug.Log("ope" + ope);
        Debug.Log("ref1" + ref1);
        Debug.Log("arg2" + arg2);
        Debug.Log("test :" + ope.Evaluate(ctx).Value);

        float testValue;
        testValue = ope.Evaluate(ctx).Value;
        float expected;
        expected = ((ABScalar)ref1.Value.GetAttr(s)).Value;
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + ref1 + ", " + s + ") should be '" + expected + "' but it is '" + testValue + "'");
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

    private static ABParam<ABRef> CreateRef(float scalAttrVal) {
        ABRef myRef = TypeFactory.CreateEmptyRef();
        ABScalar scal = TypeFactory.CreateEmptyScalar();
        scal.Value = scalAttrVal;
        myRef.SetAttr("Id", scal);
        ABParam<ABRef> param = new ABRefParam("const", myRef);

        return param;
    }

}

