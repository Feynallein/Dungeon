using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerControle : MonoBehaviour {
    public PlayerHandler handler;

	float xMove;

    void FixedUpdate(){
        if (handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Movable"))
        {
            xMove = Input.GetAxis("Horizontal") * handler.speed;
            Flip(xMove);
            Movement();
        }
        else {
            handler.body.velocity = new Vector2(0, 0);
        }
    }

    private void Movement(){
        if(xMove != 0) handler.animator.SetBool("isMoving", true);
	    else handler.animator.SetBool("isMoving", false);
	    handler.body.velocity = new Vector2(xMove, handler.body.velocity.y);
    	
    }

    private void Flip(float xMove){
    	if((xMove > 0 && !handler.facingRight) || (xMove < 0 && handler.facingRight)) {
    		handler.facingRight = !handler.facingRight;
    		Vector3 newScale = transform.localScale;
    		newScale.x *= -1;
    		transform.localScale = newScale;
    	} 
    }
}
