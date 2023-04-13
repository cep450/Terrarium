using System.Collections;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    static Texture2D cursorClickable, cursorUnclickable, cursorClicking;
    
    public static TileResourceInfo tileHover;

    void Awake() {
        cursorClickable = Resources.Load<Texture2D>("pointers_point");  //pointer hand 
        cursorUnclickable = Resources.Load<Texture2D>("pointers_open"); //open hand
        cursorClicking = Resources.Load<Texture2D>("pointers_closed");   //grabby hand
        tileHover = GetComponent<TileResourceInfo>();
    }

    void Start() {
        SetUnclickable();
    }

    void Update() {
        tileHover.transform.position = Input.mousePosition;
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