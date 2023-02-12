using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHex
{

	/*
        Internal representation of a hex instance.
        Stores the data particular to an instance that can change.
        Checks if inputs are satisfied, if they are, consumes what it consumes and produces what it produces
    */

	//TODO probably has a reference to the coordinate map representation of where the hex is

	//is it a plant, a house, ect 
	public SimHexType type { get; private set; }

	public Cube cube { get; private set; }
	public VisualHex visualHex { get; private set; }

	//resource amounts. index of array corresponds to resource id. get by resource id.
	//every SimHex takes up a const amount of memory.
	//these inform graphical rendering, sprite blending, making sprites appear
	//go up to 255
	public byte[] resourcesHas = new byte[Res.resources.Length]; //this changes 


	//for tracking if stuff was satisfied the last tick 
	int currentTick = -1;
	bool inputsSatisfied = false;

	public SimHex(SimHexType t, Cube c)
	{
		cube = c;
		cube.simHex = this;

		//TODO move this 
		visualHex = GameObject.Instantiate(Sim.visualHexPrefab);
		visualHex.AssignSimHex(this);

		ChangeType(t);
	}

	//Called when this type changes type.
	public void ChangeType(SimHexType newType)
	{
		if (type!=null)
		{
			Debug.Log("type changed at " + cube + "from " + type.name + " to " + newType.name);
		}
		
		type = newType;

		//add each starting resource to this tile
		foreach (Res.ResStarting rs in type.resourcesStarting)
		{
			resourcesHas[rs.id] += rs.amount;
		}

		visualHex.VisualUpdate();

	}


	//TODO be checking the tick number for safety 
	public void InputTick(int tickNum)
	{
		if (CheckInputs())
		{
			ConsumeInputs();
			currentTick = tickNum;
			inputsSatisfied = true;
		}
	}

	public void OutputTick(int tickNum)
	{

		Debug.Log("doing output tick in this hex");

		if (tickNum != currentTick + 1)
		{
			Debug.LogError("ERR: skipped a tick somewhere! fix this/account for this!");
			return;
		}

		if (inputsSatisfied)
		{
			CreateOutputs();
			inputsSatisfied = false;
		}
	}

	public bool CheckInputs()
	{

		Debug.Log("type is " + type.name);
		Debug.Log("resources requires length is " + type.resourcesRequired.Length);

		foreach (Res.ResRequired rr in type.resourcesRequired)
		{

			Debug.Log("resource was " + Res.NameById(rr.id) + " amount " + rr.amount + " has " + resourcesHas[rr.id]);

			if (rr.id == (int)Res.Resource.NULL)
			{
				Debug.Log("no resource stored");
				continue;
			}

			//TODO factor in radius

			if (!(resourcesHas[rr.id] >= rr.amount))
			{
				Debug.Log("resource requirements not met");
				return false;
			}
		}
		Debug.Log("Resource requirements met!");
		return true;

	}

	public void ConsumeInputs()
	{

		foreach (Res.ResRequired rr in type.resourcesRequired)
		{

			if (rr.isConsumed)
			{

				//TODO factor in radius 

				resourcesHas[rr.id] -= rr.amount;

			}
		}

		visualHex.VisualUpdate();
	}

	public void CreateOutputs()
	{

		foreach (Res.ResProduced rp in type.resourcesProduced)
		{

			//TODO factor in if is hex 

			resourcesHas[rp.id] += rp.amount;

		}

		Debug.Log("resources now: 0:" + resourcesHas[0] + " 1:" + resourcesHas[1] +
					" 2:" + resourcesHas[2] + " 3:" + resourcesHas[3]);

		visualHex.VisualUpdate();
	}
}
