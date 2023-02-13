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
        AgentDirector.XToYByName("plant", "crops");
    }

    public void WaterToPlants() {
        AgentDirector.XToYByName("water", "plant");
    }

    public void CropsToWater() {
        AgentDirector.XToYByName("crops", "water");
    }

    public void VineToDirt() {
        AgentDirector.XToYByName("vine", "dirt");
    }

    public void DirtToCrop() {
        AgentDirector.XToYByName("dirt", "crops");
    }

    public void DirtToWater() {
        AgentDirector.XToYByName("dirt", "water");
    }
}
