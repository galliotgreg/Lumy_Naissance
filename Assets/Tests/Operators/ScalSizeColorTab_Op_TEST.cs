using UnityEngine;
using System.Collections;
class ScalSizeColorTab_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "SsizeC[]";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < nbTests; i++) {
            RandomizeTest();
        }
    }

    private void RandomizeTest() {
        int size = Random.Range(1, 100);
        int[] redTab = new int[size];
        int[] greenTab = new int[size];
        int[] blueTab = new int[size];

        for (int i = 0; i < redTab.Length; i++) {
            redTab[i] = Random.Range(1, 255);
        }

        for (int i = 0; i < greenTab.Length; i++) {
            greenTab[i] = Random.Range(1, 255);
        }

        for (int i = 0; i < blueTab.Length; i++) {
            blueTab[i] = Random.Range(1, 255);
        }

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Size_ColorTab_Operator ope = (AB_Scal_Size_ColorTab_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABColor>> arg = (ABParam<ABTable<ABColor>>)ABParamFactory.CreateColorTableParam("const", redTab, greenTab, blueTab);
        ope.Inputs[0] = arg;

        //Test
        int testValue = (int)ope.Evaluate(ctx).Value;
        int expected = size;
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + size.ToString() + ") should be '" + expected + "' but it is '" + testValue + "'");
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

