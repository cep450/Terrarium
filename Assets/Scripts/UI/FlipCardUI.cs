using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlipCardUI : MonoBehaviour 
{

    /*
        Flip card UI element. 
        can be applied as a work order, and have the from/to flipped 
    */

    [SerializeField] SpriteRenderer renderer1, renderer2;
    //[SerializeField] textmeshpro text field 
    public FlipCard cardData;

    bool isAToB = true;

    //these functions will be called by buttons 
    //thatre chidlren of this element with a reference to it in the prefab

    //changing tile A to B --> changing tile B to A 
    public void FlipOrder() {


        //wishlist: card could look like its being flipped over to the other side?
        //or do they just switch places 


        //TODO coroutine animation? unless zeru knows animators well in unity 
        //could disable the flip button until the animation is over? but feels bad unless the animation is very quick 
        //maybe theyre constantly on a lerp to their target location, and this button just changes their target location 
        //this is temp until animation 

        if(isAToB) {
            //renderer1.sprite = HexTypes.TypeByName(cardData.typeB).cardSprite;
            //renderer2.sprite = HexTypes.TypeByName(cardData.typeA).cardSprite;
            renderer1.sprite = cardData.typeB.cardSprite;
            renderer2.sprite = cardData.typeA.cardSprite;
            //TMP element.text = cardData.AToBDesc;
            isAToB = false;
        } else {
            renderer1.sprite = cardData.typeA.cardSprite;
            renderer2.sprite = cardData.typeB.cardSprite;
            //TMP elment.text = cardData.AToBDesc;
            isAToB = true;
        }
    }

    //queue as a work order 
    public void QueueWorkOrder() {

        //TODO ask zeru how to hook into work order system
        //pass in target type, change type, ticks, ect- maybe just pass in the scriptable obj?
        //maybe theres a work order struct that it has 2 of that the work system also gets 
        //or it gets broken down 

        if(isAToB) {

        } else {

        }

    }

    //use typeA.cardSprite and store sprite on a type so can reuse 
}