using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyBone : MonoBehaviour {
    //UI Vars
    [SerializeField]
    private Vector2 origin;
    [SerializeField]
    private Vector2 end;
    [SerializeField]
    private float localTeta = 0f;
    [SerializeField]
    private float worldTeta = 0f;
    [SerializeField]
    private float lenght = 0.5f;
    [SerializeField]
    private PhyJoin baseJoin;
    [SerializeField]
    private PhyJoin headJoin;

    [SerializeField]
    private Vector2 IKTarget;

    public PhyJoin BaseJoin
    {
        get
        {
            return baseJoin;
        }

        set
        {
            baseJoin = value;
        }
    }

    public PhyJoin HeadJoin
    {
        get
        {
            return headJoin;
        }

        set
        {
            headJoin = value;
        }
    }

    public float Lenght
    {
        get
        {
            return lenght;
        }

        set
        {
            lenght = value;
        }
    }

    public float LocalTeta
    {
        get
        {
            return localTeta;
        }

        set
        {
            localTeta = value;
        }
    }

    public float WorldTeta
    {
        get
        {
            return worldTeta;
        }

        set
        {
            worldTeta = value;
        }
    }

    public Vector2 Origin
    {
        get
        {
            return origin;
        }

        set
        {
            origin = value;
        }
    }

    public Vector2 End
    {
        get
        {
            return end;
        }

        set
        {
            end = value;
        }
    }

    public void ComputeEnd()
    {
        Vector2 dir = new Vector2(
            Lenght * Mathf.Cos(Mathf.Deg2Rad * WorldTeta),
            Lenght * Mathf.Sin(Mathf.Deg2Rad * WorldTeta));
        End = Origin + dir;
    }
}
