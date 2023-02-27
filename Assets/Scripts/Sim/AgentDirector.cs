using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentDirector : MonoBehaviour
{

	/*
        Manages the Agents in the simulation the way SimGrid manages the SimHexes. 

        Handles stuff like re-using objects and ticking Agents. 
    */

	static List<Agent> agents = new List<Agent>();
	//public static int destinationTypeIndex;
	//public static int desiredTypeIndex;

	public static void Init()
	{
		for (int i = 0; i < Sim.gnomesToSpawn; i++)
		{
			SpawnAgents();
		}
		CreateTaskListForAgents();
	}

	public static void AgentTick(int tickNum)
	{
		foreach (Agent a in agents)
		{
			a.Tick(tickNum);
		}
	}
	public static List<Agent> GetAgents()
	{
		return agents;
	}


	public static void SpawnAgents()
	{
		Cube spawnCube = Sim.hexMap.grid.Hexes[UnityEngine.Random.Range(0, Sim.hexMap.grid.Hexes.Count)]; // TODO: where to spawn
		SimHex simHex = spawnCube.simHex;
		GameObject _gnome = GameObject.Instantiate(Sim.gnomePrefab, simHex.visualHex.transform.position, new Quaternion());
		//Debug.Log("gnome spawned at " + spawnCube.position);
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
	public static void AddTask(SimHexType destinationType, SimHexType desiredType)
	{
		int[] taskListLengths = new int[agents.Count];
		for (int i = 0; i < agents.Count; i++)
		{
			taskListLengths[i] = agents[i].taskList.Count;
		}
		int minIndex = 0;
		try
		{
			minIndex = Array.IndexOf(taskListLengths, taskListLengths.Min());
		}
		catch
		{

		}

		agents[minIndex].AddTask(destinationType, desiredType);
	}

	public static void XToYByName(string xTypeName, string yTypeName)
	{

		AddTask(HexTypes.TypeByName(xTypeName), HexTypes.TypeByName(yTypeName));
	}

	public static string AllTaskLists()
	{
		string text = "Work orders: ";
		List<Task> list = new List<Task>();
		List<List<Task>> listOfLists = new List<List<Task>>();
		int count = 0;


		foreach (Agent a in agents)
		{
			List<Task> shallowCopy = new List<Task>(a.taskList);
			listOfLists.Add(shallowCopy);
			count += a.taskList.Count;
		}

		for (int i = 0; i < count; i++)
		{
			for (int j = 0; j < listOfLists.Count; j++)
			{
				if (listOfLists[j].Count > 0)
				{
					list.Add(listOfLists[j][0]);
					listOfLists[j].RemoveAt(0);
				}
			}
		}
		foreach (Task t in list)
		{
			text += "\n" + "Converting " + t.destinationType.name + " to " + t.desiredType.name;
		}
		return text;
	}
}