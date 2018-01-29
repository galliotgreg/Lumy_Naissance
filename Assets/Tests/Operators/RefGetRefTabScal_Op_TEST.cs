using UnityEngine;

class RefGetRefTabScal_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "RgetR[]S";
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
        


        ABScalar r2 = new ABScalar();
        r2.Value = Random.Range(0, 4);

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Ref_Get_RefTab_Scal_Operator ope = (AB_Ref_Get_RefTab_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABRef>> tabRef1 = CreateTabRef(2, 6);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2.Value);
        ope.Inputs[0] = tabRef1;
        ope.Inputs[1] = arg2;

        //Test
        ABRef testValue = new ABRef();
        testValue = ope.Evaluate(ctx);
        ABRef expected = new ABRef();
        expected = tabRef1.Value.Values[(int)r2.Value];
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + tabRef1 + ", " + r2 + ") should be '" + expected + "' but it is '" + testValue + "'");
        }
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

    // Update is called once per frame
    void Update() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Destroy(this);
#endif
    }
}

