using System.Collections;
using UnityEngine;

public class VisualHex : MonoBehaviour {

    public SimHex simHex { get; private set; }

    //float elevationScale = 1f; //visual scale of internal elevation value

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer billboardedSprite;

    GameObject visualHex;

    public void AssignSimHex(SimHex sHex) {
        simHex = sHex;
        transform.position = Sim.hexMap.grid.HexToCenter(simHex.cube).position;

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

    Color dead = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public void Die() {
        SpriteRenderer [] childSprites = visualHex.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in childSprites) {
            if(s.Equals(this.spriteRenderer)) continue;
            s.color = dead;
        }
    }

    //mouse enters collider
    void OnMouseOver() {
        CustomCursor.SetClickable();
        if(simHex == null) {
            Debug.LogError("moused over visualHex with null simHex");
            return;
        }
        if(simHex.type == null) {
            Debug.LogError("moused over visual hex with simhex with null type");
            return;
        }
        CustomCursor.tileHover.SetType(simHex.type);
    }

    //mouse exits collider 
    void OnMouseExit() {
        CustomCursor.SetUnclickable();
        CustomCursor.tileHover.Hide();
    }

    //mouse clicks on collider 
    void OnMouseDown() {

        BuildXOnYUI.instance.SelectOn(simHex.type);

    }

}