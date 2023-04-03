using System.Collections;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    static Texture2D cursorClickable, cursorUnclickable;

    void Awake() {
        cursorClickable = Resources.Load<Texture2D>("/Cursor/Clickable");
        cursorClickable = Resources.Load<Texture2D>("/Cursor/Unclickable");
    }

    public static void SetClickable() {
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static void SetUnclickable() {
        Cursor.SetCursor(cursorUnclickable, Vector2.zero, CursorMode.ForceSoftware);
    
    }

    public static void Clicked() {
        //if we want to play an animation or anything 
    }
}