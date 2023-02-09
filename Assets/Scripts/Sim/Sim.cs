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
    [SerializeField] VisualHex _visualHexPrefab;
    public static VisualHex visualHexPrefab;

    void Awake() {
        hexMap = _hexMap;
        visualHexPrefab = _visualHexPrefab;

        Clock.Tick += HandleTick;
        Init();
    }
    void Start() {
        SimGrid.Init(); //needs to happen after HexGrid initializes
        AgentDirector.Init();
    }

    void Init() {

        //TODO remove this replace this 
        HexTypes.PrototypeCreateObjs();

    }

    //Manage the order of sub ticks.
    public static void HandleTick(object obj, TickArgs tickArgs) {

        int tickOrder = tickArgs.tickNum % 3;

        Debug.Log("tick number: " + tickArgs.tickNum + " tick order: " + tickOrder);

        if(tickOrder == 0) {
            Debug.Log("doing input tick");
            SimGrid.TickInputs(tickArgs.tickNum);
        } else if(tickOrder == 1) {
            Debug.Log("doing output tick ");
            SimGrid.TickOutputs(tickArgs.tickNum);
        } else if(tickOrder == 2) {
            Debug.Log("doing agent tick");
            AgentDirector.AgentTick(tickArgs.tickNum);
        }
    }
}
