using System.Collections;
using UnityEngine;

public class BoolLessThanScalScal_Op_Test : MonoBehaviour {

    [SerializeField]
    private string symbol = "B<SS";
    [SerializeField]
    private int nbTests = 100;


	// Use this for initialization
	void Start () {
        for (int i = 0; i < nbTests; i++)
        {
            RandomizeTest();
        }
	}
	
    private void RandomizeTest()
    {
        //Generate rabdom values
        float r1 = Random.value * 100 - 50;
        float r2 = Random.value * 100 - 50;

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_LessThan_Scal_Scal_Operator ope = (AB_Bool_LessThan_Scal_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABScalar> arg1 = ABParamFactory.CreateScalarParam("const", r1);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", r2);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected = r1 < r2;
        if (testValue== expected)
        {
            Debug.Log(this.GetType().Name + " OK");
        } else
        {
            Debug.Log(this.GetType().Name + " KO ! result for (" + r1 + ", "+r2+") should be '"+ expected + "' but it is '" + testValue + "'");
        }
    }


	// Update is called once per frame
	void Update () {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Destroy(this);
#endif
    }
}
