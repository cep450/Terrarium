using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Res {

    /*
        Enums and structs for resources we want to use everywhere.

        To register a resource, just add it to the enum.
    */

    //A resource. Allows referring to resources by name and for code completion. 
    public enum Resource {
        NULL, WATER, DETRITUS, 
        PLANTMATTER, NUTRIENTS, PATH
    }

    //List of resources parallel with the enum.
    public static readonly Resource [] resources = (Resource[])Enum.GetValues(typeof(Resource));

    //int to enum
    public static Resource ResById(int id) {
        if(id >= resources.Length || id < 0) {
            Debug.LogError("ERR: asked for Resource at out of bounds id " + id);
            return Resource.NULL;
        }
        return resources[id];
    }

    //enum to int can simply be done by casting. This function makes it clearer ig
    public static int IdByRes(Resource res) {
        return (int)(res);
    }

    public static string NameByRes(Resource res) {
        return Enum.GetName(typeof(Resource), res);
    }
    public static string NameById(int id) {
        return Enum.GetName(typeof(Resource), id);
    }


    //Resource checked for and potentially consumed at the inputs step
    [Serializable]
    public struct ResRequired {
        public int id;          //Resource enum value as int, can also be used as an index
        public byte amount;
        public byte radius;
        public bool isConsumed; //does this tile consume this resource or just need it to exist?
    }

    //Resource (or tile) created at the outputs step 
    [Serializable]
    public struct ResProduced {
        public int id;          //if hex, this number will be a hex id, otherwise, a resource id
        public byte amount;     //if hex, the number of hexes. the hex will know what resources
        public byte radius;
        public bool isHex;      //does this represent a hex or a resource?
    }

    //when a hex changes to this type, it starts with this resource
    [Serializable]
    public struct ResStarting {
        public int id;
        public byte amount;
    }

}