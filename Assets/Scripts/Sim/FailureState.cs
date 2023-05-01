using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureState : MonoBehaviour
{
	[SerializeField] int rateOfChange;
	static int myRateOfChange;
	public static int approvalCounter;
	[SerializeField] int failureThreshold;
	static int myFailureThreshold;
	[SerializeField] int failureCap;
	static int myFailureCap;
	[SerializeField] int victoryThreshold;
	static int myVictoryThreshold;
	[SerializeField] int victoryCap;
	static int myVictoryCap;
	[SerializeField] GameObject failureSlider;
	static GameObject mySlider;
	[SerializeField] GameObject restart;
	static GameObject myRestart;
	[SerializeField] GameObject victory;
	static GameObject myVictory;
	// Start is called before the first frame update
	void Start()
	{
		approvalCounter = 0;
		myFailureThreshold = failureThreshold;
		myFailureCap = failureCap;
		myVictoryThreshold = victoryThreshold;
		myVictoryCap = victoryCap;
		mySlider = failureSlider;
		myRateOfChange = rateOfChange;
		mySlider.GetComponent<Slider>().maxValue = myVictoryCap;
		mySlider.GetComponent<Slider>().minValue = myFailureCap;
		mySlider.GetComponent<Slider>().value = approvalCounter;
		myRestart = restart;
		myVictory = victory;
		mySlider.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{

	}
	public static void UpdateCounter()
	{
		if (AgentDirector.AverageWeightedSatisfaction() < myFailureThreshold)
		{

			approvalCounter -= myRateOfChange;
			if (approvalCounter <= myFailureCap)
			{
				Fail();
			}
		}
		else if (AgentDirector.AverageWeightedSatisfaction() > myVictoryThreshold)
		{
			approvalCounter += myRateOfChange;
			if (approvalCounter >= myVictoryCap)
			{
				Win();
			}

		}
		else if (approvalCounter > AgentDirector.AverageWeightedSatisfaction())
		{
			approvalCounter -= myRateOfChange;
		}
		else if (approvalCounter < AgentDirector.AverageWeightedSatisfaction())
		{
			approvalCounter += myRateOfChange;
		}
		else
		{
			approvalCounter = AgentDirector.AverageWeightedSatisfaction();
		}
		mySlider.GetComponent<Slider>().value = approvalCounter;
	}
	static void Fail()
	{
		Debug.Log("You Failed");
		myRestart.SetActive(true);
		Clock.Pause();
		Clock.canPlay = false;
	}
	static void Win()
	{
		Debug.Log("You Win");
		myVictory.SetActive(true);
	}
	static void Disappear()
	{

		mySlider.SetActive(false);
	}
}
