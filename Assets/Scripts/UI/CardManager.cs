using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour {

    /*
        Generates the visual hand of cards based on the given ScriptableObjects for the level
    */

    public FlipCard [] levelCards; //TODO move this to the sim, since most level data is in sim

    public void BuildHand() {
        foreach(FlipCard cardData in Sim.flipCards) {
            //instantiate FlipCardUI gameobjects
            //probably use one of those auto layout groups 
        }
    }
}