using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    /*
        Manages the custom simulation tick.
        Player can speed up, slow down, and pause the sim using this. 
        Singleton.
    */

    [SerializeField] float _tickBaseSeconds = 0.5f;
    static float tickBaseSeconds = 0.5f; //constant 
    static float tickSpeedMult = 1f;  //changed by player
    static float tickSeconds = 1f;    //updates based on mult
    static float timeElapsed = 0f;    //a counter 
    static bool paused = true;
    static float speedMultMax = 3f;
    static int tickCounter = 0;

    public static event EventHandler<TickArgs> Tick;


    // Start is called before the first frame update
    void Awake()
    {
        tickBaseSeconds = _tickBaseSeconds;
        timeElapsed = 0f;
        paused = true;
        tickCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(paused) {
            return;
        }

        timeElapsed += Time.deltaTime;

        if(timeElapsed >= tickSeconds) {
            timeElapsed = timeElapsed % tickSeconds; //this will skip ticks if lag. use subtraction if want to catch up
            Tick?.Invoke(this, new TickArgs { tickNum = tickCounter } );
            tickCounter++; //will start on 0th tick 
        }
    }

    public static void IncrementSpeed() {

        if(paused) {
            UnPause();
            return;
        }

        if(tickSpeedMult + 1f > speedMultMax) {
            //can't go any faster
            return;
        }

        SetSpeed(++tickSpeedMult);

    }

    public static void DecrementSpeed() {
        
        if(tickSpeedMult - 1f <= 0f) {
            Pause();
            return;
        }

        SetSpeed(--tickSpeedMult);

    }

    static void SetSpeed(float newSpeedMult) {
        tickSeconds = tickBaseSeconds / newSpeedMult;
    }

    public static void TogglePause() {
        if(paused) {
            UnPause();
        } else {
            Pause();
        }
    }

    static void Pause() {
        paused = true;
    }

    static void UnPause() {
        paused = false;
    }

}