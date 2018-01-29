using UnityEngine;
using System.Collections;

public class BoolNotEqualsColorColor_Op_TEST : MonoBehaviour
{

    [SerializeField]
    private string symbol = "B!=CC";
    // Use this for initialization
    void Start()
    {
        NotEqualTest();
        EqualTest();
    }
    private void EqualTest()
    {

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_NotEquals_Color_Color_Operator ope = (AB_Bool_NotEquals_Color_Color_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABColor> arg1 = (ABParam<ABColor>)ABParamFactory.CreateColorParam("const", 255, 0, 0);
        ABParam<ABColor> arg2 = (ABParam<ABColor>)ABParamFactory.CreateColorParam("const", 0, 0, 255);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected = true;

        if (testValue == expected)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + arg1 + ", " + arg2 + ") should be '" + expected + "' but it is '" + testValue + "'");
        }
    }

    private void NotEqualTest()
    {

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Bool_NotEquals_Color_Color_Operator ope = (AB_Bool_NotEquals_Color_Color_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABColor> arg1 = (ABParam<ABColor>)ABParamFactory.CreateColorParam("const", 255, 0, 0);
        ABParam<ABColor> arg2 = (ABParam<ABColor>)ABParamFactory.CreateColorParam("const", 255, 0, 0);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        bool testValue = ope.Evaluate(ctx).Value;
        bool expected = false;

        if (testValue == expected)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + arg1 + ", " + arg2 + ") should be '" + expected + "' but it is '" + testValue + "'");
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
