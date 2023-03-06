using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TaskResChange {
    public string name;
    public int id;
    public int amount;
    public bool removeFromTile;
    public bool addToTile;
    public bool removeFromGnome;
    public bool addToGnome;
    public bool removeFromGlobal;
    public bool addToGlobal;
}

[CreateAssetMenu]
public class GnomeTask : ScriptableObject 
{
    public string name = "default";     //name of task 
    public int id = -1;

    public int numTurns = 1;            //how many turns this task takes 

    public string [] hexTypesToAffect;  //what hex types to target
    public int [] hexTypeIdsToAffect;
    
    public string newHexType;           //hex type to change to

    public TaskResChange [] resourceChanges;


    //Loads associated ids by name 
    //TODO make sure to call this! 
    public void Init() { 

        //todo generate an id for itself?

        for(int i = 0; i < resourceChanges.Length; i++) {
            resourceChanges[i].id = Resource.IdByName(resourceChanges[i].name);
        }

    }

}
