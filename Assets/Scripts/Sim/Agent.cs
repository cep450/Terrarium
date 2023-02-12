using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

	/*
        Something in the sim that can move to different tiles.
        Like a gnome or a creature. 
    */

	//TODO knows its location, its type,
	public SimHex simHex;
	public Cube cube;
	public Cube goal;
	public HexMap map;
	public List<Task> taskList;
	public List<SimHexType> simHexTypes;
	public bool isTaskInProgress = false;
	Task currentTask;
	Queue<SimHex> pathQueue;
	private void Start()
	{

		cube = simHex.cube;
		map = Sim.hexMap;

	}
	public void CreateTaskList()
	{

		taskList = new List<Task>();
		Task sampleTask0 = new Task(simHex.type, simHex.type);
		Debug.Log("sampletask0: destinationType is " + sampleTask0.destinationType.name + " and desiredType is " + sampleTask0.desiredType.name);
		taskList.Add(sampleTask0);
	}
	public void AddTask(int destinationTypeIndex, int desiredTypeIndex)
	{
		Task sampleTask0 = new Task(simHexTypes[destinationTypeIndex], simHexTypes[desiredTypeIndex]);
		taskList.Add(sampleTask0);
	}
	public enum agentType
	{
		gnome,
		somethingElse
	}
	public Agent(SimHex simHex)
	{
		this.simHex = simHex;
		this.cube = simHex.cube;
	}
	public void Tick(int tickNum)
	{
		Debug.Log("agent ticking");
		if (map != null)
		{
			transform.position = map.grid.HexToCenter(cube).position;
		}
		if (isTaskInProgress)
		{
			if (currentTask != null)
			{
				if (currentTask.destination != simHex)
				{
					if (pathQueue.Count > 0)
					{
						simHex = pathQueue.Dequeue();
						cube = simHex.cube;
					}
				}
				else // upon arrival
				{
					simHex.ChangeType(currentTask.desiredType);
					currentTask.isComplete = true;
					isTaskInProgress = false;
				}
			}
		}
		else
		{
			Debug.Log("task sequence begins");
			currentTask = FindTask();
			isTaskInProgress = true;
			List<SimHex> pathList = FindPathToType(simHex, currentTask.destinationType);
			currentTask.destination = pathList[pathList.Count - 1];
			pathQueue = new Queue<SimHex>(FindPathToType(simHex, currentTask.destinationType));
		}

	}
	Task FindTask()
	{
		if (taskList.Count <= 0)
		{
			CreateTaskList();

		}

		Task task = taskList[0];
		taskList.Remove(task);
		Debug.Log("current task is " + task.destinationType.name);
		return task;


	}

	List<SimHex> FindPathToType(SimHex start, SimHexType destinationType)
	{
		Queue<Cube> frontier = new Queue<Cube>();
		frontier.Enqueue(start.cube);
		Dictionary<Cube, Cube> came_from = new Dictionary<Cube, Cube>();
		came_from[start.cube] = null;
		Cube current;
		while (frontier.Count > 0)
		{
			current = frontier.Dequeue();

			if (current.simHex.type == destinationType)
			{
				// do something!
				goal = current;
				Debug.Log("found goal at " + current.position + " and it's a " + current.simHex.type.name);
				break;
			}
			else
			{
				Debug.Log("goal not found. destinationType is " + destinationType);
			}

			foreach (Cube next in map.grid.Neighbors(current))
			{
				if (!came_from.ContainsKey(next))
				{
					frontier.Enqueue(next);
					came_from[next] = current;
				}
			}
		}

		//Retrace
		current = goal;
		List<SimHex> path = new List<SimHex>();
		while (current != start.cube)
		{
			path.Add(current.simHex);
			current = came_from[current];
		}

		path.Add(start);
		path.Reverse();
		return path;
	}

}