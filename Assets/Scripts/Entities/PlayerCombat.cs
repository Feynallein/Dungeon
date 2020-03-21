using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour{
    public PlayerHandler handler; 

	public Transform attackPoint;
    public LayerMask enemyLayers;
    private float helding;
    private float charged;
    private bool showCharge = false, showRevive = false;
    private int currentHealth;

    private void Start()
    {
        currentHealth = handler.maxHealth;
    }

    void Update(){
        if(!handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                helding = Time.time;
            }
            if(Input.GetKeyUp(KeyCode.UpArrow)){
                if(handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Arming")){
                    handler.animator.SetBool("isArming", false);
                    if (showCharge)     //A FAIRE DANS UNE FONCTION A PART MAIS FONCTIONNE !!!
                    {
                        foreach (FloatingText txt in handler.textToDestroy.ToArray())
                        {
                            if (txt.tag == "charge")
                            {
                                txt.Destroy();
                                handler.textToDestroy.Remove(txt);
                            }
                        }
                        showCharge = false;
                    }
                    Attack();
                }
            }
            if(Input.GetKey(KeyCode.UpArrow)){
               if(!handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Arming")){
                    handler.animator.SetBool("isArming", true);
                    AnimatorClipInfo[] clip = handler.animator.GetCurrentAnimatorClipInfo(0);
                    charged = clip[0].clip.length;
               }
               if(Time.time - helding > charged && !showCharge) {
                   if (handler.FloatingTextPrefab) handler.ShowFloatingText("Charged!", false, "charge", Color.white);
                   showCharge = true;
               }
        	}
        }

        if (Input.GetMouseButtonUp(0)) GetHit(10);
        if (Input.GetKeyUp(KeyCode.Space)) Revive();

        if (currentHealth <= 0 && !showRevive)
        {
            //handler.ShowFloatingText("PRESS SPACE TO REVIVE", false, "revive");
            showRevive = true;
        }
    }

    void OnDrawGizmosSelected(){
    	if(attackPoint == null) return;
    	Gizmos.DrawWireSphere(attackPoint.position, handler.attackRange);
    }








    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, handler.attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (Time.time - helding > charged) enemy.GetComponent<Enemy>().TakeDammages(5 * handler.attackDammage);
            else enemy.GetComponent<Enemy>().TakeDammages(handler.attackDammage);
        }
    }

    public void GetHit(int dammages){
        Color color;
        if (dammages > 200) color = Color.red;
        else if (dammages > 100) color = new Color(1.0f, 0.64f, 0.0f);
        else color = Color.yellow;
        if(true)
        {
            currentHealth -= dammages;
            handler.animator.SetTrigger("Dammaged");
            handler.ShowFloatingText(dammages.ToString(), true, "dammage", color);
            if(currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            handler.ShowFloatingText("Dodge!", true, "dammage", Color.white);
        }
    }

    void Revive()
    {
        showRevive = false;
        currentHealth = handler.maxHealth;
        handler.animator.ResetTrigger("Dammaged");
        handler.animator.SetBool("isDead", false);
        //GetComponent<Collider2D>().enabled = true;
    }
    void Die()
    {
        handler.animator.SetBool("isDead", true);
        //GetComponent<Collider2D>().enabled = false;
    }
}
