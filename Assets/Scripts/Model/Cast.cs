using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast {
    private string name;
    private string behaviorModelIdentifier;
    private List<ComponentInfo> head = new List<ComponentInfo>();
    private List<ComponentInfo> tail = new List<ComponentInfo>();
    private Cast parent;
    private IList<Cast> childs = new List<Cast>();

    public string BehaviorModelIdentifier
    {
        get
        {
            return behaviorModelIdentifier;
        }

        set
        {
            behaviorModelIdentifier = value;
        }
    }

    public List<ComponentInfo> Head
    {
        get
        {
            return head;
        }

        set
        {
            head = value;
        }
    }

    public List<ComponentInfo> Tail
    {
        get
        {
            return tail;
        }

        set
        {
            tail = value;
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

    public Cast Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public IList<Cast> Childs
    {
        get
        {
            return childs;
        }

        set
        {
            childs = value;
        }
    }
}
