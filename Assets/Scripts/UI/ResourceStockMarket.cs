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

    public Slider satisfactionSlider;
    public Image satisfactionFill, satisfactionBackground;
    public int needIndex = -1;
    [SerializeField] Color basic, amenity;

    public void SetResource(ResourceInfo resource) {
        icon.sprite = resource.icon;
        icon.color = resource.color;
        sliderFillArea.color = resource.color;
        satisfactionFill.color = resource.color;

        //index of the need associated with the resoruce, if there is one.
        needIndex = AgentDirector.needs.IndexOf(AgentDirector.needs.Find(i => i.needName.Equals(resource.name)));
        if(needIndex > -1) {
            satisfactionSlider.gameObject.SetActive(true);
            bool isBasic = AgentDirector.needs[needIndex].isNecessary;
            satisfactionBackground.color = (isBasic ? basic : amenity);
        }

    }

}