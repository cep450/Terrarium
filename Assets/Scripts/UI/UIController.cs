using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	static ResourceListUI resourceListUI;
	[SerializeField] ResourceListUI _resourceListUI;
	
	[SerializeField] GameObject escMenu;
	static GameObject instanceEscMenu;
	static bool pausedBeforeEsc = false;

	// Start is called before the first frame update
	void Awake() //I Don't know why I need Awake here, but Start just doesnt work
	{
		resourceListUI = _resourceListUI;
	}

	void Start() {
		instanceEscMenu = escMenu;
	}

	public static void Init()
	{
		resourceListUI.PopulateList();
		resourceListUI.Tick();

	}
	public static void UITicking()
	{
		resourceListUI.Tick();
	}

	public static void ToggleEscMenu() {
		if(instanceEscMenu.activeSelf) {
			instanceEscMenu.SetActive(false);
			if(!pausedBeforeEsc) {
				Clock.UnPause();
			}
			Clock.canPlay = true;
		} else {
			pausedBeforeEsc = Clock.paused;
			instanceEscMenu.SetActive(true);
			Clock.Pause();
			Clock.canPlay = false;
		}
	}
}
