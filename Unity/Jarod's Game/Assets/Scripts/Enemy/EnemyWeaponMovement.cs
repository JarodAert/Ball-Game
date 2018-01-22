using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponMovement : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    public float nextFire=0;
    public float fireRate=0.5f;

    // Runs First frame this object is active. Finds the GameFunctions object and finds the offsets from the player
    void Start()
    {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        positionOffset = GameFunctions.GetComponent<GameFunctions>().FindPositionOffset(transform, transform.parent.GetChild(0).transform);
        rotationOffset = GameFunctions.GetComponent<GameFunctions>().FindRotationOffset(transform, transform.parent.GetChild(0).transform);
    }


    // Update is called once per frame
    //Makes sure the weapon hand spot is correctly positioned about the player at all times
    void Update()
    {

        if (GameController.GetComponent<GameController>().EndGame != true)
        {
            GameFunctions.GetComponent<GameFunctions>().SetCircularRotation(rotationOffset, positionOffset, this.gameObject, transform.parent.GetChild(0).gameObject);
            transform.eulerAngles = new Vector3(transform.parent.GetChild(0).GetChild(1).GetComponent<GunEnemyController>().xRotation, transform.parent.GetChild(0).GetChild(1).GetComponent<GunEnemyController>().yRotation, 0);

            GameFunctions.GetComponent<GameFunctions>().checkRange(this.gameObject, GameFunctions.GetComponent<GameFunctions>().player, ref nextFire, fireRate, -transform.parent.GetChild(0).GetChild(1).GetComponent<GunEnemyController>().yRotation+90, transform.parent.GetChild(0).GetChild(1).GetComponent<GunEnemyController>().xRotation);
        }

    }
}
