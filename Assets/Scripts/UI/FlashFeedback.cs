using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashFeedback : MonoBehaviour {

    [SerializeField] Color flashColor;
    Color startingColor;
    Image imgToFlash;
    [SerializeField] float flashTime = 0.2f;
    bool flashing = false;

    void Start() {
        imgToFlash = this.gameObject.GetComponent<Image>();
    }

    public void Flash() {
        if(flashing) {
            StopCoroutine(FlashCoroutine());
            Reset();
        }
        StartCoroutine(FlashCoroutine());
    }

    void Reset() {
        imgToFlash.color = startingColor;
        flashing = false;
    }

    IEnumerator FlashCoroutine() {

        flashing = true;

        startingColor = imgToFlash.color;
        imgToFlash.color = flashColor;

        float tracker = 0; 
        while(tracker < flashTime) {
            tracker += Time.deltaTime;
            yield return null;
        }

        Reset();
        
    }
}