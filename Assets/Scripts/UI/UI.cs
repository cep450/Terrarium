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
        AgentDirector.XToYByName("Plant", "Crop A");
    }

    public void WaterToPlants() {
        AgentDirector.XToYByName("Water", "Plant");
    }

    public void CropsToWater() {
        AgentDirector.XToYByName("Crop A", "Water");
    }

    public void PlantsToDirt() {
        AgentDirector.XToYByName("Plant", "Dirt");
    }

    public void DirtToCrop() {
        AgentDirector.XToYByName("Dirt", "Crop A");
    }

    public void DirtToWater() {
        AgentDirector.XToYByName("Dirt", "Water");
    }

    public void BuildWell() {
        AgentDirector.XToYByName("Water", "Well");
    }

    public void BuildHouse() {
        AgentDirector.XToYByName("Dirt", "House");
    }
}
