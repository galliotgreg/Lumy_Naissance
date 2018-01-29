using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefTabAggRefStar_Op_TEST : MonoBehaviour {
    [SerializeField]
    private string symbol = "R[]aggR*";

    // Use this for initialization
    void Start () {
        Test();
	}

    private void Test()
    {
        //Build inputs
        ABContext ctx = new ABContext();
        AB_RefTab_Agg_RefStar_Operator ope = (AB_RefTab_Agg_RefStar_Operator) ABOperatorFactory.CreateOperator(symbol);
        ABParam<ABRef> ref1 = CreateRef(0);
        ABParam<ABRef> ref2 = CreateRef(1);
        ABParam<ABTable<ABRef>> tabRef1 = CreateTabRef(2, 6);
        ABParam<ABRef> ref3 = CreateRef(6);
        ABParam<ABRef> ref4 = CreateRef(7);
        ABParam<ABTable<ABRef>> tabRef2 = CreateTabRef(8, 11);
        ABParam<ABRef> ref5 = CreateRef(11);
        ope.Inputs[0] = ref1;
        ope.Inputs[1] = ref2;
        ope.Inputs[3] = tabRef1;
        ope.Inputs[5] = ref3;
        ope.Inputs[6] = ref4;
        ope.Inputs[7] = tabRef2;
        ope.Inputs[12] = ref5;

        //Test
        float[] refVal = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        ABRef[] testVal = ope.Evaluate(ctx).Values;
        bool testOk = true;
        int failIdx = -1;
        for (int i = 0; i < refVal.Length; i++)
        {
            if (refVal[i] != ((ABScalar) testVal[i].GetAttr("Id")).Value)
            {
                testOk = false;
                failIdx = i;
                break;
            }
        }
        if (testOk)
        {
            Debug.Log(this.GetType().Name + " OK");
        }
        else
        {
            Debug.LogError(this.GetType().Name + " KO ! test value differs from reference at index " + failIdx);
        }
    }

    private ABParam<ABTable<ABRef>> CreateTabRef(int v1, int v2)
    {
        ABTable<ABRef> refTab = TypeFactory.CreateEmptyTable<ABRef>();
        refTab.Values = new ABRef[v2 - v1];
        for (int i = 0; i < refTab.Values.Length; i++)
        {
            refTab.Values[i] = CreateRef(v1 + i).Value;
        }
        ABParam<ABTable<ABRef>> param = new ABTableParam<ABRef>("const", refTab);

        return param;
    }

    private static ABParam<ABRef> CreateRef(float scalAttrVal)
    {
        ABRef myRef = TypeFactory.CreateEmptyRef();
        ABScalar scal = TypeFactory.CreateEmptyScalar();
        scal.Value = scalAttrVal;
        myRef.SetAttr("Id", scal);
        ABParam<ABRef> param = new ABRefParam("const", myRef);

        return param;
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
