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

    public void VineToDirt() {
        AgentDirector.XToYByName("vine", "dirt");
    }

    public void DirtToCrop() {
        AgentDirector.XToYByName("dirt", "crop");
    }

    public void DirtToWater() {
        AgentDirector.XToYByName("dirt", "water");
    }
}
