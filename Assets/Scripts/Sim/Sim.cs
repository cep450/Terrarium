using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim : MonoBehaviour
{

    /*
        MonoBehavior singleton that controls the internal sim.
    */

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init() {
        SimGrid.Init();
    }
}
