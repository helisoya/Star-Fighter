using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Shot
{
    public GameObject prefabExplosion;


    void Update()
    {
        if(timeBeforeExplosion>0){
            timeBeforeExplosion-=Time.deltaTime;
        }else{
            Explode();
            return;
        }
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    void OnTriggerEnter(Collider col){
        if(playerShot && col.tag == "Ennemy"){
            FindParent(col.transform,"Ennemy").GetComponent<EnnemyHealth>().TakeDamage(dmg);
            
        }else if (!playerShot && col.tag == "Player"){
            FindParent(col.transform,"Player").GetComponent<PlayerHealth>().TakeDamage(dmg);
        }
    
        if(!(playerShot && col.tag=="Player") && !(!playerShot && col.tag=="Ennemy")){ 
            Explode();
        }
    }


    void Explode(){
        GameObject ob = Instantiate(prefabExplosion,transform.position,new Quaternion());
        Destroy(ob,2);
        Destroy(gameObject);
    }


    Transform FindParent(Transform start, string tag){
        while(start.parent != null && start.parent.tag==tag){
            start = start.parent;
        }
        return start;
    }
}
