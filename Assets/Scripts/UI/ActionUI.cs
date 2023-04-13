using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionUI : MonoBehaviour
{
	[SerializeField] TMP_Dropdown convertX;
	[SerializeField] TMP_Dropdown toY;
	SimHexType x;
	SimHexType y;

	public void SetX()
	{
		x = Sim.hexTypes[convertX.value];
	}
	public void SetY()
	{
		y = Sim.hexTypes[toY.value];
	}
	public void Convert()
	{
		if (x != null && y != null && !(x == y))
		{
			AgentDirector.XToYByName(x.name, y.name, UnityEngine.Random.Range(5, 15)); // random duration for testing
		}

	}

	void Start() {
		StartCoroutine(WaitForSimInit());
	}
	IEnumerator WaitForSimInit() {

		while(Sim.hexTypes == null) {
			yield return null;
		}
		Init();
	}

	// Start is called before the first frame update
	void Init()
	{
		foreach (SimHexType s in Sim.hexTypes)
		{
			convertX.options.Add(new TMP_Dropdown.OptionData(s.displayName));
			toY.options.Add(new TMP_Dropdown.OptionData(s.displayName));
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
}
