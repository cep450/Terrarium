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
	static List<Need> needs = new List<Need>();
	public static int[] shallowResources = new int[0];
	public static int lengthOfContent = 0;
	public static int satisfactionThreshold;
	public static void Init()
	{
		agents = new List<Agent>();
		for (int i = 0; i < Sim.gnomesToSpawn; i++)
		{
			SpawnAgents();
		}
		CreateTaskListForAgents();
		CreateNeedListForAgents();
	}

	public static void AgentTick(int tickNum)
	{
		shallowResources = (int[])GlobalPool.resources.Clone(); // make a shallow copy of resources
		foreach (Agent a in agents)
		{
			a.Tick(tickNum);
		}
		FailureState.UpdateCounter();
		if (agents.Count > 0)
		{
			ContinuousContentTicking(satisfactionThreshold); // the parameter is for the threshold above which win condition progresses

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
	public static void CreateNeedListForAgents()
	{
		NeedList();
		foreach (Agent a in agents)
		{
			a.CreateNeedList(needs);
		}
	}
	static List<Need> NeedList()
	{
		needs = new List<Need>();
		Need food = 	new Need("Food", 	50, 10, 3, true, true);
		Need housing = 	new Need("Housing", 50, 1, 2, true, false);
		Need water = 	new Need("Water", 	50, 10, 3, true, true);
		Need honey = 	new Need("Honey", 	50, 10, 1, false, true);
		Need space = 	new Need("Leisure", 50, 2, 1, false, true);
		needs.Add(food);
		needs.Add(housing);
		needs.Add(water);
		needs.Add(honey);
		needs.Add(space);
		return needs;
	}
	public static int AverageWeightedSatisfaction()
	{
		int aws = 0;
		if (agents.Count > 0)
		{

			int totalSatisfaction = 0;
			foreach (Agent a in agents)
			{
				totalSatisfaction += a.WeightedSatisfaction();
			}
			aws = totalSatisfaction / agents.Count;
			//Debug.Log("AWS is " + aws);
		}

		return aws;
	}
	public static string SatisfactionsList()
	{
		string satisfactions = "Satisfactions:";
		string satNeeds = "Needs:\n--------";
		string satWants = "Quality of Life:\n--------";

		for (int i = 0; i < needs.Count; i++)
		{
			if(needs[i].isNecessary) {
				satNeeds += "\n" + needs[i].needName + ": " + AverageSatisfactionOfOneNeed(i) + "%";
			} else {
				satWants += "\n" + needs[i].needName + ": " + AverageSatisfactionOfOneNeed(i) + "%";
			}
		}
		satisfactions += "\n\n" + satNeeds + "\n\n" + satWants;
		satisfactions += "\n\n" + "Overall Approval Rating: " + AverageWeightedSatisfaction() + "%";
		return satisfactions;
	}
	static int AverageSatisfactionOfOneNeed(int needIndex)
	{
		int averageSatisfaction = 0;
		foreach (Agent a in agents)
		{
			if (a.needs != null && a.needs.ElementAtOrDefault(needIndex) != null)
			{
				//Debug.Log("Agent " + agents.IndexOf(a) + "has satisfaction for " + a.needs.ElementAtOrDefault(needIndex).needName + " at " + a.needs[needIndex].value);
				averageSatisfaction += a.needs[needIndex].value;

			}

		}
		averageSatisfaction = averageSatisfaction / agents.Count;
		return averageSatisfaction;
	}
	public static void AddTask(SimHexType destinationType, SimHexType desiredType, int duration)
	{
		int[] taskListDurations = new int[agents.Count];
		for (int i = 0; i < agents.Count; i++)
		{
			for (int j = 0; j < agents[i].taskList.Count; j++)
			{
				taskListDurations[i] += agents[i].taskList[j].duration;
			}

		}
		int minIndex = 0;
		try
		{
			minIndex = Array.IndexOf(taskListDurations, taskListDurations.Min());
		}
		catch
		{

		}
		agents[minIndex].AddTask(destinationType, desiredType, duration);
	}

	public static void XToYByName(string xTypeName, string yTypeName, int duration)
	{

		AddTask(HexTypes.TypeByName(xTypeName), HexTypes.TypeByName(yTypeName), duration);
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

	public static void ContinuousContentTicking(int threshold)
	{
		if (AverageWeightedSatisfaction() >= threshold)
		{
			lengthOfContent++;
		}
		else
		{
			lengthOfContent = 0;
		}

	}
}