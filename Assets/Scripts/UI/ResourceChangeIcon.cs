using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceChangeIcon : MonoBehaviour
{
    [SerializeField] Image renderer; //TODO numbers or counts of resource or size? size could be useful
    [SerializeField] Image upArrow, downArrow;

    public void Fill(Sprite sprite, bool produces, bool consumes, int amount = 1) {

        Empty();

        renderer.enabled = true;
        renderer.sprite = sprite;

        renderer.transform.localScale = Vector3.one * amount; //for now, showing amount with scale

        if(produces) {
            upArrow.enabled = true;
        } else if(consumes) {
            downArrow.enabled = true;
        }
    }

    public void Empty() {
        renderer.transform.localScale = Vector3.one;
        renderer.enabled = false;
        upArrow.enabled = false;
        downArrow.enabled = false;
    }
}
