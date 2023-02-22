using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    /*
        MonoBehaviour switchboard that can call functions in other classes.
        Attached to the canvas.
    */

    //TODO allow players to pick from a dropdown or something

    public void PlantsToCrops() {
        AgentDirector.XToYByName("plant", "crop A");
    }

    public void WaterToPlants() {
        AgentDirector.XToYByName("water", "plant");
    }

    public void CropsToWater() {
        AgentDirector.XToYByName("crop A", "water");
    }

    public void VineToDirt() {
        AgentDirector.XToYByName("vine", "dirt");
    }

    public void DirtToCrop() {
        AgentDirector.XToYByName("dirt", "crop A");
    }

    public void DirtToWater() {
        AgentDirector.XToYByName("dirt", "water");
    }
}
