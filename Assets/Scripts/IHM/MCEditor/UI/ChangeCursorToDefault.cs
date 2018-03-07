using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorToDefault : MonoBehaviour {
    
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void OnClickChangeCursor()
    {
        Cursor.SetCursor(null, this.transform.position, cursorMode);
    }
}
