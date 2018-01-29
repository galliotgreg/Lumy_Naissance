using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentComponent : MonoBehaviour {
    //Generic
    [SerializeField]
	private int id;
    [SerializeField]
	private string name;
    [SerializeField]
	private Color32 color;
    [SerializeField]
	private float prodCost;
    [SerializeField]
	private float buyCost;

    //Passive Buffs
    [SerializeField]
	private float moveSpeedBuff;
    [SerializeField]
	private float actionSpeedBuff;
    [SerializeField]
	private float strengthBuff;
    [SerializeField]
	private float vitalityBuff;
    [SerializeField]
	private float staminaBuff;
    [SerializeField]
	private float visionRangeBuff;
    [SerializeField]
	private float vibrationRangeBuff;
    [SerializeField]
	private float heatRangeBuff;
    [SerializeField]
	private float smellRangeBuff;
    [SerializeField]
	private bool visionIndetectable;
    [SerializeField]
	private bool vibrationIndetectable;
    [SerializeField]
	private bool heatIndetectable;
    [SerializeField]
	private bool smellIndetectable;

    //Actions
	private bool enableGotoHold;
    [SerializeField]
	private bool enableStrike;
    [SerializeField]
	private bool enablePickDrop;
    [SerializeField]
	private bool enableLay;

    //Sensors
    [SerializeField]
	private float visionRange;
    [SerializeField]
	private float vibrationRange;
    [SerializeField]
	private float heatRange;
    [SerializeField]
	private float smellRange;

    //Not handled
    [SerializeField]
	private List<String> notHandledTokens = new List<String>();

	#region GETTERS and SETTERS
	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public Color32 Color {
		get {
			return color;
		}
		set {
			color = value;
		}
	}

	public float ProdCost {
		get {
			return prodCost;
		}
		set {
			prodCost = value;
		}
	}

	public float BuyCost {
		get {
			return buyCost;
		}
		set {
			buyCost = value;
		}
	}

	#region PASSIVES

	public float MoveSpeedBuff {
		get {
			return moveSpeedBuff;
		}
		set {
			moveSpeedBuff = value;
		}
	}

	public float ActionSpeedBuff {
		get {
			return actionSpeedBuff;
		}
		set {
			actionSpeedBuff = value;
		}
	}

	public float StrengthBuff {
		get {
			return strengthBuff;
		}
		set {
			strengthBuff = value;
		}
	}

	public float VitalityBuff {
		get {
			return vitalityBuff;
		}
		set {
			vitalityBuff = value;
		}
	}

	public float StaminaBuff {
		get {
			return staminaBuff;
		}
		set {
			staminaBuff = value;
		}
	}

	public float VisionRangeBuff {
		get {
			return visionRangeBuff;
		}
		set {
			visionRangeBuff = value;
		}
	}

	public float VibrationRangeBuff {
		get {
			return vibrationRangeBuff;
		}
		set {
			vibrationRangeBuff = value;
		}
	}

	public float HeatRangeBuff {
		get {
			return heatRangeBuff;
		}
		set {
			heatRangeBuff = value;
		}
	}

	public float SmellRangeBuff {
		get {
			return smellRangeBuff;
		}
		set {
			smellRangeBuff = value;
		}
	}

	public bool VisionIndetectable {
		get {
			return visionIndetectable;
		}
		set {
			visionIndetectable = value;
		}
	}

	public bool VibrationIndetectable {
		get {
			return vibrationIndetectable;
		}
		set {
			vibrationIndetectable = value;
		}
	}

	public bool HeatIndetectable {
		get {
			return heatIndetectable;
		}
		set {
			heatIndetectable = value;
		}
	}

	public bool SmellIndetectable {
		get {
			return smellIndetectable;
		}
		set {
			smellIndetectable = value;
		}
	}

	#endregion

	#region ACTIONS att
	public bool EnableGotoHold {
		get {
			return enableGotoHold;
		}
		set {
			enableGotoHold = value;
		}
	}

	public bool EnableStrike {
		get {
			return enableStrike;
		}
		set {
			enableStrike = value;
		}
	}

	public bool EnablePickDrop {
		get {
			return enablePickDrop;
		}
		set {
			enablePickDrop = value;
		}
	}

	public bool EnableLay {
		get {
			return enableLay;
		}
		set {
			enableLay = value;
		}
	}
	#endregion

	#region Sensors
	public float VisionRange {
		get {
			return visionRange;
		}
		set {
			visionRange = value;
		}
	}

	public float VibrationRange {
		get {
			return vibrationRange;
		}
		set {
			vibrationRange = value;
		}
	}

	public float HeatRange {
		get {
			return heatRange;
		}
		set {
			heatRange = value;
		}
	}

	public float SmellRange {
		get {
			return smellRange;
		}
		set {
			smellRange = value;
		}
	}
	#endregion

	public List<String> NotHandledTokens {
		get {
			return notHandledTokens;
		}
		set {
			notHandledTokens = value;
		}
	}
	#endregion

	public void init(){
		this.notHandledTokens = new List<string>();
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
