using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipController : MonoBehaviour
{
    [Header("Ship control data")]
    public float thrust = 1000f;
    public float torque = 500f;
    public float maxSpeed = 20f;

    [Header("Bullet data")]
    public GameObject bullet;
    public Transform nozzule;
    public Transform[] treeShotDir;
    Rigidbody rb_ship;
    float thrustInput;
    float turnInput;

    void Awake()
    {
        rb_ship = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        thrustInput = 0f;
        turnInput = 0f;
    }

    void Update()
    {
        if (GameManager.instance.presentState == GameManager.GameState.IS_PLAYING)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            {
                Shoot();
            }
            turnInput = Input.GetAxis("Horizontal");
            thrustInput = Mathf.Clamp01(Input.GetAxis("Vertical")); //set value between 0 to 1
        }
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        Speed();
    }

    void Move()
    {
        // Move ship through input
        Vector3 thrustForce = thrustInput * thrust * Time.deltaTime * transform.up;
        rb_ship.AddForce(thrustForce);
    }

    void Turn()
    {
        // Rotate ship through input
        float turn = turnInput * torque * Time.deltaTime;
        Vector3 zTorque = transform.forward * -turn;
        rb_ship.AddTorque(zTorque);
    }

    void Speed()
    {
        //moving speed of ship
        rb_ship.velocity = Vector3.ClampMagnitude(rb_ship.velocity, maxSpeed);
    }

    void Shoot()
    {
        if (!GameManager.instance.PUM.isThreeShot)
        {
            GameObject bulletClone = Instantiate(bullet, new Vector2(nozzule.transform.position.x, nozzule.transform.position.y), transform.rotation);

            bulletClone.GetComponent<Bullet>().RemoveBullet(3f);
            SoundManager.instance.PlayClip(EAudioClip.BULLET_SFX,0.8f);
        }
        else
        {
            ShootSpread();
        }

      //  bulletClone.GetComponent<Rigidbody>().AddForce(transform.up * 350);
    }

    void ShootSpread()
    {
        for (int i = 0; i <3; i++)
        {
           
            Vector3 direction = treeShotDir[i].up;
            GameManager.instance.PUM.dir = transform.up;
            GameObject bulletClone = Instantiate(bullet, new Vector3(treeShotDir[i].transform.position.x, treeShotDir[i].transform.position.y, treeShotDir[i].position.z),transform.rotation);
            SoundManager.instance.PlayClip(EAudioClip.BULLET_SFX, 0.8f);
            bulletClone.GetComponent<Bullet>().RemoveBullet(3f);
        }

    }
}

