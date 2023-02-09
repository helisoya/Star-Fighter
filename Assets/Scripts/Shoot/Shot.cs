using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shot : MonoBehaviour
{
    public float timeBeforeExplosion;
    public float speed;

    public bool playerShot = true;

    [HideInInspector]
    public float timeBeforeExplosionMax;

    public int dmg = 2;

    void Start(){
        timeBeforeExplosionMax = timeBeforeExplosion;
    }

    void OnTriggerEnter(Collider col){
        if(playerShot && col.tag == "Ennemy"){
            FindParent(col.transform,"Ennemy").GetComponent<EnnemyHealth>().TakeDamage(dmg);
            
        }else if (!playerShot && col.tag == "Player"){
            FindParent(col.transform,"Player").GetComponent<PlayerHealth>().TakeDamage(dmg);
        }
    
        if(!(playerShot && col.tag=="Player") && !(!playerShot && col.tag=="Ennemy")){ 
            Destroy(gameObject);
        }
        
    }

    Transform FindParent(Transform start, string tag){
        while(start.parent != null && start.parent.tag==tag){
            start = start.parent;
        }
        return start;
    }
}
