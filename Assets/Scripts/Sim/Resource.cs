using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Resource {

    /*
        Lookup and structs for resources we want to use everywhere.
        To register a resource, add its name to the array in Sim.
    */

    //used for clarity 
    public static readonly int nullResId = 0; //because this is the default value of ints in the struct
    public static readonly string nullResName = "null";

    public static string NameById(int id) {
        if(id < 0 || id >= Sim.resources.Length) {
            Debug.LogError("ERR: tried to ask for Resource at out of bounds id " + id);
            return "OUTOFBOUNDS";
        }
        return Sim.resources[id];
    }
    public static int IdByName(string name) {
        int index = Array.IndexOf<string>(Sim.resources, name);
        if(index < 0) {
            Debug.LogError("ERR: asked for id of Resource " + name + " which was not found");
        }
        return index;
    }


    //Resource checked for and potentially consumed at the inputs step
    [Serializable]
    public struct ResRequired {
        public string name;     //for the inspector 
        public int id;          //Resource enum value as int, can also be used as an index
        public byte amount;
        public byte radius;
        public byte tickMod;    //fires when tickNum % tickMod == 0
        public bool isConsumed; //does this tile consume this resource or just need it to exist?
        public float falloff;   //multiply this by distance for amount changed. 0 is no falloff
    }

    //Resource (or tile) created at the outputs step 
    [Serializable]
    public struct ResProduced {
        public string name;     //for the inspector
        public int id;          //if hex, this number will be a hex id, otherwise, a resource id
        public byte amount;     //if hex, the number of hexes. the hex will know what resources
        public byte radius;
        public byte limit;      //stops producing if hex is already at this amount
        public bool isHex;      //does this represent a hex or a resource?
        public byte tickMod;    //fires when tickNum % tickMod == 0
        public float falloff;   //multiply this by distance for amount changed. 0 is no falloff
    }

    //when a hex changes to this type, it starts with this resource
    [Serializable]
    public struct ResStarting {
        public string name;     //for the inspector
        public int id;
        public byte amount;
    }

}