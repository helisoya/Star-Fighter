using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    public int creditsReward;
    public int health;

    public GameObject prefabExplosion;

    public bool target = false;

    public void TakeDamage(int dmg){
        health-=dmg;
        if(health<=0){
            if(target){
                MissionManager.instance.RemoveTarget(this);
            }
            GameManager.instance.AddCredits(creditsReward);
            GameObject ob = Instantiate(prefabExplosion,transform.position,new Quaternion());
            Destroy(ob,2);
            Destroy(gameObject);
        }
    }
}
