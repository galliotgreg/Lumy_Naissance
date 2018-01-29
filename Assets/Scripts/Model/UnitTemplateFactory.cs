using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplateInitializer {
    public static void InitTemplate(Cast cast, GameObject agentTemplate, GameObject componentTemplate)
    {
        AgentBehavior agentBehavior = agentTemplate.GetComponent<AgentBehavior>();
        agentBehavior.BehaviorModelIdentifier = cast.BehaviorModelIdentifier;
        //Set Head Components
        GameObject headObject = agentTemplate.transform.Find("Head").gameObject;
        foreach (AgentComponent agentComponent in cast.Head)
        {
            GameObject componentObject = GameObject.Instantiate(componentTemplate);
            componentObject.transform.parent = headObject.transform;
            CopyComponentValues(agentComponent, componentObject.GetComponent<AgentComponent>());
        }
        //Set Tail Components
        GameObject tailObject = agentTemplate.transform.Find("Tail").gameObject;
        foreach (AgentComponent agentComponent in cast.Tail)
        {
            GameObject componentObject = GameObject.Instantiate(componentTemplate);
            componentObject.transform.parent = tailObject.transform;
            CopyComponentValues(agentComponent, componentObject.GetComponent<AgentComponent>());
        }
    }

    private static void CopyComponentValues(AgentComponent src, AgentComponent dst)
    {
        //Generic
        dst.Id = src.Id;
        dst.Name = src.Name;
        dst.Color = src.Color;
        dst.ProdCost = src.ProdCost;
        dst.BuyCost = src.BuyCost;

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
