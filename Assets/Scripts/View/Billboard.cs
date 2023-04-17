using System.Collections;
using UnityEngine;

public class Billboard : MonoBehaviour {

    [SerializeField] Transform [] billboardedSprites;

    void Update() {

        foreach(Transform t in billboardedSprites) {
            Vector3 orig = t.rotation.eulerAngles;
            t.LookAt(Camera.main.transform);
            t.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, orig.y, orig.z);
        }

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