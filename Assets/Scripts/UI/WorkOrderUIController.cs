using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOrderUIController : MonoBehaviour
{

    List<WorkOrderUI> list;
    [SerializeField] GameObject workOrderUIPrefab;
    static WorkOrderUIController instance;
    public static int index = 0;

    void Start() {
        instance = this;
        list = new List<WorkOrderUI>();
    }

    //reuse one if available, if not, make a new one 
    public void CreateWorkOrderUI(Task task, Agent agent) {
        bool found = false;
        foreach(WorkOrderUI wo in list) {
            if(!wo.inUse) {
                found = true;
                wo.LoadInfo(task, agent);
                break;
            }
        }
        if(!found) {
            WorkOrderUI newWO = Instantiate(workOrderUIPrefab, this.transform).GetComponent<WorkOrderUI>();
            newWO.LoadInfo(task, agent);
        }
    }

    public static void AddWorkOrderUI(Task task, Agent agent) {
        instance.CreateWorkOrderUI(task, agent);
    }
}
