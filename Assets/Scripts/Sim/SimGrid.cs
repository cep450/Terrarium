using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGrid
{
	/*
        INTERNAL representation of the hex grid.

        For stuff like avoiding the "half my neighbors have updated but 
        half haven't and now the calculation is wrong" problem.
    */

	//Parallel list with the list of Cubes in the HexGrid in the HexMap. 
	public static SimHex[] hexes;
	public static int[] typeCountsAvailable {get; private set;} //band aid- parallel with Sim.hexTypes, updated by work orders

	public static void Init()
	{

		typeCountsAvailable = new int[Sim.hexTypes.Length];

		hexes = new SimHex[Sim.hexMap.grid.Hexes.Count];

		Generate(RanjitToPercent(Sim.hexGenRanjit));

	}

	//number of hexes of this type on the board.
	public static int NumberOfType(SimHexType type) {
		int counter = 0;
		foreach(SimHex hex in hexes) {
			if(hex.type.Equals(type)) {
				counter++;
			}
		}
		return counter;
	}

	//number of hexes of this type on the board that aren't marked for work.
	//right now, hexes are marked for work when the gnome starts doing the work order, not when it recieves it
	//so currently using the typeCounts for checking if can add work order instead. 
	public static int NumberOfTypeAvailable(SimHexType type) {
		int counter = 0;
		foreach(SimHex hex in hexes) {
			if(hex.type.Equals(type) && !hex.workTarget) {
				counter++;
			}
		}
		return counter;
	}

	public static void IncrementAvailable(SimHexType type) {
		typeCountsAvailable[type.id]++;
	}
	public static void DecrementAvailable(SimHexType type) {
		typeCountsAvailable[type.id]--;
	}
	//is there a hex on the board of this type that doesn't have a pending work order?
	public static bool TypeAvailable(SimHexType type) {
		return (typeCountsAvailable[type.id] > 0 );
	}

	//pick a random hex on the board that's not marked for work.
	//public static SimHex ChooseAvailableHex(SimHexType type) {
		//TODO 
		//TODO: where does a task choose its destination? the destination hex now needs to know it's marked for work.
		//ok it chooses line 206 of Agent 
		//the nearest simhex 
		//but we really need to update the counts beforehand so you can't queue work orders beforehand 
		//and what about the whole turning to dirt thing? 
		//test this out 
	//}

	//returns CUMULATIVE percent thresholds so the last one should be 100%
	//cumulative probability spread 
	static float[] RanjitToPercent(float[] ranjit)
	{
		float[] percents = new float[ranjit.Length];
		float total = 0;
		for (int i = 0; i < ranjit.Length; i++)
		{
			total += ranjit[i];
		}
		for (int i = 0; i < ranjit.Length; i++)
		{
			percents[i] = ranjit[i] / total;
			if (i > 0)
			{
				percents[i] = percents[i - 1] + percents[i];
			}
		}
		return percents;
	}

	static int IndexFromPercent(float percent, float[] percentArr)
	{

		for (int i = 0; i < percentArr.Length; i++)
		{
			if (percentArr[i] > percent)
			{
				return i;
			}
		}
		return percentArr.Length - 1;
	}

	//Generate a map.
	static void Generate(float[] typePercents)
	{

		if (typePercents.Length != Sim.hexTypes.Length)
		{
			Debug.LogError("ERR: ranjit range proportions arr did not match length of hex types arr. did you forget to add a value to this parallel array?");
			return;
		}

		//TODO prototype temp- randomized types 
		for (int i = 0; i < hexes.Length; i++)
		{
			int rand = IndexFromPercent(Random.Range(0f, 1f), typePercents);
			hexes[i] = new SimHex(Sim.hexTypes[rand], Sim.hexMap.grid.Hexes[i]);
			Sim.hexMap.grid.Hexes[i].simHex = hexes[i];
		}
		//done after all initialized to avoid null neighbors
		foreach (SimHex hex in hexes)
		{
			hex.LoadNeighbors();
		}

		GenElevation();
		GenWaterInLowElevations();

		foreach (SimHex hex in hexes)
		{
			hex.visualHex.VisualUpdate();
		}

	}

	static void GenElevation()
	{

		foreach (SimHex hex in SimGrid.hexes)
		{
			hex.elevation = Mathf.PerlinNoise((float)hex.cube.position.x, (float)hex.cube.position.y);
			//Debug.Log(hex.elevation);
			//Debug.Log("x" + hex.cube.position.x + " y" + hex.cube.position.y + " z" + hex.cube.position.z);
		}
	}

	static float waterThreshold = 0.1f;
	static void GenWaterInLowElevations()
	{
		foreach (SimHex hex in hexes)
		{
			if (hex.elevation <= waterThreshold)
			{
				hex.ChangeType(HexTypes.TypeByName("Water"));
			}
		}
	}

	//Step 1: try to do inputs, check for and consume resources. 
	//this step is where resources are consumed/removed. 
	//Don't make changes beyond consumption, which is there to make sure something is only consumed once.
	//TODO: what if we want certain things to have consumption priority over other things?
	public static void TickInputs(int tickNum)
	{
		foreach (SimHex h in hexes)
		{
			h.InputTick(tickNum);
		}
	}

	//Step 2: do outputs if requirements met.
	//this step is where resources are created. 
	public static void TickOutputs(int tickNum)
	{
		foreach (SimHex h in hexes)
		{
			h.OutputTick(tickNum);
		}
	}

}