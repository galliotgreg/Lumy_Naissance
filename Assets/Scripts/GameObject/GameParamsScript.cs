using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParamsScript : MonoBehaviour {
    [AttrName(Identifier = "seamSize")]
    [SerializeField]
    private int seamSize;

    [AttrName(Identifier = "caveMultiplier")]
    [SerializeField]
    private float caveMultiplier;

    [AttrName(Identifier = "totalTime")]
    [SerializeField]
    private float totalTime;

    [AttrName(Identifier = "time")]
    [SerializeField]
    private float time;

    [AttrName(Identifier = "maxPop")]
    [SerializeField]
    private int maxPop;

    public int SeamSize
    {
        get
        {
            return seamSize;
        }

        set
        {
            seamSize = value;
        }
    }

    public float CaveMultiplier
    {
        get
        {
            return caveMultiplier;
        }

        set
        {
            caveMultiplier = value;
        }
    }

    public float TotalTime
    {
        get
        {
            return totalTime;
        }

        set
        {
            totalTime = value;
        }
    }

    public float Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }
    }

    public int MaxPop
    {
        get
        {
            return maxPop;
        }

        set
        {
            maxPop = value;
        }
    }

    // Use this for initialization
    void Start () {
        GameManager.instance.TimerLeft = totalTime;
    }
	
	// Update is called once per frame
	void Update () {
        time = totalTime - GameManager.instance.TimerLeft;
	}
}
