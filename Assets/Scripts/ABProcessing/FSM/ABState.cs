using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABState {
    private int id;
    private string name;
    private ABAction action;

    private List<ABTransition> outcomes;

    public ABState(int id, string name)
    {
        this.Id = id;
        this.name = name;
        this.outcomes = new List<ABTransition>();
    }

    public ABAction Action
    {
        get
        {
            return action;
        }

        set
        {
            action = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
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

    public List<ABTransition> Outcomes
    {
        get
        {
            return outcomes;
        }

        set
        {
            outcomes = value;
        }
    }
}
