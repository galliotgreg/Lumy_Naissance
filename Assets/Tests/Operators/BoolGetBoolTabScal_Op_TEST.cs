using UnityEngine;

class BoolGetBoolTabScal_Op_TEST : MonoBehaviour {

    [SerializeField]
    private string symbol = "BgetB[]S";
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

        int size = Random.Range(1, 100);
        bool[] tab = new bool[size];

        for (int i = 0; i < tab.Length; i++) {
            tab[i] = true;
        }

        ABScalar r2 = new ABScalar();
        r2.Value = Random.Range(0,size);

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_Get_BoolTab_Scal_Operator ope = (AB_Bool_Get_BoolTab_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABBool>> arg1 = (ABParam<ABTable<ABBool>>)ABParamFactory.CreateBoolTableParam("const", tab);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2.Value);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABBool testValue = new ABBool();
        testValue = ope.Evaluate(ctx);
        Debug.Log("testValue : " + testValue.Value);
        ABBool expected = new ABBool();
        expected.Value = tab[(int)r2.Value];
        Debug.Log("Expected : " + expected.Value);
        if (testValue.Value == expected.Value) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + tab + ", " + r2 + ") should be '" + expected + "' but it is '" + testValue + "'");
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

