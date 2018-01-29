using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABNode {
    protected ABNode output;

    public ABNode Output
    {
        get
        {
            return output;
        }

        set
        {
            output = value;
        }
    }
}
