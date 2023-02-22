using System.Collections;
using UnityEngine;

public class VisualHex : MonoBehaviour {

    public SimHex simHex { get; private set; }

    float verticalWiggle = 0.15f;

    SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AssignSimHex(SimHex sHex) {
        simHex = sHex;
        transform.position = Sim.hexMap.grid.HexToCenter(simHex.cube).position;

        //a bit of vertical wiggle 
        transform.Translate(new Vector3(0, 0, Random.Range(-verticalWiggle, verticalWiggle)));
    }

    public void VisualUpdate() {
        spriteRenderer.color = simHex.type.color;
        if(simHex.type.sprite != null) {
            spriteRenderer.sprite = simHex.type.sprite;
        }
    }

}