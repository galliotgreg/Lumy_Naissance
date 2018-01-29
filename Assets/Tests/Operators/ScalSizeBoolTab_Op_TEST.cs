using UnityEngine;
using System.Collections;
class ScalSizeBoolTab_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "SsizeB[]";
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
        bool[] tab = new bool[size];

        for(int i = 0; i < tab.Length; i++) {
            tab[i] = true;
        }

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Size_BoolTab_Operator ope = (AB_Scal_Size_BoolTab_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABBool>> arg = (ABParam<ABTable<ABBool>>)ABParamFactory.CreateBoolTableParam("const", tab);
        ope.Inputs[0] = arg;

        //Test
        int testValue = (int)ope.Evaluate(ctx).Value;
        int expected = tab.Length;
        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + tab.ToString() + ") should be '" + expected + "' but it is '" + testValue + "'");
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

