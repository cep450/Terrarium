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

	SimHex [] neighbors;

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

	public void LoadNeighbors() {
		List<Cube> neighborCubes = Sim.hexMap.grid.Neighbors(this.cube);
		neighbors = new SimHex[neighborCubes.Count];
		for(int i = 0; i < neighborCubes.Count; i++) {
			neighbors[i] = neighborCubes[i].simHex;
		}
	}

	//Called when this type changes type.
	public void ChangeType(SimHexType newType)
	{
		if(newType == null) {
			Debug.LogError("ERR: tried to change type to null");
			return;
		}

		if(type != null) {
			if(type.Equals(newType)) {
				Debug.LogError("tried to change a " + type.name + " tile to its own type");
				return; //also prevents adding starting resources
			}

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
		InputStatus result = CheckInputs();
		if(result == InputStatus.Satisfied) {
			ConsumeInputs();
			currentTick = tickNum;
			inputsSatisfied = true;
		} else if(result == InputStatus.NotSatisfiedDies) {
			Die();
		}
	}

	public void OutputTick(int tickNum)
	{

		//TODO get tick checking correct
		/*if (tickNum != currentTick + 1)
		{
			Debug.LogError("ERR: skipped a tick somewhere! fix this/account for this!");
			return;
		}*/

		if (inputsSatisfied)
		{
			CreateOutputs();
			inputsSatisfied = false;
		}
	}

	public enum InputStatus {
		NotSatisfiedDies, NotSatisfied, Satisfied
	}
	public InputStatus CheckInputs()
	{

		foreach (ResRequired rr in type.resourcesRequired)
		{

			if (rr.id == Resource.nullResId)
			{
				continue;
			}

			int sum = 0;

			//factor in self 
			sum += resourcesHas[rr.id];

			//factor in neighbors, if applicable 
			//TODO for now, only possible to check immediate neighbors, radius needs more
			if(rr.radius > 0) {
				foreach(SimHex neighbor in neighbors) {
					sum += neighbor.resourcesHas[rr.id];
				}
			}
			
			if (!(sum >= rr.amount))
			{
				if(rr.diesIfNotNet) {
					return InputStatus.NotSatisfiedDies;
				} else {
					return InputStatus.NotSatisfied;
				}
			}
		}
		return InputStatus.Satisfied;
	}

	public void ConsumeInputs()
	{

		foreach (ResRequired rr in type.resourcesRequired)
		{

			if (rr.isConsumed)
			{

				int amount = rr.amount;



				//tries self first 

				//then checks neighbors if applicable 


				//TODO factor in radius 

				resourcesHas[rr.id] -= rr.amount;

			}
		}

		visualHex.VisualUpdate();
	}

	public void CreateOutputs()
	{

		//TODO factor in modulo

		foreach (ResProduced rp in type.resourcesProduced)
		{

			if(rp.isHex) {
				//flipping a hex

				//TODO we'll need rules about priority. maybe use their order in the hextype []
				//TODO also factor in larger radiuses

				//look for a tile we can affect, starting from a random index
				int rand = Random.Range(0, neighbors.Length);
				int changeIndex = -1;
				for(int i = 0; i < neighbors.Length; i++) {
					int index = (rand + i) % neighbors.Length;
					SimHex neighbor = neighbors[index]; //wrap

					//if we can change it
					if(System.Array.IndexOf<string>(rp.changes, neighbor.type.name) >= 0) {
						changeIndex = index;
						break;
					}
				}
				if(changeIndex == -1) {
					//no valid tile to flip 
					continue;
				}
				neighbors[changeIndex].ChangeType(HexTypes.TypeByName(rp.name));
				//TODO probably have to factor in if multiple change types affect the same tile in the same frame
				//chekcing the type might work?
			} else {
				//producing resources 
				resourcesHas[rp.id] += rp.amount;

				//TODO account for higher radius and falloff 
				if(rp.radius > 0) {
					foreach(SimHex neighbor in neighbors) {
						neighbor.resourcesHas[rp.id] += rp.amount;
					}
				}

				//visualHex.VisualUpdate(); //TODO when adding sprite effects

			}
		}
	}

	public void Die() {

		//todo it could also leave behind resources on death?

		ChangeType(HexTypes.TypeByName(type.deathHexName));
	}
}
