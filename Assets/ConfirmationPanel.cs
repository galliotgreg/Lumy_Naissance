using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationPanel : MonoBehaviour {
    [SerializeField]
    private List<GameObject> confirmationPanels;


    public void TogglePanel(int index)
    {
        foreach (GameObject panel in confirmationPanels)
        {
            if(confirmationPanels.IndexOf(panel) != index)
            {
                panel.SetActive(false);
            }
        }
        confirmationPanels[index].SetActive(!confirmationPanels[index].activeSelf);
    }
}
