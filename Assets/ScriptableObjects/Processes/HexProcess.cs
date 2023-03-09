using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResourceUsed {

    public string name;
    public int id;
    public bool isConsumed;         //consumed or just required to be there?
    public bool hexDiesIfNotMet;    //if this resource isnt there, hex will convert to death hex
    public bool consumesGlobal;     //consumes from global pool or local hex?

}

public struct ResourceProduced {

    public string name;
    public int id;
    public int amount;
    public int limitOnHex;      //will not produce more than this amount onto this tile
    public bool producesGlobal; //produce to local hex or global pool?

}

public class HexProcess : ScriptableObject
{
    
    //Evolution of SimHex ScriptableObject. 
    //Multiple processes can be on a hex. 
    //Multiple hexes can use the same process. 

    public int ticks;       //this Process will attempt every x ticks.

    [SerializeField] public ResourceUsed [] rr;
    [SerializeField] public ResourceProduced [] rp;


}
