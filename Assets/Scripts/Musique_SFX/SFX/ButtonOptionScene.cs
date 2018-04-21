using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOptionScene : MonoBehaviour {

    public Button menuPrincipal;
    public Button parametres;
    public Button credits;
    public Button menuPrinciapl2;
    public Button parametres2;
    public Button credits2;

    void Start () {
        menuPrincipal.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        parametres.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        credits.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        menuPrinciapl2.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        parametres2.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        credits2.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
    }

    public void PlayHoverSFX()
    {
        SoundManager.instance.PlayOnHoverButtonSFX();
    }

}
