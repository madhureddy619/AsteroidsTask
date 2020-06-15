using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Range(5, 30)]
    public int showTime = 10;

    [Range(5, 30)]
    public int powerDuration = 10;

    [SerializeField]
    float initialForce = 100f;

    [SerializeField]
    float initialTorque = 100f;

    public PowerType type;

    void Start()
    {
        ApplySpawnVariance();
    }

   
    void OnTriggerEnter(Collider otherCollision)
    {
        GameObject otherObject = otherCollision.gameObject;
        if (otherObject.tag == "Ship")
        {
            GrantPower();
        }
    }

    public  void GrantPower()
    {
        GameManager.instance.PUM.ActivatePower(type);
        HidePower();  
    }

    public void HidePower()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(PowerUpUsed());
    }

    IEnumerator PowerUpUsed()
    {
        yield return new WaitForSeconds(powerDuration);
        GameManager.instance.PUM.DeActivatePower(type);
        Destroy(this.gameObject,0.1f);
    }

    void ApplySpawnVariance()
    {        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            //Set random Force on Spawn
            Vector3 randomForce = initialForce * Random.insideUnitSphere;
            randomForce.z = GameManager.instance.spaceShip_pc.transform.position.z;
            rb.velocity = Vector3.zero;
            rb.AddForce(randomForce);

            //Set random Torque on Spawn
            Vector3 randomTorque = initialTorque * Random.insideUnitSphere;
            rb.angularVelocity = Vector3.zero;
            rb.AddTorque(randomTorque);
        }
    }

}

public enum PowerType
{
    THREE_FIRE,   
    LIFE,
    SHIELD,    
    NONE
}
