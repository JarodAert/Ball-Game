using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunEnemyController : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;
    public GameObject explosion;
    private Text killsText;

    public float health = 100;

    private float nextAttack = 0;
    private float attackSpeed = 1;
    public float attackStrength = 2;

    public float yRotation = 0;
    public float xRotation = 0;
    public float rotationSpeed = 5;

    // At dtart of enemy's existance finds the Game functions object and the healthText UI object
    void Start()
    {
        killsText = GameObject.Find("Canvas/NumberOfKillsText").GetComponent<Text>();
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame, not used yet
    void Update()
    {

        if (health <= 0)
        {
            GameFunctions.GetComponent<GameFunctions>().DestroyBall(this.transform.parent.gameObject, explosion);
            GameController.GetComponent<GameController>().kills++;
            GameController.GetComponent<GameController>().activeEnemies--;
            killsText.text = "Kills: " + GameController.GetComponent<GameController>().kills;
        }

        GameFunctions.GetComponent<GameFunctions>().RotationToFaceObject(this.gameObject, GameFunctions.GetComponent<GameFunctions>().player,ref yRotation,ref xRotation);
        Debug.Log(yRotation);

    }

    //Each frame that the enemy is in contact with the player the enemy attacks the player and damages him.
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time > nextAttack)
            {
                GameFunctions.GetComponent<GameFunctions>().DamageObject(other.gameObject, attackStrength);
                nextAttack = Time.time + attackSpeed;
            }
        }
    }
}
