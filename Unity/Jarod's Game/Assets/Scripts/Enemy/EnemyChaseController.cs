using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseController : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;

    public float enemySpeed = 5;
    public float chaseRange = 75;
    private bool chase = false;
    


	// Find the Game Functions object when existance begins
	void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }
	
	// Update is called once per frame, not used yet
	void Update () {
        if (GameController.GetComponent<GameController>().EndGame == false)
        {
            if (Vector3.Distance(GameFunctions.GetComponent<GameFunctions>().player.transform.position, transform.position) <= chaseRange)
            {
                chase = true;
            }
            else
            {
                chase = false;
            }
        }
	}

    void FixedUpdate()
    {
        if (chase)
        {
            Vector3 movement = GameFunctions.GetComponent<GameFunctions>().FindChaseForce(GameFunctions.GetComponent<GameFunctions>().player.transform, transform, enemySpeed);
            GetComponentInParent<Rigidbody>().AddForce(movement);
        }
    }


}
