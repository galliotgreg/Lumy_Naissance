using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMainMenu : MonoBehaviour {

    public void PlayOnClickSFX()
    {
        SoundManager.instance.PlayOnClickMenuSFX();
    }

    public void PlayOnPointerEnterSFX()
    {
        SoundManager.instance.PlayOnHoverMenuSFX();
    }
    
    public void PlayWelcomeOnClick()
    {
        SoundManager.instance.PlayLaunchGameSFX();
    }

}
