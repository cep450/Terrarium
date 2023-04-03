using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct BuildInfo {
    public SimHexType type;
    public OnInfo [] canBeBuiltOn;
}

public struct OnInfo {
    public SimHexType type;
    public string desc;
    public int numTicks;
}

public class BuildXOnYUI : MonoBehaviour {

    [SerializeField] GameObject buttonQueue;
    [SerializeField] GameObject buttonBuildClear, buttonOnClear;

    [SerializeField] GameObject listEntryPrefab;
    GameObject [] listBuildUIs, listOnUIs; //list build parallel with Sim.buildInfos

    //-1 if none 
    int buildIndex = -1;    //parallel with Sim.buildInfos
    int onIndex = -1;       //index in the BuildInfo

    public static BuildXOnYUI instance;

    public void Init() {

        instance = this; 
        
        //use Sim.BuildInfos[]

        //TODO. how are the indexes determined 

        ConstructLists();
        ListBuilds();
    }

    //Fills each list with all possible tiles based on level data. 
    public void ConstructLists() {

        //Instantiate gameobjects from list entry prefab
        //fill that prefab with its data 

    }

    //List all build possibilities. 
    public void ListBuilds() {

        if(buildIndex == -1) {
            //special case: no build selected, show empty / hide all 
        } else {

            //show the stuff that can be built from this 

        }

    }
    //when we select something to build, fill the ui
    public void SelectBuild(int index) {

        buildIndex = index;


    }
    //where we selected a build, clear it back to the list
    public void ClearBuild() {

        buildIndex = -1;

        //close that element that was expanded 

        ListBuilds();
    }

    public void ListOns() {

    }
    //when we select what to build on, fill the ui 
    public void SelectOn(int index) {

        onIndex = index;


    }
    //where we selected an on, clear it back to its list, or nothing if none selected 
    public void ClearOn() {

        onIndex = -1;

        //close that element that was displayed 

        ListOns();
    }


    public void QueueWorkOrder() {

        //SimHexType typeTarget = listOn[onIndex].type;
        //SimHexType typeDestination = listBuild[buildIndex].type;
        //int numTicks = 

        //TODO check validity? 

        //AgentDirector.AddTask(typeTarget, typeDestination, ticks);
    }
}
