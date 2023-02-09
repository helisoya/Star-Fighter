using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Transform target;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        transform.LookAt(target);
    }
}
