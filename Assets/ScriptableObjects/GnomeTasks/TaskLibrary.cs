using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskLibrary : MonoBehaviour
{
    [SerializeField] GnomeTask [] tasks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //TODO create some kind of map on startup so dont have to do these expensive lookups every time?



    //Converting tile X to tile Y...

    //Know X, what can i convert it to?
    //returns an empty list if no such tiles.
    public List<GnomeTask> PopulateXFromY(int hexid) {

        List<GnomeTask> validTasks = new List<GnomeTask>();

        foreach(GnomeTask task in tasks) {

            //TODO 

        }

        return validTasks;
    }

    //Know Y, what tiles can I get to Y from?
    //returns an empty list if no such tiles.
    public List<GnomeTask> PopulateYFromX(int hexid) {

        List<GnomeTask> validTasks = new List<GnomeTask>();

        foreach(GnomeTask task in tasks) {

            //TODO 

        }

        return validTasks;
    }

    //Knowing X and Y, what task will turn X to Y?
    //returns null if no such task.
    public GnomeTask TaskForXToY(int idX, int idY) {

        foreach(GnomeTask task in tasks) {

            //TODO 

        }

        return null;
    }

}
