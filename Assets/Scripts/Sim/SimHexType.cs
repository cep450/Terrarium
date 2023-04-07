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
    
    public string name = "default";         //used as an internal id
    public string displayName = null;  //displayed in the game. defaults to name if none set
    public int id = -1;                     //its calling card for equals and lookup. its index in the array
    public bool passable = true;            //can gnomes pathfind/walk through this hex?
    public int beauty = 0; 
	public int ugliness = 0;
    public string deathHexName = "Dirt";        //hex it flips to if it dies 

    //defines behavior 
    public ResRequired [] resourcesRequired = new ResRequired[2];
    public ResProduced [] resourcesProduced = new ResProduced[2];
    public ResStarting [] resourcesStarting = new ResStarting[2];

    public GameObject visualHexPrefab;

    public Sprite cardSprite;
    
    //visual display- old
    public Color color;
    public Sprite sprite;
    public Sprite billboardSprite;

    public Sprite icon;


    //Loads associated ids by name 
    public void Init() {

        id = HexTypes.IdByName(name);

        if(displayName == null) {
            displayName = name;
        }

        for(int i = 0; i < resourcesRequired.Length; i++) {
            resourcesRequired[i].id = Resource.IdByName(resourcesRequired[i].name);
        }
        for(int i = 0; i < resourcesProduced.Length; i++) {
            if(resourcesProduced[i].isHex) {
                resourcesProduced[i].id = HexTypes.IdByName(resourcesProduced[i].name);
            } else {
                resourcesProduced[i].id = Resource.IdByName(resourcesProduced[i].name);
            }    
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