using UnityEngine;

class BoolNotEqualsRefRef_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "B!=RR";
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

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_NotEquals_Ref_Ref_Operator ope = (AB_Bool_NotEquals_Ref_Ref_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABRef> ref1 = CreateRef(0);
        ABParam<ABRef> ref2 = CreateRef(1);
        ope.Inputs[0] = ref1;
        ope.Inputs[1] = ref2;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected;
        if (ref1 != ref2) {
            expected = true;
        }
        else {
            expected = false;
        }
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + ref1 + ", " + ref2 + ") should be '" + expected + "' but it is '" + testValue + "'");
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
