using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour{
    public PlayerControle controle;
    public PlayerCombat combat;
    public Animator animator;
    public Rigidbody2D body;
    public GameObject FloatingTextPrefab;
    public List<FloatingText> textToDestroy;

    public int maxHealth, critChance, attackDammage, dodge;
    public float speed, attackRange = 0.5f;

    void Start(){
        animator = GetComponent<Animator>();
        textToDestroy = new List<FloatingText>();
    }

    public void ShowFloatingText(string str, bool destroyable, string tag, Color color) {
        var go = Instantiate(FloatingTextPrefab, transform.position + (FloatingTextPrefab.transform.up * 1.5f), Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = str;
        go.GetComponent<TextMesh>().color = color;
        go.GetComponent<FloatingText>().tag = tag;
        go.GetComponent<FloatingText>().destroy = destroyable;
        if (!destroyable) textToDestroy.Add(go.GetComponent<FloatingText>());
    }
}
