using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	int rateOfSatisfactionChange = 20;
	[SerializeField] public VisualGnome visualGnome;

	private void Start()
	{
		cube = simHex.cube;
		map = Sim.hexMap;
		consumptionRate = 10;
		rateOfSatisfactionChange = 20;
		lockedTarget = cube;
		visualGnome.AnimIdle();
	}
	public void CreateNeedList(List<Need> needs)
	{
		this.needs = new List<Need>();
		foreach (Need n in needs)
		{
			this.needs.Add((Need)n.ShallowCopy());
		}

	}

	public void Consume()
	{
		foreach (Need n in needs)
		{
			if (n.isConsumed) // food water honey
			{
				if (GlobalPool.CanConsume(n.needName, n.consumptionPerTick))
				{
					GlobalPool.Consume(n.needName, n.consumptionPerTick);
					n.value += rateOfSatisfactionChange; // need satisfaction goes up if met, numbers arbitrary
					if (n.value >= 100)
					{
						n.value = 100; // maximum 100
					}
				}
				else
				{
					n.value -= rateOfSatisfactionChange; // need satisfaction goes down if not met, numbers arbitrary
					if (n.value <= 0)
					{
						n.value = 0; // minimum 0
					}
				}
			}
			else // leisure housing
			{
				//Debug.Log("I have this many resources" + AgentDirector.shallowResources[Resource.IdByName(n.needName)] + " for " + n.needName);
				if (AgentDirector.shallowResources[Resource.IdByName(n.needName)] >= n.consumptionPerTick)
				{
					AgentDirector.shallowResources[Resource.IdByName(n.needName)] -= n.consumptionPerTick;
					//Debug.Log("I ate " + n.consumptionPerTick + " and now i have " + AgentDirector.shallowResources[Resource.IdByName(n.needName)]);
					n.value += rateOfSatisfactionChange; // need satisfaction goes up if met, numbers arbitrary
					if (n.value >= 100)
					{
						n.value = 100; // maximum 100
					}
				}
				else
				{
					n.value -= rateOfSatisfactionChange; // need satisfaction goes down if not met, numbers arbitrary
					if (n.value <= 0)
					{
						n.value = 0; // minimum 0
					}
				}
				Debug.Log("my " + n.needName + " need is at " + n.value);

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

		// cap the satisfaction at the least satisfied necessary need
		List<Need> necessities = new List<Need>();
		foreach (Need n in needs)
		{
			if (n.isNecessary)
			{
				necessities.Add(n);
			}
		}
		int leastSatisfiedNecessity = necessities.Min(need => need.value);
		//Debug.Log("my least satisfied necessity is at " + leastSatisfiedNecessity);
		int finalSatisfication;
		if (leastSatisfiedNecessity < weightedSatisfaction)
		{
			finalSatisfication = leastSatisfiedNecessity;
		}
		else
		{
			finalSatisfication = weightedSatisfaction;
		}

		//Debug.Log("my final satisfaction is " + finalSatisfication);
		return finalSatisfication; // return the lower of the two
	}
	public void CreateTaskList()
	{
		taskList = new List<Task>();
	}


	public void AddTask(SimHexType destinationType, SimHexType desiredType, int duration)
	{
		Task sampleTask0 = new Task(destinationType, desiredType, duration);
		taskList.Add(sampleTask0);
		WorkOrderUIController.AddWorkOrderUI(sampleTask0, this);
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
					if (currentTask.duration > 0) // decrement duration count
					{
						//visualGnome.myRenderer.color = Color.green;
						visualGnome.AnimWorking();

						currentTask.duration--;
					}
					else
					{
						//visualGnome.myRenderer.color = Color.white;
						visualGnome.AnimIdle();

						simHex.ChangeType(currentTask.desiredType);
						currentTask.isComplete = true; // doesnt do anything
						lockedTarget = cube;
						taskList.Remove(currentTask);
						isTaskInProgress = false;
					}
				}
			}
		}
		else
		{
			//task sequence begins
			currentTask = FindTask();
			if (currentTask != null)
			{
				List<SimHex> pathList = FindPathToType(simHex, currentTask.destinationType);
				if (pathList.Count > 0)
				{
					currentTask.destination = pathList[pathList.Count - 1];
					pathQueue = new Queue<SimHex>(FindPathToType(simHex, currentTask.destinationType));
					isTaskInProgress = true;

					visualGnome.AnimWalking();
				}
				else
				{
					currentTask.isComplete = true;
					taskList.Remove(currentTask);
				}
			}
			else
			{
				visualGnome.AnimIdle();
				//No task
			}
		}
	}

	Task FindTask()
	{
		if (taskList.Count > 0)
		{
			Task task = taskList[0];
			
			//Debug.Log("current task is " + task.destinationType.name);
			return task;
		}
		else
		{
			return null;
		}


	}

	public void Tick(int tickNum)
	{
		if (map != null)
		{
			transform.position = map.grid.HexToCenter(cube).position;
		}

		ExecuteTask();
		Consume();
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
				//Debug.Log("found goal at " + current.position + " and it's a " + current.simHex.type.name);
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
			//Debug.Log("Goal not found, destination type is " + destinationType.name);
			List<SimHex> path = new List<SimHex>();
			//taskList.Remove(currentTask);
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