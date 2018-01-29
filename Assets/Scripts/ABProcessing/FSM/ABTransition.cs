using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTransition {
    private int id;
    private ABState start;
    private ABState end;
    private AB_BoolGate_Operator condition;

    public ABTransition(int id, ABState start, ABState end)
    {
        this.id = id;
        this.start = start;
        this.end = end;
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public ABState Start
    {
        get
        {
            return start;
        }

        set
        {
            start = value;
        }
    }

    public ABState End
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

    public AB_BoolGate_Operator Condition
    {
        get
        {
            return condition;
        }

        set
        {
            condition = value;
        }
    }
}
