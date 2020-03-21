using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerControle : MonoBehaviour {
    public PlayerHandler handler;
	public bool facingRight;

	float xMove;

    void FixedUpdate(){
    	xMove = Input.GetAxis("Horizontal") * handler.speed;
    	Flip(xMove);
		Movement();
    }

    private void Movement(){
    	if(!handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Arming")){
	    	if(xMove != 0) handler.animator.SetBool("isMoving", true);
	    	else handler.animator.SetBool("isMoving", false);
	    	handler.body.velocity = new Vector2(xMove, handler.body.velocity.y);
    	}
    	else if(handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Arming")){
    		handler.body.velocity = new Vector2(0, 0);
    	}
    }

    private void Flip(float xMove){
    	if((xMove > 0 && !facingRight) || (xMove < 0 && facingRight)) {
    		facingRight = !facingRight;
    		Vector3 newScale = transform.localScale;
    		newScale.x *= -1;
    		transform.localScale = newScale;
    	} 
    }
}
