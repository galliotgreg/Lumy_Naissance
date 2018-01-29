using UnityEngine;
using System.Collections;

public class BoolNotBool_Op_TEST : MonoBehaviour
{
    private string symbol = "B!B";
    [SerializeField]
    private int nbTests = 100;
    // Use this for initialization
    void Start()
    {
        bool b1 = false;

        Test(b1);
    }

    private void Test(bool b1)
    {
        //Generate rabdom values

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_Not_Bool_Operator ope = (AB_Bool_Not_Bool_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABBool> arg1 = ABParamFactory.CreateBoolParam("const", b1);
        ope.Inputs[0] = arg1;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected = !b1;
        if (testValue == expected)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.Log(this.GetType().Name + " KO ! result for (" + b1 + ", " +  ") should be '" + expected + "' but it is '" + testValue + "'");
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
