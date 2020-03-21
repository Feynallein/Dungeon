using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
	public Animator animator;
    public GameObject FloatingTextPrefab;
    public EntityHandler entityHandler;

	public int maxHealth = 100;
	private int currentHealth;

    void Start(){
    	currentHealth = maxHealth;
    }

    void Update(){  
        if(Input.GetKeyUp(KeyCode.E) && currentHealth <= 0){
            entityHandler.Recover(ref animator, ref currentHealth, maxHealth);
        }
    }

    public void TakeDammages(int dammages)
    {
        //entityHandler.TakeDammages(dammages, ref currentHealth, ref animator, FloatingTextPrefab, facingRight);
    }
}
