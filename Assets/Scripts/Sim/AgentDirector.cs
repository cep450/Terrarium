using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDirector {

    /*
        Manages the Agents in the simulation the way SimGrid manages the SimHexes. 

        Handles stuff like re-using objects and ticking Agents. 
    */

    static List<Agent> agents = new List<Agent>();

    public static void Init() {
        //TODO 
    }

    public static void AgentTick(int tickNum) {
        foreach(Agent a in agents) {
            a.Tick(tickNum);
        }
    }

}