using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkOrderUI : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI tileOldText, tileNewText;
    [SerializeField] Image tileOldBackground, tileNewBackground;
    Task task = null;
    public bool inUse = false;
    bool dismissing = false;
    
    public void LoadInfo(Task t) {

        task = t;

        tileOldText.text = task.destinationType.displayName;
        tileNewText.text = task.desiredType.displayName;

        tileOldBackground.color = task.destinationType.color;
        tileNewBackground.color = task.desiredType.color;

        slider.maxValue = task.duration;

        inUse = true;
        this.transform.SetAsFirstSibling();
        this.gameObject.SetActive(true);
    }

    void Update() {
        if(inUse) {
            slider.value = slider.maxValue - task.duration;
            if(task.isComplete && !dismissing) {
                dismissing = true;
                Dismiss();
            }
        }
    }

    public void Dismiss() {
        StartCoroutine(DismissCoroutine());
    }


    float dismissTime = 1f;
    IEnumerator DismissCoroutine() {

        float tracker = 0; 
        while(tracker < dismissTime) {
            tracker += Time.deltaTime;
            yield return null;
        }

        this.transform.SetAsLastSibling();
        inUse = false;
        this.gameObject.SetActive(false);
        dismissing = false;
    }
}
