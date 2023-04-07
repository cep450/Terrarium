using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour {

    /*
        Generates the visual hand of cards based on the given ScriptableObjects for the level
    */

    [SerializeField] FlipCardUI flipCardUIPrefab;

    /*
    
    public void BuildHand() {
        foreach(FlipCard cardData in Sim.flipCards) {

            //TODO formatting, location, probably use one of those auto layout groups 

            Instantiate<FlipCardUI>(flipCardUIPrefab).Construct(cardData);
        }
    }
    */
}