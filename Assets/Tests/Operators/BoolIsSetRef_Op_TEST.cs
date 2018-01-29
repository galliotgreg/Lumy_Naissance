using UnityEngine;



class BoolIsSetRef_Op_TEST : MonoBehaviour{

    [SerializeField]
    private string symbol = "BisSetR";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < nbTests; i++) {
            RandomizeTest();
        }
    }

    private void RandomizeTest() {
        

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_IsSet_Ref_Operator ope = (AB_Bool_IsSet_Ref_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABRef> ref1 = CreateRef(0);

        ope.Inputs[0] = ref1;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected;
        
        if(ref1 != null) {
            expected = true;
        }
        else {
            expected = false;
        }

        if (testValue == expected) {
            Debug.Log(this.GetType().Name + " OK");
        }
        else {
            Debug.LogError(this.GetType().Name + " KO ! test value differs from reference at index ");
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
