using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [Header("Asteroid control")]
    public float initialForce = 100f;
    public float initialTorque = 100f;

    Rigidbody rb_asteroid;

    // Start is called before the first frame update
    void Start()
    {
        rb_asteroid = GetComponent<Rigidbody>();
        if (rb_asteroid)
        {
            SetRandomForce(rb_asteroid, initialForce);
            SetRandomTorque(rb_asteroid, initialTorque);
        }
        transform.position = FindOpenPosition();
    }

   
    void SetRandomForce(Rigidbody rb, float maxForce)
    {
        Vector3 randomForce = maxForce * Random.insideUnitSphere;
        rb.velocity = Vector3.zero;
        rb.AddForce(randomForce);
    }

    void SetRandomTorque(Rigidbody rb, float maxTorque)
    {
        Vector3 randomTorque = maxTorque * Random.insideUnitSphere;
        rb.angularVelocity = Vector3.zero;
        rb.AddTorque(randomTorque);
    }

    Vector3 FindOpenPosition(int layerMask = ~0)
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float collisionSphereRadius = x > y ? x : y;
     
        bool overlap = false;
        Vector3 openPosition;
        do
        { 
            openPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
            openPosition.z = 0;
            overlap = Physics.CheckSphere(openPosition, collisionSphereRadius, layerMask);
        } while (overlap);
        return openPosition;
    }

   void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject, 0f);
            Destroy(otherCollider.gameObject);
        }
    }
    void OnCollisionEnter(Collision otherCollision)
    {
        if (otherCollision.gameObject.tag == "Ship")
        {
            print("Collided with Ship");
        }
    }

}
