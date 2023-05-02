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
    static float tickBaseSeconds;       //constant. how many seconds in a level.
    public static float tickSpeedLevel {get; private set;} //number of levels from 0.
    static float tickSeconds = 1f;      //updates based on mult
    static float timeElapsed = 0f;      //a counter 
    public static bool paused {get; private set;}
    static float speedMultMax = 3f;
    static int tickCounter = 0;
    public static bool canPlay = false; //can you unpause and run the game? set to false when game over 

    public static event EventHandler<TickArgs> Tick;

    static GameObject pauseObject;
    static SpeedUI speedUI;

    void Awake() {
        tickBaseSeconds = _tickBaseSeconds;
    }

    public static void Init() { 
        pauseObject = GameObject.FindGameObjectWithTag("Pause"); 
        speedUI = FindObjectOfType<SpeedUI>();
        Pause();
        timeElapsed = 0f;
        tickCounter = 0;
        tickSpeedLevel = 1f;
        canPlay = true;
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
            //todo account for int overflow lol
        }
    }

    public static void IncrementSpeed() {

        if(paused) {
            UnPause();
            return;
        }

        if(tickSpeedLevel + 1f > speedMultMax) {
            //can't go any faster
            return;
        }

        SetSpeed(++tickSpeedLevel);

    }

    public static void DecrementSpeed() {
        
        if(tickSpeedLevel - 1f <= 0f) {
            Pause();
            return;
        }

        SetSpeed(--tickSpeedLevel);

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

    public static void Pause() {
        paused = true;
        pauseObject.SetActive(true);
        speedUI.UIPause();
    }

    public static void UnPause() {
        if(canPlay) {
            paused = false;
            pauseObject.SetActive(false);
            speedUI.UIPlay();
        }
    }

}