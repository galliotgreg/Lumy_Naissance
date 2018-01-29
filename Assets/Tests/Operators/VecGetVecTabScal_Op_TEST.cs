using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecGetVecTabScal_Op_TEST : MonoBehaviour {
    [SerializeField]
    private string symbol = "VgetV[]S";
    [SerializeField]
    private int nbTests = 100;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < nbTests; i++)
        {
            RandomizeTest();
        }
    }

    private void RandomizeTest()
    {
        ABTable<ABVec> tab = TypeFactory.CreateEmptyTable<ABVec>();
        float[] xTab = new float[10];        
        float[] yTab = new float[10];
        for (int i = 0; i < 10; i++)
        {
            //Generate random values
            float x = Random.value * 100 - 50;
            float y= Random.value * 100 - 50;
            xTab[i] = x;
            yTab[i] = y;
        }
        //Build test operator
        ABContext ctx = new ABContext();
        AB_Vec_Get_VecTab_Scal_Operator ope = (AB_Vec_Get_VecTab_Scal_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABVec>> arg1 = (ABParam<ABTable<ABVec>>)ABParamFactory.CreateVecTableParam("const", xTab, yTab);
        ABParam<ABScalar> arg2 = ABParamFactory.CreateScalarParam("const", 5);
        ope.Inputs[0] = arg1;
        ope.Inputs[1] = arg2;

        //Test
        float xVec = ope.Evaluate(ctx).X;
        float yVec = ope.Evaluate(ctx).Y;
        float xExpected = xTab[5];
        float yExpected = yTab[5];
        if (xVec == xExpected && yVec == yExpected)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result should be (" + xExpected +" ,"+ yExpected + ") but it is (" + xVec +"," +yVec+ ")");
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
