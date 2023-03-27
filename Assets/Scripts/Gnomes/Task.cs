using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
	public SimHexType destinationType; // what type to look for
	public SimHexType desiredType; // convert to what
	public SimHex destination; // where to go
	public bool isComplete = false;
	public int duration;

	public Task(SimHexType destinationType, SimHexType desiredType, int duration)
	{
		this.destinationType = destinationType;
		this.desiredType = desiredType;
		this.duration = duration;

	}
	public void SetDestination(SimHex destination)
	{
		this.destination = destination;
	}
	public void ConvertHex()
	{
		destination.ChangeType(desiredType);
	}
}
