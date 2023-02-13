using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Resource; //allows ResourceRequires rather than Res.ResourceRequired

[CreateAssetMenu]
public class SimHexType : ScriptableObject {

    /*
        Represents a type of hex. 
        A single instance of this type is created and its parameters changed.
        The data here will always be true for a hex of this type. 
        It won't reflect the data for an instance of a hex. 
        Follows the flyweight pattern iirc. 
    */
    
    public string name = "default";         //this will also be the name of the json file it loads from
    public int id = -1;                     //its calling card for equals and lookup. its index in the array
    public bool passable = true;            //can gnomes pathfind/walk through this hex?

    //defines behavior 
    public ResRequired [] resourcesRequired = new ResRequired[2];
    public ResProduced [] resourcesProduced = new ResProduced[2];
    public ResStarting [] resourcesStarting = new ResStarting[2];
    
    //visual display
    public Color color;
    public Sprite sprite;


    //Loads associated ids by name 
    public void Init() {

        for(int i = 0; i < resourcesRequired.Length; i++) {
            resourcesRequired[i].id = Resource.IdByName(resourcesRequired[i].name);
        }
        for(int i = 0; i < resourcesProduced.Length; i++) {
            resourcesProduced[i].id = Resource.IdByName(resourcesProduced[i].name);
        }
        for(int i = 0; i < resourcesStarting.Length; i++) {
            resourcesStarting[i].id = Resource.IdByName(resourcesStarting[i].name);
        }
    }


    public override bool Equals(object other) {

        SimHexType otherAsType = other as SimHexType;

        if(otherAsType == null) {
            return false;
        }

        return otherAsType.id == this.id;

    }
    public override int GetHashCode()
    {
        return id;
    }
}