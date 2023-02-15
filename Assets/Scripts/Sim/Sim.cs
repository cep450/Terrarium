using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //list of hex type scriptable objects
    [SerializeField] public SimHexType [] _hexTypes;
    public static SimHexType [] hexTypes;

    //parallel with hexTypes, ranjit range for % generated
    [SerializeField] public float [] _hexGenRanjit;
    public static float [] hexGenRanjit;

    //list of resource names 
    [SerializeField] public string [] _resources;
    public static string [] resources;

    
    void Awake() {


        //TODO theres got to be a better way to do this.

        hexMap = _hexMap;

        visualHexPrefab = _visualHexPrefab;
        gnomePrefab = _gnomePrefab;

        gnomesToSpawn = _gnomesToSpawn;

        hexTypes = _hexTypes;
        hexGenRanjit = _hexGenRanjit;
        resources = _resources;

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
