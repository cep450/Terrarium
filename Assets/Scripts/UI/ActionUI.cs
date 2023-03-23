using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionUI : MonoBehaviour
{
	[SerializeField] SimHexType[] types;
	[SerializeField] TMP_Dropdown convertX;
	[SerializeField] TMP_Dropdown toY;
	SimHexType x;
	SimHexType y;

	public void SetX()
	{
		x = types[convertX.value];
	}
	public void SetY()
	{
		y = types[toY.value];
	}
	public void Convert()
	{
		if (x != null && y != null && !(x == y))
		{
			AgentDirector.XToYByName(x.name, y.name);
		}

	}
	// Start is called before the first frame update
	void Start()
	{
		foreach (SimHexType s in types)
		{
			convertX.options.Add(new TMP_Dropdown.OptionData(s.name));
			toY.options.Add(new TMP_Dropdown.OptionData(s.name));
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
}
