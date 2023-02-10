using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HexTypes {

    /*
        Hex type dictionary. 
        Hex types are defined here. 
        To add a type, define an object, add it to the array, and create its json file with the same name.
    */

    //TODO do like an enum to use where matches file names and creates from there automatically

    public static SimHexType plant = new SimHexType("plant");
    public static SimHexType water = new SimHexType("water");
    public static SimHexType crops = new SimHexType("crops");
    //ect 

    public static List<SimHexType> types = new List<SimHexType>(3);
    //TODO sync up the array and types properly, shouldnt have to specify number


    //TODO make something to be able to look up/get a type by id. 
    //maybe make it only by id/enum like resources with conversion to ints and strings from the enum name even for the file name



    public static void PrototypeCreateObjs() {

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


        //TODO this should just have an array of objects, read in from the array 
        //arbitrary length 
        //and the class should have some kind of name to object converter lookup 

        //TODO have to fix this 
        types.Add(plant);
        types.Add(water);
        types.Add(crops);

    }

}