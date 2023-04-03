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

    public SimHexType type;

    bool maximized = false; //filling whole window
    bool minimized = false; //hidden to nothing 

    int maximizeHeight = 200;
    int normalizeHeight = 50;
    int minimizeHeight = 0;


    //Populates itself based on the data in its type.
    //For Build 
    public void Construct(OnInfo info) {
        
        Construct(info.type);

        //TODO 

        //set number of ticks 
        //set description 

    }  
    //For On  
    public void Construct(SimHexType t) {

        type = t;

        //set color based on type 
        buttonBackground.tintColor = type.color;

        //set icon based on type 
    }

    public void ClickedOn() {

        if(maximized) {
            Normalize();
            

        } else {
            Maximize();

        }

    }

    public void Maximize() {

    }

    public void Minimize() {

    }

    public void Normalize() {

    }
}