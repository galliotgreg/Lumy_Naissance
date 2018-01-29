using System.Collections;
using System.Linq;
using UnityEngine;

public class ScalMinIdScalTab_Op_TEST : MonoBehaviour {

    [SerializeField]
    private string symbol = "SminIdS[]";
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
        float[] tab = new float[10];        

        for (int i = 0; i < 10; i++)
        {
            //Generate random values
            float r = Random.value * 100 - 50;
            tab[i] = r;
        }                             

        //Build test operator
        ABContext ctx = new ABContext();
        AB_Scal_MinId_ScalTab_Operator ope = (AB_Scal_MinId_ScalTab_Operator)ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABTable<ABScalar>> arg = (ABParam<ABTable<ABScalar>>) ABParamFactory.CreateScalarTableParam("const", tab);        
        ope.Inputs[0] = arg;        

        //Test
        int testValue = (int)ope.Evaluate(ctx).Value;
        int expected = 0;
        for(int i = 0; i < tab.Length; i++)
        {
            if (tab[i] == tab.Min())
                expected = i;
        }
        if (testValue == expected)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! result for (" + tab.ToString() + ") should be '" + expected + "' but it is '" + testValue + "'");
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
