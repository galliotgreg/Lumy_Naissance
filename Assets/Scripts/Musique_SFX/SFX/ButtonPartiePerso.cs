using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPartiePerso : MonoBehaviour {

    public Button menuPrincipal;
    public Button editLeft;
    public Button editRight;
    public Button mapLeft;
    public Button mapRight;

    public Button play;

	void Start () {
        menuPrincipal.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        mapLeft.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        mapRight.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        editLeft.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);
        editRight.onClick.AddListener(SoundManager.instance.PlayOnClickButtonSFX);

        play.onClick.AddListener(SoundManager.instance.PlayLaunchGameSFX);
    }
	
    public void PlayHoverSFX()
    {
        SoundManager.instance.PlayOnHoverButtonSFX();
    }

}
