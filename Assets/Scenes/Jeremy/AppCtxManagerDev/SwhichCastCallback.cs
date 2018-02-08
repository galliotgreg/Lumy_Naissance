using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwhichCastCallback : MonoBehaviour {
    [SerializeField]
    private Dropdown castDropdown;

    public void Callback()
    {
        string name = castDropdown.options[castDropdown.value].text;
        AppContextManager.instance.SwitchActiveCast(name);
    }
}
