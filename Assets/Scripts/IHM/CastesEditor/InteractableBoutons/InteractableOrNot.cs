using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableOrNot : MonoBehaviour {

    [Header ("Set in inspector")]
    [SerializeField]
    private GameObject statBarPrefab;
    [SerializeField]
    private GameObject bckStatBarPrefab;
    [SerializeField]
    private float statBarHSpacing;
    [SerializeField]
    private float statBarVertSpacing;
    [Header("Stat Bars Params")]
    [SerializeField]
    private float statBarLeftXpos;
    [SerializeField]
    private float statBarRightXpos;
    [SerializeField]
    private float statBarYPos;
    [SerializeField]
    private Color32 statsColor;
    [SerializeField]
    private Color32 statsBaseColor;

    private bool isFirstRefresh = true;

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

    private List<GameObject> barLeftStatsList;
    private List<GameObject> barRightStatsList;

    // Use this for initialization
    /*
    void Start () {
        DisplayLeftBars();
        DisplayRightBars();
        ButtonListener();
    }

    #region First Refresh
    //First Refresh
    private void LateUpdate()
    {
        if (isFirstRefresh)
        {
            RefreshView();
            isFirstRefresh = false;
        }
    }

    private void RefreshView()
    {
        RefreshStrength();
        RefreshVisionRange();
        RefreshPickRange();
        RefreshAttackRange();
        RefreshVitality();
        RefreshStamina();
        RefreshMoveSpeed();
        RefreshActionSpeed();  
    }
    #endregion

    #region Display stat bars
    /// <summary>
    /// Instantiate left column stat bars
    /// </summary>
    private void DisplayLeftBars()
    {
        barLeftStatsList = new List<GameObject>();

        //Instantiate 4 rows
        for (int i = 0; i < 4; i++)
        {   //Instantiate 1 Prefab
            for (int j = 0; j < 3; j++)
            {
                GameObject statLeftBar = Instantiate(statBarPrefab, new Vector3(statBarLeftXpos + j * statBarHSpacing, statBarYPos - i * statBarVertSpacing, 0f), Quaternion.identity);
                statLeftBar.transform.SetParent(this.gameObject.transform, false);
                barLeftStatsList.Add(statLeftBar);
            }
        }
    }

    /// <summary>
    /// Instantiate right column stat bars
    /// </summary>
    private void DisplayRightBars()
    {
        barRightStatsList = new List<GameObject>();

        //Instantiate 4 rows
        for (int i = 0; i < 4; i++)
        {   //Instantiate 1 Prefab
            for (int j = 0; j < 3; j++)
            {
                GameObject statBar = Instantiate(statBarPrefab, new Vector3(statBarRightXpos + j * statBarHSpacing, statBarYPos - i * statBarVertSpacing, 0f), Quaternion.identity);
                statBar.transform.SetParent(this.gameObject.transform, false);
                barRightStatsList.Add(statBar);
            }
        }   
    }
    #endregion

    #region Button Listener
    private void ButtonListener()
    {
        //vitality
        vitalityLess.onClick.AddListener(RefreshVitality);
        vitalityPlus.onClick.AddListener(RefreshVitality);

        //stamina
        staminaLess.onClick.AddListener(RefreshStamina);
        staminaPlus.onClick.AddListener(RefreshStamina);

        //strength
        strengthLess.onClick.AddListener(RefreshStrength);
        strengthPlus.onClick.AddListener(RefreshStrength);

        //actionSpeed
        actSpeedLess.onClick.AddListener(RefreshActionSpeed);
        actSpeedPlus.onClick.AddListener(RefreshActionSpeed);

        //moveSpeed
        moveSpeedLess.onClick.AddListener(RefreshMoveSpeed);
        moveSpeedPlus.onClick.AddListener(RefreshMoveSpeed);

        //visionRange
        visionRangeLess.onClick.AddListener(RefreshVisionRange);
        visionRangePlus.onClick.AddListener(RefreshVisionRange);

        //pickRange
        pickRangeLess.onClick.AddListener(RefreshPickRange);
        pickRangePlus.onClick.AddListener(RefreshPickRange);

        //attackRange
        atkRangeLess.onClick.AddListener(RefreshAttackRange);
        atkRangePlus.onClick.AddListener(RefreshAttackRange);
    }

    private void SwapColor()
    {
       if(barLeftStatsList != null)
        {
            barLeftStatsList[0].GetComponent<Image>().color = new Color32(255,255,0,255);
        }
    }

    #endregion

    #region Refresh Stat Bars

    //Left column
    private void RefreshStrength()
    {
        //Clear stats
        for (int i = 0; i < 3; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.Strength)
        {
            case 0:
                break;
            case 1:
                barLeftStatsList[0].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barLeftStatsList[0].GetComponent<Image>().color = statsColor;
                barLeftStatsList[1].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barLeftStatsList[0].GetComponent<Image>().color = statsColor;
                barLeftStatsList[1].GetComponent<Image>().color = statsColor;
                barLeftStatsList[2].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
        
    }

    private void RefreshVisionRange()
    {
        //Clear stats
        for (int i = 3; i < 6; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.VisionRange)
        {
            case 0:
                break;
            case 1:
                barLeftStatsList[3].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barLeftStatsList[3].GetComponent<Image>().color = statsColor;
                barLeftStatsList[4].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barLeftStatsList[3].GetComponent<Image>().color = statsColor;
                barLeftStatsList[4].GetComponent<Image>().color = statsColor;
                barLeftStatsList[5].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
    }

    private void RefreshPickRange()
    {
        //Clear stats
        for (int i = 6; i < 9; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.PickRange)
        {
            case 0:
                break;
            case 1:
                barLeftStatsList[6].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barLeftStatsList[6].GetComponent<Image>().color = statsColor;
                barLeftStatsList[7].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barLeftStatsList[6].GetComponent<Image>().color = statsColor;
                barLeftStatsList[7].GetComponent<Image>().color = statsColor;
                barLeftStatsList[8].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
    }

    private void RefreshAttackRange()
    {
        //Clear stats
        for (int i = 9; i < 12; i++)
        {
            barLeftStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.AtkRange)
        {
            case 0:
                break;
            case 1:
                barLeftStatsList[9].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barLeftStatsList[9].GetComponent<Image>().color = statsColor;
                barLeftStatsList[10].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barLeftStatsList[9].SetActive(true);
                barLeftStatsList[10].SetActive(true);
                barLeftStatsList[11].SetActive(true);
                break;
            default:
                break;
        }
    }
    //Right Column
    private void RefreshVitality()
    {
        //Clear stats
        for (int i = 0; i < 3; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.Vitality)
        {
            case 0:
                break;
            case 1:
                barRightStatsList[0].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barRightStatsList[0].GetComponent<Image>().color = statsColor;
                barRightStatsList[1].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barRightStatsList[0].GetComponent<Image>().color = statsColor;
                barRightStatsList[1].GetComponent<Image>().color = statsColor;
                barRightStatsList[2].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
    }

    private void RefreshStamina()
    {
        //Clear stats
        for (int i = 3; i < 6; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.Stamina)
        {
            case 0:
                break;
            case 1:
                barRightStatsList[3].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barRightStatsList[3].GetComponent<Image>().color = statsColor;
                barRightStatsList[4].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barRightStatsList[3].GetComponent<Image>().color = statsColor;
                barRightStatsList[4].GetComponent<Image>().color = statsColor;
                barRightStatsList[5].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
    }

    private void RefreshMoveSpeed()
    {
        //Clear stats
        for (int i = 6; i < 9; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.MoveSpeed)
        {
            case 0:
                break;
            case 1:
                barRightStatsList[6].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barRightStatsList[6].GetComponent<Image>().color = statsColor;
                barRightStatsList[7].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barRightStatsList[6].GetComponent<Image>().color = statsColor;
                barRightStatsList[7].GetComponent<Image>().color = statsColor;
                barRightStatsList[8].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
    }

    private void RefreshActionSpeed()
    {
        //Clear stats
        for (int i = 9; i < 12; i++)
        {
            barRightStatsList[i].GetComponent<Image>().color = statsBaseColor;
        }
        //Display new
        switch (SwarmEditUIController.instance.LumyStats.ActSpeed)
        {
            case 0:
                break;
            case 1:
                barRightStatsList[9].GetComponent<Image>().color = statsColor;
                break;
            case 2:
                barRightStatsList[9].GetComponent<Image>().color = statsColor;
                barRightStatsList[10].GetComponent<Image>().color = statsColor;
                break;
            case 3:
                barRightStatsList[9].GetComponent<Image>().color = statsColor;
                barRightStatsList[10].GetComponent<Image>().color = statsColor;
                barRightStatsList[11].GetComponent<Image>().color = statsColor;
                break;
            default:
                break;
        }
    }

    #endregion
    */

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
