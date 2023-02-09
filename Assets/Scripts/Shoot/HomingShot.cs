using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShot : Shot
{

    Vector3 target; 

    void Start(){
        timeBeforeExplosionMax = timeBeforeExplosion;
        target = transform.forward;
    }
   
    void Update()
    {
        if(timeBeforeExplosion >= 2 && timeBeforeExplosion <= timeBeforeExplosionMax-1 &&
        ((playerShot && GameObject.FindGameObjectsWithTag("Ennemy").Length != 0 ) || !playerShot )){
            Vector3 pos = FindClosestEnnemy();
            float dist = Distance(pos,transform.position);
            target = new Vector3((pos.x - transform.position.x)/dist,(pos.y - transform.position.y)/dist,(pos.z - transform.position.z)/dist) ;
        }

        if(timeBeforeExplosion>0){
            timeBeforeExplosion-=Time.deltaTime;
        }else{
            Destroy(gameObject);
            return;
        }
        transform.position += target * speed * Time.deltaTime;
        transform.LookAt(transform.position+target);
    }

    Vector3 FindClosestEnnemy(){
        if(!playerShot){
            return GameObject.FindWithTag("Player").transform.position;
        }
        float dist = 999999;
        Vector3 pos = Vector3.zero;
        foreach(GameObject ob in GameObject.FindGameObjectsWithTag("Ennemy")){
            if(Distance(ob.transform.position,transform.position ) < dist){
                dist = Distance(ob.transform.position,transform.position);
                pos = ob.transform.position;
            }
        }
        return pos;
    }

    float Distance(Vector3 a,Vector3 b){
        return  Mathf.Sqrt(Mathf.Pow(a.x-b.x,2)+Mathf.Pow(a.y-b.y,2)+Mathf.Pow(a.z-b.z,2));
    }

}
