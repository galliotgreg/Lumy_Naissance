using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumyUIController : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static LumyUIController instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject libraryCompPrefab;
    [SerializeField]
    private GameObject voletGauche;
    [SerializeField]
    private GameObject infobulle;
    [SerializeField]
    private GameObject stats;
    [Range(0, 3)]
    public float infobulleX;
    [Range(0, 6)]
    public float infobulleY;
    [Range(0f, 0.7f)]
    public float deltaTexte;
   
    private float deltaY = 0;

    private Text nameText;
    private Text prodCostText;
    private Text moveSpeedBuffText;
    private Text actionSpeedBuffText;
    private Text strengthBuffText;
    private Text vitalityBuffText;
    private Text staminaBuffText;
    private Text atkRangeBuffText;
    private Text pickRangeBuffText;
    private Text visionRangeBuffText;
    private Text vibrationRangeBuffText;
    private Text heatRangeBuffText;
    private Text smellRangeBuffText;
    private Text visionIndetectableText;
    private Text vibrationIndetectableText;
    private Text heatIndetectableText;
    private Text smellIndetectableText;
    private Text enableGoToHoldText;
    private Text enableStrikeText;
    private Text enablePickDropText;
    private Text enableLayText;
    private Text visionRangeText;
    private Text vibrationRangeText;
    private Text heatRangeText;
    private Text smellRangeText;
    
    
    private Text txt_Cost_Red;
    private Text txt_Cost_Green;
    private Text txt_Cost_Blue;
    private Text moveSpeedBuffStatText;
    private Text actionSpeedBuffStatText;
    private Text strengthBuffStatText;
    private Text vitalityBuffStatText;
    private Text staminaBuffStatText;
    private Text visionRangeBuffStatText;
    private Text vibrationRangeBuffStatText;
    private Text heatRangeBuffStatText;
    private Text smellRangeBuffStatText;
    private Text visionIndetectableStatText;
    private Text vibrationIndetectableStatText;
    private Text heatIndetectableStatText;
    private Text smellIndetectableStatText;
    private Text enableGoToHoldStatText;
    private Text enableStrikeStatText;
    private Text enablePickDropStatText;
    private Text enableLayStatText;
    private Text visionRangeStatText;
    private Text vibrationRangeStatText;
    private Text heatRangeStatText;
    private Text smellRangeStatText;


    // Use this for initialization
    void Start () {
        if (LumyEditorManager.instance.EditedLumy != null)
        {
            LumyEditorManager.instance.EditedLumy.SetActive(true);
        }
        RetreiveData();
    }

    private void RetreiveData()
    {
        int nbColumns = 4;
        IList<ComponentInfo> compos = LumyEditorManager.instance.CompoLibrary;
        for (int i = 0; i < compos.Count; i++)
        {
            //Instanciate Compo button
            GameObject libComp = Instantiate(libraryCompPrefab);
            LibraryCompoData compoData = libComp.GetComponent<LibraryCompoData>();
            compoData.ComponentInfo = compos[i];

            //Set position
            int x = i % nbColumns;
            int y = i / nbColumns;
            RectTransform rectTransform = libComp.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(-160f + x * 110f, 380f - y * 140f, 0f);
            libComp.transform.SetParent(voletGauche.transform, false);

            //Congigure Button
            Text text = libComp.transform.Find("Text").gameObject.GetComponent<Text>();
            text.text = compoData.ComponentInfo.Name;
        }
    }

    // Update is called once per frame
    void Update () {

        DrawInfobulleEditedLumy();
        SetStats();

    }

    void SetStats() {
        int redCost=0;
        int greenCost = 0;
        int blueCost = 0;
        float atkRangeBuff = 0;
        float pickRangeBuff = 0;
        float moveSpeedBuff =0;
        float actionSpeedBuff=0;
        float strengthBuff=0;
        float vitalityBuff=0;
        float staminaBuff=0;
        float visionRangeBuff=0;
        float vibrationRangeBuff=0;
        float heatRangeBuff=0;
        float smellRangeBuff=0;
        bool visionIndetectable=false;
        bool vibrationIndetectable = false;
        bool heatIndetectable = false;
        bool smellIndetectable = false;
        bool enableGoToHold = false;
        bool enableStrike = false;
        bool enablePickDrop = false;
        bool enableLay = false;
        float visionRange=0;
        float vibrationRange=0;
        float heatRange=0;
        float smellRange=0;

        redCost = CostManager.instance.ComputeRedCost(LumyEditorManager.instance.EditedLumy);
        greenCost = CostManager.instance.ComputeGreenCost(LumyEditorManager.instance.EditedLumy);
        blueCost = CostManager.instance.ComputeBlueCost(LumyEditorManager.instance.EditedLumy);
        foreach (AgentComponent compo in LumyEditorManager.instance.EditedLumy.GetComponentsInChildren<AgentComponent>()) {

            //cost += compo.ProdCost;
            atkRangeBuff += compo.AtkRangeBuff;
            pickRangeBuff += compo.PickRangeBuff; 
            moveSpeedBuff += compo.MoveSpeedBuff;
            actionSpeedBuff += compo.ActionSpeedBuff;
            strengthBuff += compo.StrengthBuff;
            vitalityBuff += compo.VitalityBuff;
            staminaBuff += compo.StaminaBuff;
            visionRangeBuff += compo.VisionRangeBuff;
            vibrationRangeBuff += compo.VibrationRangeBuff;
            heatRangeBuff += compo.HeatRangeBuff;
            smellRangeBuff += compo.SmellRangeBuff;

            if(compo.VisionIndetectable == true) {
                visionIndetectable = compo.VisionIndetectable;
            }
            if (compo.VibrationIndetectable == true) {
                vibrationIndetectable = compo.VibrationIndetectable;
            }
            if (compo.HeatIndetectable == true) {
                heatIndetectable = compo.HeatIndetectable;
            }
            if (compo.SmellIndetectable == true) {
                smellIndetectable = compo.SmellIndetectable;
            }
            if (compo.EnableGotoHold == true) {
                enableGoToHold = compo.EnableGotoHold;
            }
            if (compo.EnableStrike == true) {
                enableStrike = compo.EnableStrike;
            }
            if (compo.EnablePickDrop == true) {
                enablePickDrop = compo.EnablePickDrop;
            }
            if (compo.EnableLay == true) {
                enableLay = compo.EnableLay;
            }
            
            visionRange += compo.VisionRange;
            vibrationRange += compo.VibrationRange;
            heatRange += compo.HeatRange;
            smellRange += compo.HeatRange;

        }
        txt_Cost_Red = stats.transform.Find("txt_Cost_Red").gameObject.GetComponent<Text>();
        txt_Cost_Red.text = "Red Cost : " + redCost.ToString();
        txt_Cost_Green = stats.transform.Find("txt_Cost_Green").gameObject.GetComponent<Text>();
        txt_Cost_Green.text = "Green Cost : " + greenCost.ToString();
        txt_Cost_Blue = stats.transform.Find("txt_Cost_Blue").gameObject.GetComponent<Text>();
        txt_Cost_Blue.text = "Blue Cost : " + blueCost.ToString();
        atkRangeBuffText = stats.transform.Find("atkRangeBuffatText").gameObject.GetComponent<Text>();
        atkRangeBuffText.text = "Atk Range : " + atkRangeBuff.ToString();
        pickRangeBuffText = stats.transform.Find("pickRangeBuffatText").gameObject.GetComponent<Text>();
        pickRangeBuffText.text = "Pick Range : " + pickRangeBuff.ToString();
        moveSpeedBuffStatText = stats.transform.Find("moveSpeedBuffStatText").gameObject.GetComponent<Text>();
        moveSpeedBuffStatText.text = "Move Speed : " + moveSpeedBuff.ToString();
        actionSpeedBuffStatText = stats.transform.Find("actionSpeedBuffStatText").gameObject.GetComponent<Text>();
        actionSpeedBuffStatText.text = "Action Speed : " + actionSpeedBuff.ToString();
        strengthBuffStatText = stats.transform.Find("strengthBuffStatText").gameObject.GetComponent<Text>();
        strengthBuffStatText.text = "Strenght : " + strengthBuff.ToString();
        vitalityBuffStatText = stats.transform.Find("vitalityBuffStatText").gameObject.GetComponent<Text>();
        vitalityBuffStatText.text = "Vitality : " + vitalityBuff.ToString();
        staminaBuffStatText = stats.transform.Find("staminaBuffStatText").gameObject.GetComponent<Text>();
        staminaBuffStatText.text = "Stamina : " + staminaBuff.ToString();
        visionRangeBuffStatText = stats.transform.Find("visionRangeBuffStatText").gameObject.GetComponent<Text>();
        visionRangeBuffStatText.text = "Vision Range Buff : " + visionRangeBuff.ToString();
        vibrationRangeBuffStatText = stats.transform.Find("vibrationRangeBuffStatText").gameObject.GetComponent<Text>();
        vibrationRangeBuffStatText.text = "Vibration Range Buff : " + vibrationRangeBuff.ToString();
        heatRangeBuffStatText = stats.transform.Find("heatRangeBuffStatText").gameObject.GetComponent<Text>();
        heatRangeBuffStatText.text = "Heat Range Buff : " + heatRangeBuff.ToString();
        smellRangeBuffStatText = stats.transform.Find("smellRangeBuffStatText").gameObject.GetComponent<Text>();
        smellRangeBuffStatText.text = "Smell Range Buff : " + smellRangeBuff.ToString();
        visionIndetectableStatText = stats.transform.Find("visionIndetectableStatText").gameObject.GetComponent<Text>();

        visionIndetectableStatText.text = "Vision Indetectacle : " + visionIndetectable.ToString();
        vibrationIndetectableStatText = stats.transform.Find("vibrationIndetectableStatText").gameObject.GetComponent<Text>();
        vibrationIndetectableStatText.text = "Vibration Indetectable : " + vibrationIndetectable.ToString();
        heatIndetectableStatText = stats.transform.Find("heatIndetectableStatText").gameObject.GetComponent<Text>();
        heatIndetectableStatText.text = "Heat Indetectable : " + heatIndetectable.ToString();
        smellIndetectableStatText = stats.transform.Find("smellIndetectableStatText").gameObject.GetComponent<Text>();
        smellIndetectableStatText.text = "Smell Indetectable : " + smellIndetectable.ToString();
        enableGoToHoldStatText = stats.transform.Find("enableGoToHoldStatText").gameObject.GetComponent<Text>();
        enableGoToHoldStatText.text = "Goto : " + enableGoToHold.ToString();
        enableStrikeStatText = stats.transform.Find("enableStrikeStatText").gameObject.GetComponent<Text>();
        enableStrikeStatText.text = "Strike : " + enableStrike.ToString();
        enablePickDropStatText = stats.transform.Find("enablePickDropStatText").gameObject.GetComponent<Text>();
        enablePickDropStatText.text = "Pick and Drop : " + enablePickDrop.ToString();
        enableLayStatText = stats.transform.Find("enableLayStatText").gameObject.GetComponent<Text>();
        enableLayStatText.text = "Lay : " + enableLay.ToString();

        visionRangeStatText = stats.transform.Find("visionRangeStatText").gameObject.GetComponent<Text>();
        visionRangeStatText.text = "Vision Range : " + visionRange.ToString();
        vibrationRangeStatText = stats.transform.Find("vibrationRangeStatText").gameObject.GetComponent<Text>();
        vibrationRangeStatText.text = "Vibration Range : " + vibrationRange.ToString();
        heatRangeStatText = stats.transform.Find("heatRangeStatText").gameObject.GetComponent<Text>();
        heatRangeStatText.text = "Heat Range : " + heatRange.ToString();
        smellRangeStatText = stats.transform.Find("smellRangeStatText").gameObject.GetComponent<Text>();
        smellRangeStatText.text = "Smell Range : " + smellRange.ToString();
    }

    void DrawInfobulleEditedLumy()
    {
        List<GameObject> listeTextes = new List<GameObject>();
        List<string> listeStats = new List<string>();
        
        //infobulles composants edited Lumy
        if (LumyEditorManager.instance.HoveredLumyCompo != null && LumyEditorManager.instance.SelectedLumyCompo == null)
        {
            //set infobulle
            infobulle.SetActive(true);
            infobulle.transform.position = LumyEditorManager.instance.HoveredLumyCompo.transform.position;
            infobulle.transform.position += new Vector3(infobulleX, infobulleY, 0f);

            //texte
            nameText = infobulle.transform.Find("nameText").gameObject.GetComponent<Text>();
            nameText.text = "Name : " + ((AgentComponent)LumyEditorManager.instance.HoveredLumyCompo).Name;
  
            prodCostText = infobulle.transform.Find("prodCostText").gameObject.GetComponent<Text>();
            prodCostText.text = "Cost : " + LumyEditorManager.instance.HoveredLumyCompo.ProdCost.ToString();

            atkRangeBuffText = infobulle.transform.Find("atkRangeBuffText").gameObject.GetComponent<Text>();
            atkRangeBuffText.text = "Atk Range : " + LumyEditorManager.instance.HoveredLumyCompo.AtkRangeBuff.ToString();

            pickRangeBuffText = infobulle.transform.Find("atkRangeBuffText").gameObject.GetComponent<Text>();
            pickRangeBuffText.text = "Pick Range : " + LumyEditorManager.instance.HoveredLumyCompo.PickRangeBuff.ToString();

            moveSpeedBuffText = infobulle.transform.Find("moveSpeedBuffText").gameObject.GetComponent<Text>();
            moveSpeedBuffText.text = "Speed : + " + LumyEditorManager.instance.HoveredLumyCompo.MoveSpeedBuff.ToString();           

            actionSpeedBuffText = infobulle.transform.Find("actionSpeedBuffText").gameObject.GetComponent<Text>();
            actionSpeedBuffText.text = "Action Speed : + " + LumyEditorManager.instance.HoveredLumyCompo.ActionSpeedBuff.ToString();        

            strengthBuffText = infobulle.transform.Find("strengthBuffText").gameObject.GetComponent<Text>();
            strengthBuffText.text = "Strength : + " + LumyEditorManager.instance.HoveredLumyCompo.StrengthBuff.ToString();
           
            vitalityBuffText = infobulle.transform.Find("vitalityBuffText").gameObject.GetComponent<Text>();
            vitalityBuffText.text = "Vitality : + " + LumyEditorManager.instance.HoveredLumyCompo.VitalityBuff.ToString();

            staminaBuffText = infobulle.transform.Find("staminaBuffText").gameObject.GetComponent<Text>();
            staminaBuffText.text = "Stamina : + " + LumyEditorManager.instance.HoveredLumyCompo.StaminaBuff.ToString();

            visionRangeBuffText = infobulle.transform.Find("visionRangeBuffText").gameObject.GetComponent<Text>();
            visionRangeBuffText.text = "Vision Range : + " + LumyEditorManager.instance.HoveredLumyCompo.VisionRangeBuff.ToString();

            vibrationRangeBuffText = infobulle.transform.Find("vibrationRangeBuffText").gameObject.GetComponent<Text>();
            vibrationRangeBuffText.text = "Vibration Range : + " + LumyEditorManager.instance.HoveredLumyCompo.VibrationRangeBuff.ToString();

            heatRangeBuffText = infobulle.transform.Find("heatRangeBuffText").gameObject.GetComponent<Text>();
            heatRangeBuffText.text = "Heat Range : + " + LumyEditorManager.instance.HoveredLumyCompo.HeatRangeBuff.ToString();
           
            smellRangeBuffText = infobulle.transform.Find("smellRangeBuffText").gameObject.GetComponent<Text>();
            smellRangeBuffText.text = "Smell Range : + " + LumyEditorManager.instance.HoveredLumyCompo.SmellRangeBuff.ToString();
        
            visionIndetectableText = infobulle.transform.Find("visionIndetectableText").gameObject.GetComponent<Text>();
            visionIndetectableText.text = "Vision Indetectable ";
           
            vibrationIndetectableText = infobulle.transform.Find("vibrationIndetectableText").gameObject.GetComponent<Text>();
            vibrationIndetectableText.text = "Vibration Indetectable " ;
           
            heatIndetectableText = infobulle.transform.Find("heatIndetectableText").gameObject.GetComponent<Text>();
            heatIndetectableText.text = "Heat Indetectable " ;
            
            smellIndetectableText = infobulle.transform.Find("smellIndetectableText").gameObject.GetComponent<Text>();
            smellIndetectableText.text = "Smell Indetectable ";
            
            enableGoToHoldText = infobulle.transform.Find("enableGoToHoldText").gameObject.GetComponent<Text>();
            enableGoToHoldText.text = "Enable Go To Hold " ;
            
            enableStrikeText = infobulle.transform.Find("enableStrikeText").gameObject.GetComponent<Text>();
            enableStrikeText.text = "Enable Strike " ;
           
            enablePickDropText = infobulle.transform.Find("enablePickDropText").gameObject.GetComponent<Text>();
            enablePickDropText.text = "Enable Pick Drop ";
           
            enableLayText = infobulle.transform.Find("enableLayText").gameObject.GetComponent<Text>();
            enableLayText.text = "Enable Lay ";
          
            visionRangeText = infobulle.transform.Find("visionRangeText").gameObject.GetComponent<Text>();
            visionRangeText.text = "Vision Range : " + LumyEditorManager.instance.HoveredLumyCompo.VisionRange.ToString();
           
            vibrationRangeText = infobulle.transform.Find("vibrationRangeText").gameObject.GetComponent<Text>();
            vibrationRangeText.text = "Vibration Range : " + LumyEditorManager.instance.HoveredLumyCompo.VibrationRange.ToString();
           
            heatRangeText = infobulle.transform.Find("heatRangeText").gameObject.GetComponent<Text>();
            heatRangeText.text = "Heat Range : " + LumyEditorManager.instance.HoveredLumyCompo.HeatRange.ToString();
            
            smellRangeText = infobulle.transform.Find("smellRangeText").gameObject.GetComponent<Text>();
            smellRangeText.text = "Smell Range : " + LumyEditorManager.instance.HoveredLumyCompo.SmellRange.ToString();


            //tri affichage
            listeTextes.Add(infobulle.transform.Find("nameText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.name);

            listeTextes.Add(infobulle.transform.Find("prodCostText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.ProdCost.ToString());

            listeTextes.Add(infobulle.transform.Find("atkRangeBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.AtkRangeBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("PickRangeBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.PickRangeBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("moveSpeedBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.MoveSpeedBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("actionSpeedBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.ActionSpeedBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("strengthBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.StrengthBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("vitalityBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VitalityBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("staminaBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.StaminaBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("visionRangeBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VisionRangeBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("vibrationRangeBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VibrationRangeBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("heatRangeBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.HeatRangeBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("smellRangeBuffText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.SmellRangeBuff.ToString());

            listeTextes.Add(infobulle.transform.Find("visionIndetectableText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VisionIndetectable.ToString());

            listeTextes.Add(infobulle.transform.Find("vibrationIndetectableText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VibrationIndetectable.ToString());

            listeTextes.Add(infobulle.transform.Find("heatIndetectableText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.HeatIndetectable.ToString());

            listeTextes.Add(infobulle.transform.Find("smellIndetectableText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.SmellIndetectable.ToString());

            listeTextes.Add(infobulle.transform.Find("enableGoToHoldText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.EnableGotoHold.ToString());

            listeTextes.Add(infobulle.transform.Find("enableStrikeText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.EnableStrike.ToString());

            listeTextes.Add(infobulle.transform.Find("enablePickDropText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.EnablePickDrop.ToString());

            listeTextes.Add(infobulle.transform.Find("enableLayText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.EnableLay.ToString());

            listeTextes.Add(infobulle.transform.Find("visionRangeText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VisionRange.ToString());

            listeTextes.Add(infobulle.transform.Find("vibrationRangeText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.VibrationRange.ToString());

            listeTextes.Add(infobulle.transform.Find("heatRangeText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.HeatRange.ToString());

            listeTextes.Add(infobulle.transform.Find("smellRangeText").gameObject);
            listeStats.Add(LumyEditorManager.instance.HoveredLumyCompo.SmellRange.ToString());

            for (int i = 0; i < listeStats.Count; i++)
            {
                if (string.Compare(listeStats[i], "0") == 0 || string.Compare(listeStats[i], "False") == 0)
                {
                    listeTextes[i].SetActive(false);
                }

                else
                {
                    
                    listeTextes[i].SetActive(true);
                    listeTextes[i].transform.position = listeTextes[0].transform.position + new Vector3(0f, -deltaY, 0f);
                    deltaY += deltaTexte;

                }

            }
            deltaY = 0;
        }
        else
        {
            infobulle.SetActive(false);
        }


    }



    void OnDestroy()
    {
        if (LumyEditorManager.instance.EditedLumy != null)
        {
            LumyEditorManager.instance.EditedLumy.SetActive(false);
        }
    }
}
