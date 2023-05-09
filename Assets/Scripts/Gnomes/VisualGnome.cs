using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGnome : MonoBehaviour
{

	static GameObject [] variants = {null, null, null};
	[SerializeField] public SpriteRenderer myRenderer;
	[SerializeField] Animator animator;
	[SerializeField] Sprite [] icons;
	public Sprite icon;
	int animationState;
	int animIdle = 0; int animWalking = 1; int animWorking = 2;

	// Start is called before the first frame update
	void Start()
	{

		if(variants[0] == null) {
			variants[0] = Resources.Load("babyGnomeSprite") as GameObject;
			variants[1] = Resources.Load("ladyGnomeSprite") as GameObject;
			variants[2] = Resources.Load("lordGnomeSprite") as GameObject;
		}

		//become a random gnome
		int rand = Random.Range(0, 3);
		animator = Instantiate(variants[rand], this.transform).GetComponent<Animator>();
		icon = icons[rand];
	}

	public void AnimIdle() {
		if(animationState != animIdle) {
			animationState = animIdle;
			animator.SetInteger("state", animIdle);
		}
	}

	public void AnimWalking() {
		if(animationState != animWalking) {
			animationState = animWalking;
			animator.SetInteger("state", animWalking);
		}
	}

	public void AnimWorking() {
		if(animationState != animWorking) {
			animationState = animWorking;
			animator.SetInteger("state", animWorking);
		}
	}
}
