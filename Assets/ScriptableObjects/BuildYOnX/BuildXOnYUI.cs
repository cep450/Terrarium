using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    Image queueButtonBackground;
    [SerializeField] Image fromUIBorder, toUIBorder;
    [SerializeField] Color invalid;
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

        queueButtonBackground = buttonQueueWorkOrder.GetComponent<Image>();
        
        buildIndex = -1;
        onIndex = -1;

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

        //are 2 hexes selected in the ui?
        if(onIndex < 0 || buildIndex < 0) {
            if(onIndex < 0) {
                InvalidFeedback(fromUIBorder);
            }
            if(buildIndex < 0) {
                InvalidFeedback(toUIBorder);
            }
            InvalidFeedback(queueButtonBackground);
            return;
        }

        //todo check validity of conversion 
        
            SimHexType typeOn = listOnUIs[onIndex].type;
            SimHexType typeBuild = listBuildUIs[buildIndex].type;

            //is there a hex to build this on?
            if(SimGrid.NumberOfType(typeOn) <= 0) {
                InvalidFeedback(queueButtonBackground);
                InvalidFeedback(fromUIBorder);
                return;
            }

            //TODO get ticks from build info 
            int numTicks = 3;

            AgentDirector.AddTask(typeOn, typeBuild, numTicks);

    }

    void InvalidFeedback(Image img) {
        StartCoroutine(FlashRed(img));
    }

    float flashTime = 0.2f;
    IEnumerator FlashRed(Image img) {

        Color oldColor = img.color;
        img.color = invalid;

        float tracker = 0; 
        while(tracker < flashTime) {
            tracker += Time.deltaTime;
            yield return null;
        }

        img.color = oldColor;
        
    }
}
