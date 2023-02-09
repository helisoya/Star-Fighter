using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalTurret : MonoBehaviour
{
    Transform player;

    public Transform turretPart;

    public float cooldown;
    float maxCooldown;

    public GameObject prefabShot;

    public Transform bulletHolder;

    public float maxDistance;

    public Transform canon;

    void Start(){
        maxCooldown = cooldown;
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        turretPart.LookAt(player);
        if(cooldown > 0){
            cooldown-=Time.deltaTime;
        }else if(Vector3.Distance(transform.position,player.position) <= maxDistance){
            RaycastHit raycast;
            if(Physics.Raycast(canon.position,player.transform.position-canon.position,out raycast)){
                if(raycast.transform.gameObject.tag=="Player"){ 
                    cooldown = maxCooldown;
                    Shoot();   
                }
            }
        }
    }

    void Shoot(){
        GameObject ob = Instantiate(prefabShot,bulletHolder);
        ob.transform.position = transform.position;
        ob.transform.rotation = turretPart.transform.rotation;
        ob.GetComponent<Shot>().playerShot = false;
    }
}
