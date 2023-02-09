using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterShoot : MonoBehaviour
{
    public GameObject prefabShotNormal;
    public GameObject prefabShotHoming;
    public GameObject prefabShotBomb;
    public GameObject prefabShotTriLaser;
    public float maxCooldown;
    float cooldown = 0;

    public Transform bulletParent;

    public Transform shootPosition_Middle;
    public Transform shootPosition_Left;
    public Transform shootPosition_Right;

    public shoot_type shoot_mode = shoot_type.NORMAL;


    public GameObject prefabTarget;

    Transform tg = null;

    public FighterUI ui;

    public enum shoot_type {
        NORMAL,
        TWOSHOT,
        TRISHOT,
        BOMB,
        HOMINGMISSILE,
        SHOTGUN
    };

    void Update()
    {
        if(!MissionManager.instance.canMove){
            return;
        }

        if(Input.GetMouseButtonDown(1)){
            GetNextTarget();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SetShootType(shoot_type.NORMAL);
        }else if(Input.GetKeyDown(KeyCode.Alpha2) && GameManager.instance.HasItem("DOUBLE")){
            SetShootType(shoot_type.TWOSHOT);
        }else if(Input.GetKeyDown(KeyCode.Alpha3) && GameManager.instance.HasItem("TRI")){
            SetShootType(shoot_type.TRISHOT);
        }else if(Input.GetKeyDown(KeyCode.Alpha4) && GameManager.instance.HasItem("BOMB")){
            SetShootType(shoot_type.BOMB);
        }else if(Input.GetKeyDown(KeyCode.Alpha5) && GameManager.instance.HasItem("SHOTGUN")){
            SetShootType(shoot_type.SHOTGUN);
        }else if(Input.GetKeyDown(KeyCode.Alpha6) && GameManager.instance.HasItem("HOMING")){
            SetShootType(shoot_type.HOMINGMISSILE);
        }


        if(cooldown > 0){
            cooldown-=Time.deltaTime;
            return;
        }
        
        if(Input.GetMouseButton(0)){
            cooldown = maxCooldown;

            switch(shoot_mode){
                case shoot_type.NORMAL:
                    SpawnBullet(prefabShotNormal,shootPosition_Middle);
                    break;
                case shoot_type.TWOSHOT:
                    SpawnBullet(prefabShotNormal,shootPosition_Left);
                    SpawnBullet(prefabShotNormal,shootPosition_Right);
                    break;
                case shoot_type.TRISHOT:
                    SpawnBullet(prefabShotNormal,shootPosition_Left);
                    SpawnBullet(prefabShotNormal,shootPosition_Middle);
                    SpawnBullet(prefabShotNormal,shootPosition_Right);
                    break;
                case shoot_type.HOMINGMISSILE:
                    SpawnBullet(prefabShotHoming,shootPosition_Middle);
                    break;
                case shoot_type.BOMB:
                    SpawnBullet(prefabShotBomb,shootPosition_Middle);
                    break;
                case shoot_type.SHOTGUN:
                    SpawnBullet(prefabShotTriLaser,shootPosition_Middle);
                    break;
            }


        }
    }


    void SpawnBullet(GameObject prefab,Transform pos){
        GameObject ob = Instantiate(prefab,bulletParent);
        ob.transform.position = pos.position;
        ob.transform.rotation = pos.rotation;

        if(GameManager.instance.HasItem("TARGET") && tg != null){
            ob.transform.LookAt(tg);
        }
    }


    void GetNextTarget(){
        if(tg!= null){
            Destroy(tg.gameObject);
        }
        tg = null;

        GameObject target = FindClosestEnnemy();
        if(target!= null){
            tg = Instantiate(prefabTarget, FindParent(target.transform,"Ennemy")).transform;
        }

    }

    GameObject FindClosestEnnemy(){
        float dist = 999999;
        GameObject pos = null;
        foreach(GameObject ob in GameObject.FindGameObjectsWithTag("Ennemy")){
            if(Distance(ob.transform.position,transform.position ) < dist){
                dist = Distance(ob.transform.position,transform.position);
                pos = ob;
            }
        }
        return pos;
    }

    float Distance(Vector3 a,Vector3 b){
        return  Mathf.Sqrt(Mathf.Pow(a.x-b.x,2)+Mathf.Pow(a.y-b.y,2)+Mathf.Pow(a.z-b.z,2));
    }

    Transform FindParent(Transform start, string tag){
        while(start.parent != null && start.parent.tag==tag){
            start = start.parent;
        }
        return start;
    }


    void SetShootType(shoot_type newType){
        shoot_mode = newType;
        switch(newType){
            case shoot_type.NORMAL:
                ui.UpdateWeapon(GameManager.instance.GetItem("STANDARD").image);
                break;
            case shoot_type.TWOSHOT:
                ui.UpdateWeapon(GameManager.instance.GetItem("DOUBLE").image);
                break;
            case shoot_type.TRISHOT:
                ui.UpdateWeapon(GameManager.instance.GetItem("TRI").image);
                break;
            case shoot_type.HOMINGMISSILE:
                ui.UpdateWeapon(GameManager.instance.GetItem("HOMING").image);
                break;
            case shoot_type.BOMB:
                ui.UpdateWeapon(GameManager.instance.GetItem("BOMB").image);
                break;
            case shoot_type.SHOTGUN:
                ui.UpdateWeapon(GameManager.instance.GetItem("SHOTGUN").image);
                break;
        }
    }
}
