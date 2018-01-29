using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class VecGetRefTxt_Op_TEST : MonoBehaviour {
    [SerializeField]
    private string symbol = "VgetRT";    

    // Use this for initialization
    void Start()
    {
        // asset = true
        Test("test1", 1.0f, 2.0f);

        //assert = false
        Test("test2", 1.0f, 2.0f);
    }

    private void Test(string texte, float x, float y)
    {
        ABRef  refer = TypeFactory.CreateEmptyRef();
        ABVec vec = TypeFactory.CreateEmptyVec();
        vec.X = x;
        vec.Y = y; 
        refer.SetAttr("test1", vec);
        ABParam<ABText> text = (ABParam<ABText>)ABParamFactory.CreateTextParam("const", texte);

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Vec_Get_Ref_Txt_Operator ope = (AB_Vec_Get_Ref_Txt_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABRef> arg = new ABRefParam("const",refer);

        ope.Inputs[0] = arg;
        ope.Inputs[1] = text;

        //Test
        ABVec testValue = (ABVec)ope.Evaluate(ctx);
        if(testValue == null)
        {
            Debug.Log("OK pas de ABRef matchant avec 'test1'");            
        } else
        {
            ABVec expected = TypeFactory.CreateEmptyVec();
            expected.X = x;
            expected.Y = y;
            if (testValue.X == expected.X && testValue.Y == expected.Y)
            {
                Debug.Log(this.GetType().Name + " OK");
            }
            else
            {
                Debug.LogError(this.GetType().Name + " KO ! result for (" + vec.ToString() + ") should be '" + expected + "' but it is '" + testValue + "'");
            }
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
