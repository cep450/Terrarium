using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkListUI : MonoBehaviour
{
    public string workListText;
    List<Agent> agents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        workListText = AgentDirector.AllTaskLists();
        gameObject.GetComponent<TextMeshProUGUI>().text = workListText;
    }
}
