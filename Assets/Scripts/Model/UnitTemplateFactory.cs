using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplateInitializer {
    private static readonly string PRYSME_BEHAVIOR_IDENTIFIER = "prysme_behavior";
    private static readonly int PRYSME_COMPO_ID = 0;
    private static readonly string PRYSME_COMPO_NAME = "prysme power";
    private static readonly Color32 PRYSME_COMPO_COLOR = Color.white;
    private static readonly int PRYSME_COMPO_PRODCOST = 0;
    private static readonly float PRYSME_MOVESPD = 0f;
    private static readonly float PRYSME_ACTSPD = 5f;
    private static readonly float PRYSME_VITALITY = 100f;
    private static readonly float PRYSME_STAMINA = 0f;
    private static readonly float PRYSME_VISION_RANGE_BUFF = 0f;
    private static readonly float PRYSME_VIBRATION_RANGE_BUFF = 0f;
    private static readonly float PRYSME_HEAT_RANGE_BUFF = 0f;
    private static readonly float PRYSME_SMELL_RANGE_BUFF = 0f;
    private static readonly bool PRYSME_VISION_INDETECTABLE = false;
    private static readonly bool PRYSME_VIBRATION_INDETECTABLE = false;
    private static readonly bool PRYSME_HEAT_INDETECTABLE = false;
    private static readonly bool PRYSME_SMELL_INDETECTABLE = false;
    private static readonly bool PRYSME_GOTO_HOLD = false;
    private static readonly bool PRYSME_STRIKE = false;
    private static readonly bool PRYSME_PICK_DROP = false;
    private static readonly bool PRYSME_LAY = true;
    private static readonly float PRYSME_VISION_RANGE = 0f;
    private static readonly float PRYSME_VIBRATION_RANGE = 0f;
    private static readonly float PRYSME_HEAT_RANGE = 0f;
    private static readonly float PRYSME_SMELL_RANGE = 0f;

    public static void InitTemplate(Cast cast, GameObject agentTemplate, GameObject componentTemplate)
    {
		AgentEntity agentEntity = agentTemplate.GetComponent<AgentEntity>();
		agentEntity.BehaviorModelIdentifier = cast.BehaviorModelIdentifier;
        //Set Head Components
        GameObject headObject = agentTemplate.transform.Find("Head").gameObject;
        foreach (ComponentInfo agentComponent in cast.Head)
        {
            GameObject componentObject = GameObject.Instantiate(componentTemplate);
            componentObject.transform.parent = headObject.transform;
            CopyComponentValues(agentComponent, componentObject.GetComponent<AgentComponent>());
        }
        //Set Tail Components
        GameObject tailObject = agentTemplate.transform.Find("Tail").gameObject;
        foreach (ComponentInfo agentComponent in cast.Tail)
        {
            GameObject componentObject = GameObject.Instantiate(componentTemplate);
            componentObject.transform.parent = tailObject.transform;
            CopyComponentValues(agentComponent, componentObject.GetComponent<AgentComponent>());
        }
    }

    public static void InitPrysmeTemplate(GameObject agentTemplate, GameObject componentTemplate)
    {
        AgentEntity agentEntity = agentTemplate.GetComponent<AgentEntity>();
        agentEntity.BehaviorModelIdentifier = PRYSME_BEHAVIOR_IDENTIFIER;

        //Set only component on prisme head
        GameObject headObject = agentTemplate.transform.Find("Head").gameObject;
        GameObject componentObject = GameObject.Instantiate(componentTemplate);
        componentObject.transform.parent = headObject.transform;
        AgentComponent prysmeComponent = componentObject.GetComponent<AgentComponent>();
        PhyJoin phyJoin = prysmeComponent.GetComponent<PhyJoin>();
        phyJoin.enabled = false;

        InitPrysmeComponentValues(prysmeComponent);
    }

    private static void InitPrysmeComponentValues(AgentComponent prysmeComponent)
    {
        //Generic
        prysmeComponent.Id = PRYSME_COMPO_ID;
        prysmeComponent.Name = PRYSME_COMPO_NAME;
        prysmeComponent.Color = PRYSME_COMPO_COLOR;
        prysmeComponent.ProdCost = PRYSME_COMPO_PRODCOST;

        //Passive Buff
        prysmeComponent.MoveSpeedBuff = PRYSME_MOVESPD;
        prysmeComponent.ActionSpeedBuff = PRYSME_ACTSPD;
        prysmeComponent.StrengthBuff = PRYSME_MOVESPD;
        prysmeComponent.VitalityBuff = PRYSME_VITALITY;
        prysmeComponent.StaminaBuff = PRYSME_STAMINA;
        prysmeComponent.VisionRangeBuff = PRYSME_VISION_RANGE_BUFF;
        prysmeComponent.VibrationRangeBuff = PRYSME_VIBRATION_RANGE_BUFF;
        prysmeComponent.HeatRangeBuff = PRYSME_HEAT_RANGE_BUFF;
        prysmeComponent.SmellRangeBuff = PRYSME_SMELL_RANGE_BUFF;
        prysmeComponent.VisionIndetectable = PRYSME_VISION_INDETECTABLE;
        prysmeComponent.VibrationIndetectable = PRYSME_VIBRATION_INDETECTABLE;
        prysmeComponent.HeatIndetectable = PRYSME_HEAT_INDETECTABLE;
        prysmeComponent.SmellIndetectable = PRYSME_SMELL_INDETECTABLE;

        //Actions
        prysmeComponent.EnableGotoHold = PRYSME_GOTO_HOLD;
        prysmeComponent.EnableStrike = PRYSME_STRIKE;
        prysmeComponent.EnablePickDrop = PRYSME_PICK_DROP;
        prysmeComponent.EnableLay = PRYSME_LAY;

        //Sensors
        prysmeComponent.VisionRange = PRYSME_VISION_RANGE;
        prysmeComponent.VibrationRange = PRYSME_VIBRATION_RANGE;
        prysmeComponent.HeatRange = PRYSME_HEAT_RANGE;
        prysmeComponent.SmellRange = PRYSME_SMELL_RANGE;
    }

    public static void CopyComponentValues(ComponentInfo src, AgentComponent dst)
    {
        //Generic
        dst.Id = src.Id;
        dst.Name = src.Name;
        dst.Color = src.Color;
        dst.ProdCost = src.ProdCost;

        //Passive Buff
        dst.MoveSpeedBuff = src.MoveSpeedBuff;
        dst.ActionSpeedBuff = src.ActionSpeedBuff;
        dst.StrengthBuff = src.StrengthBuff;
        dst.VitalityBuff = src.VitalityBuff;
        dst.StaminaBuff = src.StaminaBuff;
        dst.VisionRangeBuff = src.VisionRangeBuff;
        dst.VibrationRangeBuff = src.VibrationRangeBuff;
        dst.HeatRangeBuff = src.HeatRangeBuff;
        dst.SmellRangeBuff = src.SmellRangeBuff;
        dst.VisionIndetectable = src.VisionIndetectable;
        dst.VibrationIndetectable = src.VibrationIndetectable;
        dst.HeatIndetectable = src.HeatIndetectable;
        dst.SmellIndetectable = src.SmellIndetectable;

        //Actions
        dst.EnableGotoHold = src.EnableGotoHold;
        dst.EnableStrike = src.EnableStrike;
        dst.EnablePickDrop = src.EnablePickDrop;
        dst.EnableLay = src.EnableLay;

        //Sensors
        dst.VisionRange = src.VisionRange;
        dst.VibrationRange = src.VibrationRange;
        dst.HeatRange = src.HeatRange;
        dst.SmellRange = src.SmellRange;

        //Not Handled
        dst.NotHandledTokens.AddRange(src.NotHandledTokens);
    }
}
