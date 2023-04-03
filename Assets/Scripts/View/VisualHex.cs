using System.Collections;
using UnityEngine;

public class VisualHex : MonoBehaviour {

    public SimHex simHex { get; private set; }

    //float elevationScale = 1f; //visual scale of internal elevation value

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer billboardedSprite;

    [SerializeField] GameObject popup;

    GameObject visualHex;

    public void AssignSimHex(SimHex sHex) {
        simHex = sHex;
        transform.position = Sim.hexMap.grid.HexToCenter(simHex.cube).position;

        //a bit of vertical wiggle 
        //transform.Translate(new Vector3(0, 0, Random.Range(-verticalWiggle, verticalWiggle)));
    
        //elevation 
        //transform.Translate(new Vector3(0, 0, simHex.elevation * elevationScale));
    }

    public void VisualUpdate() {

        if(simHex.type.visualHexPrefab != null) {

            spriteRenderer.enabled = false;
            billboardedSprite.enabled = false;

            if(visualHex != null) {
                Destroy(visualHex);
            }

            visualHex = Instantiate(simHex.type.visualHexPrefab, this.transform);
            visualHex.transform.Rotate(0, 0, Random.Range(0, 6) * 60f);

        } else {

            spriteRenderer.enabled = true;
            billboardedSprite.enabled = true;

            VisualUpdateOld();
        }
    }

    public void VisualUpdateOld() {
        spriteRenderer.color = simHex.type.color;
        if(simHex.type.sprite != null) {
            spriteRenderer.sprite = simHex.type.sprite;
        }
        if(simHex.type.billboardSprite != null) {
            billboardedSprite.sprite = simHex.type.billboardSprite;
        }
    }


    //mouse enters collider
    void OnMouseOver() {
        CustomCursor.SetClickable();
        popup.SetActive(true);
        
    }

    //mouse exits collider 
    void OnMouseExit() {
        CustomCursor.SetUnclickable();
        popup.SetActive(false);
    }

    //mouse clicks on collider 
    void OnMouseDown() {

        BuildXOnYUI.instance.SelectOn(simHex.type);

    }

}