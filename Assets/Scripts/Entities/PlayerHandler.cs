using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour{
    public PlayerControle controle;
    public PlayerCombat combat;
    public Animator animator;
    public Rigidbody2D body;
    public GameObject FloatingTextPrefab;
    public EntityHandler entityHandler;
    public bool facingRight;

    public int maxHealth, critChance, attackDammage, dodge;
    public float speed, attackRange = 0.5f;

    void Start(){
        animator = GetComponent<Animator>();
    }
}
