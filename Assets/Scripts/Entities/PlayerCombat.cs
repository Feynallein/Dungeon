using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour{
    public PlayerHandler handler;

	public Transform attackPoint;
    public LayerMask enemyLayers;
    private float helding;
    private float charged;
    private bool showCharge = false;
    private int currentHealth;

    private void Start()
    {
        currentHealth = handler.maxHealth;
    }

    void Update(){
        if (!handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                helding = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Arming"))
                {
                    handler.animator.SetBool("isArming", false);
                    if (showCharge)     //A FAIRE DANS UNE FONCTION A PART MAIS FONCTIONNE !!!
                    {
                        foreach (FloatingText txt in handler.entityHandler.textToDestroy.ToArray())
                        {
                            if (txt.tag == "charge")
                            {
                                txt.Destroy();
                                handler.entityHandler.textToDestroy.Remove(txt);
                            }
                        }
                        showCharge = false;
                    }
                    Attack();
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!handler.animator.GetCurrentAnimatorStateInfo(0).IsTag("Arming"))
                {
                    handler.animator.SetBool("isArming", true);
                    AnimatorClipInfo[] clip = handler.animator.GetCurrentAnimatorClipInfo(0);
                    charged = clip[0].clip.length;
                }
                if (Time.time - helding > charged && !showCharge)
                {
                    if (handler.FloatingTextPrefab) handler.entityHandler.ShowFloatingText("Charged!", false, "charge", Color.white, handler.FloatingTextPrefab);
                    showCharge = true;
                }
            }
        }

        // TEMPORAIRE

        if (Input.GetMouseButtonUp(0)) TakeDammages(10);
        if (Input.GetKeyUp(KeyCode.Space)) handler.entityHandler.Recover(ref handler.animator, ref currentHealth, handler.maxHealth);
    }
    
    void OnDrawGizmosSelected(){
    	if(attackPoint == null) return;
    	Gizmos.DrawWireSphere(attackPoint.position, handler.attackRange);
    }

    public void TakeDammages(int dammages)
    {
        handler.entityHandler.TakeDammages(dammages, ref currentHealth, ref handler.animator, handler.FloatingTextPrefab);
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
}
