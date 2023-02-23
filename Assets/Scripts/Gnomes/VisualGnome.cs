using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGnome : MonoBehaviour
{

    [SerializeField] Sprite [] sprites;

    [SerializeField] SpriteRenderer myRenderer;
    [SerializeField] SpriteRenderer myRendererShadow;

    // Start is called before the first frame update
    void Start()
    {
        //become a random gnome
        int rand = Random.Range(0, sprites.Length);
        myRenderer.sprite = sprites[rand];
        myRendererShadow.sprite = sprites[rand];
    }
}
