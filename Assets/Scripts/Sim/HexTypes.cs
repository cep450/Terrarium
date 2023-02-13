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
        return Sim.hexTypes[Array.IndexOf<string>(names, name)];
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


/*
    public enum HexType {
        DIRT,
        PLANT,
        WATER,
        CROPS,
        VINE
    }

    public static List<SimHexType> types { get; private set; }

    public SimHexType TypeById(int id) {
        return types[id];
    }
    public SimHexType TypeByEnum(HexType type) {
        return types[(int)type];
    }
    public HexType EnumById(int id) {
        return (HexType)id;
    }
    public int IdByEnum(HexType type) {
        return (int)type;
    }
    public int IdByType(SimHexType simType) {
        return types.IndexOf(simType);
    }
    public HexType EnumByType(SimHexType simType) {
        return (HexType)types.IndexOf(simType);
    }

    public static void LoadTypes() {

        HexType[] listOfEnumVals = (HexType[])Enum.GetValues(typeof(HexType));

        types = new List<SimHexType>(listOfEnumVals.Length);

        for(int i = 0; i < listOfEnumVals.Length; i++) {

            string name = Enum.GetName(typeof(HexType), listOfEnumVals[i]);

            SimHexType newType = new SimHexType(name);

            types[i] = newType;

        }
    }

 */

    //below this is prototype stuff
    /*

    public static SimHexType plant = new SimHexType("plant");
    public static SimHexType water = new SimHexType("water");
    public static SimHexType crops = new SimHexType("crops");
    //ect 
    public static void PrototypeCreateObjs() {

        types = new List<SimHexType>(3);


        //TODO for the purposes of this prototype doing manually, but would actually use json later 

        water.color = Color.blue;

        crops.color = Color.yellow;


        Res.ResRequired [] rrplant = new Res.ResRequired[2];
        rrplant[0] = new Res.ResRequired();
        rrplant[1] = new Res.ResRequired();
        rrplant[0].id = (int)Res.Resource.WATER;
        rrplant[0].amount = 2;
        rrplant[0].isConsumed = true;
        rrplant[1].id = (int)Res.Resource.NUTRIENTS;
        rrplant[1].amount = 1;
        rrplant[1].isConsumed = true;

        Res.ResProduced [] rpplant = new Res.ResProduced[1];
        rpplant[0] = new Res.ResProduced();
        rpplant[0].id = (int)Res.Resource.PLANTMATTER;
        rpplant[0].amount = 1;

        plant = new SimHexType(
            true,
            rrplant, 
            rpplant
        );
        plant.resourcesStarting[0].id = (int)Res.Resource.WATER;
        plant.resourcesStarting[0].amount = 100;
        plant.resourcesStarting[1].id = (int)Res.Resource.NUTRIENTS;
        plant.resourcesStarting[1].amount = 100;
        plant.color = Color.green;
        plant.name = "plant";


        //TODO this should just have an array of objects, read in from the array 
        //arbitrary length 
        //and the class should have some kind of name to object converter lookup 

        //TODO have to fix this 
        types.Add(plant);
        types.Add(water);
        types.Add(crops);



    }
*/
}