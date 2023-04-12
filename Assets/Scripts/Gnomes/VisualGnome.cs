using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGnome : MonoBehaviour
{

	[SerializeField] Sprite[] sprites;
	[SerializeField] public SpriteRenderer myRenderer;
	[SerializeField] Animator animator;
	int animationState;
	int animIdle = 0; int animWalking = 1; int animWorking = 2;

	// Start is called before the first frame update
	void Start()
	{
		//become a random gnome
		int rand = Random.Range(0, sprites.Length);
		myRenderer.sprite = sprites[rand];
	}

	public void AnimIdle() {
		if(animationState != animIdle) {
			animator.SetInteger("state", animIdle);
		}
	}

	public void AnimWalking() {
		if(animationState != animWalking) {
			animator.SetInteger("state", animWalking);
		}
	}

	public void AnimWorking() {
		if(animationState != animWorking) {
			animator.SetInteger("state", animWorking);
		}
	}
}
