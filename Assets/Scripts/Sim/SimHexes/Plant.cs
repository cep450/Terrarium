using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : SimHex
{
    public override void Init() { //TODO is override the best way to do this. also make sure to change back making the simhex class abstract

        type = HexType.PLANT;

        //starting
        resourcesHas.water = 100;
        resourcesHas.nutrients = 100;

        //set up what it does 
        resourcesRequired.water = 2;
        resourcesRequired.nutrients = 1;

        //what it can make 
        resourcesCreated.plantmatter = 1;

    }
}
