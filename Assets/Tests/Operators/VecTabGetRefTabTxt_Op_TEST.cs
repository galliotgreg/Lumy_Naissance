using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecTabGetRefTabTxt_Op_TEST : MonoBehaviour {
    [SerializeField]
    private string symbol = "V[]getR[]T";

    // Use this for initialization
    void Start()
    {
        // asset = tableau de taille 5
        Test("test1");

        //assert = tableau de taille 4
        Test("test2");
    }

    private void Test(string texte)
    {
        ABTable<ABRef> tab = TypeFactory.CreateEmptyTable<ABRef>();
        tab.Values = new ABRef[9];
        for (int i=0; i <5; i++)
        {
            ABRef refer = TypeFactory.CreateEmptyRef();
            ABVec vec = TypeFactory.CreateEmptyVec();
            vec.X = Random.value * 100 - 50;
            vec.Y = Random.value * 100 - 50;
            refer.SetAttr("test1", vec);
            tab.Values[i] = refer;
        }
        for (int i = 5; i < 9; i++)
        {
            ABRef refer = TypeFactory.CreateEmptyRef();
            ABVec vec = TypeFactory.CreateEmptyVec();
            vec.X = Random.value * 100 - 50;
            vec.Y = Random.value * 100 - 50;
            refer.SetAttr("test2", vec);
            tab.Values[i] = refer;
        }        

        //Build test operator
        ABContext ctx = new ABContext();
        AB_VecTab_Get_RefTab_Txt_Operator ope = (AB_VecTab_Get_RefTab_Txt_Operator) ABOperatorFactory.CreateOperator(symbol) ;
        ABParam<ABTable<ABRef>> arg = new ABTableParam<ABRef>("const", tab);
        ABParam<ABText> tParam = ABParamFactory.CreateTextParam("const", texte);
        ope.Inputs[0] = arg;
        ope.Inputs[1] = tParam;

        //Test
        ABTable<ABVec> testValue = ope.Evaluate(ctx);
        ABVec[] valuesExpected = new ABVec[tab.Values.Length];
        for (int i = 0; i < tab.Values.Length; i++)
        {
            valuesExpected[i] = (ABVec)tab.Values[i].GetAttr(tParam.Value.Value);
        }

        if (testValue.Values.Length == valuesExpected.Length)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result should be '" + valuesExpected.ToString() + "' but it is '" + testValue.ToString() + "'");        
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
