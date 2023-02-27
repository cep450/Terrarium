using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceListUI : MonoBehaviour
{
    public string ResourceListText;
    public string SatisfactionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResourceListText = GlobalPool.ResourceTextFullText();
        SatisfactionText = "Needs Satisfaction: " + AgentDirector.AverageWeightedSatisfaction();
        gameObject.GetComponent<TextMeshProUGUI>().text = ResourceListText + '\n' + SatisfactionText;
    }
}
