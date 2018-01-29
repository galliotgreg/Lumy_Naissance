using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrName : System.Attribute {
    private string identifier;

    public string Identifier
    {
        get
        {
            return identifier;
        }

        set
        {
            identifier = value;
        }
    }
}
