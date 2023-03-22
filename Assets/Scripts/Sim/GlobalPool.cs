using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPool
{
	public static int[] resources = new int[Sim.resources.Length];

	public static void Init()
	{ //load initial values
		resources = new int[Sim.resources.Length];
		for (int i = 0; i < Sim.resources.Length; i++)
		{
			resources[i] = Sim.resourceInitialValues[i];
		}
	}

    public static void Add(int id, int amount) {

        //won't go over cap 
        if(Sim.resourceGlobalCaps[id] > 0) {
            int diff = Sim.resourceGlobalCaps[id] - resources[id];
            amount = Mathf.Min(amount, diff);
        }
        resources[id] += amount;
        Tracker.AddedRes(id, amount);
    }
    public static void Add(string name, int amount) {
        Add(Resource.IdByName(name), amount);
    }

	public static bool CanConsume(int id, int amount)
	{
		return resources[id] >= amount;
	}
	public static bool CanConsume(string name, int amount)
	{
		return CanConsume(Resource.IdByName(name), amount);
	}

	public static void Consume(int id, int amount)
	{
		resources[id] -= amount;
		Tracker.UsedRes(id, amount);
		//TODO make sure not go into negatives
	}
	public static void Consume(string name, int amount)
	{
		Consume(Resource.IdByName(name), amount);
	}

	public static int Amount(string name)
	{
		return Amount(Resource.IdByName(name));
	}
	public static int Amount(int id)
	{
		return resources[id];
	}

}
