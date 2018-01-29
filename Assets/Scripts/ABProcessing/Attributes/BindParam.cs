using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindParam : System.Attribute {
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
