using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class BuildUIEntry : MonoBehaviour {

    [SerializeField] TextMeshProUGUI displayName;
    [SerializeField] Image icon;
    [SerializeField] Image buttonBackground;
    [SerializeField] TextMeshProUGUI ticks;
    [SerializeField] RectTransform rect;


    bool maximized = false; //filling whole window
    bool minimized = false; //hidden to nothing 

    Vector2 maximizeHeight, normalizeHeight, minimizeHeight;
    

    public SimHexType type;
    public OnInfo onInfo;
    bool isOn = false;

    void Start() {
        maximizeHeight = new Vector2(rect.sizeDelta.x, 200);
        normalizeHeight = new Vector2(rect.sizeDelta.x, 50);
        minimizeHeight = new Vector2(rect.sizeDelta.x, 0);
    }


    //Populates itself based on the data in its type.
    //For Build 
    public void Construct(OnInfo info) {
        
        Construct(info.type);

        isOn = true;

    }  
    //For On  
    public void Construct(SimHexType t) {

        type = t;

        //set color based on type 
        buttonBackground.tintColor = type.color;

        //set icon based on type 

        //TODO set list of resources used produced also 

    }

    public void ClickedOn() {

        if(maximized) {
            UnMaximize();

        } else {
            Maximize();

        }
    }

    public void Maximize() {

        rect.sizeDelta = maximizeHeight;

        SetActiveInfo(true);

        if(isOn) {
            BuildXOnYUI.instance.desc.text = onInfo.desc;
        }

        maximized = true;
        minimized = false;

        if(isOn) {
            ToggleOthersMinimized(true, BuildXOnYUI.instance.listOnUIs);
        } else {
            ToggleOthersMinimized(true, BuildXOnYUI.instance.listBuildUIs);
        }
    }

    public void Minimize() {

        rect.sizeDelta = minimizeHeight;

        SetActiveInfo(false);

        minimized = true; 
        maximized = false;

    }

    public void UnMaximize() {

        if(isOn) {
            ToggleOthersMinimized(false, BuildXOnYUI.instance.listOnUIs);
        } else {
            ToggleOthersMinimized(false, BuildXOnYUI.instance.listBuildUIs);
        }

        Normalize();
    }

    public void Normalize() {

        rect.sizeDelta = normalizeHeight;

        SetActiveInfo(true);

        minimized = false;
        maximized = false;

    }

    void ToggleOthersMinimized(bool minimize, BuildUIEntry [] array) {
        foreach(BuildUIEntry ui in array) {
            if(ui.type != this.type) {
                if(minimize) {
                    ui.Minimize();
                } else {
                    ui.Normalize();
                }
            }
        }
    }

    void SetActiveInfo(bool active) {

        if(active) {
            displayName.text = type.displayName;
        } else {
            displayName.text = "";
        }

        if(isOn) {

            if(active) {
                ticks.text = onInfo.numTicks.ToString();
            } else {
                displayName.text = "";
            }

        }
    }
}