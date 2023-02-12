using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDirector //: MonoBehaviour
{

	/*
        Manages the Agents in the simulation the way SimGrid manages the SimHexes. 

        Handles stuff like re-using objects and ticking Agents. 
    */

	static List<Agent> agents = new List<Agent>();
	public static List<SimHexType> simHexTypes;
	public int destinationTypeIndex;
	public int desiredTypeIndex;

	public static void Init()
	{
		//TODO 
		simHexTypes = HexTypes.types;
		foreach (SimHexType s in simHexTypes)
		{
			Debug.Log(s.name + "is a type");
		}

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
		gnomeAgent.simHexTypes = simHexTypes;

		agents.Add(gnomeAgent);
		
	}
	public static void CreateTaskListForAgents()
	{
		foreach (Agent a in agents)
		{
			a.CreateTaskList();
		}
	}
	public void AddTask()
	{
		foreach (Agent a in agents)
		{
			a.AddTask(destinationTypeIndex, desiredTypeIndex);
		}
	}
	public void PlantsToCrops()
	{
		destinationTypeIndex = 0;
		desiredTypeIndex = 2;
		AddTask();
	}
	public void WaterToPlants()
	{
		destinationTypeIndex = 1;
		desiredTypeIndex = 0;
		AddTask();
	}
	public void CropsToWater()
	{
		destinationTypeIndex = 2;
		desiredTypeIndex = 1;
		AddTask();
	}
}