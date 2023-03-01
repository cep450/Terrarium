using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProcesses : MonoBehaviour
{

    static GlobalProcesses instance;

    // Start is called before the first frame update
    void Start() {
        instance = this;
    }



    [SerializeField] int rainfallPerHex = 15;
    [SerializeField] int rainfallAtmosphereThreshPerHex = 20;
    [SerializeField] int rainfallTryModulo = 15;
    [SerializeField] VisualRain visualRain;
    void Rainfall() {

        int numHexes = Sim.hexMap.grid.Hexes.Count;

        if(rainfallAtmosphereThreshPerHex * numHexes <= GlobalPool.Amount("Water")) {
            GlobalPool.Consume("Water", rainfallPerHex * numHexes);
            foreach(Cube cube in Sim.hexMap.grid.Hexes) {
                cube.simHex.AddResource("Water", rainfallPerHex);
            }

            visualRain.DisplayRain();
        } else {
            Debug.Log("not enough water in the air to rain");
        }

    }



    static void WaterSpreads() {
        foreach(SimHex hex in SimGrid.hexes) {
            //TODO 
        }
    }
  



    public static void Tick(int tickNum) {

        Debug.Log("tick is " + tickNum + " and modulo is " + instance.rainfallTryModulo);

        if(tickNum % instance.rainfallTryModulo == 0) {
            instance.Rainfall();
        } else {
            WaterSpreads();
        }
       
    }

}
