using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyVine : SimHex
{

    /*
        Vine that spreads absolutely out of control. 
    */

    public override void Init() {
        type = HexType.CRAZYVINE;
    }

}