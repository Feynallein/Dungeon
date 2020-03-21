using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
	public Animator animator;
    public GameObject FloatingTextPrefab; 

	public int maxHealth = 100;
	private int currentHealth;

    void Start(){
    	currentHealth = maxHealth;
    }

    void Update(){  
        if(Input.GetKeyUp(KeyCode.E) && currentHealth <= 0){
            Recover();
        }
    }






    public void TakeDammages(int dammages){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("Recovering")){
        	currentHealth -= dammages;
        	animator.SetTrigger("Hurt");
            if(FloatingTextPrefab) ShowFloatingText(dammages);
        	if(currentHealth <= 0){
        		Die();
        	}
        }
    }

    void ShowFloatingText(int dammages){
        var go = Instantiate(FloatingTextPrefab, transform.position + (FloatingTextPrefab.transform.up * 1.5f), Quaternion.identity, transform); 
        go.GetComponent<TextMesh>().text = dammages.ToString();
        go.GetComponent<FloatingText>().destroy = true;
    }

    void Recover(){
        currentHealth = maxHealth;
        animator.SetBool("isDead", false);
        GetComponent<Collider2D>().enabled = true;
    }

    void Die(){
    	animator.SetBool("isDead", true);
    	GetComponent<Collider2D>().enabled = false;
    }
}
