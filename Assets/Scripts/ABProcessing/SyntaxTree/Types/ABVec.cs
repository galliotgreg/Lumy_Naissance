using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABVec : IABSimpleType
{
    private float x;
    private float y;

    public ABVec()
    {
        this.x = 0;
        this.y = 0;
    }

    public ABVec(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public float X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    public float Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }
}
