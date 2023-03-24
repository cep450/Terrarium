using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need
{

	public string needName;
	public int initValue;
	public int consumptionPerTick;
	public int weight;
	public int value;
	public bool isNecessary;

	public Need(string needName, int initValue, int consumptionPerTick, int weight, bool isNecessary)
	{
		this.needName = needName;
		this.initValue = initValue;
		this.consumptionPerTick = consumptionPerTick;
		this.weight = weight;
		this.isNecessary = isNecessary;
		value = initValue;
	}
}
