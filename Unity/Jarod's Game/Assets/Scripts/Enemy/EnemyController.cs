using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;
    public GameObject explosion;
    private Text healthText;
    private Text killsText;

    public float health = 100;

    private float nextAttack=0;
    private float attackSpeed = 1;
    public float attackStrength = 2;

    // At dtart of enemy's existance finds the Game functions object and the healthText UI object
    void Start () {
        healthText = GameObject.Find("Canvas/HealthText").GetComponent<Text>();
        killsText = GameObject.Find("Canvas/NumberOfKillsText").GetComponent<Text>();
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }
	
	// Update is called once per frame, not used yet
	void Update () {
      
            if (health <= 0)
            {
                GameFunctions.GetComponent<GameFunctions>().DestroyBall(this.transform.parent.gameObject, explosion);
                GameController.GetComponent<GameController>().kills++;
                GameController.GetComponent<GameController>().activeEnemies--;
                killsText.text = "Kills: "+ GameController.GetComponent<GameController>().kills;
            }
        
	}

    //Each frame that the enemy is in contact with the player the enemy attacks the player and damages him.
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (Time.time>nextAttack)
            {
                GameFunctions.GetComponent<GameFunctions>().DamageObject(other.gameObject,attackStrength);
                nextAttack = Time.time + attackSpeed; 
            }
        }
    }
}
