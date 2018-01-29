using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentContext : MonoBehaviour
{
    /*[BindParam(Identifier = "testBool")]
    bool boolParam = true;
    [BindParam(Identifier = "testByte")]
    byte byteParam = 8;
    [BindParam(Identifier = "testShort")]
    short shortParam = -1;
    [BindParam(Identifier = "testInt")]
    int intParam = 3;
    [BindParam(Identifier = "testLong")]
    long longParam = 1024;
    [BindParam(Identifier = "testFloat")]
    float floatParam = 5.2f;
    [BindParam(Identifier = "testDouble")]
    double doubleParam = -5.12547;
    [BindParam(Identifier = "testString")]
    string stringParam = "hello world!";
    [BindParam(Identifier = "testVector2")]
    Vector2 vec2Param = new Vector2(0, 1);

    [BindParam(Identifier = "testBool[]")]
    bool[] boolParamTab = new bool[3];
    [BindParam(Identifier = "testByte[]")]
    byte[] byteParamTab = new byte[3];
    [BindParam(Identifier = "testShort[]")]
    short[] shortParamTab = new short[3];
    [BindParam(Identifier = "testInt[]")]
    int[] intParamTab = new int[3];
    [BindParam(Identifier = "testLong[]")]
    long[] longParamTab = new long[3];
    [BindParam(Identifier = "testFloat[]")]
    float[] floatParamTab = new float[3];
    [BindParam(Identifier = "testDouble[]")]
    double[] doubleParamTab = new double[3];
    [BindParam(Identifier = "testString[]")]
    string[] stringParamTab = new string[3];
    [BindParam(Identifier = "testVector2[]")]
    Vector2[] vec2ParamTab = new Vector2[3];

    [BindParam(Identifier = "prey")]
    [SerializeField]
    public GameObject locust;
    [BindParam(Identifier = "preys")]
    [SerializeField]
    public GameObject[] locusts;
    [BindParam(Identifier = "curPos")]
    [SerializeField]
    public Vector2 location;
    [BindParam(Identifier = "trgPos")]
    [SerializeField]
    public Vector2 targetPt;
    [BindParam(Identifier = "hive")]
    [SerializeField]
    public GameObject hive;
    [BindParam(Identifier = "rRes")]
    [SerializeField]
    public GameObject[] redResources = new GameObject[0];
    [BindParam(Identifier = "gRes")]
    [SerializeField]
    public GameObject[] greenResources = new GameObject[0];
    [BindParam(Identifier = "bRes")]
    [SerializeField]
    public GameObject[] blueResources = new GameObject[0];
    [BindParam(Identifier = "enemies")]
    [SerializeField]
    public GameObject[] enemies = new GameObject[0];

    // Use this for initialization
    void Start()
    {
        location = new Vector3(transform.position.x, transform.position.z);
        targetPt = location;

        AgentEntity agentScript = gameObject.GetComponent<AgentEntity>();
        if (agentScript.Authority == PlayerAuthority.Player1)
        {
            hive = GameManager.instance.P1_hive;
        } else if (agentScript.Authority == PlayerAuthority.Player2)
        {
            hive = GameManager.instance.P2_hive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        location = new Vector3(transform.position.x, transform.position.z);
    }*/
    [BindParam(Identifier = "self")]
    [SerializeField]
    private GameObject self;
    [BindParam(Identifier = "hive")]
    [SerializeField]
    private GameObject home;

    [BindParam(Identifier = "enemies")]
    [SerializeField]
    private GameObject[] enemies = new GameObject[0];
    [BindParam(Identifier = "allies")]
    [SerializeField]
    private GameObject[] allies = new GameObject[0];
    [BindParam(Identifier = "resources")]
    [SerializeField]
    private GameObject[] resources = new GameObject[0];
    [BindParam(Identifier = "traces")]
    [SerializeField]
    private GameObject[] traces = new GameObject[0];

    public GameObject Self
    {
        get
        {
            return self;
        }

        set
        {
            self = value;
        }
    }

    public GameObject Home
    {
        get
        {
            return home;
        }

        set
        {
            home = value;
        }
    }

    public GameObject[] Enemies
    {
        get
        {
            return enemies;
        }

        set
        {
            enemies = value;
        }
    }

    public GameObject[] Allies
    {
        get
        {
            return allies;
        }

        set
        {
            allies = value;
        }
    }

    public GameObject[] Resources
    {
        get
        {
            return resources;
        }

        set
        {
            resources = value;
        }
    }

    public GameObject[] Traces
    {
        get
        {
            return traces;
        }

        set
        {
            traces = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        AgentEntity agentScript = gameObject.GetComponent<AgentEntity>();
        Home = GameManager.instance.GetHome(agentScript.Authority).gameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
