using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;
    public GameObject grenadeExplosion;
    public GameObject explosion;
    public float throwSpeed = 3;
    public float explodeTime = 0;

    // Use this for initialization
    void Start()
    {
        explodeTime = Time.time + 3;
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GetComponent<Rigidbody>().velocity = GameFunctions.GetComponent<GameFunctions>().FindBulletVelocity(this.gameObject, throwSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > explodeTime)
        {
            GameFunctions.GetComponent<GameFunctions>().GrenadeExplode(this.gameObject, grenadeExplosion);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameFunctions.GetComponent<GameFunctions>().RocketExplode(this.gameObject, other.gameObject, grenadeExplosion, explosion);
        }
    }
}
