using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCasteStatPlus : MonoBehaviour {
    public void PlayOnClickSFX()
    {
        SoundManager.instance.PlayAddSwarmSFX();
    }
}
