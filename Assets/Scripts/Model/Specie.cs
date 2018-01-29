using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specie {
    private string name;
    private string queenCastName;
    private Dictionary<string, Cast> casts = new Dictionary<string, Cast>();
    private float redResAmount;
    private float greenResAmount;
    private float blueResAmount;

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

    public Dictionary<string, Cast> Casts
    {
        get
        {
            return casts;
        }

        set
        {
            casts = value;
        }
    }

    public float RedResAmount
    {
        get
        {
            return redResAmount;
        }

        set
        {
            redResAmount = value;
        }
    }

    public float GreenResAmount
    {
        get
        {
            return greenResAmount;
        }

        set
        {
            greenResAmount = value;
        }
    }

    public float BlueResAmount
    {
        get
        {
            return blueResAmount;
        }

        set
        {
            blueResAmount = value;
        }
    }

    public string QueenCastName
    {
        get
        {
            return queenCastName;
        }

        set
        {
            queenCastName = value;
        }
    }
}
