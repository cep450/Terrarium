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

	public float elevation = 0f;

	//resource amounts. index of array corresponds to resource id. get by resource id.
	//every SimHex takes up a const amount of memory.
	//these inform graphical rendering, sprite blending, making sprites appear
	//go up to 255
	public int[] resourcesHas = new int[Sim.resources.Length]; //this changes 

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
			Tracker.AddedHex(newType.id, 1);
			Tracker.RemovedHex(type.id, 1);

			//override old starting res if needed 
			foreach(ResStarting rs in type.resourcesStarting) {
				if(rs.overrides && !rs.local) {
					GlobalPool.Consume(rs.id, rs.amount);
				}
			}
		}

		//add each starting resource to this tile
		foreach (ResStarting rs in newType.resourcesStarting)
		{
			if(rs.local) {
				if(rs.overrides) {
					ConsumeResource(rs.id, resourcesHas[rs.id]);
				}
				AddResource(rs.id, rs.amount);
			} else {
				GlobalPool.Add(rs.id, rs.amount);
			}
			
		}

		type = newType;

		visualHex.VisualUpdate();

	}

	//add a resource to this hex
	public void AddResource(int id, int amount) {
		resourcesHas[id] += amount;
		Tracker.AddedRes(id, amount);
	}
	public void AddResource(string name, int amount) {
		AddResource(Resource.IdByName(name), amount);
	}
	public bool HasResource(int id, int amount) {
		return resourcesHas[id] >= amount;
	}
	public void HasResource(string name, int amount) {
		HasResource(Resource.IdByName(name), amount);
	}
	//consume a resource from this hex
	public void ConsumeResource(string name, int amount) {
		ConsumeResource(Resource.IdByName(name), amount);
	}
	public void ConsumeResource(int id, int amount) {
		resourcesHas[id] -= amount;
		Tracker.UsedRes(id, amount);
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
			CreateOutputs(tickNum);
			inputsSatisfied = false;
		}
	}

	public enum InputStatus {
		NotSatisfiedDies, NotSatisfied, Satisfied
	}
	public InputStatus CheckInputs()
	{
		InputStatus status = InputStatus.Satisfied;

		foreach (ResRequired rr in type.resourcesRequired)
		{

			if (rr.id == Resource.nullResId)
			{
				continue;
			}

			int sum = 0;

			if(!rr.local) { //drawing from global 

				sum = GlobalPool.resources[rr.id];

			} else { //drawing from local- self and neighbors 

				//factor in self 
				sum += resourcesHas[rr.id];

				//factor in neighbors, if applicable 
				//TODO for now, only possible to check immediate neighbors, radius needs more
				if(rr.radius > 0) {
					foreach(SimHex neighbor in neighbors) {
						sum += neighbor.resourcesHas[rr.id];
					}
				}
			}
			
			if (sum < rr.amount) {
				if(rr.diesIfNotNet) {
					return InputStatus.NotSatisfiedDies; //dies, end 
				} else {
					status = InputStatus.NotSatisfied; //continue checking if it might die
				}
			}
		}
		return status;
	}

	public void ConsumeInputs()
	{

		foreach (ResRequired rr in type.resourcesRequired)
		{

			if (rr.isConsumed)
			{

				if(!rr.local) { //consumes from global pool

					GlobalPool.Consume(rr.id, rr.amount);

				} else if(rr.radius > 0) { //consumes from self and neighbors

					int amount = rr.amount;

					//TODO factor in radius 

					//cycle thru self and neighbors consuming resources til satisfied
					//TODO start i at a random neighbor index?
					for(int i = 0; amount > 0; i++) {
						if(i == 0) {
							this.ConsumeResource(rr.id, 1);
						} else {
							neighbors[i - 1].ConsumeResource(rr.id, 1);
						}
						amount--;
						if(i > neighbors.Length) {
							i = 0;
						}
					}
					
				} else { //just consumes from self

					ConsumeResource(rr.id, rr.amount);

				}
			}
		}

		visualHex.VisualUpdate();
	}

	public void CreateOutputs(int tickNum)
	{

		//TODO factor in modulo

		foreach (ResProduced rp in type.resourcesProduced)
		{

			if(rp.isHex) {
				//flipping a hex

				//TODO we'll need rules about priority. maybe use their order in the hextype []
				//TODO also factor in larger radiuses

				//if we only do this every x ticks
				if(rp.tickMod > 0) {
					if(tickNum % rp.tickMod != 0) {
						//doesn't produce this tick
						continue;
					}
				}

				//look for a tile we can affect, starting from a random index
				int rand = Random.Range(0, neighbors.Length);

				//if we try every neighbor
				if(rp.tryAll) {
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
						//no valid hex to flip 
						continue;
					}
					rand = changeIndex;
				} else {
					//just the one 
					if(!(System.Array.IndexOf<string>(rp.changes, neighbors[rand].type.name) >= 0)) {
						//no valid hex to flip here 
						continue;
					}
				}
				//ok, flip this 
				neighbors[rand].ChangeType(HexTypes.TypeByName(rp.name));
				//TODO probably have to factor in if multiple change types affect the same tile in the same frame
				//chekcing the type might work?

			} else {
				//producing resources 

				if(!rp.local) { //producing to global pool 

					if(rp.cap > 0) { //don't produce over cap 
						int diff = rp.cap - GlobalPool.resources[rp.id];
						GlobalPool.Add(rp.id, Mathf.Min(rp.amount, diff));
					} else {
						GlobalPool.Add(rp.id, rp.amount);
					}

				} else { //producing to self and neighbors 

					if(rp.cap > 0) { //don't produce over cap 
						int diff = rp.cap - resourcesHas[rp.id];
						AddResource(rp.id, Mathf.Min(rp.amount, diff));
					} else {
						AddResource(rp.id, rp.amount);
					}

					//TODO what if a tile is full up?
					//TODO is the amount per tile or does it get spread between them?
				
					//TODO account for higher radius and falloff 
					if(rp.radius > 0) {
						foreach(SimHex neighbor in neighbors) {

							if(rp.cap > 0) { //don't produce over cap 
								int diff = rp.cap - neighbor.resourcesHas[rp.id];
								neighbor.AddResource(rp.id, Mathf.Min(rp.amount, diff));
							} else {
								neighbor.AddResource(rp.id, rp.amount);
							}
						}
					}

				}

				//visualHex.VisualUpdate(); //TODO when adding sprite effects
				//TODO maybe put this in AddResource and Consume so it auto updates?
				//visual update should check its hex's resources if it meets threshes for showing variuos stuff 
				
			}
		}
	}

	public void Die() {

		//todo it could also leave behind resources on death?

		ChangeType(HexTypes.TypeByName(type.deathHexName));
	}
}
