using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    public float maxSpeed;

    public float maxReverseSpeed;

    public float accelarationSpeed;

    public float speed = 0;

    public float speedH = 0;

    float vInput;

    float hInput;

    float rollInput;

    public float sensibility;

    public FighterUI ui;

    public Rigidbody rb;

    public Transform cam;

    Vector2 PlayerCamMovement;

    Vector2 screenCenter = new Vector2(Screen.width * .5f,Screen.height * .5f);

    Vector2  lookInput;

    void Start(){
        if(GameManager.instance.HasItem("SPEED")){
            maxSpeed+=25;
            maxReverseSpeed-=10;
        }
    }

    void Update()
    {
        if(!MissionManager.instance.canMove){
            return;
        }
        
        vInput = Input.GetAxis("Vertical");
        if(vInput != 0.0){
            speed = Mathf.Clamp(speed+vInput*accelarationSpeed,maxReverseSpeed,maxSpeed);
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = Mathf.MoveTowards(speed,0,accelarationSpeed);
        }

        ui.UpdateUI(speed,maxSpeed,maxReverseSpeed);

        hInput = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");
    
        lookInput = Input.mousePosition;
        PlayerCamMovement.x = (lookInput.x-screenCenter.x) / screenCenter.x;
        PlayerCamMovement.y = (lookInput.y-screenCenter.y) / screenCenter.y;
    }

    void TryMove(Vector3 move,bool moveForward){
        if(!Physics.Raycast(transform.position,move,Vector3.Distance(transform.position,transform.position+move),3)){
            rb.MovePosition(rb.position+move);
        }else{
            if(moveForward){
                speed = 0;
            }else{
                hInput = 0;
            }
        }
    }

    void FixedUpdate(){
        TryMove(transform.forward * speed * Time.deltaTime,true);

        if(hInput != 0.0){
           TryMove(transform.right * speedH * hInput * Time.deltaTime,false);
        }

        transform.Rotate(-PlayerCamMovement.y * sensibility*Time.deltaTime,PlayerCamMovement.x * sensibility*Time.deltaTime,0f);
        transform.Rotate(0,0,-rollInput*sensibility*Time.deltaTime);
        
        cam.LookAt(rb.position);
        cam.position = rb.position - transform.forward * 10 + transform.up * 3;

        if(speed == 0){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
        }
    }
}
