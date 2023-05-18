using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildUIEntry : MonoBehaviour {

    [SerializeField] RectTransform rect;
    [SerializeField] RectTransform rectChild;
    [SerializeField] TileResourceInfo tileInfo;
    [SerializeField] TextMeshProUGUI ticks;


    bool maximized = false; //filling whole window
    bool minimized = false; //hidden to nothing 

    Vector2 maximizeHeight, normalizeHeight, minimizeHeight;
    

    public SimHexType type;
    public OnInfo onInfo;
    bool isOn = false;
    public int index; //its index/id

    void Start() {
        maximizeHeight = new Vector2(rect.sizeDelta.x, 220);
        normalizeHeight = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
        minimizeHeight = new Vector2(rect.sizeDelta.x, 0);
    }


    //Populates itself based on the data in its type.
    public void Construct(SimHexType t, int i, bool _isOn = false) {

        type = t;
        index = i;
        isOn = _isOn;

        tileInfo.SetType(type);

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
        rectChild.sizeDelta = maximizeHeight;

        SetActiveInfo(true);

        if(isOn) {
            BuildXOnYUI.instance.desc.text = onInfo.desc;
        }

        maximized = true;
        minimized = false;

        if(isOn) {
            BuildXOnYUI.instance.SelectOn(this.index);
            ToggleOthersMinimized(true, BuildXOnYUI.instance.listOnUIs);
        } else {
            BuildXOnYUI.instance.SelectBuild(this.index);
            ToggleOthersMinimized(true, BuildXOnYUI.instance.listBuildUIs);
        }
    }

    public void Minimize() {

        rect.sizeDelta = minimizeHeight;
        rectChild.sizeDelta = minimizeHeight;

        SetActiveInfo(false);

        minimized = true; 
        maximized = false;

    }

    public void UnMaximize() {

        if(isOn) {
            BuildXOnYUI.instance.ClearOn();
            ToggleOthersMinimized(false, BuildXOnYUI.instance.listOnUIs);
        } else {
            BuildXOnYUI.instance.ClearBuild();
            ToggleOthersMinimized(false, BuildXOnYUI.instance.listBuildUIs);
        }

        Normalize();
    }

    public void Normalize() {

        rect.sizeDelta = normalizeHeight;
        rectChild.sizeDelta = normalizeHeight;

        SetActiveInfo(true);

        minimized = false;
        maximized = false;

    }

    void ToggleOthersMinimized(bool minimize, List<BuildUIEntry> list) {
        foreach(BuildUIEntry ui in list) {
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
            tileInfo.Show();
            if(isOn) {
                ticks.text = onInfo.numTicks.ToString(); //TODO set this
            }
        } else {
            tileInfo.Hide();
            ticks.text = "";
        }
    }
}