using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class HexTypes {

    /*
        Hex type dictionary. 
        Helper functions for lookup.
        To add a type, create a ScriptableObject and add it to the Sim's list. 
    */

    //parallel list with id for lookup
    static string [] names;


    //Autofills ids on startup.
    public static void InitializeLookup(SimHexType[] types) {

        names = new string[types.Length];

        for(int i = 0; i < types.Length; i++) {
            types[i].id = i;
            names[i] = types[i].name;
        }
    }

    public static SimHexType TypeByName(string name) {
        int index = Array.IndexOf<string>(names, name);
        if(index == -1) {
            Debug.LogError("couldn't find type \'" + name + "\" by name");
            return null;
        }
        return Sim.hexTypes[index];
    }
    public static SimHexType TypeById(int id) {
        return Sim.hexTypes[id];
    }
    public static int IdByName(string name) {
        return Array.IndexOf<string>(names, name);
    }
    public static int IdByType(SimHexType type) {
        return type.id;
    }
    public static string NameById(int id) {
        return names[id];
    }
    public static string NameByType(SimHexType type) {
        return names[Array.IndexOf<SimHexType>(Sim.hexTypes, type)];
    }
    
}