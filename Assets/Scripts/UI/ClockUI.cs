using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockUI : MonoBehaviour
{

    /*
        Will eventually manage the clock arrows ect 
    */

    string [] textSpeed  = new string [] { "PAUSED", "SPEED I", "SPEED II", "SPEED III" };

    [SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Clock.paused) {
            text.text = textSpeed[0];
        } else {
            text.text = textSpeed[(int)Clock.tickSpeedLevel];
        }
    }
}
