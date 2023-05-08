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
	static Slider mySlider;
	[SerializeField] GameObject restart;
	static GameObject myRestart;
	[SerializeField] GameObject victory;
	static GameObject myVictory;
	[SerializeField] GameObject arrowRight;
	static GameObject myArrowRight;
	[SerializeField] GameObject arrowLeft;
	static GameObject myArrowLeft;

	[SerializeField] Sprite gnomeSad, gnomeNeutral, gnomeHappy;
	[SerializeField] float sadNeutralPercent, neutralHappyPercent;
	[SerializeField] Image face;

	static FailureState instance;

	// Start is called before the first frame update
	void Start()
	{
		instance = this;
		approvalCounter = 0;
		myFailureThreshold = failureThreshold;
		myFailureCap = failureCap;
		myVictoryThreshold = victoryThreshold;
		myVictoryCap = victoryCap;
		mySlider = failureSlider.GetComponent<Slider>();
		myRateOfChange = rateOfChange;
		mySlider.GetComponent<Slider>().maxValue = myVictoryCap;
		mySlider.GetComponent<Slider>().minValue = myFailureCap;
		mySlider.GetComponent<Slider>().value = approvalCounter;
		myRestart = restart;
		myVictory = victory;
		myArrowLeft = arrowLeft;
		myArrowRight = arrowRight;
		mySlider.gameObject.SetActive(true);
	}

	public static void UpdateCounter()
	{
		if (AgentDirector.AverageWeightedSatisfaction() < myFailureThreshold)
		{

			approvalCounter -= myRateOfChange;
			myArrowLeft.SetActive(true);
			myArrowRight.SetActive(false);
			if (approvalCounter <= myFailureCap)
			{
				Fail();
			}
		}
		else if (AgentDirector.AverageWeightedSatisfaction() > myVictoryThreshold)
		{
			approvalCounter += myRateOfChange;
			myArrowRight.SetActive(true);
			myArrowLeft.SetActive(false);
			if (approvalCounter >= myVictoryCap)
			{
				Win();
			}

		}
		else if (approvalCounter > AgentDirector.AverageWeightedSatisfaction())
		{
			approvalCounter -= myRateOfChange;
			myArrowLeft.SetActive(true);
			myArrowRight.SetActive(false);

		}
		else if (approvalCounter < AgentDirector.AverageWeightedSatisfaction())
		{
			approvalCounter += myRateOfChange;
			myArrowRight.SetActive(true);
			myArrowLeft.SetActive(false);
		}
		else
		{
			approvalCounter = AgentDirector.AverageWeightedSatisfaction();
			myArrowLeft.SetActive(false);
			myArrowRight.SetActive(false);
		}
		mySlider.value = approvalCounter;

		instance.SetFace();

	}

	void SetFace() {
		float percent = (mySlider.value - mySlider.minValue) / (mySlider.maxValue - mySlider.minValue);
		if(percent < sadNeutralPercent) {
			face.sprite = gnomeSad;
		} else if(percent < neutralHappyPercent) {
			face.sprite = gnomeNeutral;
		} else {
			face.sprite = gnomeHappy;
		}
	}

	static void Fail()
	{
		myRestart.SetActive(true);
		Clock.Pause();
		Clock.canPlay = false;
	}
	static void Win()
	{
		myVictory.SetActive(true);
	}
	static void Disappear()
	{
		mySlider.gameObject.SetActive(false);
	}
}
