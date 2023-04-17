using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    [SerializeField] Transform [] billboardedSprites;
    [SerializeField] bool billboardAllChildren = false;

    void Start() {
        if(billboardAllChildren) {
            List<Transform> all = new List<Transform>();
            GetComponentsInChildren<Transform>(all);
            all.Remove(this.transform);
            billboardedSprites = all.ToArray();
        }
    }

    void Update() {

        foreach(Transform t in billboardedSprites) {
            Vector3 direction = Camera.main.transform.position - t.position;
            //t.forward = direction;
            //t.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, 0, 0);
            t.forward = new Vector3(0, direction.y, direction.z);
        }

        /*
        foreach(Transform t in billboardedSprites) {
            Vector3 orig = t.rotation.eulerAngles;
            t.LookAt(Camera.main.transform);
            t.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, orig.y, orig.z);
        }*/

        /*
        float angle = Camera.main.transform.rotation.eulerAngles.x;
        Vector3 rotationEuler;

        Vector3 r = billboardedSprites[0];
        

        foreach(Transform t in billboardedSprites) {
            if(t.rotation.eulerAngles.x != rotationEuler.x) {
                t.rotation.eulerAngles = new Vector3(rotationEuler.x, )
            }
            
        }
        */
    }

}