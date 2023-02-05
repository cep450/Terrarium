using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGrid
{
    /*
        INTERNAL representation of the hex grid.

        For stuff like avoiding the "half my neighbors have updated but 
        half haven't and now the calculation is wrong" problem.
    */

    //make sure this gets called! 
    public static void Init() {
        Clock.Tick += DoTick;
    }

    public static void DoTick(object obj, TickArgs tickArgs) {
        Debug.Log("tick recieved! tick number: " + tickArgs.tickNum);
    }
}