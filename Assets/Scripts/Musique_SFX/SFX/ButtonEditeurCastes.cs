using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEditeurCastes : MonoBehaviour {

    public Button menuPrincipal;
    public Button affrontement;
    public Button tuto;
    public Button editeurMC;

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

}
