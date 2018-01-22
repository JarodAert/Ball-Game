using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMovement : MonoBehaviour {

    private GameObject GameFunctions;
    public GameObject explosion;
    public float bulletSpeed = 50;
    public float bulletStrength = 10;



    // Runs on the first frame the bullet object exists, fidnss GameObject and finds the velocity vector3 of the bullet
    void Start () {
        Destroy(this.gameObject, 1.5f);
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
	}
	
	// Update is called once per frame, not used yet
	void Update () {
		
	}

    //Function called when the bullet enters into another object
    //If the collision is with an enemy then it calls damage enemy
    //TODO: Right now bullets only damage enemies. it needs to be universal so that bullets can harm any 'living' objects
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerTrigger"))
        {
                GameFunctions.GetComponent<GameFunctions>().DamageObject(other.transform.parent.gameObject, bulletStrength);
                Destroy(this.gameObject);
        }

        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("GunEnemy")){
            GameFunctions.GetComponent<GameFunctions>().DamageObject(other.gameObject, bulletStrength);
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().hit = true;
            }
            else if (other.CompareTag("GunEnemy"))
            {
                other.GetComponent<GunEnemyController>().hit = true;
            }
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Terrian"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
    }
}
