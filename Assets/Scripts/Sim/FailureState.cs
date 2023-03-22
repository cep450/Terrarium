using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureState : MonoBehaviour
{
	public static int failureCounter;
	[SerializeField] int failureThreshold;
	static int myThreshold;
	[SerializeField] int failureCap;
	static int myCap;
	static Slider failureSlider;
	[SerializeField] GameObject restart;
	static GameObject myRestart;
	// Start is called before the first frame update
	void Start()
	{
		failureCounter = 0;
		myThreshold = failureThreshold;
		myCap = failureCap;
		failureSlider = gameObject.GetComponent<Slider>();
		failureSlider.maxValue = myCap;
		failureSlider.minValue = 0;
		myRestart = restart;
	}

	// Update is called once per frame
	void Update()
	{

	}
	public static void UpdateCounter()
	{
		if (AgentDirector.AverageWeightedSatisfaction() < myThreshold)
		{
			failureSlider.enabled = true;
			failureCounter++;
			failureSlider.value = failureCounter;
			if (failureCounter >= myCap)
			{
				Fail();
			}
		}
		else
		{
			failureCounter--;
			if (failureCounter <= 0)
			{
				failureCounter = 0;
				Disappear();
			}
		}
	}
	static void Fail()
	{
		Debug.Log("You Failed");
		myRestart.SetActive(true);
	}
	static void Disappear()
	{

		failureSlider.enabled = false;
	}
}
