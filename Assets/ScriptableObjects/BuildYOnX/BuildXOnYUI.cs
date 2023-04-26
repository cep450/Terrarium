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

    [SerializeField] GameObject buildListParent, onListParent;

    [SerializeField] GameObject buttonQueueWorkOrder;
    //[SerializeField] GameObject buttonBuildClear, buttonOnClear;
    public TextMeshProUGUI desc;

    [SerializeField] GameObject listEntryPrefab;
    public List<BuildUIEntry> listBuildUIs, listOnUIs; //lists parallel with Sim hextypes 

    //-1 if none 
    //index in the list of the selected type
    //parllel with the list in this class and with Sim.buildInfos 
    int buildIndex = -1;
    int onIndex = -1;

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

        foreach(SimHexType type in Sim.hexTypes) {
            BuildUIEntry build = Instantiate(listEntryPrefab, buildListParent.transform).GetComponent<BuildUIEntry>();
            BuildUIEntry on = Instantiate(listEntryPrefab, onListParent.transform).GetComponent<BuildUIEntry>();
            build.Construct(type, listBuildUIs.Count, false);
            on.Construct(type, listOnUIs.Count, true);
            listBuildUIs.Add(build);
            listOnUIs.Add(on);
        }

    }


    //TODO!!!!!!! have to update the ones shown with the correct number of ticks. 



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

        if(onIndex == -1 || buildIndex == -1) {
            Debug.Log("type not selected");
        }

        SimHexType typeOn = listOnUIs[onIndex].type;
        SimHexType typeBuild = listBuildUIs[buildIndex].type;

        //TODO get ticks from build info 
        int numTicks = 1;

        //TODO check validity
        //TODO check if tile exists on board 

        AgentDirector.AddTask(typeOn, typeBuild, numTicks);
    }
}
