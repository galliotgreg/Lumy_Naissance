
using UnityEngine;

class RefTabWhereEqualsRefTabTxtBool_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "R[]where==R[]TB";
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
        
        string r2 = "true";
        bool r3 = true;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_RefTab_WhereEquals_RefTab_Txt_Bool_Operator ope = (AB_RefTab_WhereEquals_RefTab_Txt_Bool_Operator)ABOperatorFactory.CreateOperator(symbol);

        ABParam<ABTable<ABRef>> tabRef1 = CreateTabRef(2, 6);
        ABParam<ABText> arg2 = ABParamFactory.CreateTextParam("const", r2);
        ABParam<ABBool> arg3 = ABParamFactory.CreateBoolParam("const", r3);
        ope.Inputs[0] = tabRef1;
        ope.Inputs[1] = arg2;
        ope.Inputs[2] = arg3;

        //Test
        ABRef[] testValue = ope.Evaluate(ctx).Values;
        ABRef[] expected = null;

        for(int i = 0; i < tabRef1.Value.Values.Length; i++) {
            if(tabRef1.Value.Values[i].GetAttr(r2) != null && ((ABBool) tabRef1.Value.Values[i].GetAttr(arg2.Value.Value)).Value == arg3.Value.Value) {
                expected.SetValue(tabRef1.Value.Values[i], i);
            }
        }

        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + r2 + ") should be '" + expected + "' but it is '" + testValue + "'");
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

    private ABParam<ABTable<ABRef>> CreateTabRef(int v1, int v2) {
        ABTable<ABRef> refTab = TypeFactory.CreateEmptyTable<ABRef>();
        refTab.Values = new ABRef[v2 - v1];
        for (int i = 0; i < refTab.Values.Length; i++) {
            refTab.Values[i] = CreateRef(v1 + i).Value;
        }
        ABParam<ABTable<ABRef>> param = new ABTableParam<ABRef>("const", refTab);

        return param;
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

