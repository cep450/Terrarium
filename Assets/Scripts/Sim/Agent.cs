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
	int consumptionRate;
	Task currentTask;
	Queue<SimHex> pathQueue;
	public Cube lockedTarget;
	public List<Need> needs;
	private void Start()
	{
		cube = simHex.cube;
		map = Sim.hexMap;
		consumptionRate = 10;
		lockedTarget = cube;
	}
	public void CreateNeedList(List<Need> needs)
	{
		this.needs = new List<Need>(needs); // create a shallow copy 
	}
	public void Consume()
	{
		foreach (Need n in needs)
		{
			if (GlobalPool.CanConsume(n.needName, n.consumptionPerTick))
			{
				GlobalPool.Consume(n.needName, n.consumptionPerTick);
				n.value += 1; // need satisfaction goes up if met, numbers arbitrary
				if (n.value >= 100)
				{
					n.value = 100; // maximum 100
				}
			}
			else
			{
				n.value -= 1; // need satisfaction goes down if not met, numbers arbitrary
				if (n.value <= 0)
				{
					n.value = 0; // minimum 0
				}
			}

		}
	}
	public int WeightedSatisfaction()
	{

		int weightedValueSum = 0;
		int weights = 0;
		foreach (Need n in needs)
		{
			weightedValueSum += n.value * n.weight;
			weights += n.weight;
		}
		int weightedSatisfaction = weightedValueSum / weights;
		return weightedSatisfaction;
	}
	public void CreateTaskList()
	{

		taskList = new List<Task>();
		Task sampleTask0 = new Task(simHex.type, simHex.type);
		Debug.Log("sampletask0: destinationType is " + sampleTask0.destinationType.name + " and desiredType is " + sampleTask0.desiredType.name);
		taskList.Add(sampleTask0);
	}


	public void AddTask(SimHexType destinationType, SimHexType desiredType)
	{
		Task sampleTask0 = new Task(destinationType, desiredType);
		taskList.Add(sampleTask0);
	}

	public Agent(SimHex simHex)
	{
		this.simHex = simHex;
		this.cube = simHex.cube;
	}




	void ExecuteTask()
	{
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
					currentTask.isComplete = true; // doesnt do anything
					lockedTarget = cube;
					isTaskInProgress = false;
				}
			}
		}
		else
		{
			Debug.Log("task sequence begins");
			currentTask = FindTask();

			List<SimHex> pathList = FindPathToType(simHex, currentTask.destinationType);
			if (pathList.Count > 0)
			{
				currentTask.destination = pathList[pathList.Count - 1];
				pathQueue = new Queue<SimHex>(FindPathToType(simHex, currentTask.destinationType));
				isTaskInProgress = true;
			}

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

	public void Tick(int tickNum)
	{
		if (map != null)
		{
			transform.position = map.grid.HexToCenter(cube).position;
		}

		ExecuteTask();
		Consume();
		// consume a crops tile every X ticks

		//if (tickNum % consumptionRate == 0)
		//{
		//AddTask(HexTypes.TypeByName("vine"), HexTypes.TypeByName("dirt")); // convert crops to plants

		//}
	}


	List<SimHex> FindPathToType(SimHex start, SimHexType destinationType)
	{
		bool goalFound = false;
		Queue<Cube> frontier = new Queue<Cube>();
		frontier.Enqueue(start.cube);
		Dictionary<Cube, Cube> came_from = new Dictionary<Cube, Cube>();
		came_from[start.cube] = null;
		Cube current;
		while (frontier.Count > 0)
		{
			current = frontier.Dequeue();

			if (current.simHex.type == destinationType && !IsOccupied(current) && !IsLocked(current))
			{

				// do something!
				goal = current;
				Debug.Log("found goal at " + current.position + " and it's a " + current.simHex.type.name);
				lockedTarget = current;
				goalFound = true;
				break;
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
		if (goalFound)
		{
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
		else
		{
			Debug.Log("Goal not found, destination type is " + destinationType.name);
			List<SimHex> path = new List<SimHex>();
			isTaskInProgress = false;
			return path;
		}


	}

	bool IsOccupied(Cube targetCube)
	{
		bool isOccupied = false;
		foreach (Agent a in AgentDirector.GetAgents())
		{
			if (a != this)
			{
				if (targetCube == a.cube)
				{
					isOccupied = true;
					break;
				}
			}

		}
		return isOccupied;
	}
	bool IsLocked(Cube targetCube)
	{
		bool isLocked = false;
		foreach (Agent a in AgentDirector.GetAgents())
		{
			if (a != this)
			{
				if (targetCube == a.lockedTarget)
				{
					isLocked = true;
					break;
				}
			}

		}
		return isLocked;
	}

}