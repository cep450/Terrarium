using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker {

    public static int [] resourcesProduced = new int [Sim.resources.Length];
    public static int [] resourcesConsumed = new int [Sim.resources.Length];
    public static int [] resourcesNet = new int [Sim.resources.Length];

    public static int [] hexesAdded = new int [Sim.hexTypes.Length];
    public static int [] hexesRemoved = new int [Sim.hexTypes.Length];
    public static int [] hexesNet = new int [Sim.hexTypes.Length];

    public static void CalculateEndOfTick() {

        for(int i = 0; i < Sim.resources.Length; i++) {
            resourcesNet[i] = resourcesProduced[i] - resourcesConsumed[i];
            resourcesProduced[i] = 0;
            resourcesConsumed[i] = 0;
        }

        for(int i = 0; i < Sim.hexTypes.Length; i++) {
            hexesNet[i] = hexesAdded[i] - hexesRemoved[i];
            hexesAdded[i] = 0;
            hexesRemoved[i] = 0;
        }
    }

    public static void AddedRes(int id, int amount) {
        resourcesProduced[id] += amount;
    }
    public static void UsedRes(int id, int amount) {
        resourcesConsumed[id] += amount;
    }
    public static void AddedHex(int id, int amount) {
        hexesAdded[id] += amount;
    }
    public static void RemovedHex(int id, int amount) {
        hexesRemoved[id] += amount;
    }
}