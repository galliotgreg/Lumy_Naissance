
using UnityEngine;
class ColorGetColorTabScal_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "CgetC[]S";
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
        int[] redTab = new int[size];
        int[] greenTab = new int[size];
        int[] blueTab = new int[size];

        for (int i = 0; i < redTab.Length; i++) {
            redTab[i] = 255;
        }

        for (int i = 0; i < greenTab.Length; i++) {
            greenTab[i] = 0;
        }

        for (int i = 0; i < blueTab.Length; i++) {
            blueTab[i] = 0;
        }

        ABScalar r2 = new ABScalar();
        r2.Value = Random.Range(0, size);

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Color_Get_ColorTab_Scal_Operator ope = (AB_Color_Get_ColorTab_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABColor>> arg1 = (ABParam<ABTable<ABColor>>)ABParamFactory.CreateColorTableParam("const", redTab, greenTab, blueTab);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2.Value);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        ABColor testValue = new ABColor();
        testValue = ope.Evaluate(ctx);
        ABColor expectedColor = new ABColor();
        expectedColor.Value = ABColor.Color.Red;
        //float expectedRed = redTab[(int)r2.Value];
        //float expectedGreen = greenTab[(int)r2.Value];
        //float expectedBlue = blueTab[(int)r2.Value];
        //expectedColor.Value = ;
        if (testValue.Value == expectedColor.Value) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + redTab + ", " + r2 + ") should be '" + expectedColor + "' but it is '" + testValue + "'");
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
