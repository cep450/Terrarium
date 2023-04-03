using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildUIEntry : MonoBehaviour {

    public SimHexType type;

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
        //set icon based on type 
    }

    public void Expand() {

    }

    public void Contract() {

    }
}