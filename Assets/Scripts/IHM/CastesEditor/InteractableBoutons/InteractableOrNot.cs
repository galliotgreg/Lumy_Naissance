using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableOrNot : MonoBehaviour {


    public Button vitalityPlus;//
    public Button vitalityLess;

    public Button staminaPlus;//
    public Button staminaLess;

    public Button strengthPlus;//
    public Button strengthLess;

    public Button actSpeedPlus;//
    public Button actSpeedLess;

    public Button moveSpeedPlus;//
    public Button moveSpeedLess;

    public Button visionRangePlus;//
    public Button visionRangeLess;
    
    public Button pickRangePlus;//
    public Button pickRangeLess;

    public Button atkRangePlus;//
    public Button atkRangeLess;


    #region  Allow Increase / Decrease
    // Update is called once per frame
    void Update()
    {
        //VITA+
        if (SwarmEditUIController.instance.CanIncrVitality())
        {
            vitalityPlus.interactable = true;
        }
        else
        {
            vitalityPlus.interactable = false;
        }
        //VITA-
        if (SwarmEditUIController.instance.CanDecrVitality())
        {
            vitalityLess.interactable = true;
        }
        else
        {
            vitalityLess.interactable = false;
        }

        //STAMINA+
        if (SwarmEditUIController.instance.CanIncrStamina())
        {
            staminaPlus.interactable = true;
        }
        else
        {
            staminaPlus.interactable = false;
        }
        //STAMINA-
        if (SwarmEditUIController.instance.CanDecrStamina())
        {
            staminaLess.interactable = true;
        }
        else
        {
            staminaLess.interactable = false;
        }

        //STRENGTH+
        if (SwarmEditUIController.instance.CanIncrStrength())
        {
            strengthPlus.interactable = true;
        }
        else
        {
            strengthPlus.interactable = false;
        }
        //STRENGTH-
        if (SwarmEditUIController.instance.CanDecrStrength())
        {
            strengthLess.interactable = true;
        }
        else
        {
            strengthLess.interactable = false;
        }

        //ACTSPEED+
        if (SwarmEditUIController.instance.CanIncrActSpeed())
        {
            actSpeedPlus.interactable = true;
        }
        else
        {
            actSpeedPlus.interactable = false;
        }
        //ACTSPEED-
        if (SwarmEditUIController.instance.CanDecrActSpeed())
        {
            actSpeedLess.interactable = true;
        }
        else
        {
            actSpeedLess.interactable = false;
        }

        //MOVESPEED+
        if (SwarmEditUIController.instance.CanIncrMoveSpeed())
        {
            moveSpeedPlus.interactable = true;
        }
        else
        {
            moveSpeedPlus.interactable = false;
        }
        //MOVESPEED-
        if (SwarmEditUIController.instance.CanDecrMoveSpeed())
        {
            moveSpeedLess.interactable = true;
        }
        else
        {
            moveSpeedLess.interactable = false;
        }

        //VISION+
        if (SwarmEditUIController.instance.CanIncrVisionRange())
        {
            visionRangePlus.interactable = true;
        }
        else
        {
            visionRangePlus.interactable = false;
        }
        //VISION-
        if (SwarmEditUIController.instance.CanDecrVisionRange())
        {
            visionRangeLess.interactable = true;
        }
        else
        {
            visionRangeLess.interactable = false;
        }

        //PICKRANGE+
        if (SwarmEditUIController.instance.CanIncrPickRange())
        {
            pickRangePlus.interactable = true;
        }
        else
        {
            pickRangePlus.interactable = false;
        }
        //PICKRANGE-
        if (SwarmEditUIController.instance.CanDecrPickRange())
        {
            pickRangeLess.interactable = true;
        }
        else
        {
            pickRangeLess.interactable = false;
        }

        //ATKRANGE+
        if (SwarmEditUIController.instance.CanIncrAtkRange())
        {
            atkRangePlus.interactable = true;
        }
        else
        {
            atkRangePlus.interactable = false;
        }
        //ATKRANGE-
        if (SwarmEditUIController.instance.CanDecrAtkRange())
        {
            atkRangeLess.interactable = true;
        }
        else
        {
            atkRangeLess.interactable = false;
        }
        
    }
    #endregion

}
