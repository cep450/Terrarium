using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimHex
{

    public enum HexType {
        NOTYPE, EMPTY, PLANT, CRAZYVINE
    }

    //its calling card
    public static HexType type = HexType.NOTYPE;

    //so we can refer to resources by name, but they're packed tightly in memory,
    //and every simhex takes up a constant amount of memory
    //these will also inform graphical rendering, sprite blending and making sprites appear
    //goes up to 255
    public struct ResourceCount {
        public byte water, nutrients, plantmatter, path;
    }
    //TODO make another data structure thats specifically for inputs and outputs
    //with additional info like bools for if a required resource is consumed or not 

    public  ResourceCount resourcesHas = new ResourceCount(); //this changes 
    protected static ResourceCount resourcesRequired = new ResourceCount();
    protected static ResourceCount resourcesCreated = new ResourceCount();

    //for tracking if stuff was satisfied the last tick 
    int currentTick = -1;
    bool inputsSatisfied = false;


    //TODO 
    /*
        better organize functions ect 
        just getting stuff up for the prototype 
        cause itll change what game we're making 
    */

    /*
        INTERNAL representation of a single hex.
        Has Processes that, when their Inputs are satisfied, carry out.

        (Numerical) Resources 
        what the tile Has
        things that can be measured by an amount
        examples: 'nutrients' 'water' 'path' 'pollution' 
        may be consumed or produced by other things 
        identified by numbers 

        Process resources
        what the tile Is
        a single thing that can consume/produce resources or otherwise 'do soemthing' 
        examples: 'plant' 'tree' 'house' 'crop' 'structure' 
        has properties about itself

        a Plant takes in nutrients, water and can grow 

        hmm is this too flexible? idk 
        cause there is that thing about all the tiles being the same size 
        vs the uhh flyweight

        to implement behavior could inherit from SimHex- but how does this work out w objects being the same size?
        should keep them the same size for efficiency 
        but they necesarily need diff stuff based on class 
        TODO tackle this later, just getting a prototype up. 

    */

    public abstract void Init();


    //TODO be checking the tick number for safety 

    public void InputTick(int tickNum) {
        if(CheckInputs()) {
            ConsumeInputs();
            currentTick = tickNum;
            inputsSatisfied = true;
        }
    }

    public void OutputTick(int tickNum) {
        if(inputsSatisfied && tickNum == currentTick) {
            ConsumeInputs();
            inputsSatisfied = false;
        }
    }

    public bool CheckInputs() {

        //TODO: compare structs in a better way, custom operator or function or something
        //this sucks and is for this prototype 
        if(resourcesHas.water >= resourcesRequired.water) {
            if(resourcesHas.nutrients >= resourcesRequired.nutrients) {
                Debug.Log("resource requirements met");
                return true;
            }
        }
        Debug.Log("resource requiremnts not met");
        return false;
    }

    public void ConsumeInputs() {

        //TODO another struct thing 

        resourcesHas.water -= resourcesRequired.water;
        resourcesHas.nutrients -= resourcesRequired.nutrients;

        Debug.Log("water is now " + resourcesHas.water + " nutrients is now " + resourcesHas.nutrients);

    }

    public void CreateOutputs() {

        //TODO another struct thing 

        resourcesHas.plantmatter += resourcesCreated.plantmatter;

        Debug.Log("plant matter is now " + resourcesHas.plantmatter);

    }

}
