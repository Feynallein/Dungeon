using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour{
	public bool destroy;

	void Start(){
		if (destroy)
		{
			Animator animator = gameObject.GetComponent<Animator>();
			AnimatorClipInfo[] clip = animator.GetCurrentAnimatorClipInfo(0);
			float length = clip[0].clip.length;
			Destroy(gameObject, length);
		}
		else
		{
			gameObject.GetComponent<Animator>().enabled = false;
		}
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
