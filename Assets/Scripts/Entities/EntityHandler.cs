using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHandler : MonoBehaviour{
    [HideInInspector] 
    public List<FloatingText> textToDestroy;

    private void Start()
    {
        textToDestroy = new List<FloatingText>();
    }

    public void TakeDammages(int dammages, ref int currentHealth, ref Animator animator, GameObject FloatingTextPrefab)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Recovering"))
        {
            Color color = Color.yellow;
            if (dammages >= 200) color = Color.red;
            else if (dammages >= 100) color = new Color(1.0f, 0.64f, 0.0f);
            currentHealth -= dammages;
            if (FloatingTextPrefab) ShowFloatingText(dammages.ToString(), true, "dammage", color, FloatingTextPrefab);
            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                animator.SetBool("isDead", true);
                //animator.ResetTrigger("Hurt");
            }
        }
    }

    public void ShowFloatingText(string str, bool destroyable, string tag, Color color, GameObject FloatingTextPrefab)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position + (FloatingTextPrefab.transform.up * 1.5f), Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = str;
        go.GetComponent<TextMesh>().color = color;
        go.GetComponent<FloatingText>().tag = tag;
        go.GetComponent<FloatingText>().destroy = destroyable;
        if (!destroyable) textToDestroy.Add(go.GetComponent<FloatingText>());
/*        if (!facingRight)
        {
            Vector3 newScale = go.GetComponent<FloatingText>().transform.localScale;
            newScale.x *= -1;
            go.GetComponent<FloatingText>().transform.localScale = newScale;
        }*/
    }

    public void Recover(ref Animator animator, ref int currentHealth, int maxHealth)
    {
        currentHealth = maxHealth;
        animator.SetBool("isDead", false);
        GetComponent<Collider2D>().enabled = true;
    }

    public void Die(ref Animator animator)
    {
        //GetComponent<Collider2D>().enabled = false;
    }
}
