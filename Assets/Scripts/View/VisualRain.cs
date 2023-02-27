using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualRain : MonoBehaviour
{

    [SerializeField] SpriteRenderer sprRenderer;

    int rainSec = 3;

    //TODO also add a sound here 


    public void DisplayRain() {
        StopCoroutine(DisplayRainRoutine());
        sprRenderer.enabled = true;
        StartCoroutine(DisplayRainRoutine());
    }

    IEnumerator DisplayRainRoutine() {

        float timeElapsed = 0f;
        while(timeElapsed < rainSec) {

            timeElapsed += Time.deltaTime;

            float a = Mathf.Sin(timeElapsed * (Mathf.PI / rainSec));

            sprRenderer.color = new Color(1f, 1f, 1f, a);

            yield return null;

        }

        sprRenderer.enabled = false;

    }

}