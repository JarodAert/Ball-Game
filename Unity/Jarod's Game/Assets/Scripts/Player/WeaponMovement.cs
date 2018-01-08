using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    // Runs First frame this object is active. Finds the GameFunctions object and finds the offsets from the player
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        positionOffset = GameFunctions.GetComponent<GameFunctions>().FindPositionOffset(transform);
        rotationOffset = GameFunctions.GetComponent<GameFunctions>().FindRotationOffset(transform);       
    }
	
	// Update is called once per frame
    //Makes sure the weapon hand spot is correctly positioned about the player at all times
	void Update () {
        if (GameController.GetComponent<GameController>().EndGame != true)
        {
            GameFunctions.GetComponent<GameFunctions>().SetCircularRotation(rotationOffset, positionOffset, this.gameObject);
            transform.eulerAngles = new Vector3(GameFunctions.GetComponent<GameFunctions>().xRotation, 90 - GameFunctions.GetComponent<GameFunctions>().yRotation, 0);
        }
    }
}
