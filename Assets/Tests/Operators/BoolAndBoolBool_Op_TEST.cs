using UnityEngine;
using System.Collections;

public class BoolAndBoolBool_Op_TEST : MonoBehaviour
{
    [SerializeField]
    private string symbol = "B&&BB";
    [SerializeField]
    private int nbTests = 100;

    // Use this for initialization
    void Start()
    {
        bool b1 = false;
        bool b2 = true;

        Test(b1, b1);
        Test(b1, b2);
        Test(b2, b1);
        Test(b2, b2);
    }


    private void Test(bool b1, bool b2)
    {
        //Generate rabdom values

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_And_Bool_Bool_Operator ope = (AB_Bool_And_Bool_Bool_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABBool> arg1 = ABParamFactory.CreateBoolParam("const", b1);
        ABParam<ABBool> arg2 = ABParamFactory.CreateBoolParam("const", b2);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected = b1 & b2;
        if (testValue == expected)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.Log(this.GetType().Name + " KO ! result for (" + b1 + ", " + b2 + ") should be '" + expected + "' but it is '" + testValue + "'");
        }
    }


    // Update is called once per frame
    void Update()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Destroy(this);
#endif
    }
}
