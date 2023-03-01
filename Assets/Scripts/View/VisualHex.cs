using System.Collections;
using UnityEngine;

public class VisualHex : MonoBehaviour {

    public SimHex simHex { get; private set; }

    float elevationScale = 1f; //visual scale of internal elevation value

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer billboardedSprite;

    public void AssignSimHex(SimHex sHex) {
        simHex = sHex;
        transform.position = Sim.hexMap.grid.HexToCenter(simHex.cube).position;

        //a bit of vertical wiggle 
        //transform.Translate(new Vector3(0, 0, Random.Range(-verticalWiggle, verticalWiggle)));
    
        //elevation 
        transform.Translate(new Vector3(0, 0, simHex.elevation * elevationScale));
    }

    public void VisualUpdate() {
        spriteRenderer.color = simHex.type.color;
        if(simHex.type.sprite != null) {
            spriteRenderer.sprite = simHex.type.sprite;
        }
        if(simHex.type.billboardSprite != null) {
            billboardedSprite.sprite = simHex.type.billboardSprite;
        }
    }

}