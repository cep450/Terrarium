using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	static ResourceListUI resourceListUI;
	[SerializeField] ResourceListUI _resourceListUI;
	//static SatisfactionSlider satisfactionUI;
	//[SerializeField] SatisfactionSlider _satisfactionUI;
	// Start is called before the first frame update
	void Awake() //I Don't know why I need Awake here, but Start just doesnt work
	{
		resourceListUI = _resourceListUI;
		//satisfactionUI = _satisfactionUI;
	}

	public static void Init()
	{
		resourceListUI.PopulateList();
		//satisfactionUI.PopulateList();
		resourceListUI.Tick();
		//satisfactionUI.Tick();

	}
	public static void UITicking()
	{
		resourceListUI.Tick();
		//satisfactionUI.Tick();
	}
}
