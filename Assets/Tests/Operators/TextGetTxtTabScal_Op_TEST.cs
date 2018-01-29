using UnityEngine;
class TextGetTxtTabScal_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "TgetT[]S";
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
        string[] tab = new string[size];

        for (int i = 0; i < tab.Length; i++) {
            tab[i] = "bonjour";
        }

        ABScalar r2 = new ABScalar();
        r2.Value = Random.Range(0, size);

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Txt_Get_TxtTab_Scal_Operator ope = (AB_Txt_Get_TxtTab_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABText>> arg1 = (ABParam<ABTable<ABText>>)ABParamFactory.CreateTextTableParam("const", tab);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2.Value);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABText testValue = new ABText();
        testValue = ope.Evaluate(ctx);
        ABText expected = new ABText();
        expected.Value = tab[(int)r2.Value];
        //float expectedRed = redTab[(int)r2.Value];
        //float expectedGreen = greenTab[(int)r2.Value];
        //float expectedBlue = blueTab[(int)r2.Value];
        //expectedColor.Value = ;
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

