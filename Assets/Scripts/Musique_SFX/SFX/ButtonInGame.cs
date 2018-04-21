using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInGame : MonoBehaviour {

    public Button menuDeroulant;

    public Button menuPrincipal;
    public Button affrontement;
    public Button nuee;
    public Button options;

    public Button slideContexte;
    public Button slideStats;
    public Button slideMC;

    public Button errorLogger;

	void Start () {
        menuDeroulant.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        errorLogger.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);

        slideContexte.onClick.AddListener(SoundManager.instance.PlayOnHoverButtonSFX);
        slideStats.onClick.AddListener(SoundManager.instance.PlayOnHoverButtonSFX);
        slideMC.onClick.AddListener(SoundManager.instance.PlayOnHoverButtonSFX);
    }

    public void PlayHoverSFX()
    {
        SoundManager.instance.PlayOnHoverButtonSFX();
    }

}
