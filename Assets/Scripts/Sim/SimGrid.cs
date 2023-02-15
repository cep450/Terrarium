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

    //Parallel list with the list of Cubes in the HexGrid in the HexMap. 
    static SimHex [] hexes;
    
    public static void Init() {

        hexes = new SimHex[Sim.hexMap.grid.Hexes.Count];

        Generate(RanjitToPercent(Sim.hexGenRanjit));

    }

    //returns CUMULATIVE percent thresholds so the last one should be 100%
    //cumulative probability spread 
    static float [] RanjitToPercent(float [] ranjit) {
        float [] percents = new float[ranjit.Length];
        float total = 0;
        for(int i = 0; i < ranjit.Length; i++) {
            total += ranjit[i];
        }
        for(int i = 0; i < ranjit.Length; i++) {
            percents[i] = ranjit[i] / total;
            if(i > 0) {
                percents[i] = percents[i - 1] + percents[i];
            }
        }
        return percents;
    }

    static int IndexFromPercent(float percent, float [] percentArr) {

        for(int i = 0; i < percentArr.Length; i++) {
            if(percentArr[i] > percent) {
                return i;
            }
        }
        return percentArr.Length - 1;
    }

    //Generate a map.
    static void Generate(float [] typePercents) {

        if(typePercents.Length != Sim.hexTypes.Length) {
            Debug.LogError("ERR: ranjit range proportions arr did not match length of hex types arr. did you forget to add a value to this parallel array?");
            return;
        }

        //TODO prototype temp- randomized types 
        for(int i = 0; i < hexes.Length; i++) {
            int rand = IndexFromPercent(Random.Range(0f, 1f), typePercents);
            hexes[i] = new SimHex(Sim.hexTypes[rand], Sim.hexMap.grid.Hexes[i]);
            Sim.hexMap.grid.Hexes[i].simHex = hexes[i];
        }
        //done after all initialized to avoid null neighbors
        foreach(SimHex hex in hexes) {
            hex.LoadNeighbors();
        }
        
    }

    //Step 1: try to do inputs, check for and consume resources. 
    //this step is where resources are consumed/removed. 
    //Don't make changes beyond consumption, which is there to make sure something is only consumed once.
    //TODO: what if we want certain things to have consumption priority over other things?
    public static void TickInputs(int tickNum) {
        foreach(SimHex h in hexes) {
            h.InputTick(tickNum);
        }
    }

    //Step 2: do outputs if requirements met.
    //this step is where resources are created. 
    public static void TickOutputs(int tickNum) {
        foreach(SimHex h in hexes) {
            h.OutputTick(tickNum);
        }
    }

}