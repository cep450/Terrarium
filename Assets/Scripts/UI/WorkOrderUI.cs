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

    public void LoadInfo(SimHexType tileOld, SimHexType tileNew) {

        tileOldText.text = tileOld.displayName;
        tileNewText.text = tileNew.displayName;

        tileOldBackground.color = tileOld.color;
        tileNewBackground.color = tileNew.color;
    }

    //TODO how to update the value?

    //TODO probably want to only instantiate a few, and reuse them 
}
