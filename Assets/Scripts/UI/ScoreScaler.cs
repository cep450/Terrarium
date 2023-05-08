using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScaler : MonoBehaviour
{

    [SerializeField] RectTransform refRect;
    [SerializeField] RectTransform fullsize;
    [SerializeField] RectTransform rectToScale;

    [SerializeField] float leftright = 400f;

    // Start is called before the first frame update
    void Update()
    {
        float widthHave = fullsize.rect.width - (leftright * 2f);
        if(refRect.rect.width > widthHave) {
            float percent = widthHave / refRect.rect.width;
            rectToScale.localScale = Vector3.one * percent;
        }
    }
}
