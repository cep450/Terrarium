using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct BuildInfo {
    public SimHexType type;
    public OnInfo [] canBeBuiltOn;
}

[System.Serializable]
public struct OnInfo {
    public SimHexType type;
    public string desc;
    public int numTicks;
}

public class BuildXOnYUI : MonoBehaviour {

    [SerializeField] GameObject buttonQueue;
    [SerializeField] GameObject buttonBuildClear, buttonOnClear;
    public TextMeshProUGUI desc;

    [SerializeField] GameObject listEntryPrefab;
    public BuildUIEntry [] listBuildUIs, listOnUIs; //list build parallel with Sim.buildInfos

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

        desc.text = "";

        buildIndex = -1;

        //close that element that was expanded 

        ListBuilds();

    }

    public void ListOns() {

    }
    //when we select what to build on, fill the ui 
    public void SelectOn(SimHexType type) {

        //this gets called when click on tile 
        //TODO need to convert from type to this internal stuff

    }
    public void SelectOn(int index) {

        //TODO check if BUild not selected or wrong and if so re-filter to match clicked on

        onIndex = index;


    }
    //where we selected an on, clear it back to its list, or nothing if none selected 
    public void ClearOn() {

        desc.text = "";

        onIndex = -1;

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