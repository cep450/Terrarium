using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	static ResourceListUI resourceListUI;
	[SerializeField] ResourceListUI _resourceListUI;
	// Start is called before the first frame update
	void Awake() //I Don't know why I need Awake here, but Start just doesnt work
	{
		resourceListUI = _resourceListUI;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public static void Init()
	{
		resourceListUI.PopulateList();
	}
}
