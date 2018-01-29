using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABRef : IABSimpleType
{
    private Dictionary<String, IABType> attrs = new Dictionary<String, IABType>();

    public void SetAttr(String key, IABType type)
    {
        attrs[key] = type;
    }

    public IABType GetAttr(String key)
    {
        IABType attr = null;
        attrs.TryGetValue(key, out attr);
        return attr;
    }
}
