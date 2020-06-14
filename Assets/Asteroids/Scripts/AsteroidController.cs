using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public enum asteroidType { Large , Medium , Small }
    [Header("Asteroid control")]
    public float initialForce = 100f;
    public float initialTorque = 100f;

    [Header("Select Size")]
    public asteroidType sizeType;

    [Header("")]
    public ParticleSystem exposive;

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

        if(sizeType == asteroidType.Large)
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

            switch (sizeType)
            {
                case asteroidType.Large:

                    GameManager.instance.GenerateAsteroids(GameManager.instance.asteroid_medium, 2,this.transform.position);
                    GameManager.score++;
                    break;
                case asteroidType.Medium:
                    GameManager.instance.GenerateAsteroids(GameManager.instance.asteroid_small, 2, this.transform.position);
                    GameManager.score++;
                    break;
                case asteroidType.Small:
                    Debug.Log("*************** Add Score here ***************8");
                    GameManager.score++;
                    break;
                default:
                    Debug.LogError("## Pleas assign type of asteroid - SMALL or MEDIUM or LARGE ###");
                    break;
            }


            
            Destroy(otherCollider.gameObject); // Destroy bullet
        }
    }
    void OnCollisionEnter(Collision otherCollision)
    {
        if (otherCollision.gameObject.tag == "Ship")
        {
            GameManager.instance.GameOver();
        }
    }

}
