using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseController : MonoBehaviour {

    private GameObject GameFunctions;

    public float enemySpeed = 5;
    


	// Find the Game Functions object when existance begins
	void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
    }
	
	// Update is called once per frame, not used yet
	void Update () {
		
	}

    //Each fram that the player is with the chasing range of the enemy it moves to chase the player
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 movement = GameFunctions.GetComponent<GameFunctions>().FindChaseForce(other.transform,transform,enemySpeed);
            GetComponentInParent<Rigidbody>().AddForce(movement);
        }
    }
}
