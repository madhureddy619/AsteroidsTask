using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * 350);
    }

    
    public void RemoveBullet(float time)
    {
        Destroy(gameObject, time);
    }
    
}


