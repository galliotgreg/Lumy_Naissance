using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABContext {
    private List<IABParam> parameters = new List<IABParam>();

    public IABParam GetParam(string identifier)
    {
        foreach (IABParam param in parameters)
        {
            if (param.Identifier == identifier)
            {
                return param;
            }
        }
        return null;
    }

    public void SetParam(IABParam param)
    {
        //Prevent from duplicate param
        if (GetParam(param.Identifier) != null)
        {
            throw new NotSupportedException();
        }

        parameters.Add(param);
    }
}
