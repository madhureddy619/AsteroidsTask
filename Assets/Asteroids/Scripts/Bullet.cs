using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 800;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * speed);
    }

    
    public void RemoveBullet(float time)
    {
        Destroy(gameObject, time);
    }
    
}


