using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class FlipCard : ScriptableObject 
{
    /*
        Represents the ability to change one tile type to another. 
    */

    public SimHexType typeA; //"Wildflowers"
    
    public SimHexType typeB; //"Forest"

    public bool oneWay; //if one way, can only convert A to B 

    //these will be displayed at the bottom of the card 
    public string AToBDesc; //"Ecological Succession" (wildflowers -> forest)
    public int ticksAB = 1;
    //todo if we want this to cost resources it can 

    public string BToADesc; //"Controlled Burns" (forest -> wildflowers)
    public int ticksBA = 1;
    //todo if we want this to cost resources it can 

}
