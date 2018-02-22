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
    [Range(0, 3)]
    public float infobulleX;
    [Range(0, 6)]
    public float infobulleY;
   
    private float decalageTextes = 0;

    private Text nameText;
    private Text prodCostText;
    private Text moveSpeedBuffText;
    private Text actionSpeedBuffText;
    private Text strengthBuffText;
    private Text vitalityBuffText;
    private Text staminaBuffText;
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

        DrawInfobulle();
     
    }

    void DrawInfobulle()
    {
        List<GameObject> listeTextes = new List<GameObject>();
        List<string> listeStats = new List<string>();

        //infobulles composants
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
                    decalageTextes += 0.5f;
                    listeTextes[i].SetActive(true);
                    listeTextes[i].transform.position = infobulle.transform.Find("firstSlot").gameObject.transform.position + new Vector3(0f, -decalageTextes, 0f);
                    
                }

                Debug.Log("delta=" + decalageTextes);
               
            }
            decalageTextes = 0;
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
