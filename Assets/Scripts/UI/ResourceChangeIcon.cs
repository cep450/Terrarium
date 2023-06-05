using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct ResourceArrow {
    public GameObject obj;
    public int threshold;
}

public class ResourceChangeIcon : MonoBehaviour
{
    [SerializeField] Image renderer; 
    [SerializeField] ResourceArrow[] upArrows, downArrows;

    public void Fill(ResourceInfo res, bool produces, bool consumes, int amount = 1) {

        Empty();

        renderer.enabled = true;
        renderer.sprite = res.icon;
        renderer.color = res.color;

        //arrows
        if(produces) {
            foreach(ResourceArrow arrow in upArrows) {
                if(arrow.threshold <= amount) {
                    arrow.obj.SetActive(true);
                }
            }
        } else if(consumes) {
            foreach(ResourceArrow arrow in downArrows) {
                if(arrow.threshold <= amount) {
                    arrow.obj.SetActive(true);
                }
            }
        }
    }

    public void Empty() {
        
        renderer.enabled = false;

        for(int i = 0; i < upArrows.Length; i++) {
            upArrows[i].obj.SetActive(false);
            downArrows[i].obj.SetActive(false);
        }
    }
}
