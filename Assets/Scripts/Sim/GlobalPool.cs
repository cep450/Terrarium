using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPool {
    public static int[] resources = new int[Sim.resources.Length];

    public static void Add(int id, int amount) {
        resources[id] += amount;
        Tracker.AddedRes(id, amount);
    }
    public static void Add(string name, int amount) {
        Add(Resource.IdByName(name), amount);
    }

    public static bool CanConsume(int id, int amount) {
        return resources[id] >= amount;
    }
    public static bool CanConsume(string name, int amount) {
        return CanConsume(Resource.IdByName(name), amount);
    }

    public static void Consume(int id, int amount) {
        resources[id] -= amount;
        Tracker.UsedRes(id, amount);
        //TODO make sure not go into negatives
    }
    public static void Consume(string name, int amount) {
        Consume(Resource.IdByName(name), amount);
    }

}
