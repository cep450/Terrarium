using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceChangeIcon : MonoBehaviour
{
    [SerializeField] Image renderer; //TODO numbers or counts of resource or size? size could be useful
    [SerializeField] Image upArrow, downArrow;

    static float scaleMult = 0.5f;

    public void Fill(Sprite sprite, bool produces, bool consumes, int amount = 1) {

        Empty();

        renderer.enabled = true;
        renderer.sprite = sprite;

        if(amount < 1) amount = 1;
        Vector3 amountScale = Vector3.one * (((amount - 1) * scaleMult) + 1);

        //TODO this could also be like, a triple arrow instead of a single arrow
        if(produces) {
            upArrow.enabled = true;
            upArrow.transform.localScale = amountScale;
        } else if(consumes) {
            downArrow.enabled = true;
            downArrow.transform.localScale = amountScale;
        }
    }

    public void Empty() {
        upArrow.transform.localScale = Vector3.one;
        downArrow.transform.localScale = Vector3.one;
        renderer.enabled = false;
        upArrow.enabled = false;
        downArrow.enabled = false;
    }
}
