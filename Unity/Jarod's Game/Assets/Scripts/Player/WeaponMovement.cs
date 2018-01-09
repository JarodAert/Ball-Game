using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    private int waiter = 0;

    // Runs First frame this object is active. Finds the GameFunctions object and finds the offsets from the player
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        positionOffset = GameFunctions.GetComponent<GameFunctions>().FindPositionOffset(transform, GameFunctions.GetComponent<GameFunctions>().player.transform);
        rotationOffset = GameFunctions.GetComponent<GameFunctions>().FindRotationOffset(transform, GameFunctions.GetComponent<GameFunctions>().player.transform);

        GameFunctions.GetComponent<GameFunctions>().playerCamera.GetComponent<PlayerCameraController>().positionOffset = GameFunctions.GetComponent<GameFunctions>().FindPositionOffset(GameFunctions.GetComponent<GameFunctions>().playerCamera.GetComponent<PlayerCameraController>().transform, GameFunctions.GetComponent<GameFunctions>().player.transform);
        GameFunctions.GetComponent<GameFunctions>().playerCamera.GetComponent<PlayerCameraController>().rotationOffset = GameFunctions.GetComponent<GameFunctions>().FindRotationOffset(GameFunctions.GetComponent<GameFunctions>().playerCamera.GetComponent<PlayerCameraController>().transform, GameFunctions.GetComponent<GameFunctions>().player.transform);

        GameFunctions.GetComponent<GameFunctions>().playerLight.GetComponent<PlayerLightController>().positionOffset = GameFunctions.GetComponent<GameFunctions>().FindPositionOffset(GameFunctions.GetComponent<GameFunctions>().playerLight.GetComponent<PlayerLightController>().transform, GameFunctions.GetComponent<GameFunctions>().player.transform);
        GameFunctions.GetComponent<GameFunctions>().playerLight.GetComponent<PlayerLightController>().rotationOffset = GameFunctions.GetComponent<GameFunctions>().FindRotationOffset(GameFunctions.GetComponent<GameFunctions>().playerLight.GetComponent<PlayerLightController>().transform, GameFunctions.GetComponent<GameFunctions>().player.transform);

        GameFunctions.GetComponent<GameFunctions>().SetHeightRelativeToTerrian(GameFunctions.GetComponent<GameFunctions>().player);
    }

	
	// Update is called once per frame
    //Makes sure the weapon hand spot is correctly positioned about the player at all times
	void Update () {
        

   
            if (GameController.GetComponent<GameController>().EndGame != true)
            {
                GameFunctions.GetComponent<GameFunctions>().SetCircularRotation(rotationOffset, positionOffset, this.gameObject, GameFunctions.GetComponent<GameFunctions>().player);
                transform.eulerAngles = new Vector3(GameFunctions.GetComponent<GameFunctions>().player.GetComponent<PlayerController>().xRotation, 90 - GameFunctions.GetComponent<GameFunctions>().player.GetComponent<PlayerController>().yRotation, 0);
            }
      
    }
}
