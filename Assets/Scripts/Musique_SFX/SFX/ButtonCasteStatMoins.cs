using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCasteStatMoins : MonoBehaviour {

    public void PlayOnClickSFX()
    {
        SoundManager.instance.PlayRemoveSwarmSFX();
    }
}
