using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEditeurCastes : MonoBehaviour {

    public Button menuPrincipal;
    public Button affrontement;
    public Button tuto;
    public Button editeurMC;

    public GameObject resetPanel;
    public GameObject delNueePanel;
    public GameObject delCastePanel;

	void Start () {
        menuPrincipal.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        affrontement.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        editeurMC.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        tuto.onClick.AddListener(SoundManager.instance.PlayHintSFX);

    }

    public void PlayHoverSFX()
    {
        SoundManager.instance.PlayOnHoverButtonSFX();
    }

    public void PlayWarningReset()
    {
        if (resetPanel.activeSelf == false)
        {
            SoundManager.instance.PlayPopUpDialogueSFX();
        }        
    }
    public void PlayWarningNuee()
    {
        if (delNueePanel.activeSelf == false)
        {
            SoundManager.instance.PlayPopUpDialogueSFX();
        }
    }
    public void PlayWarningCaste()
    {
        if (delCastePanel.activeSelf == false)
        {
            SoundManager.instance.PlayPopUpDialogueSFX();
        }
    }

}
