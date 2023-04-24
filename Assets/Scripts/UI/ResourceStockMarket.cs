using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceStockMarket : MonoBehaviour {

    /*
        An entry in the stock market UI. 
    */

    public TextMeshProUGUI text;
    public Slider slider;
    public Image icon;
    public Image sliderFillArea;

    public void SetResource(ResourceInfo resource) {
        icon.sprite = resource.icon;
        icon.color = resource.color;
        sliderFillArea.color = resource.color;
    }

}