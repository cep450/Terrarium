using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{

    [SerializeField] Image toggle;
    [SerializeField] Sprite pause;
    [SerializeField] Sprite play;

    public void UIPause() {
        toggle.sprite = play;
    }   

    public void UIPlay() {
        toggle.sprite = pause;
    }

    public void TogglePause() {
        Clock.TogglePause();
    }

    public void SpeedUp() {
        Clock.IncrementSpeed();
    }

    public void SlowDown() {
        Clock.DecrementSpeed();
    }
}
