using System.Collections;
using System.Collections.Generic;
using System;

public class HexTypes {

    /*
        Hex type dictionary. 
        Hex types are defined here. 
        To add a type, define an object and create its json file with the same name.
    */

    public static SimHexType plant = new SimHexType("plant");
    //public static SimHexType water = new SimHexType("water");
    //ect 


    //TODO make something to be able to look up/get a type by id. 
    //maybe make it only by id/enum like resources with conversion to ints and strings from the enum name even for the file name



    public static void PrototypeCreateObjs() {

        //TODO for the purposes of this prototype doing manually, but would actually use json later 

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

    }

}