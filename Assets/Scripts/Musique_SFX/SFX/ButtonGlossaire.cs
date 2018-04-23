using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGlossaire : MonoBehaviour {

    public Button menuPrincipal;

    public Button generalites;
    public Button mc;
    public Button operateurs;
    public Button composites;
    public Button archives;

	void Start () {
        menuPrincipal.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);

        generalites.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        mc.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        operateurs.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        composites.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        archives.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
    }

    public void PlayHoverSFX()
    {
        SoundManager.instance.PlayOnHoverButtonSFX();
    }

}
