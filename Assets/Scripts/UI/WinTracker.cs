using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinTracker : MonoBehaviour
{
	string text;
	[SerializeField] int satisfactionThreshold;
	[SerializeField] int lengthRequired;
	[SerializeField] GameObject victory;
	// Start is called before the first frame update
	void Start()
	{
		AgentDirector.satisfactionThreshold = satisfactionThreshold;



	}

	// Update is called once per frame
	void Update()
	{
		GetComponent<TextMeshProUGUI>().text = text;
		text = "Keep overall satisfaction above " + satisfactionThreshold + " for " + lengthRequired + " ticks. Current score: " + AgentDirector.lengthOfContent;
		if (AgentDirector.lengthOfContent >= lengthRequired)
		{
			victory.SetActive(true);
		}
	}
}
