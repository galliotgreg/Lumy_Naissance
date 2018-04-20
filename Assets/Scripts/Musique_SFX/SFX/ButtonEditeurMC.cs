using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEditeurMC : MonoBehaviour {

    public Button menuPrincipal;
    public Button editeurNuee;
    public Button affrontement;
       

    public GameObject quitMP;
    public GameObject quitNuee;
    public GameObject quitAffrontement;

	void Start () {
        
    }

    public void PlayHoverSFX()
    {
        SoundManager.instance.PlayOnHoverButtonSFX();
    }
    public void PlayClickTuto()
    {
        SoundManager.instance.PlayHintSFX();
    }

    public void PlayWarningMP()
    {
        if (quitMP.activeSelf == false)
        {
            SoundManager.instance.PlayPopUpDialogueSFX();
        }
    }
    public void PlayWarningNuee()
    {
        if (quitNuee.activeSelf == false)
        {
            SoundManager.instance.PlayPopUpDialogueSFX();
        }
    }
    public void PlayWarningAffrontment()
    {
        if (quitAffrontement.activeSelf == false)
        {
            SoundManager.instance.PlayPopUpDialogueSFX();
        }
    }
}
