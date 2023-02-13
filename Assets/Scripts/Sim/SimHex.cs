using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Resource;

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
	public byte[] resourcesHas = new byte[Sim.resources.Length]; //this changes 


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
		foreach (ResStarting rs in type.resourcesStarting)
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

		foreach (ResRequired rr in type.resourcesRequired)
		{

			if (rr.id == Resource.nullResId)
			{
				continue;
			}

			//TODO factor in radius

			if (!(resourcesHas[rr.id] >= rr.amount))
			{
				return false;
			}
		}
		return true;

	}

	public void ConsumeInputs()
	{

		foreach (ResRequired rr in type.resourcesRequired)
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

		foreach (ResProduced rp in type.resourcesProduced)
		{

			//TODO factor in if is hex 

			resourcesHas[rp.id] += rp.amount;

		}

		visualHex.VisualUpdate();
	}

	//List<SimHex> NeighborSimHexes() {
		
	//}
}
