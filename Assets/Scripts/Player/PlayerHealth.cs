using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    int maxHealth;
    public FighterUI ui;

    Vector3 startPos;

    void Start()
    {
        if(GameManager.instance.HasItem("HEALTH")){
            health+=health;
        }
        maxHealth = health;
        startPos = transform.position;
    }

    public void TakeDamage(int val){
        health= Mathf.Clamp(health-val,0,maxHealth);
        if(health == 0){
            transform.position = startPos;
            health = maxHealth;
        }
        ui.UpdateHealth(health,maxHealth);
    }
}
