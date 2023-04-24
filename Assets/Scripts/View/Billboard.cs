using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    [SerializeField] Transform [] billboardedSprites;
    [SerializeField] bool billboardAllChildren = false;
    [SerializeField] bool moves = false;

    void Start() {
        if(billboardAllChildren) {
            List<Transform> all = new List<Transform>();
            GetComponentsInChildren<Transform>(all);
            all.Remove(this.transform);
            billboardedSprites = all.ToArray();
        }
        if(!moves) {
            UpdateBillboards();
        }
    }

    void Update() {
        if(moves) {
            UpdateBillboards();
        }
    }

    void UpdateBillboards() {
         foreach(Transform t in billboardedSprites) {
            Vector3 direction = Camera.main.transform.position - t.position;
            t.forward = new Vector3(0, direction.y, direction.z);
        }
    }
}