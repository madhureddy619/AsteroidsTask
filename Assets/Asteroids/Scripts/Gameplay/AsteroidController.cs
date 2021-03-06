﻿using System.Collections;
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
    public GameObject explosion;

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
        if (otherCollider.gameObject.tag == "Bullet" || otherCollider.gameObject.tag == "Shield")
        {
            //Destroy(this.gameObject, 0f);
            gameObject.SetActive(false);
            switch (sizeType)
            {
                case asteroidType.Large:
                    GameManager.instance.GenerateAsteroids(asteroidType.Medium, 2,this.transform.position);
                    GameManager.instance.score+=10;
                    break;
                case asteroidType.Medium:
                    GameManager.instance.GenerateAsteroids(asteroidType.Small, 2, this.transform.position);
                    GameManager.instance.score+=20;
                    break;
                case asteroidType.Small:
                    GameManager.instance.score+=30;
                    break;
                default:
                    Debug.LogError("## Pleas assign type of asteroid - SMALL or MEDIUM or LARGE ###");
                    break;
            }
            SoundManager.instance.PlayClip(EAudioClip.DESTROY_SFX,1);

            GameManager.instance.PUM.powerUpCounter++;
            GameManager.instance.PUM.SpawnRandomPower(transform);

            if(otherCollider.gameObject.tag == "Bullet")
                Destroy(otherCollider.gameObject); // Destroy bullet
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
    void OnCollisionEnter(Collision otherCollision)
    {
        if (otherCollision.gameObject.tag == "Ship")
        {
            GameManager.instance.lives--;            
            GameManager.instance.GameOver();
            gameObject.SetActive(false);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
    
}
