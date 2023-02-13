using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    /*
        MonoBehaviour switchboard that can call functions in other classes.
        Attached to the canvas.
    */

    public void PlantsToCrops() {
        AgentDirector.PlantsToCrops();
    }

    public void WaterToPlants() {
        AgentDirector.WaterToPlants();
    }

    public void CropsToWater() {
        AgentDirector.CropsToWater();
    }
}
