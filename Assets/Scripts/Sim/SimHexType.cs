using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SimHexType {

    /*
        Represents a type of hex. 
        A single instance of this type is created and its parameters changed.
        The data here will always be true for a hex of this type. 
        It won't reflect the data for an instance of a hex. 
        Follows the flyweight pattern iirc. 
    */
    static int idCounter = 0;              //each will be assigned an id not already taken
    static string jsonPath = "SimHexes";    //folder we will look for json files TODO 


    public string name = "default";         //this will also be the name of the json file it loads from
    public int id = 0;                     //its calling card for equals and lookup
    public bool passable = true;            //can gnomes pathfind/walk through this hex?

    //defines behavior 
    public Res.ResRequired [] resourcesRequired = new Res.ResRequired[2];
    public Res.ResProduced [] resourcesProduced = new Res.ResProduced[2];
    public Res.ResStarting [] resourcesStarting = new Res.ResStarting[2];
    
    //visual display
    public Color color;
    public Sprite sprite;


    //load a type from json
    public SimHexType(string jsonFileName) {
        name = jsonFileName;

        idCounter++;
        id = idCounter;
        Debug.Log("id taken: " + id);

        //TODO load from json file with the correct name! 
        //////////
    }

    //create a type manually 
    public SimHexType(bool _passable, Res.ResRequired[] rr, Res.ResProduced[] rp) : this("createdManually") {

        passable = _passable;

        if(rr.Length > resourcesRequired.Length) {
            resourcesRequired = rr;
        } else {
            for(int i = 0; i < rr.Length; i++) {
                resourcesRequired[i] = rr[i];
            }
        }

        if(rp.Length > resourcesProduced.Length) {
            resourcesProduced = rp;
        } else {
            for(int i = 0; i < rp.Length; i++) {
                resourcesProduced[i] = rp[i];
            }
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