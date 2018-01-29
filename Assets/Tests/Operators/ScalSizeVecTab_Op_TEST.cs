
using UnityEngine;
class ScalSizeVecTab_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "SsizeV[]";
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
        float[] tabX = new float[size];
        float[] tabY = new float[size];

        for (int i = 0; i < tabX.Length; i++) {
            tabX[i] = Random.Range(1,100);
        }

        for (int i = 0; i < tabY.Length; i++) {
            tabY[i] = Random.Range(1, 100);
        }

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_Size_VecTable_Operator ope = (AB_Scal_Size_VecTable_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABVec>> arg = (ABParam<ABTable<ABVec>>)ABParamFactory.CreateVecTableParam("const", tabX, tabY);
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

