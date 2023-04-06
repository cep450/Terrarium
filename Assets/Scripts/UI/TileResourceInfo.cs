using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileResourceInfo : MonoBehaviour
{

    /*
        Displayed both in the change ui and when mousing over tiles.

        Each change ui has an instance of this. 

        The mouse has a single instance of this where the data gets updated instead of creating/destroying each time.
    */

    SimHexType type;
    [SerializeField] List<ResourceChangeIcon> icons;

    public void SetType(SimHexType newType) {

        //do nothing if same 
        if(type.Equals(newType)) {
            return;
        }

        type = newType;

        UpdateTypeInfo();
    }

    void UpdateTypeInfo() {

        int index = 0;

        foreach(Resource.ResProduced r in type.resourcesProduced) {
            ReuseOrCreateIcon(index);
            icons[index].Fill(Sim.resourceInfo[r.id].icon, true, false, r.amount);
            index++;
        } 
        foreach(Resource.ResRequired r in type.resourcesRequired) {
            ReuseOrCreateIcon(index);
            icons[index].Fill(Sim.resourceInfo[r.id].icon, false, true, r.amount);
            index++;
        }
        foreach(Resource.ResStarting r in type.resourcesStarting) {
            ReuseOrCreateIcon(index);
            icons[index].Fill(Sim.resourceInfo[r.id].icon, false, false, r.amount);
            index++;
        }
    }

    void ReuseOrCreateIcon(int index) {
        if(index >= icons.Count) {
            icons.Add(Instantiate<ResourceChangeIcon>(icons[0], this.transform));
        }
    }
}
