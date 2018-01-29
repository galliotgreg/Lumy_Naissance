using UnityEngine;

class ScalGetScalTabScal_Op_TEST : MonoBehaviour {

    [SerializeField]
    private string symbol = "SgetS[]S";
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
        float[] tab = new float[size];

        for (int i = 0; i < tab.Length; i++) {
            tab[i] = Random.Range(0,100);
        }

        ABScalar r2 = new ABScalar();
        r2.Value = Random.Range(0, size);

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Get_ScalTab_Scal_Operator ope = (AB_Scal_Get_ScalTab_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABScalar>> arg1 = (ABParam<ABTable<ABScalar>>)ABParamFactory.CreateScalarTableParam("const", tab);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2.Value);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABScalar testValue = new ABScalar();
        testValue = ope.Evaluate(ctx);
        ABScalar expected = new ABScalar();
        expected.Value = tab[(int)r2.Value];
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

