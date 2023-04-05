using System.Collections;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    static Texture2D cursorClickable, cursorUnclickable, cursorClicking;

    void Awake() {
        cursorClickable = Resources.Load<Texture2D>("pointers_0");  //pointer hand 
        cursorUnclickable = Resources.Load<Texture2D>("pointers_1"); //open hand
        cursorClicking = Resources.Load<Texture2D>("pointers_2");   //grabby hand
    }

    public static void SetClickable() {
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.ForceSoftware);
    }
    public static void SetUnclickable() {
        Cursor.SetCursor(cursorUnclickable, Vector2.zero, CursorMode.ForceSoftware);
    }
    public static void SetClicking() {
        Cursor.SetCursor(cursorClicking, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static void Clicked() {
        //if we want to play an animation or anything 
    }
}