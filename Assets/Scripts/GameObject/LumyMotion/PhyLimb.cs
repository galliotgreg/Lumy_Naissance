using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyLimb {
    private List<PhyLimb> childs = new List<PhyLimb>();
    private List<PhyBone> bones = new List<PhyBone>();

    public List<PhyLimb> Childs
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

    public List<PhyBone> Bones
    {
        get
        {
            return bones;
        }

        set
        {
            bones = value;
        }
    }
}
