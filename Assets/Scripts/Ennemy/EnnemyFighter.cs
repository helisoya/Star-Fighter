using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFighter : MonoBehaviour
{
    Transform player;

    public Transform shipGun;

    public float cooldown;
    float maxCooldown;

    public GameObject prefabShot;

    public Transform bulletHolder;
    public float maxDistance;
    Vector3 target = Vector3.zero;

    public float speed;

    public float maxFlightRandomDistance;


    void Start(){
        maxCooldown = cooldown;
        player = GameObject.Find("Player").transform;
        GetRandomPosition();
    }


    void Update(){
        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }
        if(Vector3.Distance(player.position,transform.position) <= maxDistance){ // Joueur dans les parages
            if(CheckIfPlayerVisible()){
                target = player.position;
                if(cooldown<=0){
                    cooldown = maxCooldown;
                    Fire();
                }
            }
            
        }


        transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
        transform.LookAt(target);

        if(target == transform.position){
            GetRandomPosition();
        }  
    }


    bool CheckIfPlayerVisible(){
        RaycastHit raycast;

        if(Physics.Raycast(transform.position,player.transform.position-transform.position,out raycast)){
            return raycast.transform.gameObject.tag=="Player";
        }
        return false;
    }


    void Fire(){
        GameObject ob = Instantiate(prefabShot,bulletHolder);
        ob.transform.position = transform.position;
        ob.transform.rotation = transform.rotation;
        ob.GetComponent<Shot>().playerShot = false;
    }

    void GetRandomPosition(){
        target = new Vector3(
            Random.Range(-maxFlightRandomDistance,maxFlightRandomDistance),
            Random.Range(-maxFlightRandomDistance,maxFlightRandomDistance),
            Random.Range(-maxFlightRandomDistance,maxFlightRandomDistance)
        );
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == null){
            GetRandomPosition();
        }
    }
}
