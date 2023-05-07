using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOrderUIController : MonoBehaviour
{

    List<WorkOrderUI> list;
    [SerializeField] GameObject workOrderUIPrefab;
    static WorkOrderUIController instance;

    void Start() {
        instance = this;
        list = new List<WorkOrderUI>();
    }

    //reuse one if available, if not, make a new one 
    public void CreateWorkOrderUI(Task task) {
        bool found = false;
        foreach(WorkOrderUI wo in list) {
            if(!wo.inUse) {
                found = true;
                wo.LoadInfo(task);
                break;
            }
        }
        if(!found) {
            WorkOrderUI newWO = Instantiate(workOrderUIPrefab, this.transform).GetComponent<WorkOrderUI>();
            newWO.LoadInfo(task);
        }
    }

    public static void AddWorkOrderUI(Task task) {
        instance.CreateWorkOrderUI(task);
    }
}
