using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct ResourceInfo
{
	public string name;
	public string displayName;
	public int initialGlobalAmount;
	public int globalCap;
	public Sprite icon;
	public Color color;
}

[System.Serializable]
public struct HexTypeInfo
{
	public SimHexType type;
	public float ranjit;
}

public class Sim : MonoBehaviour
{

	/*
        MonoBehavior singleton that controls the internal sim.

        Recieves tick events and controls the order of sub-tick events in the sim.
    */

	//[SerializeField] HexMap _hexMap;
	public static HexMap hexMap;

	//prefabs
	[SerializeField] VisualHex _visualHexPrefab;
	public static VisualHex visualHexPrefab;
	[SerializeField] GameObject _gnomePrefab; //TODO specify this as a Gnome or Agent 
	public static GameObject gnomePrefab;

	//numbers
	[SerializeField] public int _gnomesToSpawn;
	public static int gnomesToSpawn;

	[SerializeField] public HexTypeInfo[] hexTypeInfo;
	public static SimHexType[] hexTypes;       //list of hex type scriptable objects
	public static float[] hexGenRanjit;        //parallel with hexTypes, ranjit range for % generated


	[SerializeField] public ResourceInfo[] _resourceInfo;
	public static ResourceInfo[] resourceInfo;
	public static string[] resources;          //list of resource names 
	public static int[] resourceInitialValues;
	public static int[] resourceGlobalCaps;

	[SerializeField] public BuildInfo [] _buildRelations;
	public static BuildInfo [] buildInfos;

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("called start on sim");

		Clock.Init();

		Init();
		SimGrid.Init(); //needs to happen after HexGrid initializes
		AgentDirector.Init();
		UIController.Init();
	}

	public void Init()
	{

		//fill in information provided in inspector

		hexMap = GetComponent<HexMap>();

		resourceInfo = _resourceInfo;
		visualHexPrefab = _visualHexPrefab;
		gnomePrefab = _gnomePrefab;
		gnomesToSpawn = _gnomesToSpawn;
		buildInfos = _buildRelations;

		hexTypes = new SimHexType[hexTypeInfo.Length];
		hexGenRanjit = new float[hexTypeInfo.Length];
		for (int i = 0; i < hexTypeInfo.Length; i++)
		{
			hexTypes[i] = hexTypeInfo[i].type;
			hexGenRanjit[i] = hexTypeInfo[i].ranjit;
		}

		resources = new string[resourceInfo.Length];
		resourceInitialValues = new int[resourceInfo.Length];
		resourceGlobalCaps = new int[resourceInfo.Length];
		for (int i = 0; i < resourceInfo.Length; i++)
		{
			resources[i] = resourceInfo[i].name;
			resourceInitialValues[i] = resourceInfo[i].initialGlobalAmount;
			resourceGlobalCaps[i] = resourceInfo[i].globalCap;
		}
		GlobalPool.Init();
		Tracker.Init();

		HexTypes.InitializeLookup(hexTypes);
		foreach (SimHexType type in hexTypes)
		{
			type.Init();
		}

		

		FindObjectOfType<BuildXOnYUI>().Init();

		Clock.Tick -= HandleTick;
		Clock.Tick += HandleTick;

	}

	//Manage the order of sub ticks.
	public static void HandleTick(object obj, TickArgs tickArgs)
	{

		int tickOrder = tickArgs.tickNum % 4;

		if (tickOrder == 0)
		{
			SimGrid.TickInputs(tickArgs.tickNum / 4);
		}
		else if (tickOrder == 1)
		{
			SimGrid.TickOutputs(tickArgs.tickNum / 4);
		}
		else if (tickOrder == 2)
		{
			AgentDirector.AgentTick(tickArgs.tickNum / 4);
		}
		else if (tickOrder == 3)
		{
			//GlobalProcesses.Tick(tickArgs.tickNum / 4);
			Tracker.CalculateEndOfTick();
			UIController.UITicking();
		}
	}
}

