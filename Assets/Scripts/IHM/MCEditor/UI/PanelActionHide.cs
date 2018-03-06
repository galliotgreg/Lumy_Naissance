using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelActionHide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hover = false;    

    // Update is called once per frame
    bool clickReleased = false;

    protected void Update()
    {
        if (clickReleased)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!hover)
                {
                    // Deactivate
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (!(Input.GetMouseButtonUp(0)))
            {
                clickReleased = true;
            }
        }
    }

    #region IPointerEnterHandler implementation

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    #endregion

    #region IPointerExitHandler implementation

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    #endregion
}
