using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDirector : MonoBehaviour
{

	/*
        Manages the Agents in the simulation the way SimGrid manages the SimHexes. 

        Handles stuff like re-using objects and ticking Agents. 
    */

	static List<Agent> agents = new List<Agent>();
	public static int destinationTypeIndex;
	public static int desiredTypeIndex;

	public static void Init()
	{

		SpawnAgents();
        CreateTaskListForAgents();

	}

	public static void AgentTick(int tickNum)
	{
		foreach (Agent a in agents)
		{
			a.Tick(tickNum);
		}
	}

	
	public static void SpawnAgents()
	{
		Cube spawnCube = Sim.hexMap.grid.Hexes[0]; // TODO: where to spawn
		SimHex simHex = spawnCube.simHex;
		GameObject _gnome = GameObject.Instantiate(Sim.gnomePrefab, simHex.visualHex.transform.position,new Quaternion());
		Debug.Log("gnome spawned at " + spawnCube.position);
		Agent gnomeAgent = _gnome.GetComponent<Agent>();
		gnomeAgent.simHex = simHex;

		agents.Add(gnomeAgent);
		
	}
	public static void CreateTaskListForAgents()
	{
		foreach (Agent a in agents)
		{
			a.CreateTaskList();
		}
	}
	public static void AddTask()
	{
		foreach (Agent a in agents)
		{
			a.AddTask(destinationTypeIndex, desiredTypeIndex);
		}
	}
	public static void PlantsToCrops()
	{
		destinationTypeIndex = HexTypes.IdByName("plant");
		desiredTypeIndex = HexTypes.IdByName("crops");
		AddTask();
	}
	public static void WaterToPlants()
	{
		destinationTypeIndex = HexTypes.IdByName("water");
		desiredTypeIndex = HexTypes.IdByName("plant");
		AddTask();
	}
	public static void CropsToWater()
	{
		destinationTypeIndex = HexTypes.IdByName("crops");
		desiredTypeIndex = HexTypes.IdByName("water");
		AddTask();
	}
}