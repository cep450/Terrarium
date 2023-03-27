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

    //ui displayed
    [SerializeField] SpriteRenderer renderer1, renderer2;
    [SerializeField] TextMeshProUGUI uiDesc;
    [SerializeField] TextMeshProUGUI uiTickNum;
    [SerializeField] GameObject reverseButton;

    //scriptableobject data pulling from
    public FlipCard cardData;

    //state 
    bool isAToB = true;
    bool isValid = true;
    string invalidReason = "";

    string [] invalidReasons = {
        "No hex of this type!",
        "Resource cost not met!",
        "Resource requirement not met!"
    };

    public void Construct(FlipCard card) {

        cardData = card;

        //sets its sprites and desc and tick num
        FillCardData();

        //generates a flip button if reversible, not if one-way (so flip never gets called)
        if(!cardData.oneWay) {
            reverseButton.SetActive(true);
        }
    }

    void FillCardData() {
        if(isAToB) {
            renderer1.sprite = cardData.typeB.cardSprite;
            renderer2.sprite = cardData.typeA.cardSprite;
            uiDesc.text = cardData.AToBDesc;
            uiTickNum.text = cardData.ticksAB.ToString();
        } else {
            renderer1.sprite = cardData.typeA.cardSprite;
            renderer2.sprite = cardData.typeB.cardSprite;
            uiDesc.text = cardData.BToADesc;
            uiTickNum.text = cardData.ticksBA.ToString();
        }
    }

    //these functions will be called by buttons 
    //thatre chidlren of this element with a reference to it in the prefab

    //changing tile A to B --> changing tile B to A 
    public void FlipOrder() {

        isAToB = !isAToB;

        FillCardData();

        UpdateOrderValidity();

        //TODO call animation coroutine 

        //wishlist: card could look like its being flipped over to the other side?
        //or do they just switch places 

        //TODO coroutine animation? unless zeru knows animators well in unity 
        //could disable the flip button until the animation is over? but feels bad unless the animation is very quick 
        //maybe theyre constantly on a lerp to their target location, and this button just changes their target location 
        //this is temp until animation 

    }

    //queue as a work order 
    public void QueueWorkOrder() {

        if(isAToB) {
            TryAddWorkOrder(cardData.typeA, cardData.typeB, cardData.ticksAB);
        } else {
            TryAddWorkOrder(cardData.typeB, cardData.typeA, cardData.ticksBA);
        }

    }

    void TryAddWorkOrder(SimHexType typeTarget, SimHexType typeDestination, int ticks) {

        if(UpdateOrderValidity()) {
            //AgentDirector.AddTask(typeTarget, typeDestination, ticks);
        }
    }

    //is the work order valid? sets isValid and also returns for convenience
    bool UpdateOrderValidity() {

        bool valid = true;

        //are there hexes on the board of the target type?
        //if resources, do we have those resources?

        //TODO also set validity reason string if using more than one reason 

        //(this can be used to grey out a card if it can't be added) 

        //if the state has changed, visually update the ui
        if(isValid && !valid) {
            
            //grey out button

        } else if(!isValid && valid) {
            
            //re-enable button 

        }
        
        isValid = valid;
        return isValid;
    }
}