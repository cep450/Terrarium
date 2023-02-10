using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    /*
        KeyCodes go here so they're not hardcoded.
    */

    /*
    KeyCode up = KeyCode.W;
    KeyCode down = KeyCode.S;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    */
    KeyCode togglePause = KeyCode.Space;
    KeyCode [] incrementSimSpeed = { KeyCode.RightArrow, KeyCode.UpArrow };
    KeyCode [] decrementSimSpeed = { KeyCode.LeftArrow, KeyCode.DownArrow };
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach(KeyCode key in incrementSimSpeed) {
            if(Input.GetKeyDown(key)) {
                Clock.IncrementSpeed();
                break;
            }
        }

        foreach(KeyCode key in decrementSimSpeed) {
            if(Input.GetKeyDown(key)) {
                Clock.DecrementSpeed();
                break;
            }
        }

        if(Input.GetKeyDown(togglePause)) {
            Clock.TogglePause();
        }
        
    }
}
