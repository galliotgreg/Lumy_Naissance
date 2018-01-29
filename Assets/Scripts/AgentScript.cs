using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour {
    [AttrName(Identifier = "cast")]
    [SerializeField]
    private string cast;
    [AttrName(Identifier = "curPos")]
    [SerializeField]
    private Vector2 curPos;
    [AttrName(Identifier = "trgPos")]
    [SerializeField]
    private Vector2 trgPos;
    [AttrName(Identifier = "vitalityMax ")]
    [SerializeField]
    private float vitalityMax;
    [AttrName(Identifier = "vitality ")]
    [SerializeField]
    private float vitality;
    [AttrName(Identifier = "strength ")]
    [SerializeField]
    private float strength;
    [AttrName(Identifier = "stamina ")]
    [SerializeField]
    private float stamina;
    [AttrName(Identifier = "actSpd ")]
    [SerializeField]
    private float actSpd;
    [AttrName(Identifier = "moveSpd ")]
    [SerializeField]
    private float moveSpd;
    [AttrName(Identifier = "nbItemMax ")]
    [SerializeField]
    private int nbItemMax;
    [AttrName(Identifier = "nbItem ")]
    [SerializeField]
    private int nbItem;
    [AttrName(Identifier = "atkRange ")]
    [SerializeField]
    private float atkRange;
    [AttrName(Identifier = "pickRange ")]
    [SerializeField]
    private Vector2 pickRange;

    public string Cast
    {
        get
        {
            return cast;
        }

        set
        {
            cast = value;
        }
    }

    public Vector2 CurPos
    {
        get
        {
            return curPos;
        }

        set
        {
            curPos = value;
        }
    }

    public Vector2 TrgPos
    {
        get
        {
            return trgPos;
        }

        set
        {
            trgPos = value;
        }
    }

    public float VitalityMax
    {
        get
        {
            return vitalityMax;
        }

        set
        {
            vitalityMax = value;
        }
    }

    public float Vitality
    {
        get
        {
            return vitality;
        }

        set
        {
            vitality = value;
        }
    }

    public float Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public float ActSpd
    {
        get
        {
            return actSpd;
        }

        set
        {
            actSpd = value;
        }
    }

    public float MoveSpd
    {
        get
        {
            return moveSpd;
        }

        set
        {
            moveSpd = value;
        }
    }

    public int NbItemMax
    {
        get
        {
            return nbItemMax;
        }

        set
        {
            nbItemMax = value;
        }
    }

    public int NbItem
    {
        get
        {
            return nbItem;
        }

        set
        {
            nbItem = value;
        }
    }

    public float AtkRange
    {
        get
        {
            return atkRange;
        }

        set
        {
            atkRange = value;
        }
    }

    public Vector2 PickRange
    {
        get
        {
            return pickRange;
        }

        set
        {
            pickRange = value;
        }
    }

    // Use this for initialization
    void Start () {
        CurPos = new Vector3(transform.position.x, transform.position.z);
        TrgPos = CurPos;
    }
	
	// Update is called once per frame
	void Update () {
        CurPos = new Vector3(transform.position.x, transform.position.z);
    }
}
