using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursorToHand : MonoBehaviour {

    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;

    // Use this for initialization
    void Start() {
        hotSpot = new Vector2(cursorTextureHand.width / 2, cursorTextureHand.height / 2);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnClickChangeCursor()
    {
        Cursor.SetCursor(cursorTextureHand, hotSpot, cursorMode);
    }
        
}
