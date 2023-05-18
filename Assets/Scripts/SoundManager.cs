using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip winSound,
                               loseSound,
                               pauseSimSound,
                               playSimSound,
                               speedUpSound,
                               slowDownSound,
                               addWorkOrderSound,
                               workCompleteSound,
                               enterGreenZoneSound,
                               enterRedZoneSound,
                               hoverTickSound,
                               selectTickSound;
    static SoundManager instance;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        instance = this;
    }

    public static void WinSoundPlayer()
    {
        instance.source.PlayOneShot(instance.winSound);
    }

    public static void LoseSoundPlayer()
    {
        instance.source.PlayOneShot(instance.loseSound);
    }

    public static void PauseSimSoundPlayer()
    {
        instance.source.PlayOneShot(instance.pauseSimSound);
    }
    public static void SpeedUpSoundPlayer()
    {
        instance.source.PlayOneShot(instance.speedUpSound);
    }

    public static void SlowDownSoundPlayer()
    {
        instance.source.PlayOneShot(instance.slowDownSound);
    }
    public static void AddWorkOrderSoundPlayer()
    {
        instance.source.PlayOneShot(instance.addWorkOrderSound);
    }
    public static void WorkCompleteSoundPlayer()
    {
        instance.source.PlayOneShot(instance.workCompleteSound);
    }
    public static void EnterGreenZoneSoundPlayer()
    {
        instance.source.PlayOneShot(instance.enterGreenZoneSound);
    }
    public static void EnterRedZoneSoundPlayer()
    {
        instance.source.PlayOneShot(instance.enterRedZoneSound);
    }
    public static void HoverTickSoundPlayer()
    {
        instance.source.PlayOneShot(instance.hoverTickSound);
    }
    public static void SelectTickSoundPlayer()
    {
        instance.source.PlayOneShot(instance.selectTickSound);
    }
}
