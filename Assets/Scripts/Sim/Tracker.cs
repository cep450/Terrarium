using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker {

    public static int [] resourcesProduced = new int[0];
    public static int [] resourcesConsumed = new int[0];
    public static int [] resourcesNet = new int[0];

    public static int [] hexesAdded = new int[0];
    public static int [] hexesRemoved = new int[0];
    public static int [] hexesNet = new int[0];

    public static void Init() {
        resourcesProduced = new int [Sim.resources.Length];
        resourcesConsumed = new int [Sim.resources.Length];
        resourcesNet = new int [Sim.resources.Length];

        hexesAdded = new int [Sim.hexTypes.Length];
        hexesRemoved = new int [Sim.hexTypes.Length];
        hexesNet = new int [Sim.hexTypes.Length];
    }

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