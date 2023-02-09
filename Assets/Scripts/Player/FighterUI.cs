using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterUI : MonoBehaviour
{
    public Color forwardColor;

    public Color backwardColor;

    public Image speed_front;

    public Image health_front;

    public Image weapon;

    public void UpdateUI(float speed, float maxSpeed,float maxReverseSpeed){
        if(speed < 0){
            speed_front.color = backwardColor;
            speed_front.fillAmount = speed/maxReverseSpeed;
        }else{
            speed_front.color = forwardColor;
            speed_front.fillAmount = speed/maxSpeed;
        }
    }

    public void UpdateHealth(int health,int maxHealth){
        health_front.fillAmount = (float)health/(float)maxHealth;
    }

    public void UpdateWeapon(Sprite sprite){
        weapon.sprite = sprite;
    }
}
