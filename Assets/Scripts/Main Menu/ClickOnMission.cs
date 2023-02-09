using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnMission : MonoBehaviour
{
    string mission;
    void Start()
    {
        mission = gameObject.name;
    }

    void OnMouseDown(){
        MenuUI.instance.ShowBriefingForMission(mission,gameObject);
    }
}
