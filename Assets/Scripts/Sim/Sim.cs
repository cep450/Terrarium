using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ResourceInfo {
    public string name;
    public int initialGlobalAmount;
}

[System.Serializable]
public struct HexTypeInfo {
    public SimHexType type;
    public float ranjit;
}

public class Sim : MonoBehaviour
{

    /*
        MonoBehavior singleton that controls the internal sim.

        Recieves tick events and controls the order of sub-tick events in the sim.
    */

    [SerializeField] HexMap _hexMap;
    public static HexMap hexMap;
    
    //prefabs
    [SerializeField] VisualHex _visualHexPrefab;
    public static VisualHex visualHexPrefab;
    [SerializeField] GameObject _gnomePrefab; //TODO specify this as a Gnome or Agent 
    public static GameObject gnomePrefab;

    //numbers
    [SerializeField] public int _gnomesToSpawn;
    public static int gnomesToSpawn;

    [SerializeField] public HexTypeInfo [] hexTypeInfo;
    public static SimHexType [] hexTypes;       //list of hex type scriptable objects
    public static float [] hexGenRanjit;        //parallel with hexTypes, ranjit range for % generated

    
    [SerializeField] public ResourceInfo [] resourceInfo;
    public static string [] resources;          //list of resource names 
    public static int[] resourceInitialValues;

    void Awake() {

        //fill in information provided in inspector

        hexMap = _hexMap;

        visualHexPrefab = _visualHexPrefab;
        gnomePrefab = _gnomePrefab;

        gnomesToSpawn = _gnomesToSpawn;

        hexTypes = new SimHexType[hexTypeInfo.Length];
        hexGenRanjit = new float[hexTypeInfo.Length];
        for(int i = 0; i < hexTypeInfo.Length; i++) {
            hexTypes[i] = hexTypeInfo[i].type;
            hexGenRanjit[i] = hexTypeInfo[i].ranjit;
        }

        resources = new string[resourceInfo.Length];
        resourceInitialValues = new int[resourceInfo.Length];
        for(int i = 0; i < resourceInfo.Length; i++) {
            resources[i] = resourceInfo[i].name;
            resourceInitialValues[i] = resourceInfo[i].initialGlobalAmount;
        }
        GlobalPool.Init();

        HexTypes.InitializeLookup(hexTypes);
        foreach(SimHexType type in hexTypes) {
            type.Init();
        }

        Clock.Tick += HandleTick;
        
    }

    void Start() {

        SimGrid.Init(); //needs to happen after HexGrid initializes
        AgentDirector.Init();
        
    }

    //Initialize values for a new game. Ex. set initial global values.
    static void Init() {

    }

    //Manage the order of sub ticks.
    public static void HandleTick(object obj, TickArgs tickArgs) {

        int tickOrder = tickArgs.tickNum % 4;

        if(tickOrder == 0) {
            //Debug.Log("doing input tick " + tickArgs.tickNum + " " + tickArgs.tickNum / 3);
            SimGrid.TickInputs(tickArgs.tickNum / 3);
        } else if(tickOrder == 1) {
            //Debug.Log("doing output tick " + tickArgs.tickNum + " " + tickArgs.tickNum / 3);
            SimGrid.TickOutputs(tickArgs.tickNum / 3);
        } else if(tickOrder == 2) {
            //Debug.Log("doing agent tick" + tickArgs.tickNum + " " + tickArgs.tickNum / 3);
            AgentDirector.AgentTick(tickArgs.tickNum / 3);
        } else if(tickOrder == 3) {
            //end of tick
            Tracker.CalculateEndOfTick();
        }
    }
}

