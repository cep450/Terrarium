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

    //TODO temp for prototype
    static List<SimHex> hexes = new List<SimHex>();

    public static void Init() {

        //TODO prototype temp 
        hexes.Add(new Plant());


        foreach(SimHex hex in hexes) {
            hex.Init();
        }

        Clock.Tick += HandleTick;

    }

    public static void HandleTick(object obj, TickArgs tickArgs) {
        Debug.Log("tick recieved! tick number: " + tickArgs.tickNum);

        if(tickArgs.tickNum % 2 == 0) {
            Debug.Log("tick was even");
            TickInputs(tickArgs.tickNum);
        } else {
            Debug.Log("tick was odd");
            TickOutputs(tickArgs.tickNum);
        }

    }

    //Step 1: try to do inputs, check for and consume resources. 
    //this step is where resources are consumed/removed. 
    //Don't make changes beyond consumption, which is there to make sure something is only consumed once.
    //TODO: what if we want certain things to have consumption priority over other things?
    static void TickInputs(int tickNum) {
        foreach(SimHex h in hexes) {
            h.InputTick(tickNum);
        }
    }

    //Step 2: do outputs if requirements met.
    //this step is where resources are created. 
    static void TickOutputs(int tickNum) {
        foreach(SimHex h in hexes) {
            h.OutputTick(tickNum);
        }
    }
}