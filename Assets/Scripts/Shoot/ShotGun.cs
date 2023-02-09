using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Shot
{
    public GameObject prefabSmallerShot;

    void Update()
    {
        if(timeBeforeExplosion>0){
            timeBeforeExplosion-=Time.deltaTime;
        }else{
            SpawnTriLaser();
            Destroy(gameObject);
            return;
        }
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    void SpawnTriLaser(){
        SpawnLaser(transform.forward);
        SpawnLaser(transform.forward+transform.right*0.25f);
        SpawnLaser(transform.forward-transform.right*0.25f);
        SpawnLaser(transform.forward+transform.up*0.25f);
        SpawnLaser(transform.forward-transform.up*0.25f);
    }

    void SpawnLaser(Vector3 lookAt){
        GameObject ob = Instantiate(prefabSmallerShot,transform.parent);
        ob.transform.position = transform.position;
        ob.transform.LookAt(transform.position+lookAt);
    }
}
