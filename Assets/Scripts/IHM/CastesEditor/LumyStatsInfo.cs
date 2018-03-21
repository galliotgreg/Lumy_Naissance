using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumyStatsInfo {
    private int ptsMax = 10;

    private int vitality;
    private int stamina;
    private int strength;
    private int actSpeed;
    private int moveSpeed;
    private int visionRange;
    private int pickRange;
    private int atkRange;

    public int Vitality
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

    public int Stamina
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

    public int Strength
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

    public int ActSpeed
    {
        get
        {
            return actSpeed;
        }

        set
        {
            actSpeed = value;
        }
    }

    public int MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public int VisionRange
    {
        get
        {
            return visionRange;
        }

        set
        {
            visionRange = value;
        }
    }

    public int PickRange
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

    public int AtkRange
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

    public int PointsLeft
    {
        get
        {
            return ptsMax - vitality - stamina - strength - actSpeed
                - moveSpeed - visionRange - pickRange - atkRange;
        }
    }
}
