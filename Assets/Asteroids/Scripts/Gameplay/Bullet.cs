using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 800;

    public Vector3 dir ;

    private void Awake()
    {
        dir = gameObject.transform.up;
    }
    void Start()
    {
        if (!GameManager.instance.PUM.isThreeShot)
            GetComponent<Rigidbody>().AddForce(dir * speed);
        else
        {
           
            GetComponent<Rigidbody>().AddForce(GameManager.instance.PUM.dir * speed);
        }
    }

   
    
    public void RemoveBullet(float time)
    {
        Destroy(gameObject, time);
    }
    
}


